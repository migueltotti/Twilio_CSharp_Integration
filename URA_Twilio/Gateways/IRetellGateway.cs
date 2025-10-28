using URA_Twilio.Models;

namespace URA_Twilio.Gateways;

public interface IRetellGateway
{
    public Task<HttpResponseMessage> CreatePhoneCallAsync(MakePhoneCallRequest phoneCallRequest);
}