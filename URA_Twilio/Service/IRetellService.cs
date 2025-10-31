namespace URA_Twilio.Service;

public interface IRetellService
{
    public bool VerifyRetellOrigination(string rawBody, string authenticationHeaderValue);
}