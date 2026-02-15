using System.ComponentModel.DataAnnotations;

namespace Coffee.ViewModels
{
    public class EventEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public decimal Price { get; set; }

        public int Capacity { get; set; }

        public int LecturerId { get; set; }

        public IFormFile? NewUploadedCover { get; set; }
    }
}
