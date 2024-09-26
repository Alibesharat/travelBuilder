namespace TravelBuilder.DAL.Models
{
    public class Itinerary
    {
         public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
    }
}