using System;
using System.Collections.Generic;

namespace BT
{
    public class Builder
    {
        private INode curNode = null;

        private Stack<IParentNode> parentNodeStack = new Stack<IParentNode>();

        public Builder Do(string name, Func<float, Status> fn){
            if(parentNodeStack.Count <= 0){
                throw new ApplicationException("Can't create an unnested ActionNode, it must be a leaf node.");
            }

            var actionNode = new ActionNode(name, fn);
            parentNodeStack.Peek().AddChild(actionNode);
            return this;
        }

        public Builder Condition(string name, Func<float, bool> fn){
            return Do(name, t => fn(t) ? Status.Success : Status.Failure);
        }

        public Builder Inverter(string name, Func<float, bool> fn){
            var inverterNode = new InverterNode(name);
            if(parentNodeStack.Count > 0){
                parentNodeStack.Peek().AddChild(inverterNode);
            }

            parentNodeStack.Push(inverterNode);
            return this;
        }

        public Builder Sequence(string name){
            var sequenceNode = new SequenceNode(name);
            if(parentNodeStack.Count > 0){
                parentNodeStack.Peek().AddChild(sequenceNode);
            }

            parentNodeStack.Push(sequenceNode);
            return this;
        }

        public Builder Parallel(string name, int numRequiredToFail, int numRequiredToSucceed){
            var parallelNode = new ParallelNode(name, numRequiredToFail, numRequiredToSucceed);
            if(parentNodeStack.Count > 0){
                parentNodeStack.Peek().AddChild(parallelNode);
            }

            parentNodeStack.Push(parallelNode);
            return this;
        }

        public Builder Selector(string name){
            var selectorNode = new SelectorNode(name);
            if(parentNodeStack.Count > 0){
                parentNodeStack.Peek().AddChild(selectorNode);
            }

            parentNodeStack.Push(selectorNode);
            return this;
        }

        public Builder Splice(INode subTree){
            if(subTree == null){
                throw new ArgumentException("subTree");
            }
            if(parentNodeStack.Count <= 0){
                throw new ApplicationException("Can't splice an unnested sub-tree, there must be a parent-tree.");
            }
            parentNodeStack.Peek().AddChild(subTree);
            return this;
        }

        public INode Build(){
            if(curNode == null){
                throw new ApplicationException("Can't create a behaviour tree with zero nodes");
            }
            return curNode;
        }

        public Builder End(){
            curNode = parentNodeStack.Pop();
            return this;
        }
    }

    
}