using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmpathyAPI.Core.EntityLayer;
using System.Net.Http;
using Newtonsoft.Json;

namespace EmpathyAPI.Core.DataLayer
{
    public class EmpathyRepository : IEmpathyRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<Profile> GetUserProfile(string userId, string ChannelAccessToken)
        {
            string TokenString = $"Bearer {ChannelAccessToken}";
            string Url = AppSettings.LineProfile;

            string WEBSERVICE_URL = Url + userId;

            HttpClient client = new HttpClient();

            try
            {
                client.DefaultRequestHeaders.Add("Authorization", TokenString);
                client.BaseAddress = new Uri(WEBSERVICE_URL);

                HttpResponseMessage response = client.GetAsync(WEBSERVICE_URL).Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    var result = content.ReadAsStringAsync();
                    res = result.Result;
                }


                var profile = Task.Run(() => JsonConvert.DeserializeObject<Profile>(res));

                return profile;
            }
            catch (Exception)
            {
                return Task.Run(() => new Profile());
            }
            
        }
    }
}
