namespace RentChallenge.Domain.Entities
{
    public class EventMotorcycleRegistered
    {
        public int Id { get; set; }
        public string MotorcycleId { get; set; }
        public int Year { get; set; }
        public DateTime DateRecived { get; set; }
        public required string SerializedPayload { get; set; }
    }
}
