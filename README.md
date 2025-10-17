# Twilio_CSharp_Integration
This repo shows how to easily integrate Twilio in your ASP.NET Web API to make outbound calls and implement simple IVR.

## Start the Application
Remember to set your `TWILIO_ACCOUNT_SID`, `TWILIO_AUTH_TOKEN`, `TWILIO_PHONE_NUMBER` and `CLIENT_PHONE_NUMBER` environment variables to authenticate to Twilios API and be able to make the call.

### 1. Install ngrok
> Use ngrok only if you'r running this application localy.
> It enables Twilio to use Webhook with our API.

After installing, create a account in ngrok [site]("https://ngrok.com/") and set the `AUTH_TOKEN` in your machine.
```bash
ngrok config add-authtoken YOUR_AUTHTOKEN
```

### 2. Run you Application
If you're using ngrok, run the Application with hot-reload. If you're not using ngrok, just run it normaly
```bash
dotnet watch run
```

### 3. Start ngrok
You need to start ngrok with the same port used by your Application.
Assuming  the API is running http on port 5000, run this command to start ngrok.
```bash
ngrox http 50
```
After ngrok start, copy the given url and paste it in your code (sorry for this hard-code).

### 4. Hot Reload the Application
With the ngrok url set, just save your file and dotnet will automatically reload the Application

### 5. Make a call
Send a request to `/api/voice/start` from your API to start a call and check the ngrok console or web app (http://localhost:4040) to see Twilio making a request to the API.
