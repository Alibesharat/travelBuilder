namespace TravelBuilder.DAL.Models
{
    public class TravelPackage
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public int ItineraryId { get; set; }
        public Itinerary Itinerary { get; set; }
    }
}