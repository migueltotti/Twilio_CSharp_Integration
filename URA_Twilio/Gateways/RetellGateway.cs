using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using URA_Twilio.Models;

namespace URA_Twilio.Gateways;

public class RetellGateway : IRetellGateway
{
    private readonly HttpClient _httpClient;
    private readonly string _apiToken;

    public RetellGateway(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiToken = Environment.GetEnvironmentVariable("RETELL_API_KEY") ?? "<token>";
    }

    public async Task<HttpResponseMessage> CreatePhoneCallAsync(MakePhoneCallRequest phoneCallRequest)
    {
        var requestData = new CreatePhoneCallRequest
        {
            FromNumber = phoneCallRequest.FromNumber,
            ToNumber = phoneCallRequest.ToNumber,
            Metadata = phoneCallRequest.Metadata ?? new Dictionary<string, object>(),
            RetellLlmDynamicVariables = phoneCallRequest.DynamicVariables ?? new Dictionary<string, string>(),
            IgnoreE164Validation = true
        };

        var json = JsonSerializer.Serialize(requestData, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.retellai.com/v2/create-phone-call");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        return await _httpClient.SendAsync(request);
    }
}