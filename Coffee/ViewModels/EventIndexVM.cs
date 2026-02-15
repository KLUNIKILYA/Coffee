using System.ComponentModel.DataAnnotations;

namespace Coffee.ViewModels
{
    public class EventIndexVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public int PlacesTaken { get; set; }
        public DateTime StartDate { get; set; }
        public int LecturerId { get; set; }
        public decimal Price { get; set; }
    }
}
