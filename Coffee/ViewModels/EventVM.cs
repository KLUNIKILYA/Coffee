using Coffee.Core.Entities;

namespace Coffee.ViewModels
{
    public class EventVM
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public int Capacity { get; set; }
        public int PlacesTaken { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int LecturerId { get; set; }

        public Lecturer Lecturer { get; set; }

        public ICollection<WaitlistEntry> WaitlistEntries { get; set; } = new List<WaitlistEntry>();
    }
}
