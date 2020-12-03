namespace EspartoWorld.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Common.Models;

    public class ShoppingCartItem : BaseModel<int>
    {
        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
