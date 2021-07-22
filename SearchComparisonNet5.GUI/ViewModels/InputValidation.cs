using FluentValidation;
using SearchComparisonNet5.Kernel.Models;
using System.Globalization;

namespace SearchComparisonNet5.GUI.ViewModels
{
    public class InputValidation : AbstractValidator<MainViewModel>
    {
        //public static int MinNoOfEntries => 1000;

        //public static int MaxNoOfEntries => 200_000;

        //public static int MinNoOfSearches => 100;

        //public static int MaxNoOfSearches => 4_000;

        //public static int MinEntryValue => 0;

        //public static int MaxEntryValue => int.MaxValue;

        private static string NullOrEmptyNoOfEntriesMsg => "NoOfEntriesText is a required field.";

        private static string NullOrEmptyNoOfSearchesMsg => $"NoOfSearchesText is a required field.";

        private static string InvalidNoOfEntriesMsg => "NoOfEntriesText must be a valid integer.";

        private static string InvalidNoOfSearchesMsg => "NoOfSearchesText must be a valid integer.";

        private static string OutOfRangeNoOfEntriesMsg => "NoOfEntriesText must be an integer in the " +
                                                         $"interval [{ProblemConstants.MinNoOfEntries}, {ProblemConstants.MaxNoOfEntries}].";

        private static string OutOfRangeNoOfSearchesMsg => "NoOfSearchesText must be an integer in the " +
                                                          $"interval [{ProblemConstants.MinNoOfSearches}, {ProblemConstants.MaxNoOfSearches}].";

        public InputValidation()
        {
            RuleSet("RuleForNoOfEntries", () =>
            {
                _ = RuleFor(vm => vm.NoOfEntriesText)
                    .NotNull().NotEmpty()
                    .WithMessage(NullOrEmptyNoOfEntriesMsg)
                    .Must(value => int.TryParse(value.ToString(), out _))
                    .WithMessage(InvalidNoOfEntriesMsg)
                    .Must(value => ProblemConstants.MinNoOfEntries <= int.Parse(value, CultureInfo.InvariantCulture) &&
                        int.Parse(value, CultureInfo.InvariantCulture) <= ProblemConstants.MaxNoOfEntries)
                    .WithMessage(OutOfRangeNoOfEntriesMsg);
            });


            // NoofSearches
            RuleSet("RuleForNoOfSearches", () =>
            {
                _ = RuleFor(vm => vm.NoOfSearchesText)
               .NotNull().NotEmpty()
               .WithMessage(NullOrEmptyNoOfSearchesMsg)
               .Must(value => int.TryParse(value.ToString(), out _))
               .WithMessage(InvalidNoOfSearchesMsg)
               .Must(value => ProblemConstants.MinNoOfSearches <= int.Parse(value, CultureInfo.InvariantCulture) &&
                   int.Parse(value, CultureInfo.InvariantCulture) <= ProblemConstants.MaxNoOfSearches)
               .WithMessage(OutOfRangeNoOfSearchesMsg);
            });
        }
    }
}
