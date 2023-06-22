using backend.Data;
using backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("Audience")]
    public class AudienceController : Controller
    {
        private readonly PresentationVotesDBContex presentationVotesDBContex;

        public AudienceController(PresentationVotesDBContex presentationVotesDBContex)
        {
            this.presentationVotesDBContex = presentationVotesDBContex;
        }

        /// <summary>
        /// Endpoints used by audience mobile app
        /// </summary>
        /// <param name="presentation_id"></param>
        /// <param name="vote"></param>
        /// <returns></returns>
        [HttpPost("{presentation_id}/polls/current/votes")]
        public async Task<IActionResult> CreateVote(string presentation_id, [FromBody] Vote vote)
        {
            // Retrieve the current poll from the database
            var currentPoll = await presentationVotesDBContex.Poll
                .Include(p => p.PollId)
                .FirstOrDefaultAsync(p => p.PollId == presentation_id.ToString() && p.PollId == vote.PollId);
            if (currentPoll == null || presentation_id == null)
            {
                return NotFound();
            }

            // Create the vote
            presentationVotesDBContex.Vote.Add(vote);
            await presentationVotesDBContex.SaveChangesAsync();

            return Ok();
        }
    }
}
