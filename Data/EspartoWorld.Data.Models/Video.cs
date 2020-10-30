namespace EspartoWorld.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Common.Models;

    public class Video : BaseDeletableModel<int>
    {
        [Required]
        public string VideoId { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
