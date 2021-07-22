using SearchComparisonNet5.Kernel.Interfaces;

namespace SearchComparisonNet5.Kernel.Models
{
    public sealed class LinearSearch : BaseSearch
    {
        public LinearSearch(ISearchItem searchItem, IDataGenerator dataGen) : base(searchItem, dataGen)
        { }

        public override ISearchItem FindItem(int value)
        {
            int? targetIndex = null;
            var noOfIterations = 0;
            for (var i = 0; i < NoOfEntries; i++)
            {
                noOfIterations++;
                if (Data[i] < value)
                {
                    continue;
                }

                if (Data[i] > value)
                {
                    break;
                }

                targetIndex = i;
                break;
            }

            return new SearchItem
            {
                TargetIndex = targetIndex,
                TargetValue = value,
                NoOfIterations = noOfIterations
            };
        }
    }
}
