namespace EspartoWorld.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Common.Models;

    public class Manufacturer : BaseDeletableModel<string>
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        public int Telephone { get; set; }

        [Required]
        public string Address { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
