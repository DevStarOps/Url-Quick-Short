using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Url_Quick_Short.Integrate.Fields;

namespace Url_Quick_Short.Integrate.Bitly
{
    public class BitlyPlugin : IIntegration
    {
        public const string BitlyBaseUrl = "https://api-ssl.bitly.com";
        public const string Field_Username = "Bitly Username";
        public const string Field_Password = "Bitly Password";


        public List<FieldBase> AuthenticationFields => new List<FieldBase>
        {
            new TextField(Field_Username),
            new SecureTextField(Field_Password),
        };

        public Guid Id => new Guid("{2FA7BEE4-E5B8-42D8-9B88-7DAE56F81152}");

        public string Name => "Bitly";

        public string SharedSecret => Id.ToString();

        public async Task<string> GetAuthenticationData(Dictionary<string, string> authenticationFields)
        {
            string username = authenticationFields[Field_Username];
            string password = authenticationFields[Field_Password];

            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                client.Headers.Add(HttpRequestHeader.Authorization, $"Basic {Base64Encode(username + ":" + password)}");
                return await client.UploadStringTaskAsync($"{BitlyBaseUrl}/oauth/access_token", $"grant_type=password&username={username}&password={password}");
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public async Task<string> ShortenUrl(string url, string authenticationData)
        {
            using (WebClient client = new WebClient())
            {
                return await client.DownloadStringTaskAsync($"{BitlyBaseUrl}/v3/shorten?access_token={authenticationData}&longUrl={HttpUtility.UrlEncode(url)}");
            }
        }
    }
}
