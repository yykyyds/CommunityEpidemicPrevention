using Models.Models;

namespace Models.Dtos
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public DateTime? SubmitTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string? Address { get; set; }
        public status Status { get; set; }
        public List<Goods>? Goods { get; set; }
        public string? UserName { get; set; }
    }
}
