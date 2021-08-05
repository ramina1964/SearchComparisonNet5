using FluentValidation;
using Microsoft.Toolkit.Mvvm.Input;
using SearchComparisonNet5.Kernel.Interfaces;
using SearchComparisonNet5.Kernel.Models;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SearchComparisonNet5.GUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(LinearSearch linearSearch, BinarySearch binarySearch)
        {
            SimulateCommand = new RelayCommand(Simulate, CanSimulate);
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            LinearSearch = linearSearch;
            BinarySearch = binarySearch;

            InputValidation = new InputValidation() { CascadeMode = CascadeMode.Stop };
            NoOfEntriesText = ProblemConstants.InitialNoOfEntries.ToString();
            NoOfSearchesText = ProblemConstants.InitialNoOfSearches.ToString();

            IsSimulating = false;
            ProgressBarVisibility = Visibility.Hidden;
        }

        /************************************ Public Attributes ************************************/
        public RelayCommand SimulateCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        public InputValidation InputValidation { get; set; }

        public ISearchItem SearchItem { get; set; }

        public bool IsSimulating
        {
            get => _isSimulating;
            set
            {
                if (SetProperty(ref _isSimulating, value))
                {
                    UpdateButtonFunctionality();
                }
            }
        }

        private void UpdateButtonFunctionality()
        {
            SimulateCommand.NotifyCanExecuteChanged();
            CancelCommand.NotifyCanExecuteChanged();
        }

        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set => SetProperty(ref _progressBarVisibility, value);
        }

        public double ProgressBarValue
        {
            get => _progressBarValue;
            set
            {
                _ = SetProperty(ref _progressBarValue, value);
                ProgressBarLabel = Math.Round(value, 0) + "%";
                OnPropertyChanged(nameof(ProgressBarLabel));
            }
        }

        public string ProgressBarLabel { get; set; }

        public ISimulationResults LinearSearchResults { get; set; }

        public ISimulationResults BinarySearchResults { get; set; }

        public string NoOfEntriesText
        {
            get => _noOfEntriesText;
            set
            {
                _ = SetProperty(ref _noOfEntriesText, value);
                IsInputValid = InputValidation.Validate(this).IsValid;

                if (!IsInputValid)
                {
                    IsSimulating = false;
                    return;
                }

                // Here: IsInputValid == true;
                NoOfEntries = int.Parse(value);
                UpdateButtonFunctionality();
            }
        }

        public string NoOfSearchesText
        {
            get => _noOfSearchesText;
            set
            {
                _ = SetProperty(ref _noOfSearchesText, value);
                IsInputValid = InputValidation.Validate(this).IsValid;

                if (!IsInputValid)
                {
                    IsSimulating = false;
                    return;
                }

                // Here: IsInputValid == true;
                NoOfSearches = int.Parse(value);
                UpdateButtonFunctionality();
            }
        }

        public int NoOfEntries
        {
            get => _noOfEntries;
            set => SetProperty(ref _noOfEntries, value);
        }

        public int NoOfSearches
        {
            get => _noOfSearches;
            set => SetProperty(ref _noOfSearches, value);
        }

        public int TargetValue
        {
            get => _targetValue;

            set
            {
                var valid = int.TryParse(value.ToString(), out int result);
                if (!valid)
                { return; }

                if (SetProperty(ref _targetValue, result))
                {
                    var index = BinarySearch.FindItem(value).TargetIndex;
                    TargetIndex = index.HasValue ? index : null;
                }
            }
        }

        public int? TargetIndex
        {
            get => _targetIndex;
            set => SetProperty(ref _targetIndex, value);
        }

        public string Entries { get; set; }

        public double BinaryAvgNoOfIterations
        {
            get => _binaryAvgNoOfIterations;
            set => SetProperty(ref _binaryAvgNoOfIterations, value);
        }

        public double BinaryAvgElapsedTime
        {
            get => _binaryAvgElapsedTime;
            set => SetProperty(ref _binaryAvgElapsedTime, value);
        }

        public double LinearAvgNoOfIterations
        {
            get => _linearAvgNoOfIterations;
            set => SetProperty(ref _linearAvgNoOfIterations, value);
        }

        public double LinearAvgElapsedTime
        {
            get => _linearAvgElapsedTime;
            set => SetProperty(ref _linearAvgElapsedTime, value);
        }

        /***************************************** Private Methods *****************************************/
        private bool CanSimulate() => !IsSimulating && IsInputValid;

        private bool CanCancel() => IsSimulating;

        private async void Simulate()
        {
            IsSimulating = true;
            LinearSearchResults = await SimulateLinearSearchAsync();
            BinarySearchResults = await SimulateBinarySearchAsync();

            LinearAvgNoOfIterations = LinearSearchResults.AvgNoOfIterations;
            LinearAvgElapsedTime = LinearSearchResults.AvgElapsedTime;

            BinaryAvgNoOfIterations = BinarySearchResults.AvgNoOfIterations;
            BinaryAvgElapsedTime = BinarySearchResults.AvgElapsedTime;
            IsSimulating = false;
        }

        private void Cancel() { }

        private Task<ISimulationResults> SimulateLinearSearchAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                ProgressBarVisibility = Visibility.Visible;
                var totalNoOfIterations = 0.0;
                var stopwatch = Stopwatch.StartNew();
                for (var j = 0; j < NoOfSearches; j++)
                {
                    var value = LinearSearch.NextRandomNo();
                    var searchItem = LinearSearch.FindItem(value);
                    totalNoOfIterations += searchItem.NoOfIterations;
                    ProgressBarValue = (j + 1) * 100.0 / NoOfSearches;
                }
                stopwatch.Stop();
                var timeInSec = (double)stopwatch.ElapsedMilliseconds / 1000;
                ProgressBarValue = 0;
                ProgressBarVisibility = Visibility.Hidden;

                var elapsedTimeInSec = Math.Round(timeInSec, 1);
                return SimulationResults(totalNoOfIterations, elapsedTimeInSec);
            });
        }

        private Task<ISimulationResults> SimulateBinarySearchAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                var totalNoOfIterations = 0.0;
                var stopwatch = Stopwatch.StartNew();
                for (var j = 0; j < NoOfSearches; j++)
                {
                    var value = LinearSearch.NextRandomNo();
                    var searchItem = BinarySearch.FindItem(value);
                    totalNoOfIterations += searchItem.NoOfIterations;
                    ProgressBarValue = 100 * (j + 1) / NoOfSearches;
                }
                stopwatch.Stop();
                var timeInSec = (double)stopwatch.ElapsedMilliseconds / 1000;
                ProgressBarValue = 0;
                ProgressBarVisibility = Visibility.Hidden;

                var elapsedTimeInSec = Math.Round(timeInSec, 5);
                return SimulationResults(totalNoOfIterations, elapsedTimeInSec);
            });
        }

        private ISimulationResults SimulationResults(double totalNoOfIterations, double totalElapsedTime) =>
            new SimulationResults()
            {
                NoOfEntries = NoOfEntries,
                NoOfSearches = NoOfSearches,
                AvgNoOfIterations = totalNoOfIterations / NoOfSearches,
                AvgElapsedTime = totalElapsedTime / NoOfSearches
            };

        private string GetEntries()
        {
            if (NoOfEntries <= 32)
            {
                return GetSubString(0, NoOfEntries - 1).ToString();
            }

            const string subEtc = " ..., ";
            var mid = NoOfEntries / 2;

            var startStr = GetSubString(0, 4);
            var midStr = GetSubString(mid, mid + 2);
            var endStr = GetSubString(NoOfEntries - 2, NoOfEntries - 1);

            return startStr.Append(subEtc).Append(midStr).Append(subEtc).Append(endStr).ToString();
        }

        private StringBuilder GetSubString(int fromIndex, int toIndex)
        {
            var sb = new StringBuilder();
            for (var i = fromIndex; i < toIndex; i++)
            {
                sb.Append(BinarySearch[i] + ", ");
            }

            return (toIndex == NoOfEntries - 1)
                ? sb.Append(BinarySearch[toIndex])
                : sb.Append(BinarySearch[toIndex] + ", ");
        }

        private SearchBase LinearSearch { get; set; }

        private SearchBase BinarySearch { get; set; }

        private bool IsInputValid
        {
            get => _isInputValid;
            set
            {
                if (SetProperty(ref _isInputValid, value))
                {
                    UpdateButtonFunctionality();
                }
            }
        }

        /***************************************** Private Fields ******************************************/
        private static readonly long MinProductValue = (long)1e5;
        private static readonly long MaxProductValue = (long)5e10;
        private static readonly string MaxProductError =
            $"Product of No. of searches and No. of entries must be in the interval [{MinProductValue}, {MaxProductValue}].";

        private int _targetValue;
        private string _noOfEntriesText;
        private string _noOfSearchesText;
        private int _noOfEntries;
        private int _noOfSearches;
        private bool _isInputValid;
        private bool _isSimulating;
        private Visibility _progressBarVisibility;
        private double _progressBarValue;
        private int? _targetIndex;
        private double _linearAvgNoOfIterations;
        private double _linearAvgElapsedTime;
        private double _binaryAvgNoOfIterations;
        private double _binaryAvgElapsedTime;
    }
}
