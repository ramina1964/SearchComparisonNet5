using SearchComparisonNet5.Kernel.Models;

namespace SearchComparisonNet5.Tests
{
    public class BaseTest
    {
        public BaseTest()
        {
            DataParams = new DataParameters()
            {
                MinEntryValue = ProblemConstants.MinEntryValue,
                MaxEntryValue = ProblemConstants.MaxEntryValue,
                NoOfEntries = ProblemConstants.InitialNoOfEntries,
            };

            DataGenerator = new DataGenerator(DataParams);
        }

        public DataParameters DataParams { get; }

        public DataGenerator DataGenerator { get; }

        public SearchItem SearchItem { get; set; }
    }
}