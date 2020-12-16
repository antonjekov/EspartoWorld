namespace EspartoWorld.Web.ViewModels.Manufacturers
{
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class ManufacturersViewModel : IMapFrom<Manufacturer>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int Telephone { get; set; }
    }
}
