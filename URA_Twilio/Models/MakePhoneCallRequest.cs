namespace URA_Twilio.Models;

public record MakePhoneCallRequest(string FromNumber, string ToNumber, Dictionary<string, string>? DynamicVariables = null,
    Dictionary<string, object>? Metadata = null);