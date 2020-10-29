namespace EspartoWorld.Web.ViewModels.Courses
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using AutoMapper;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class CourseInputModel : IMapTo<Course>, IHaveCustomMappings
    {
        [Required]
        public string StartDate { get; set; }

        public DateTime StartDateDateTime => DateTime.ParseExact(this.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

        [Required]
        public string Place { get; set; }

        [Range(0, 1000)]
        public double Price { get; set; }

        [Range(1, 100)]
        public int GroupSize { get; set; }

        [Range(1, 30)]
        public int LengthInDays { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Organizer { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CourseInputModel, Course>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
