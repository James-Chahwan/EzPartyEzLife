using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class Configuration
{
    //these variables will store the clientID and clientSecret
    //by reading them from the web.config
    public readonly static string ClientId = "AYeGPV8LHSgMwLZbz9rjIN_xabY29MLmkVrgToRE7YhthnOzhNFHchkyg9spjGeCFmN00Te3w-GEN2xx";
    public readonly static string ClientSecret = "EHR6D0095CBYEEk8bqzhmuKwP6sFofBtMyq6QF4XURFTjIqM47HM3FCdpbH841_o18sNx87FdVO4AeZ4";

    static Configuration()
    {
        var config = GetConfig();

    }

    // getting properties from the web.config
    public static Dictionary<string, string> GetConfig()
    {
        Dictionary<string, string> paypalConfig = new Dictionary<string, string>();
        paypalConfig.Add("mode", "sandbox");
        paypalConfig.Add("account1.apiUsername", "j.r.chahwan-facilitator_api1.gmail.com");
        paypalConfig.Add("account1.apiPassword", "4ZB3W9CLE6GTQXS9");
        paypalConfig.Add("account1.apiSignature", "AFcWxV21C7fd0v3bYYYRCpSSRl31ALYErp.Tl8VD8qgOjeBF4E87Mx..");
        paypalConfig.Add("account1.applicationId", "ezPartyezLife");
        return paypalConfig;
    }

    private static string GetAccessToken()
    {
        // getting accesstocken from paypal                
        string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();

        return accessToken;
    }

    public static APIContext GetAPIContext()
    {
        // return apicontext object by invoking it with the accesstoken
        APIContext apiContext = new APIContext(GetAccessToken());
        apiContext.Config = GetConfig();
        return apiContext;
    }
}