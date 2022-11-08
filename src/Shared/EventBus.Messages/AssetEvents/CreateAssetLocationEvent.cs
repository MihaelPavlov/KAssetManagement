using EventBus.Messages.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.AssetEvents
{
    public class CreateAssetLocationEvent : IntegrationBaseEvent
    {
        public int AssetId { get; set; }
        public int LocationId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
