using System;
using System.Collections.ObjectModel;

namespace SearchComparisonNet5.Kernel.Interfaces
{
    public interface IDataGenerator
    {
        int NoOfEntries { get; }

        int MinValue { get; }

        int MaxValue { get; }

        int NextRandomNo();

        ObservableCollection<int> GenerateData();
    }
}
