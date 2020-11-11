namespace EspartoWorld.Web.ViewModels.Courses
{
    using System;

    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class CourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public string StartDateString => this.StartDate.ToString("dd.MM.yyyy");

        public string EndDateString => this.StartDate.AddDays(this.LengthInDays).ToString("dd.MM.yyyy");

        public string Place { get; set; }

        public double Price { get; set; }

        public int GroupSize { get; set; }

        public int LengthInDays { get; set; }

        public string Title { get; set; }

        public string Organizer { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public bool IsConfirmed { get; set; }

        public bool HaveFreePlaces { get; set; }
    }
}
