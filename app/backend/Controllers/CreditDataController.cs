using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using backend.Exceptions;
using Newtonsoft.Json;
using backend.Models.CreditDataModels;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditDataController : Controller
    {
        private const string devSkillEndpoint = "https://infra.devskills.app/api";
        //private const string applicationUrl = Environment.GetEnvironmentVariable("applicationUrl");

        [HttpGet("/credit-data/{ssn}")]
        public async Task<IActionResult> GetAggregateData(string ssn)
        {
            try
            {
                var aggregateData = new List<string>
                {
                    await GetPersonalDetails(ssn),
                    await GetAssessedIncome(ssn),
                    await GetDebt(ssn)
                };

                var aggregateJObjects = new JObject();
                aggregateData.ForEach(d => aggregateJObjects.Merge(JObject.Parse(d)));

                var content = aggregateJObjects.ToString();
                var temp = JsonConvert.DeserializeObject<AggregateDataModel>(content);

                return Ok(temp);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode(ex.StatusCode);
            }
        }

        public async Task<string> GetPersonalDetails(string ssn)
        {
            string content = string.Empty;
            using (var client = new HttpClient())
            {
                var endpoint = $"{devSkillEndpoint}/credit-data/personal-details/{ssn}";
                var response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new HttpResponseException(((int)response.StatusCode));
                }
            }
            return content;
        }

        public async Task<string> GetAssessedIncome(string ssn)
        {
            string content = string.Empty;
            using (var client = new HttpClient())
            {
                var endpoint = $"{devSkillEndpoint}/credit-data/assessed-income/{ssn}";
                var response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new HttpResponseException(((int)response.StatusCode));
                }
            }
            return content;
        }

        public async Task<string> GetDebt(string ssn)
        {
            string content = string.Empty;
            using (var client = new HttpClient())
            {
                var endpoint = $"{devSkillEndpoint}/credit-data/debt/{ssn}";
                var response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new HttpResponseException(((int)response.StatusCode));
                }
            }
            return content;
        }
    }
}
