using System;

namespace BT
{
    public class InverterNode : IParentNode
    {
        private string name;

        private INode childNode;

        public InverterNode(string name){
            this.name = name;
        }

        public Status Tick(float time){
            if(childNode == null){
                throw new ApplicationException("InverterNode must have a child node!");
            }
            var result = childNode.Tick(time);
            if(result == Status.Failure){
                return Status.Success;
            }else if(result == Status.Success){
                return Status.Failure;
            }else{
                return result;
            }
        }

        public void AddChild(INode child){
            if(this.childNode != null){
                throw new ApplicationException("Can't add more than a single child to InverterNode!");
            }
            this.childNode = child;
        }
    }
}