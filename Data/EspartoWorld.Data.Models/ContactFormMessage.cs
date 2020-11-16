namespace EspartoWorld.Data.Models
{
    using EspartoWorld.Data.Common.Models;

    public class ContactFormMessage : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Ip { get; set; }
    }
}
