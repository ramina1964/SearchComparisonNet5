using FluentValidation;
using SearchComparisonNet5.Kernel.Models;
using System.Globalization;

namespace SearchComparisonNet5.GUI.ViewModels
{
    public class InputValidation : AbstractValidator<MainViewModel>
    {
        #region Constructor

        public InputValidation() => ValidationRules();

        #endregion Constructor

        private void ValidationRules()
        {
            // NoofEntries
            _ = RuleFor(vm => vm.NoOfEntriesText)
                .NotNull().NotEmpty()
                .WithMessage(ProblemConstants.NullOrEmptyNoOfEntriesMsg)
                .Must(noe => int.TryParse(noe, out var noOfEntries))
                .WithMessage(ProblemConstants.InvalidNoOfEntriesMsg)
                .Must(noe => ProblemConstants.MinNoOfEntries <= int.Parse(noe) && int.Parse(noe) <= ProblemConstants.MaxNoOfEntries)
                .WithMessage(ProblemConstants.OutOfRangeNoOfEntriesMsg);

            // NoofSearches
            _ = RuleFor(vm => vm.NoOfSearchesText)
               .NotNull().NotEmpty()
               .WithMessage(ProblemConstants.NullOrEmptyNoOfSearchesMsg)
               .Must(value => int.TryParse(value.ToString(), out _))
               .WithMessage(ProblemConstants.InvalidNoOfSearchesMsg)
               .Must(value => ProblemConstants.MinNoOfSearches <= int.Parse(value, CultureInfo.InvariantCulture) &&
                   int.Parse(value, CultureInfo.InvariantCulture) <= ProblemConstants.MaxNoOfSearches)
               .WithMessage(ProblemConstants.OutOfRangeNoOfSearchesMsg);
        }
    }
}
