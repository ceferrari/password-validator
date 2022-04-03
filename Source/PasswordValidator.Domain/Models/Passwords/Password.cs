using System.Text.Json;
using System.Text.Json.Serialization;

namespace PasswordValidator.Domain.Models.Passwords
{
    public class Password
    {
        public Password(string value)
        {
            Value = value;
        }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
