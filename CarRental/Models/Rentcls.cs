namespace CarRental.Models
{
    public class Rentcls
    {
        public int rentid { set; get; }
        public DateTime pickup { set; get; }
        public DateTime dropoff { set; get; }
       
        public string? totalamt {  set; get; }
        public string? rentmsg {  set; get; }
    }
}
