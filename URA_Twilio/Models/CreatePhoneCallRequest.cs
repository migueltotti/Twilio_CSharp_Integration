using System.Text.Json.Serialization;

namespace URA_Twilio.Models;

public class CreatePhoneCallRequest
{
    [JsonPropertyName("from_number")]
    public string FromNumber { get; set; }

    [JsonPropertyName("to_number")]
    public string ToNumber { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    [JsonPropertyName("retell_llm_dynamic_variables")]
    public Dictionary<string, string>? RetellLlmDynamicVariables { get; set; }

    [JsonPropertyName("ignore_e164_validation")]
    public bool? IgnoreE164Validation { get; set; }
}