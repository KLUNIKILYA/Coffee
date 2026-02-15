
namespace Coffee.Core.DTOs.Event
{
    public class EventCreateDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public int LecturerId { get; set; }
        public string? ImageUrl { get; set; }

    }
}
