using Coffee.Core.Entities.Base;

namespace Coffee.Core.Entities
{
    public class Lecturer : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }
        public string? YoutubeLink { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
