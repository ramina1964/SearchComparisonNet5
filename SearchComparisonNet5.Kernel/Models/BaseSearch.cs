using SearchComparisonNet5.Kernel.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace SearchComparisonNet5.Kernel.Models
{
    public abstract class BaseSearch
    {
        protected BaseSearch(IDataGenerator dataGen)
        {
            NoOfEntries = dataGen.NoOfEntries;
            MinValue = dataGen.MinValue;
            MaxValue = dataGen.MaxValue;
            Data = dataGen.Data;
            NextRandomNo = dataGen.NextRandomNo();
        }

        public ISearchItem SearchItem { get; }

        public int NoOfEntries { get; }

        public int MaxValue { get; }

        public int MinValue { get; }
        
        public int NextRandomNo { get; }

        public int this[int index]
        {
            get => Data[index];
            set
            {
                if (0 > index || index > NoOfEntries - 1)
                { throw new IndexOutOfRangeException(IndexOutOfRangeError); }

                Data[index] = value;
            }
        }

        public string IndexOutOfRangeError => $"Index must be an integer in the interval [{0}, {NoOfEntries - 1}].";

        protected ObservableCollection<int> Data;

        public abstract ISearchItem FindItem(int value);
    }

}
