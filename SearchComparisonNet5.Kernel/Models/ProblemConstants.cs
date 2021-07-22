
namespace SearchComparisonNet5.Kernel.Models
{
    public static class ProblemConstants
    {
        // No of Entries
        public static int MinNoOfEntries => 10_000;

        public static int InitialNoOfEntries => 500_000; 

        public static int MaxNoOfEntries => 1000_000;

        // Entry Values 
        public static int MinEntryValue => 0;

        public static int MaxEntryValue => (5 * InitialNoOfEntries) - 1;

        // No of Searches
        public static int MinNoOfSearches => 1000;

        public static int InitialNoOfSearches => 5_000;

        public static int MaxNoOfSearches => 20_000;
    }
}
