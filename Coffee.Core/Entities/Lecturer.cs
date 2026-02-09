using Coffee.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
