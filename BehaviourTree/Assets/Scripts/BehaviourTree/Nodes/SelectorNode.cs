using System;
using System.Collections.Generic;

namespace BT
{
    public class SelectorNode : IParentNode
    {
        private string name;
        private List<INode> children = new List<INode>();

        public SelectorNode(string name){
            this.name = name;
        }

        public Status Tick(float time){
            for(int i = 0; i < children.Count; i++){
                var status = children[i].Tick(time);
                if(status != Status.Failure){
                    return status;
                }
            }

            return Status.Failure;
        }

        public void AddChild(INode child){
            children.Add(child);
        }
    }

}