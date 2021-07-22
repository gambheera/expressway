using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Domain
{
    public class ExchangePoint
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }

        public virtual ICollection<Ride> EntryRides { get; set; }
        public virtual ICollection<Ride> ExitRides { get; set; }
    }
}
