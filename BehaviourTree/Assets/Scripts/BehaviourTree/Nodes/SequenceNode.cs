using System;
using System.Collections.Generic;

namespace BT
{
    public class SequenceNode : IParentNode
    {
        private string name;
        private List<INode> children = new List<INode>();

        public SequenceNode(string name){
            this.name = name;
        }

        public Status Tick(float time){
            for(int i = 0; i < children.Count; i++){
                var status = children[i].Tick(time);
                if(status != Status.Success){
                    return status;
                }
            }
            return Status.Success;
        }

        public void AddChild(INode child){
            children.Add(child);
        }
    }

}