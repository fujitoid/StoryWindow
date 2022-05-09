public abstract class DecoratorNodeBase : BaseNode
{
    protected BaseNode _child;

    public BaseNode Child => _child;
}
