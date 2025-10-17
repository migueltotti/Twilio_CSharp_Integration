using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Studio.V2.Flow;
using Twilio.Rest.Voice.V1.DialingPermissions.Country;
using Twilio.TwiML;
using Twilio.TwiML.Voice;
using Twilio.Types;

namespace URA_Twilio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoiceController(ILogger<VoiceController> logger) : TwilioController
    {
        private readonly string _accountId = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID")!;
        private readonly string _authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN")!;

        private const string ApiUrl = "https://dizygotic-ethelene-elvishly.ngrok-free.dev";

        private readonly string _twilioPhoneNumber = Environment.GetEnvironmentVariable("TWILIO_PHONE_NUMBER")!;
        private readonly string _clientPhoneNumber = Environment.GetEnvironmentVariable("CLIENT_PHONE_NUMBER")!;

        [HttpPost("start")]
        public async Task<IActionResult> StartCall()
        {
            TwilioClient.Init(_accountId, _authToken);

            var response = new VoiceResponse();
            
            var gather = new Gather(
                action: new Uri($"{ApiUrl}/api/voice/handle-response"), 
                method: Twilio.Http.HttpMethod.Post,
                input: [Gather.InputEnum.Speech],
                language: Gather.LanguageEnum.PtBr,
                speechTimeout: "auto",
                hints: "sim, não"
            );
            
            gather.Say(
                message: "Olá, somos da empresa SmartConsig e verificamos que você pode ter juros abusivos nas parcelas de um financiamento." + 
                         " Podemos iniciar uma conversa pelo WhatsApp para continuar?",
                language: Say.LanguageEnum.PtBr,
                voice:  Say.VoiceEnum.GooglePtBrChirp3HdCharon
            );

            response.Append(gather);
            
            logger.LogInformation(response.ToString());
            
            var call = await CallResource.CreateAsync(
                to: new PhoneNumber(_clientPhoneNumber),
                from: new PhoneNumber(_twilioPhoneNumber),
                twiml: new Twiml(response.ToString())
            );

            return Ok(call);
        }

        [HttpPost("handle-response")]
        public TwiMLResult HandleResponse([FromForm] string speechResult, [FromForm] decimal? confidence)   
        {
            logger.LogInformation("Speech Result: {SpeechResult}, Confidence: {Confidence}", 
                speechResult ?? "null", confidence ?? 0);
            
            var speechText = speechResult?.Trim().TrimEnd('.', '!', '?').ToLower() ?? "";
    
            var response = new VoiceResponse();

            if (!string.IsNullOrEmpty(speechText) && speechText.ToLower().Contains("sim"))
            {
                response.Say(
                    "Obrigado pela resposta, vamos iniciar uma conversa no WhatsApp.",
                    voice: Say.VoiceEnum.GooglePtBrChirp3HdAoede,
                    language: Say.LanguageEnum.PtBr);

                logger.LogInformation("=== Response received and initiate conversation ===");
            }
            else
            {
                response.Say(
                    "Obrigado pela resposta, tenha um bom dia!",
                    voice: Say.VoiceEnum.GooglePtBrChirp3HdAoede,
                    language: Say.LanguageEnum.PtBr);

                logger.LogInformation("=== Response received and handled ===");
            }

            response.Hangup();

            return TwiML(response);
        }
        
        [HttpGet("teste")]
        public IActionResult Get()
        {
            return Ok("VoiceController is running.");
        }
    }
}
