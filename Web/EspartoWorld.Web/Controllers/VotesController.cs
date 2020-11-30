namespace EspartoWorld.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/[controller]")]
    public class VotesController : Controller
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostVoteViewModel>> PostAsync(PostVoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.votesService.SetVoteAsync(input.ItemId, userId, input.Value);
            var averageVote = this.votesService.GetAverageVotes(input.ItemId);
            return new PostVoteViewModel() { AverageVote = averageVote };
        }
    }
}
