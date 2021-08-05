using NUnit.Framework;

namespace SearchComparisonNet5.Tests
{
    [TestFixture]
    public class BinarySearchTests : TestBase
    {
        [TestCase(0), TestCase(1), TestCase(2), TestCase(10), TestCase(20), TestCase(50), TestCase(100), TestCase(1000)]
        [TestCase(2000), TestCase(3000), TestCase(4000), TestCase(5000), TestCase(6000), TestCase(7000), TestCase(8000)]
        [TestCase(9000), TestCase(10000), TestCase(100_000), TestCase(200_000), TestCase(300_000), TestCase(350_000)]
        [TestCase(400_000), TestCase(450_000), TestCase(480_000), TestCase(490_000), TestCase(499_999)]
        public void Should_find_correct_index_with_binary_search_when_value_exists(int index)
        {
            // Arrange
            var expectedIndex = index;

            // Act
            var value = BinarySut[index];
            var actualIndex = BinarySut.FindItem(value).TargetIndex;

            // Assert 
            Assert.That(actualIndex, Is.EqualTo(expectedIndex));
        }
    }
}
