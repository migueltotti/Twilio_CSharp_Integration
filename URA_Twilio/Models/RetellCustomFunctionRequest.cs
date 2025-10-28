using System.Text.Json.Serialization;

namespace URA_Twilio.Models;

public class RetellCustomFunctionRequest
{
    [JsonPropertyName("call")]
    public CallData Call { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("args")]
    public Dictionary<string, object> Args { get; set; }
}

public class CallData
{
    [JsonPropertyName("call_id")]
    public string CallId { get; set; }

    [JsonPropertyName("call_type")]
    public string CallType { get; set; }

    [JsonPropertyName("agent_id")]
    public string AgentId { get; set; }

    [JsonPropertyName("agent_version")]
    public int AgentVersion { get; set; }

    [JsonPropertyName("agent_name")]
    public string AgentName { get; set; }

    [JsonPropertyName("retell_llm_dynamic_variables")]
    public Dictionary<string, object> RetellLlmDynamicVariables { get; set; }

    [JsonPropertyName("collected_dynamic_variables")]
    public Dictionary<string, string> CollectedDynamicVariables { get; set; }

    [JsonPropertyName("custom_sip_headers")]
    public Dictionary<string, string> CustomSipHeaders { get; set; }

    [JsonPropertyName("call_status")]
    public string CallStatus { get; set; }

    [JsonPropertyName("start_timestamp")]
    public long StartTimestamp { get; set; }

    [JsonPropertyName("transcript")]
    public string Transcript { get; set; }

    [JsonPropertyName("transcript_object")]
    public List<TranscriptItem> TranscriptObject { get; set; }

    [JsonPropertyName("transcript_with_tool_calls")]
    public List<TranscriptWithToolCallItem> TranscriptWithToolCalls { get; set; }

    [JsonPropertyName("latency")]
    public Dictionary<string, object> Latency { get; set; }

    [JsonPropertyName("call_cost")]
    public CallCost CallCost { get; set; }

    [JsonPropertyName("opt_out_sensitive_data_storage")]
    public bool OptOutSensitiveDataStorage { get; set; }

    [JsonPropertyName("data_storage_setting")]
    public string DataStorageSetting { get; set; }

    [JsonPropertyName("from_number")]
    public string FromNumber { get; set; }

    [JsonPropertyName("to_number")]
    public string ToNumber { get; set; }

    [JsonPropertyName("direction")]
    public string Direction { get; set; }

    [JsonPropertyName("telephony_identifier")]
    public TelephonyIdentifier TelephonyIdentifier { get; set; }
}

public class TranscriptItem
{
    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("words")]
    public List<Word> Words { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object> Metadata { get; set; }
}

public class TranscriptWithToolCallItem : TranscriptItem
{
    [JsonPropertyName("tool_call_id")]
    public string ToolCallId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("arguments")]
    public string Arguments { get; set; }

    [JsonPropertyName("time_sec")]
    public double TimeSec { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}

public class Word
{
    [JsonPropertyName("word")]
    public string Text { get; set; }

    [JsonPropertyName("start")]
    public double Start { get; set; }

    [JsonPropertyName("end")]
    public double End { get; set; }
}

public class CallCost
{
    [JsonPropertyName("product_costs")]
    public List<object> ProductCosts { get; set; }

    [JsonPropertyName("total_duration_seconds")]
    public int TotalDurationSeconds { get; set; }

    [JsonPropertyName("total_duration_unit_price")]
    public decimal TotalDurationUnitPrice { get; set; }

    [JsonPropertyName("combined_cost")]
    public decimal CombinedCost { get; set; }
}

public class TelephonyIdentifier
{
    [JsonPropertyName("twilio_call_sid")]
    public string TwilioCallSid { get; set; }
}