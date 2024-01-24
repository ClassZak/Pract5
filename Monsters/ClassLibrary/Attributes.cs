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
    [AttributeUsage(AttributeTargets.Field)]
    public class MonsterNameAttribute : System.Attribute
    {
        public string[] names { get; set; }
        public MonsterNameAttribute() { this.names = null; }
        public MonsterNameAttribute(string[] names)
        {
            this.names=new string[names.Length];
            for(int i=0;i<names.Length;i++)
            {
                this.names[i] = names[i];
            }
        }
    }
}
