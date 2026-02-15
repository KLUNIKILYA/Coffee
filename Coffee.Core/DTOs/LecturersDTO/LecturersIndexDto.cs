using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.DTOs.LecturersDTO
{
    public class LecturersIndexDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Bio {  get; set; }
        public string ImageUrl { get; set; }
    }
}
