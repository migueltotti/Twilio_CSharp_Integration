using System.Text.Json.Serialization;

namespace URA_Twilio.Models;

public class RetellWebhookEvent
{
    [JsonPropertyName("event")]
    public string Event { get; set; }
    
    [JsonPropertyName("call")]
    public CallData Call { get; set; }
}