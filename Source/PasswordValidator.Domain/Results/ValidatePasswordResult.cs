using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PasswordValidator.Domain.Results
{
    public class ValidatePasswordResult
    {
        public ValidatePasswordResult(IEnumerable<string> errors = null)
        {
            // atribui null se a collection for nula ou vazia para ativar o decorator JsonIgnore,
            // escondendo a prop 'errors' na resposta em caso de sucesso na validação

            Errors = errors?.Any() == true ? errors : null;
        }

        [JsonPropertyName("is_valid")]
        public bool IsValid { get => Errors == null; }

        [JsonPropertyName("errors")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<string> Errors { get; private set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public static ValidatePasswordResult FromJson(string json)
        {
            return JsonSerializer.Deserialize<ValidatePasswordResult>(json);
        }
    }
}
