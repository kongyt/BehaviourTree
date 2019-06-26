using System;

namespace BT
{
    public class ActionNode : INode
    {
        private string name;
        private Func<float, Status> fn;

        public ActionNode(string name, Func<float, Status> fn){
            this.name = name;
            this.fn = fn;
        }

        public Status Tick(float time){
            return fn(time);
        }
    }
}