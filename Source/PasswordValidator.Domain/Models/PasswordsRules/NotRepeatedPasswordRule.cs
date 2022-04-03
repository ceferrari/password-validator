using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Models.PasswordsRules.Abstractions;
using System.Collections.Generic;

namespace PasswordValidator.Domain.Models.PasswordsRules
{
    public class NotRepeatedPasswordRule : PasswordRule
    {
        public override bool IsValid(Password password)
        {
            HashSet<char> chars = new();

            foreach (char c in password.Value)
            {
                // quando o método Add() do HashSet retorna false, significa que o elemento já está presente,
                // portanto temos um caractere duplicado e não é necessário continuar a iteração na string

                if (!chars.Add(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
