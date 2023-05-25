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
        private const string devSkillsApi = "https://infra.devskills.app/api";

        [HttpGet("/credit-data/{ssn}")]
        public async Task<IActionResult> GetAggregateData(string ssn)
        {
            try
            {
                var aggregateDataList = new List<string>
                {
                    await GetPersonalDetails(ssn),
                    await GetAssessedIncome(ssn),
                    await GetDebt(ssn)
                };

                var aggregateDataJObject = new JObject();
                aggregateDataList.ForEach(d => aggregateDataJObject.Merge(JObject.Parse(d)));

                var aggregateDataString = aggregateDataJObject.ToString();
                var aggregateDataBody = JsonConvert.DeserializeObject<AggregateDataModel>(aggregateDataString);

                return Ok(aggregateDataBody);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode(ex.StatusCode);
            }
        }

        public async Task<string>GetCreditData(string inputEndpoint) {
            string content = string.Empty;
            using (var client = new HttpClient())
            {
                var endpoint = $"{devSkillsApi}/credit-data{inputEndpoint}";
                var response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new HttpResponseException((int)response.StatusCode);
                }
            }
            return content;
        }

        public async Task<string> GetPersonalDetails(string ssn)
        {
            string endpoint = $"/personal-details/{ssn}";
            return await GetCreditData(endpoint);
        }

        public async Task<string> GetAssessedIncome(string ssn)
        {
            string endpoint = $"/assessed-income/{ssn}";
            return await GetCreditData(endpoint);
        }

        public async Task<string> GetDebt(string ssn)
        {
            string endpoint = $"/debt/{ssn}";
            return await GetCreditData(endpoint);
        }
    }
}
