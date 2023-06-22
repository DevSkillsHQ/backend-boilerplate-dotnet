using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("Common")]
    public class CommonController : Controller
    {
        private readonly PresentationVotesDBContex presentationVotesDBContex;

        public CommonController(PresentationVotesDBContex presentationVotesDBContex)
        {
            this.presentationVotesDBContex = presentationVotesDBContex;
        }

        /// <summary>
        /// Reading the specified presentation
        /// </summary>
        /// <param name="presentationId"></param>
        /// <returns></returns>
        [HttpGet("{presentation_id}/polls/current")]
        public async Task<IActionResult> GetCurrentPoll(string presentation_id)
        {
            // Retrieve the presentation from the database
            var presentation = await presentationVotesDBContex.Presentation
                .Include(p => p.Polls.PollId)
                .FirstOrDefaultAsync(p => p.Polls.PollId == presentation_id);
            if (presentation == null)
            {
                return NotFound();
            }

            var result = new
            {
                description = presentation.Polls.Question,
                poll_id = presentation.Polls.PollId,
                options = presentation.Polls.Options
            };

            // Return the current poll
            return Ok(result);
        }
    }
}

