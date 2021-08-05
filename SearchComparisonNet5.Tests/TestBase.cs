using SearchComparisonNet5.Kernel.Models;

namespace SearchComparisonNet5.Tests
{
    public class TestBase
    {
        public TestBase()
        {
            DataParams = new DataParameters();
            DataGenerator = new DataGenerator(DataParams);
            LinearSut = new LinearSearch(DataGenerator);
            BinarySut = new BinarySearch(DataGenerator);
        }

        public LinearSearch LinearSut { get; set; }

        public BinarySearch BinarySut { get; set; }

        public DataParameters DataParams { get; }

        public DataGenerator DataGenerator { get; }

        public SearchItem SearchItem { get; set; }
    }
}