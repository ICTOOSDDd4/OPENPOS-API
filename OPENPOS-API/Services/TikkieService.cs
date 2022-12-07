using System.Diagnostics;
using System.Threading.Tasks.Dataflow;
using Newtonsoft.Json.Linq;
using OpenPOS_API.Models;
using RestSharp;

namespace OPENPOS_API.Services;

public static class TikkieService
{
    public static string _tikkieAppToken;
    public static void CreateTikkieAppToken(IConfiguration config)
    {
        var client = new RestClient(config.GetValue<string>("TikkieBaseUrl"));
        var request = new RestRequest("/sandboxapps", Method.Post);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("API-Key", config.GetValue<string>("TikkieAPIKey"));
        RestResponse response = client.Execute(request);
        var content = response.Content;
				
        if (content != null)
        {
            var obj = JObject.Parse(content);
            _tikkieAppToken = obj["appToken"]?.ToString();
        } else Debug.WriteLine("No content");
    }
    public static bool SubscribeToNotifications(IConfiguration config)
    {
        
        var client = new RestClient(config.GetValue<string>("TikkieBaseUrl"));
        var request = new RestRequest("/paymentrequestssubscription");
        request.AddHeader("X-App-Token", _tikkieAppToken);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("API-Key", config.GetValue<string>("TikkieBaseUrl"));
        request.AddBody(new
        {
           url = "http://localhost:5175/api/Tikkie/paymentNotification"
        });
            
        RestResponse response = client.Execute(request);
        if (response.Content != null)
        {
            var obj = JObject.Parse(response.Content);
            if (obj["errors"] != null) // If API returns an error.
            {
                throw new Exception($"Error: {obj["errors"][0]?["message"]} ");
            }
            return true;
        }
        return false;
    }
}