using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using URA_Twilio.Gateways;
using URA_Twilio.Models;

namespace URA_Twilio.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RetellController(ILogger<RetellController> logger, IRetellGateway retellGateway) : ControllerBase
{
    /// <summary>
    /// Esse endpoint cria uma chamada usando o Agente de IA configurado para o `FromNumber` da Retell para o numero específicado em `ToNumber`
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("make_call")]
    public async Task<IActionResult> MakeCall(MakePhoneCallRequest request)
    {
        var response = await retellGateway.CreatePhoneCallAsync(request);

        if (!response.IsSuccessStatusCode)
            return BadRequest(response);
        
        return Ok(await response.Content.ReadAsStringAsync());
    }

    /// <summary>
    /// Esse endpoint simula a chamada da Função Customizada executada pelo Agente de IA para iniciar a conversa pelo WhatsApp.
    /// </summary>
    /// <returns></returns>
    [HttpPost("start")]
    public async Task<IActionResult> StartConversation()
    {
        using var reader = new StreamReader(Request.Body, Encoding.UTF8);
        
        var body = await reader.ReadToEndAsync();
        
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            PropertyNameCaseInsensitive = true
        };
        
        var request = JsonSerializer.Deserialize<RetellCustomFunctionRequest>(body, options);
        
        logger.LogInformation("Starting conversation for customer with phone number {PhoneNumber}", request.Args is null ? "+0000000000000" : request.Call.ToNumber );
        
        logger.LogInformation(body);

        return Ok();
    }

    /// <summary>
    /// Esse endpoint serve unicamente para a Retell se comunicar com a nossa API via webhook.
    /// A Retell envia um body que está detalhado na classe `RetellWebhookEvent` contendo as informações da chamada
    /// e o tipo do evento que foi lançado. 
    /// </summary>
    /// <returns></returns>
    [HttpPost("webhook-events")]
    public async Task<IActionResult> WebhookEvents()
    {
        using var reader = new StreamReader(Request.Body, Encoding.UTF8);
        
        var body = await reader.ReadToEndAsync();
        
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            PropertyNameCaseInsensitive = true
        };
        
        var request = JsonSerializer.Deserialize<RetellWebhookEvent>(body, options);

        if (request.Event is not null && request.Call is not null && request.Event == "call_ended")
        {
            logger.LogInformation("Call ended - call status: {CallStatus}", request.Call.CallStatus);
            
            logger.LogInformation(body);
        }
        
        logger.LogInformation("Webhook events received");

        return Ok();
    }

    [HttpGet("teste")]
    public IActionResult Teste()
    {
        return Ok("Retell controller is active!");
    }
}