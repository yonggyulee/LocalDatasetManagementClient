using LDMApp.Core.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace LDMApp.Core.Events
{
    public class SampleSelectedEvent : PubSubEvent<IDictionary<string,object>>
    {
    }
}
