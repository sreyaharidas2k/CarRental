namespace CarRental.Models
{
    public class bookingcls
    {
        public Carcls CarDetails { get; set; }
        public Rentcls RentalDetails { get; set; }
        public decimal DailyRate { get; set; }
    }
}
