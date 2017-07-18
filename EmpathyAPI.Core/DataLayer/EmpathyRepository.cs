using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmpathyAPI.Core.EntityLayer;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace EmpathyAPI.Core.DataLayer
{
    public class EmpathyRepository : IEmpathyRepository
    {
        private readonly IOptions<LineServiceSettings> _lineServiceSettings;

        public EmpathyRepository(IOptions<LineServiceSettings> lineServiceSettings)
        {
            _lineServiceSettings = lineServiceSettings;
        }

        private string GetLineProfileUri()
        {
            return  _lineServiceSettings.Value.LineProfileUri;
        }

        private string GetChannelAccessToken()
        {
            return _lineServiceSettings.Value.ChannelAccessToken;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<Profile> GetUserProfile(string userId)
        {
            string TokenString = $"Bearer {GetChannelAccessToken()}";
            string Url = GetLineProfileUri();
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

        public Tuple<Task<Profile>, Task<TimeSpan>> GetUserProfileTimeSpan(string userId)
        {
            string TokenString = $"Bearer {GetChannelAccessToken()}";
            string Url = GetLineProfileUri();
            string WEBSERVICE_URL = Url + userId;

            HttpClient client = new HttpClient();

            try
            {
                client.DefaultRequestHeaders.Add("Authorization", TokenString);
                client.BaseAddress = new Uri(WEBSERVICE_URL);
                // get response time
                Stopwatch timer = new Stopwatch();
                timer.Start();

                HttpResponseMessage response = client.GetAsync(WEBSERVICE_URL).Result;

                timer.Stop();
                var timeTaken = Task.Run(() => timer.Elapsed);

                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    var result = content.ReadAsStringAsync();
                    res = result.Result;
                }


                var profile = Task.Run(() => JsonConvert.DeserializeObject<Profile>(res));

                return new Tuple<Task<Profile>, Task<TimeSpan>>(profile, timeTaken);
            }
            catch (Exception)
            {
                var p = Task.Run(() => new Profile());
                var t = Task.Run(() => new TimeSpan());
                return new Tuple<Task<Profile>, Task<TimeSpan>>(p, t);
            }
        }
    }
}
