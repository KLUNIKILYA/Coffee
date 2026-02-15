using System.ComponentModel.DataAnnotations;

namespace Coffee.ViewModels.LecturerVm
{
    public class LecturerCreateVM
    {
        [Required(ErrorMessage = "Введите Имя и Фамилию")]
        [StringLength(100)]
        public string FullName { get; set; } 
        public string? Bio { get; set; }
        public string? YoutubeLink { get; set; }
        public IFormFile? UploadedPhoto { get; set; }
    }
}