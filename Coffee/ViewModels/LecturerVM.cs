using Coffee.Core.Entities;

namespace Coffee.ViewModels
{
    public class LecturerVM
    {
        public string FullName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }
        public string? YoutubeLink { get; set; }
    }
}
