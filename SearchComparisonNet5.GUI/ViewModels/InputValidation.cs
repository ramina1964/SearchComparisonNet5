using FluentValidation;
using SearchComparisonNet5.Kernel.Properties;

namespace SearchComparisonNet5.GUI.ViewModels
{
    public class InputValidation : AbstractValidator<MainViewModel>
    {
        public InputValidation() => ValidationRules();

        public static int MinNoOfEntries => 1000;

        public static int MaxNoOfEntries => 10000000;

        public static int MinNoOfSearches => 100;

        public static int MaxNoOfSearches => 1000000;

        private void ValidationRules()
        {
            // No. of Entries
            _ = RuleFor(sm => sm.NoOfEntries)
                .NotEmpty()
                .WithMessage(sm => string.Format(Resources.ValueNullOrWhiteSpaceError, nameof(sm.NoOfEntries)))
                .Must(noOfEntries => int.TryParse(noOfEntries.ToString(), out int _))
                .WithMessage(sm => string.Format(Resources.InvalidIntegerError, nameof(sm.NoOfEntries)))
                .Must(noOfEntries => noOfEntries >= MinNoOfEntries)
                .WithMessage(sm => string.Format(Resources.NoOfEntriesTooSmallError, nameof(sm.NoOfEntries), MinNoOfEntries))
                .Must(noOfEntries => noOfEntries <= MaxNoOfEntries)
                .WithMessage(sm => string.Format(Resources.NoOfEntriesTooSmallError, nameof(sm.NoOfEntries), MaxNoOfEntries));

            // No. of Searches
            _ = RuleFor(sm => sm.NoOfSearches)
                .NotEmpty()
                .WithMessage(sm => string.Format(Resources.ValueNullOrWhiteSpaceError, nameof(sm.NoOfSearches)))
                .Must(noOfSearches => int.TryParse(noOfSearches.ToString(), out int _))
                .WithMessage(sm => string.Format(Resources.InvalidIntegerError, nameof(sm.NoOfSearches)))
                .Must(noOfSearches => noOfSearches >= MinNoOfSearches)
                .WithMessage(sm => string.Format(Resources.NoOfSearchesTooSmallError, nameof(sm.NoOfSearches), MinNoOfSearches))
                .Must(noOfSearches => int.Parse(noOfSearches.ToString()) <= MaxNoOfSearches)
                .WithMessage(sm => string.Format(Resources.NoOfSearchesTooLargeError, nameof(sm.NoOfSearches), MaxNoOfSearches));
        }
    }
}
