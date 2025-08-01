using Xunit.Abstractions;

namespace CellSync.UnitTests.TestConcepts;

/*
 * In xUnit, tests are organized into collections.
 * By default, each test class represents its own collection.
 * However, to be more explicit or for specific scenarios, you can use the [Collection] attribute.
 * xUnit runs tests in parallel across different collections, but sequentially within the same collection.
 *
 * 👉 Tip: Uncomment the test classes below and run the tests to see how they execute in parallel.
 * You'll notice that methods in different collections (i.e., separate classes) run simultaneously,
 * while methods in the same collection run one after the other.
 */
// [Collection("ConceptTests1")] // Optional for this case
// public class ConceptTests1(ITestOutputHelper outputHelper)
// {
//     [Fact]
//     public async Task ConceptTests1_Test1()
//     {
//         outputHelper.WriteLine(nameof(ConceptTests1_Test1));
//         await Task.Delay(1000);
//     }
//
//     [Fact]
//     public async Task ConceptTests1_Test2()
//     {
//         outputHelper.WriteLine(nameof(ConceptTests1_Test2));
//         await Task.Delay(2000);
//     }
// }
//
// // By default, ConceptTests2 is its own collection
// public class ConceptTests2(ITestOutputHelper outputHelper)
// {
//     [Fact]
//     public async Task ConceptTests2_Test1()
//     {
//         outputHelper.WriteLine(nameof(ConceptTests2_Test1));
//         await Task.Delay(1000);
//     }
//
//     [Fact]
//     public async Task ConceptTests2_Test2()
//     {
//         outputHelper.WriteLine(nameof(ConceptTests2_Test2));
//         await Task.Delay(2000);
//     }
// }