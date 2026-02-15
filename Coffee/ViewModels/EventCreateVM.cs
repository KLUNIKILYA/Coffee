using System.ComponentModel.DataAnnotations;

namespace Coffee.ViewModels
{
    public class EventCreateVM
    {
        [Required(ErrorMessage = "Введите название")]
        [StringLength(100)]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Укажите дату")]
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(1);

        [Range(0, 100000)]
        public decimal Price { get; set; }

        [Range(1, 500)]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Выберите лектора")]
        public int LecturerId { get; set; }

        public IFormFile? UploadedCover { get; set; }
    }
}
