using CruzSebastianExamenRecuperacion.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruzSebastianExamenRecuperacion.Services
{
    public class SCDogService
    {
        private readonly HttpClient _httpClient;

        public SCDogService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<SCDog> GetDogImage()
        {
            SCDog DogImage = null;
            HttpResponseMessage response;
            string requestUrl = "https://dog.ceo/api/breed/hound/images";

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                response = await _httpClient.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    DogImage = JsonConvert.DeserializeObject<SCDog>(json);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }

            return DogImage;
        }
    }
}
