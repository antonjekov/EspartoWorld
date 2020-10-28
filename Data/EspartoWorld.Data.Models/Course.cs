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
        public string Place { get; set; }

        public double Price { get; set; }

        public int GroupSize { get; set; }

        public int LengthInDays { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Organizer { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [Required]
        public bool HaveFreePlaces { get; set; }
    }
}
