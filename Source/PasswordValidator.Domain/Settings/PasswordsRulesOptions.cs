using PasswordValidator.Domain.Models.PasswordsRules;
using System.Diagnostics.CodeAnalysis;

namespace PasswordValidator.Domain.Settings
{
    [ExcludeFromCodeCoverage]
    public class PasswordsRulesOptions
    {
        public AllowedPasswordRule Allowed { get; set; }
        public SpecialPasswordRule Special { get; set; }
        public LowercasePasswordRule Lowercase { get; set; }
        public UppercasePasswordRule Uppercase { get; set; }
        public NumericPasswordRule Numeric { get; set; }
        public LengthPasswordRule Length { get; set; }
        public NotRepeatedPasswordRule NotRepeated { get; set; }
    }
}
