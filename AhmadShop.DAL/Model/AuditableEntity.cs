namespace AhmadShop.DAL.Model
{
    public class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public string? CreatedById { get; set; }
        public ApplicationUser? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedById { get; set; }
        public ApplicationUser? UpdatedBy { get; set; }
    }
}