namespace EspartoWorld.Web.ViewModels.Home
{
    using EspartoWorld.Web.ViewModels.Courses;
    using EspartoWorld.Web.ViewModels.Videos;

    public class HomeViewModel
    {
        public VideoViewModel LastVideo { get; set; }

        public CourseViewModel NextCourse { get; set; }

        // public Article LastArticle { get; set; }
    }
}
