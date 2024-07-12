
public enum StateType
{
    State_normal = 0,
    State_attack,
    State_dead,
    State_hurted,
}

public interface IState
{
    public StateType CurrentStateType { get; }
    public void Execute();
    public void Exit();
}
