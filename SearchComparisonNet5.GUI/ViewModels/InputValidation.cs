using FluentValidation;
using SearchComparisonNet5.Kernel.Models;
using System.Globalization;

namespace SearchComparisonNet5.GUI.ViewModels
{
    public class InputValidation : AbstractValidator<MainViewModel>
    {
        public InputValidation()
        {
            RuleSet("RuleForNoOfEntries", () =>
            {
                _ = RuleFor(vm => vm.NoOfEntriesText)
                    .NotNull().NotEmpty()
                    .WithMessage(ProblemConstants.NullOrEmptyNoOfEntriesMsg)
                    .Must(value => int.TryParse(value.ToString(), out _))
                    .WithMessage(ProblemConstants.InvalidNoOfEntriesMsg)
                    .Must(value => ProblemConstants.MinNoOfEntries <= int.Parse(value, CultureInfo.InvariantCulture) &&
                        int.Parse(value, CultureInfo.InvariantCulture) <= ProblemConstants.MaxNoOfEntries)
                    .WithMessage(ProblemConstants.OutOfRangeNoOfEntriesMsg);
            });


            // NoofSearches
            RuleSet("RuleForNoOfSearches", () =>
            {
                _ = RuleFor(vm => vm.NoOfSearchesText)
               .NotNull().NotEmpty()
               .WithMessage(ProblemConstants.NullOrEmptyNoOfSearchesMsg)
               .Must(value => int.TryParse(value.ToString(), out _))
               .WithMessage(ProblemConstants.InvalidNoOfSearchesMsg)
               .Must(value => ProblemConstants.MinNoOfSearches <= int.Parse(value, CultureInfo.InvariantCulture) &&
                   int.Parse(value, CultureInfo.InvariantCulture) <= ProblemConstants.MaxNoOfSearches)
               .WithMessage(ProblemConstants.OutOfRangeNoOfSearchesMsg);
            });
        }
    }
}
