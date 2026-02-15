using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.DTOs.LecturersDTO
{
    public class LecturerCreateDto
    {
        public string FullName { get; set; }
        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }
        public string? YoutubeLink { get; set; }
    }
}
