namespace EspartoWorld.Web.ViewModels.Course
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CourseViewModel
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public string StartDateString => this.StartDate.ToString("dd.MM.yyyy");

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
