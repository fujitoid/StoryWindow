using UnityEngine;

public abstract class BaseNode : ScriptableObject
{
    protected NodeStateType _state = NodeStateType.Running;
    protected bool _isStarted = false;
    protected string _guid;
    protected Vector2 _position;

    public NodeStateType State => _state;
    public bool IsStarted => _isStarted;
    public string GUID => _guid;
    public Vector2 Position => _position;

    public virtual void Construct(string guid)
    {
        _guid = guid;
    }

    public virtual void SetPosition(Vector2 position)
    {
        _position = position;
    }

    public NodeStateType Update()
    {
        if (!_isStarted)
        {
            OnStart();
            _isStarted = true;
        }

        _state = OnUpdate();

        if (_state == NodeStateType.Failure || _state == NodeStateType.Success)
        {
            OnStop();
            _isStarted = false;
        }

        return _state;
    }

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract NodeStateType OnUpdate();
}
