namespace EspartoWorld.Web.ViewModels.Courses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class CourseInputModel : IMapTo<Course>
    {
        [Required(ErrorMessage = "Please select date.")]
        [DataType(DataType.Date)]
        [Display(Name ="Course starts at")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage ="Please select place.")]
        [MinLength(3, ErrorMessage = "Place should be minimum 3 characters long.")]
        [MaxLength(150, ErrorMessage = "Place should be maximum 150 characters long.")]
        [Display(Name = "Place")]
        public string Place { get; set; }

        [Required(ErrorMessage ="Price is required")]
        [Range(0, 1000, ErrorMessage = "Price should be between 0 and 1000.")]
        [Display(Name = "Price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please specify size of group")]
        [Range(1, 100, ErrorMessage = "Size of group should be between 0 and 100 persons.")]
        [Display(Name = "Size of group")]
        public int GroupSize { get; set; }

        [Required(ErrorMessage = "Please specify length in days")]
        [Range(1, 30, ErrorMessage = "Length should be between 1 and 30 days.")]
        [Display(Name = "Length in days")]
        public int LengthInDays { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MinLength(3, ErrorMessage = "Title should be minimum 3 characters long.")]
        [MaxLength(150, ErrorMessage = "Title should be maximum 150 characters long.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Organizer is required")]
        [MinLength(3, ErrorMessage = "Organizer should be minimum 3 characters long.")]
        [MaxLength(150, ErrorMessage = "Organizer should be maximum 150 characters long.")]
        [Display(Name = "Organizer")]
        public string Organizer { get; set; }

        [Required(ErrorMessage ="Please upload image for this course")]
        [Url(ErrorMessage ="Please fill valid URL")]
        [Display(Name = "URL of image")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(10, ErrorMessage = "Description should be minimum 3 characters long.")]
        [MaxLength(5000, ErrorMessage = "Description should be minimum 3 characters long.")]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
