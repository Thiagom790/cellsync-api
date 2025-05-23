namespace CellSync.Consumer.Helpers;

public static class EnvironmentHelpers
{
    public static string GetEnvironmentName() =>
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
}