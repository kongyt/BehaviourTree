using System;
using System.Collections.Generic;

namespace BT
{
    public class ParallelNode : IParentNode
    {
        private string name;
        private List<INode> children = new List<INode>();

        private int numRequiredToFail;
        private int numRequiredToSucceed;

        public ParallelNode(string name, int numRequiredToFail, int numRequiredToSucceed){
            this.name = name;
            this.numRequiredToFail = numRequiredToFail;
            this.numRequiredToSucceed = numRequiredToSucceed;
        }

        public Status Tick(float time){
            var numChildrenSucceeded = 0;
            var numChildrenFailed = 0;

            for(int i = 0; i < children.Count; i++){
                var status = children[i].Tick(time);
                switch(status){
                    case Status.Success: ++numChildrenSucceeded;break;
                    case Status.Failure: ++numChildrenFailed; break;
                }
            }
            if(numChildrenSucceeded > 0 && numChildrenSucceeded > numRequiredToSucceed){
                return Status.Success;
            }

            if(numChildrenFailed > 0 && numChildrenFailed >= numRequiredToFail){
                return Status.Failure;
            }

            return Status.Running;
        }

        public void AddChild(INode child){
            children.Add(child);
        }
    }

}