using Prism.Events;
using System.Collections.Generic;

namespace LDMApp.Core.Events
{
    public class SampleSelectedEvent : PubSubEvent<IDictionary<string,object>>
    {
    }
}
