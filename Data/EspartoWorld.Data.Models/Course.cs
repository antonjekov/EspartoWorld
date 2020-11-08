namespace EspartoWorld.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Common.Models;

    public class Course : BaseDeletableModel<int>
    {
        public Course()
        {
            this.IsConfirmed = false;
            this.HaveFreePlaces = true;
        }

        public DateTime StartDate { get; set; }

        [Required]
        [MaxLength(150)]
        public string Place { get; set; }

        [Range(0, 1000)]
        public double Price { get; set; }

        [Range(1, 100)]
        public int GroupSize { get; set; }

        [Range(1, 30)]
        public int LengthInDays { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        [MaxLength(150)]
        public string Organizer { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [Required]
        public bool HaveFreePlaces { get; set; }
    }
}
