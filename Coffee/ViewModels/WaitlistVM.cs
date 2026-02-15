namespace Coffee.ViewModels
{
    public class WaitlistVM
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string LecturerName { get; set; }
        public DateTime StartDate { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}