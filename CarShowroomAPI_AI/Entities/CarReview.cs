using System.ComponentModel.DataAnnotations.Schema;

namespace CarShowroomAPI_AI.Entities
{
    public class CarReview
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CarId { get; set; }

        [ForeignKey("CarId")]
        public Car Car { get; set; } = null!;
    }
}
