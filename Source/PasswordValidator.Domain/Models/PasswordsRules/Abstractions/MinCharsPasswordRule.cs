using System;
using System.Diagnostics.CodeAnalysis;

namespace PasswordValidator.Domain.Models.PasswordsRules.Abstractions
{
    [ExcludeFromCodeCoverage]
    public abstract class MinCharsPasswordRule : MinPasswordRule
    {
        private string _chars = "";
        public string Chars
        {
            get => _chars;
            set => _chars = value ?? throw new ArgumentNullException(nameof(Chars));
        }

        private string _errorMsg;
        public override string ErrorMsg
        {
            get => _errorMsg;
            set => _errorMsg = string.Format(value, Min, Chars);
        }
    }
}
