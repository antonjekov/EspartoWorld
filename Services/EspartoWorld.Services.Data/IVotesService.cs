namespace EspartoWorld.Services.Data
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetVoteAsync(int itemId, string userId, byte value);

        double GetAverageVotes(int itemId);
    }
}
