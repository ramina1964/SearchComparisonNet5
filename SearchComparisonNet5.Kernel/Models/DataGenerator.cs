using SearchComparisonNet5.Kernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SearchComparisonNet5.Kernel.Models
{
    public class DataGenerator : IDataGenerator
    {
        public DataGenerator(DataParameters dataParameters)
        {
            Random = new Random();
            NoOfEntries = dataParameters.NoOfEntries;
            MinValue = dataParameters.MinValue;
            MaxValue = dataParameters.MaxValue;
        }

        public Random Random { get; }

        #region IDataGenerator
        public int NoOfEntries { get; }

        public int MinValue { get; }

        public int MaxValue { get; }

        public int NextRandomNo() => Random.Next(MinValue, MaxValue);

        public ObservableCollection<int> GenerateData()
        {
            var data = new HashSet<int>();
            var size = NoOfEntries;

            while (data.Count < size)
            { data.Add(Random.Next(MinValue, MaxValue)); }

            var result = data.ToList();
            result.Sort();
            return new ObservableCollection<int>(result);
        }
        #endregion IDataGenerator
    }
}
