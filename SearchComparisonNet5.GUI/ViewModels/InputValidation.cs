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
            // NoOfEntries
            int noOfEntries = -1;
            _ = RuleFor(vm => vm.NoOfEntriesText)
                .NotNull().NotEmpty()
                .WithMessage(ProblemConstants.NullOrEmptyNoOfEntriesMsg)
                .Must(noe => int.TryParse(noe, out noOfEntries))
                .WithMessage(ProblemConstants.InvalidNoOfEntriesMsg)
                .Must(_ => ProblemConstants.MinNoOfEntries <= noOfEntries && noOfEntries <= ProblemConstants.MaxNoOfEntries)
                .WithMessage(ProblemConstants.OutOfRangeNoOfEntriesMsg);

            // NoOfSearches
            int noOfSearches = -1;
            _ = RuleFor(vm => vm.NoOfSearchesText)
               .NotNull().NotEmpty()
               .WithMessage(ProblemConstants.NullOrEmptyNoOfSearchesMsg)
               .Must(nos => int.TryParse(nos, out noOfSearches))
               .WithMessage(ProblemConstants.InvalidNoOfSearchesMsg)
               .Must(_ => ProblemConstants.MinNoOfSearches <= noOfSearches && noOfSearches <= ProblemConstants.MaxNoOfSearches)
               .WithMessage(ProblemConstants.OutOfRangeNoOfSearchesMsg);
        }
    }
}
