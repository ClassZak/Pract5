using System;
using System.Diagnostics;

namespace Mobs
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DaemonBrainAttribute : System.Attribute
    {
        public int brain {  get; set; }
        public DaemonBrainAttribute() { brain = 0; }
        public DaemonBrainAttribute(int brain) { this.brain = brain; }
    }
}
