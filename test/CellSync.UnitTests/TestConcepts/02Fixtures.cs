using Xunit.Abstractions;

namespace CellSync.UnitTests.TestConcepts;

/*
 * xUnit creates a new instance of the test class for each test method within a collection.
 * That means all instance properties and fields are reinitialized for every test run.
 *
 * For example, the behavior of the class below is equivalent to:
 * new ConceptTests1(outputHelper).Test1()
 * new ConceptTests1(outputHelper).Test2()
 *
 * When you run the example, you'll see that the value of _guid differs in each test,
 * because each test method gets its own instance of the test class.
 */
// public class ConceptTests1(ITestOutputHelper outputHelper)
// {
//     private readonly Guid _guid = Guid.NewGuid();
//
//     [Fact]
//     public async Task Test1()
//     {
//         outputHelper.WriteLine($"Test1: {_guid}");
//         await Task.Delay(1000);
//     }
//
//     [Fact]
//     public async Task Test2()
//     {
//         outputHelper.WriteLine($"Test2: {_guid}");
//         await Task.Delay(1000);
//     }
// }

/*
 * But sometimes, you need a shared instance across multiple tests—
 * such as a database connection, configuration, or any resource that should persist.
 *
 * In such cases, you can use fixtures. xUnit will create a single instance of the fixture class
 * and inject it into each test class. This allows data or resources to be shared.
 *
 * In the example below, since GuidClass is injected via IClassFixture,
 * the Guid value will be the same across all tests within ConceptTests2.
 *
 * ⚠️ Observation: xUnit does not clean up shared fixture instances between tests.
 * So if your fixture holds unmanaged resources, or needs teardown logic,
 * make sure your fixture class implements IDisposable or IAsyncDisposable
 * to ensure proper cleanup after all tests run.
 *
 */
// public class GuidClass
// {
//     public Guid Guid { get; set; } = Guid.NewGuid();
// }
//
// public class ConceptTests2(ITestOutputHelper outputHelper, GuidClass guidClass) : IClassFixture<GuidClass>
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