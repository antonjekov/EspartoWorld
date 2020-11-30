namespace EspartoWorld.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Data.Common.Repositories;
    using EspartoWorld.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> repositoryVotes;

        public VotesService(IRepository<Vote> repositoryVotes)
        {
            this.repositoryVotes = repositoryVotes;
        }

        public double GetAverageVotes(int itemId)
        {
            return this.repositoryVotes.All().Where(x => x.ExpositionItemId == itemId).Average(x => x.Value);
        }

        public async Task SetVoteAsync(int itemId, string userId, byte value)
        {
            var vote = this.repositoryVotes.All().FirstOrDefault(x => x.UserId == userId && x.ExpositionItemId == itemId);
            if (vote == null)
            {
                vote = new Vote()
                {
                    UserId = userId,
                    ExpositionItemId = itemId,
                    Value = value,
                };
                await this.repositoryVotes.AddAsync(vote);
                await this.repositoryVotes.SaveChangesAsync();
            }
            else
            {
                vote.Value = value;
                await this.repositoryVotes.SaveChangesAsync();
            }
        }
    }
}
