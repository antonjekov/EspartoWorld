using System.ComponentModel.DataAnnotations;

namespace EspartoWorld.Web.ViewModels.Votes
{
    public class PostVoteInputModel
    {
        public int ItemId { get; set; }

        [Range(1, 5)]
        public byte Value { get; set; }
    }
}
