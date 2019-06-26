namespace BT
{
    public interface IParentNode : INode
    {
        void AddChild(INode child);
    }
}