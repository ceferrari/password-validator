using System;
using System.Diagnostics.CodeAnalysis;

namespace PasswordValidator.Domain.Models.PasswordsRules.Abstractions
{
    [ExcludeFromCodeCoverage]
    public abstract class MinPasswordRule : PasswordRule
    {
        private int _min = 0;
        public int Min
        {
            get => _min;
            set => _min = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(Min));
        }

        private string _errorMsg;
        public override string ErrorMsg
        {
            get => _errorMsg;
            set => _errorMsg = string.Format(value, Min);
        }
    }
}
