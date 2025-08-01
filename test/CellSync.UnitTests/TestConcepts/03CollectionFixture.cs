using Xunit.Abstractions;

namespace CellSync.UnitTests.TestConcepts;

/*
 * The limitation of IClassFixture is that it creates a separate fixture instance for each test collection.
 * So if you want to share the same fixture across multiple test classes, you should use ICollectionFixture.
 *
 * ICollectionFixture allows you to define a shared fixture that spans multiple test classes,
 * as long as they belong to the same [Collection].
 *
 * To use it, you need to:
 * - Create a fixture class (e.g., GuidClass)
 * - Create a collection definition class that implements ICollectionFixture<T>
 * - Annotate each test class with [Collection("YourCollectionName")]
 *
 * ⚠️ Note: The tradeoff is that all classes within the same collection will run sequentially.
 * xUnit executes tests in parallel across different collections but sequentially within the same collection.
 * That means using ICollectionFixture may increase the total test execution time.
 */
// public class GuidClass
// {
//     public Guid Guid { get; set; } = Guid.NewGuid();
// }
//
// [CollectionDefinition("GuidCollection")]
// public class GuidCollectionFixture : ICollectionFixture<GuidClass>
// {
//     // This class is intentionally left empty.
//     // It serves as a collection fixture for GuidClass.
// }
//
// [Collection("GuidCollection")]
// public class ConceptTest1(ITestOutputHelper outputHelper, GuidClass guidClass)
// {
//     [Fact]
//     public async Task Test1()
//     {
//         outputHelper.WriteLine($"Test1: {guidClass.Guid}");
//         await Task.Delay(1000);
//     }
//
//     [Fact]
//     public async Task Test2()
//     {
//         outputHelper.WriteLine($"Test2: {guidClass.Guid}");
//         await Task.Delay(1000);
//     }
// }
//
// [Collection("GuidCollection")]
// public class ConceptTest2(ITestOutputHelper outputHelper, GuidClass guidClass)
// {
//     [Fact]
//     public async Task Test3()
//     {
//         outputHelper.WriteLine($"Test1: {guidClass.Guid}");
//         await Task.Delay(1000);
//     }
//
//     [Fact]
//     public async Task Test4()
//     {
//         outputHelper.WriteLine($"Test2: {guidClass.Guid}");
//         await Task.Delay(1000);
//     }
// }