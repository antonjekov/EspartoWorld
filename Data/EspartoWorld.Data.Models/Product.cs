namespace EspartoWorld.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, 1000)]
        public double Price { get; set; }

        public bool Visible { get; set; } = true;

        public int TimesBought { get; set; } = 0;

        [Required]
        public string ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }
    }
}
