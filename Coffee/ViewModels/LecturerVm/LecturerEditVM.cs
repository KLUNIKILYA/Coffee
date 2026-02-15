namespace Coffee.ViewModels.LecturerVm
{
    public class LecturerEditVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Bio { get; set; }
        public string? YoutubeLink { get; set; }
        public string? ExistingPhotoUrl { get; set; }
        public IFormFile? UploadedPhoto { get; set; }
    }
}
