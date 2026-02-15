namespace Coffee.ViewModels.Ticket
{
    public class TicketVM
    {
        public int Id { get; set; }
        public string EventTitle { get; set; }
        public DateTime StartDate { get; set; }
        public string Location { get; set; } = "Минск, ул. Пушкина 100";
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }

        public bool IsPast => StartDate < DateTime.Now;
    }
}
