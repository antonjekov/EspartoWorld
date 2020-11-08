namespace EspartoWorld.Web.ViewModels.Courses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class CourseInputModel : IMapTo<Course>
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Course starts at")]
        public DateTime StartDate { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Place { get; set; }

        [Range(0, 1000)]
        public double Price { get; set; }

        [Range(1, 100)]
        [Display(Name = "Size of group")]
        public int GroupSize { get; set; }

        [Range(1, 30)]
        [Display(Name = "Length in days")]
        public int LengthInDays { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Organizer { get; set; }

        [Required]
        [Url]
        [Display(Name = "URL of image")]
        public string ImageUrl { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(5000)]
        public string Description { get; set; }
    }
}
