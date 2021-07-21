using FluentValidation;
using FluentValidation.Results;
using Microsoft.Toolkit.Mvvm.Input;
using SearchComparisonNet5.Kernel.Interfaces;
using SearchComparisonNet5.Kernel.Models;
using SearchComparisonNet5.Kernel.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SearchComparisonNet5.GUI.ViewModels
{
    public class MainViewModel : ExtendedViewModel
    {
        public MainViewModel(ISearchItem searchItem)
        {
            SearchItem = searchItem;
            SimulateCommand = new RelayCommand(Simulate, CanSimulate);
            CancelCommand = new RelayCommand(Cancel, CanCancel);

            InputValidation = new InputValidation { CascadeMode = CascadeMode.Stop };
            NoOfEntries = (int)5e5;
            NoOfSearches = (int)1e3;

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
            set => SetProperty(ref _isSimulating, value);
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

        public int NoOfEntries
        {
            get => _noOfEntries;
            set => SetProperty(ref _noOfEntries, value);
            //set
            //{
            //    var isSet = SetProperty(ref _noOfEntries, value);
            //    if (!isSet) return;

            //    ValidationResult = InputValidation.Validate(this);
            //    IsInputValid = ValidationResult.IsValid;
            //    SimulateCommand.NotifyCanExecuteChanged();
            //}
        }

        public int NoOfSearches
        {
            get => _noOfSearches;
            set
            {
                SetProperty(ref _noOfSearches, value);
                ValidationResult = InputValidation.Validate(this);
                IsInputValid = ValidationResult.IsValid;
                SimulateCommand.NotifyCanExecuteChanged();
            }
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
        private void DataValidation()
        {
            // Validate No. of Entries property
            if (PropErrors.TryGetValue(nameof(NoOfEntries), out var listErrors) == false)
                listErrors = new List<string>();

            else
                listErrors.Clear();

            if (string.IsNullOrEmpty(nameof(NoOfEntries)))
                listErrors.Add("No. of Entries is required.");

            if (InputValidation.MinNoOfEntries > NoOfEntries || NoOfEntries > InputValidation.MaxNoOfEntries)
                listErrors.Add(Resources.NoOfEntriesTooSmallError);

            PropErrors[nameof(NoOfEntries)] = listErrors;

            if (listErrors.Count > 0)
            {
                OnPropertyErrorsChanged(nameof(NoOfEntries));
            }

            // Validate No. of Searches property
            if (PropErrors.TryGetValue(nameof(NoOfSearches), out listErrors) == false)
                listErrors = new List<string>();

            else
                listErrors.Clear();

            if (string.IsNullOrEmpty(nameof(NoOfSearches)))
                listErrors.Add("No. of Searches is required.");

            if (InputValidation.MinNoOfSearches > NoOfSearches || NoOfSearches > InputValidation.MaxNoOfSearches)
                listErrors.Add(Resources.NoOfSearchesTooLargeError);

            PropErrors[nameof(NoOfSearches)] = listErrors;

            if (listErrors.Count > 0)
            {
                OnPropertyErrorsChanged(nameof(NoOfSearches));
            }
        }

        private bool CanSimulate() => !IsSimulating && IsInputValid;

        private bool CanCancel() => IsSimulating;

        private void Simulate()
        {
            IsSimulating = true;
            var searches = Initialize();
            new Thread(() => Simulate(searches)).Start();
            IsSimulating = false;
        }

        private BaseSearch[] Initialize()
        {
            ProgressBarVisibility = Visibility.Visible;

            ProgressBarLabel = string.Empty;
            LinearSearch = new LinearSearch(SearchItem, NoOfEntries);
            BinarySearch = new BinarySearch(SearchItem, NoOfEntries);

            LinearAvgNoOfIterations = 0;
            LinearAvgElapsedTime = 0;
            BinaryAvgNoOfIterations = 0;
            BinaryAvgElapsedTime = 0;

            return new BaseSearch[] { LinearSearch, BinarySearch };
        }

        private void Cancel()
        {
        }

        private async void Simulate(params BaseSearch[] searchTypes)
        {
            IsSimulating = true;
            searchTypes[0].InitializeData();
            searchTypes[1].InitializeData();

            Entries = GetEntries();
            LinearSearchResults = await SimulateLinearSearchAsync(searchTypes[0]).ConfigureAwait(true);
            BinarySearchResults = await SimulateBinarySearchAsync(searchTypes[1]).ConfigureAwait(true);

            LinearAvgNoOfIterations = LinearSearchResults.AvgNoOfIterations;
            LinearAvgElapsedTime = LinearSearchResults.AvgElapsedTime;

            BinaryAvgNoOfIterations = BinarySearchResults.AvgNoOfIterations;
            BinaryAvgElapsedTime = BinarySearchResults.AvgElapsedTime;
            IsSimulating = false;
        }

        private Task<ISimulationResults> SimulateLinearSearchAsync(BaseSearch linearSearch)
        {
            return Task.Factory.StartNew(() =>
            {
                var totalNoOfIterations = 0.0;
                var stopwatch = Stopwatch.StartNew();
                for (var j = 0; j < NoOfSearches; j++)
                {
                    var value = BaseSearch.Random.Next(linearSearch.StartValue, linearSearch.EndValue);
                    var searchItem = linearSearch.FindItem(value);
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

        private Task<ISimulationResults> SimulateBinarySearchAsync(BaseSearch binarySearch)
        {
            return Task.Factory.StartNew(() =>
            {
                var totalNoOfIterations = 0.0;
                var stopwatch = Stopwatch.StartNew();
                for (var j = 0; j < NoOfSearches; j++)
                {
                    var value = BaseSearch.Random.Next(binarySearch.StartValue, binarySearch.EndValue);
                    var searchItem = binarySearch.FindItem(value);
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

        private bool IsInputValid
        {
            get => _isInputValid;
            set
            {
                SetProperty(ref _isInputValid, value);
                SimulateCommand.NotifyCanExecuteChanged();
            }
        }

        private LinearSearch LinearSearch { get; set; }

        private BinarySearch BinarySearch { get; set; }

        public ValidationResult ValidationResult { get; set; }

        /***************************************** Private Fields ******************************************/
        private static readonly long MinProductValue = (long)1e5;
        private static readonly long MaxProductValue = (long)5e10;
        private static readonly string MaxProductError =
            $"Product of No. of searches and No. of entries must be in the interval [{MinProductValue}, {MaxProductValue}].";

        private int _targetValue;
        private int _noOfEntries;
        private int _noOfSearches;
        private bool _isSimulating;
        private Visibility _progressBarVisibility;
        private double _progressBarValue;
        private int? _targetIndex;
        private double _linearAvgNoOfIterations;
        private double _linearAvgElapsedTime;
        private double _binaryAvgNoOfIterations;
        private double _binaryAvgElapsedTime;
        private bool _isInputValid;
    }
}
