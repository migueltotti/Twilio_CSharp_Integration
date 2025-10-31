using System.Security.Cryptography;
using System.Text;
using Telnyx.net.Entities.VerifyProfiles;

namespace URA_Twilio.Service;

public class RetellService(ILogger<RetellService> logger) : IRetellService
{
    private readonly string _apiKey = Environment.GetEnvironmentVariable("RETELL_API_KEY") ?? "<token>";

    public bool VerifyRetellOrigination(string rawBody, string authenticationHeaderValue)
    {
        if (string.IsNullOrEmpty(authenticationHeaderValue))
            return false;

        var parts = authenticationHeaderValue.Split(',');
        var timestamp = parts.First(p => p.StartsWith("v=")).Substring(2);
        var receivedSignature = parts.First(p => p.StartsWith("d=")).Substring(2);
        
        if (string.IsNullOrEmpty(_apiKey))
            return false;

        var signedPayload = $"{timestamp}.{rawBody}";

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_apiKey));
        var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(signedPayload));
        var computedSignature = BitConverter.ToString(hashBytes);
        
        computedSignature = computedSignature.Replace("-", "");
        
        logger.LogInformation("ComputedHash: {hash}", computedSignature);
        logger.LogInformation("ReceivedSignatureHash: {hash}", receivedSignature);

        return computedSignature == receivedSignature;
    }
}