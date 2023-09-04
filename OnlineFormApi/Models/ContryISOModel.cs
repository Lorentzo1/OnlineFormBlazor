using System.Text.Json.Serialization;

namespace OnlineForm.Models
{
    public class ContryISOModel
    {
        [JsonPropertyName("Code")]
        public string Code { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }
    }
}
