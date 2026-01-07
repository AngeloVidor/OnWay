using System;

namespace ONW_API.Domain.Entities
{
    public sealed class ShipmentTrackingEvent
    {
        public Guid Id { get; private set; }
        public Guid ShipmentId { get; private set; } // FK
        public DateTime Date { get; private set; }
        public string Location { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        private ShipmentTrackingEvent() { }

        public ShipmentTrackingEvent(Guid id, DateTime date, string location, string description)
        {
            Id = id;
            Date = date;
            Location = location;
            Description = description;
        }
    }
}
