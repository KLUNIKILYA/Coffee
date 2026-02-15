
namespace Coffee.Core.DTOs
{
    public class EventUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public int LecturerId { get; set; }
        public string? NewImageUrl { get; set; }
    }
}
