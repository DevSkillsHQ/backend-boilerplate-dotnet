using backend.Data;
using backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("presentations")]
    public class PresenterController : Controller
    {
        private readonly PresentationVotesDBContex presentationVotesDBContex;
        private readonly PresentationProxy presentaonProxy;

        public PresenterController(PresentationVotesDBContex presentationVotesDBContex, PresentationProxy presentationProxy)
        {
            this.presentationVotesDBContex = presentationVotesDBContex;
            this.presentaonProxy = presentationProxy;
        }


        /// <summary>
        /// Presenting the next poll
        /// </summary>
        /// <param name="presentation_id"></param>
        /// <param name="poll_id"></param>
        /// <returns></returns>
        [HttpPut("{presentation_id}/polls/current")]
        public async Task<IActionResult> SetCurrentPoll(string presentation_id, [FromBody] Poll poll_id)
        {
            // Retrieve the presentation from the database
            var presentation = await presentationVotesDBContex.Presentation.FindAsync(presentation_id);
            if (presentation == null)
            {
                return NotFound();
            }

            // Update the current poll for the presentation
            presentation.Polls.PollId = poll_id.ToString();
            await presentationVotesDBContex.SaveChangesAsync();

            return Ok();
        }




        /// <summary>
        /// Reading poll's voting results
        /// </summary>
        /// <param name="presentation_id"></param>
        /// <param name="poll_id"></param>
        /// <returns></returns>
        [HttpGet("{presentation_id}/polls/{poll_id}/votes")]
        public async Task<IActionResult> GetPollVotes(string presentation_id, string poll_id)
        {
            // Retrieve the poll and its votes from the database
            var poll = await presentationVotesDBContex.Poll
                .Include(p => p.PollId)
                .FirstOrDefaultAsync(p => p.PollId == presentation_id.ToString() && p.PollId == poll_id.ToString());
            if (poll == null || presentation_id == null)
            {
                return NotFound();
            }

            // Return the poll votes
            return Ok(poll.PollId);
        }

        /// <summary>
        /// Creates a new presentation
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreatePresentationAsync()
        {
            // Proxy the POST request to create a presentation
            var response = await presentaonProxy.CreatePresentationAsync();
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            // Get the presentation ID from the response
            var presentationId = await response.Content.ReadAsStringAsync();

            return Ok(presentationId);
        }

        /// <summary>
        /// Reading the specified presentation
        /// </summary>
        /// <param name="presentationId"></param>
        /// <returns></returns>
        [HttpGet("{presentation_id}")]
        public async Task<IActionResult> GetPresentationID([FromRoute] string presentation_id)
        {
            // Proxy the GET request to retrieve the presentation
            var response = await presentaonProxy.GetPresentationAsync(presentation_id);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            // Return the presentation content from the response
            var presentationContent = await response.Content.ReadAsStringAsync();

            return Ok(presentationContent);
        }
    }
}
