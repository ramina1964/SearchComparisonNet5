using NUnit.Framework;
using SearchComparisonNet5.Kernel.Models;

namespace SearchComparisonNet5.Tests
{
    [TestFixture]
    public class LinearSearchTests
    {
        [TestCase(0), TestCase(1), TestCase(2), TestCase(10), TestCase(20), TestCase(50), TestCase(100), TestCase(1000)]
        [TestCase(2000), TestCase(3000), TestCase(4000), TestCase(5000), TestCase(6000), TestCase(7000), TestCase(8000)]
        [TestCase(9000), TestCase(10000), TestCase(100_000), TestCase(200_000), TestCase(300_000)]
        [TestCase(400_000), TestCase(450_000), TestCase(490_000), TestCase(499_999)]
        public void Should_find_correct_index_when_value_exists(int index)
        {
            // Arrange
            const int noOfEntries = (int)5E5;
            var searchItem = new SearchItem();
            var expectedIndex = index;

            // Act
            var sut = new LinearSearch(searchItem, noOfEntries);
            sut.InitializeData();
            var value = sut[index];
            var actualIndex = sut.FindItem(value).TargetIndex;

            // Assert 
            Assert.That(actualIndex, Is.EqualTo(expectedIndex));
        }
    }

}
