using System.Transactions;
using Newtonsoft.Json.Linq;
using OpenPOS_API.Models;
using RestSharp;

namespace OPENPOS_API.Services;

public class TikkieService
{
    public string GetTransactionInformation(IConfiguration config)
    {
        
        var client = new RestClient(ApplicationSettings.TikkieSet.BaseUrl);
        var request = new RestRequest($"/paymentrequests/{paymentRequestToken}");
        request.AddHeader("X-App-Token", _tikkieAppToken);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("API-Key", ApplicationSettings.TikkieSet.Key);
            
        RestResponse response = client.Execute(request);
        if (response.Content != null)
        {
            var obj = JObject.Parse(response.Content);
            if (obj["errors"] != null) // If API returns a error.
            {
                throw new Exception($"Error: {obj["errors"][0]?["message"]} ");
            }
            new Tikkie
            {
                
            };
            return 
        }
        return null;
    }
}