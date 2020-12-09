using iFood.Net.Domain;
using iFood.Net.Utils;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iFood.Net.Service
{
    public class iFoodService
    {
        private string _urlBase = Constants.URL_BASE;
        public iFoodService() { }
        public iFoodService(string urlBase)
        {
            _urlBase = urlBase;
        }

        public async Task<GenericResult<Token>> OathToken(string _ClientId, string _ClientSecret, string _Username, string _Password)
        {
            var result = new GenericResult<Token>();

            var client = new RestClient(_urlBase + Constants.URL_TOKEN);
            var request = new RestRequest(Method.POST);
            request.AddParameter("client_id", _ClientId);
            request.AddParameter("client_secret", _ClientSecret);
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", _Username);
            request.AddParameter("password", _Password);
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result.Result = JsonConvert.DeserializeObject<Token>(response.Content);
                result.Success = true;
            }
            else
            {
                result.Message = response.StatusDescription;
            }

            return result;
        }
    }
}
