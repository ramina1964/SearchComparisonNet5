namespace SearchComparisonNet5.Kernel.Models
{
    public class DataParameters
    {
        public DataParameters()
        {
            NoOfEntries = ProblemConstants.InitialNoOfEntries;
        }

        public DataParameters(int noOfEntries)
        {
            NoOfEntries = noOfEntries;
        }

        public int NoOfEntries { get; set; }

        public int MinEntryValue => ProblemConstants.MinEntryValue;

        public int MaxEntryValue => (5 * NoOfEntries) - 1;
    }
}
