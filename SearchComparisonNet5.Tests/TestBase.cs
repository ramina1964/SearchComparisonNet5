using SearchComparisonNet5.Kernel.Models;

namespace SearchComparisonNet5.Tests
{
    public class TestBase
    {
        public TestBase()
        {
            DataParameters = new DataParameters();
            DataGenerator = new DataGenerator(DataParameters);
            LinearSut = new LinearSearch(DataGenerator);
            BinarySut = new BinarySearch(DataGenerator);
        }

        public DataParameters DataParameters { get; set; }

        public SearchBase LinearSut { get; set; }

        public SearchBase BinarySut { get; set; }

        public DataGenerator DataGenerator { get; }
    }
}