using System;
using System.Diagnostics.CodeAnalysis;

namespace PasswordValidator.Domain.Models.PasswordsRules.Abstractions
{
    [ExcludeFromCodeCoverage]
    public abstract class MinMaxPasswordRule : MinPasswordRule
    {
        private int _max = 0;
        public int Max
        {
            get => _max;
            set => _max = value >= 0 && value >= Min ? value : throw new ArgumentOutOfRangeException(nameof(Max));
        }

        private string _errorMsg;
        public override string ErrorMsg
        {
            get => _errorMsg;
            set => _errorMsg = string.Format(value, Min, Max);
        }
    }
}
