using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditDataController : Controller
    {
        private const string devSkillEndpoint = "https://infra.devskills.app/api";
        //private const string applicationUrl = Environment.GetEnvironmentVariable("applicationUrl");

        [HttpGet("/credit-data/{ssn}")]
        public async Task<string> GetAggregateData(string ssn)
        {
            string content = string.Empty;
            var personalDetailsBody = await GetPersonalDetails(ssn);
            var assessedIncomeBody = await GetAssessedIncome(ssn);
            var debtBody = await GetDebt(ssn);

            var personalDetailsList = JObject.Parse(personalDetailsBody);
            var assessedIncomeList = JObject.Parse(assessedIncomeBody);
            var debtList = JObject.Parse(debtBody);

            var contentObject = new JObject();
            contentObject.Merge(personalDetailsList);
            contentObject.Merge(assessedIncomeList);
            contentObject.Merge(debtList);

            content = contentObject.ToString();

            return content;
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

                }
            }
            return content;
        }
    }
}
