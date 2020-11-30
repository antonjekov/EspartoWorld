namespace EspartoWorld.Data.Models
{
    using EspartoWorld.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public int ExpositionItemId { get; set; }

        public virtual ExpositionItem ExpositionItem { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public byte Value { get; set; }
    }
}
