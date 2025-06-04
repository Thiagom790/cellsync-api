using System.Text;
using System.Text.Json;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using CellSync.Consumer.EventProcessors;
using CellSync.Consumer.Models;
using CellSync.Consumer.Settings;

namespace CellSync.Consumer.Services;

public class AzureEventHubConsumer
{
    private readonly EventProcessorClient _processor;
    private CancellationTokenSource? _cancellationToken;
    private readonly EventProcessorManager _eventProcessorManager;
    private readonly Dictionary<string, Type> _eventProcessorTypes;
   

    public AzureEventHubConsumer(IEventHubSettings eventHubSettings, IServiceProvider serviceProvider)
    {
        var storageClient = new BlobContainerClient(
            connectionString: eventHubSettings.StorageConnectionString,
            blobContainerName: eventHubSettings.StorageContainerName
        );

        _processor = new EventProcessorClient(
            checkpointStore: storageClient,
            consumerGroup: EventHubConsumerClient.DefaultConsumerGroupName,
            connectionString: eventHubSettings.EventHubConnectionString,
            eventHubName: eventHubSettings.EventHubName
        );

        _processor.ProcessEventAsync += ProcessEventHandler;
        _processor.ProcessErrorAsync += ProcessErrorHandler;
        
        _eventProcessorManager = new EventProcessorManager(serviceProvider);
        _eventProcessorTypes = _eventProcessorManager.DiscoverEventProcessors();
    }

    private async Task ProcessEventHandler(ProcessEventArgs args)
    {
        if (args.CancellationToken.IsCancellationRequested) return;

        var eventBody = Encoding.UTF8.GetString(args.Data.Body.ToArray());
        var eventObject = JsonSerializer.Deserialize<AzureEvent<object>>(eventBody);
        var eventName = eventObject?.EventName;

        if (string.IsNullOrWhiteSpace(eventName) || eventObject?.EventData is null) return;

        // Verifica se existe um processador registrado para este evento
        if (!_eventProcessorTypes.TryGetValue(eventName, out var processorType)) return;

        try
        {
            // Obtém o tipo de dados do evento
            var eventDataType = _eventProcessorManager.GetEventDataType(processorType);
            
            // Deserializa os dados do evento para o tipo correto
            var eventData = JsonSerializer.Deserialize(eventObject.EventData.ToString()!, eventDataType);
            
            if (eventData == null) return;
            
            // Obtém ou cria uma instância do processador
            var processor = _eventProcessorManager.GetProcessorInstance(processorType);
            
            // Encontra e invoca o método OnReceiveEventAsync
            var method = processorType.GetMethod("OnReceiveEventAsync");
            if (method is null) return;
            
            // Executa o processador em uma task separada
            await Task.Run(() => 
            {
                var task = (Task)method.Invoke(processor, [eventData])!;
                return task;
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing event {eventName}: {ex.Message}");
            Console.WriteLine(ex);
        }
    }

    private Task ProcessErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine("Error in EventProcessorClient");
        Console.WriteLine(args.Exception.ToString());

        return Task.CompletedTask;
    }

    public async Task StartAsync()
    {
        try
        {
            Console.WriteLine("Starting Azure Event Hub Consumer");
            Console.WriteLine($"Discovered {_eventProcessorTypes.Count} event processors:");
            foreach (var (eventName, processorType) in _eventProcessorTypes)
            {
                Console.WriteLine($"- {eventName}: {processorType.Name}");
            }
            
            _cancellationToken = new CancellationTokenSource();
            await _processor.StartProcessingAsync(_cancellationToken.Token);
            await Task.Delay(Timeout.Infinite, _cancellationToken.Token);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while starting EventHub Consumer");
            Console.WriteLine(e);
        }
        finally
        {
            Console.WriteLine("Stopping EventHub Consumer");
            await StopAsync();
        }
    }

    private async Task StopAsync()
    {
        if (_cancellationToken is not null)
            await _cancellationToken?.CancelAsync()!;

        await _processor.StopProcessingAsync();

        _processor.ProcessErrorAsync -= ProcessErrorHandler;
        _processor.ProcessEventAsync -= ProcessEventHandler;
    }
}

