public class RepeatNode : DecoratorNodeBase
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override NodeStateType OnUpdate()
    {
        _child.Update();
        return NodeStateType.Running;
    }
}
