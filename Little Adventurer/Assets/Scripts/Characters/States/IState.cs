


public interface IState
{
    public enum TYPE
    {
        Normal = 0,
        Attack,
        Dead,
        Hurted,
        Dash,
        Spawn,
    }

    public TYPE Type { get; }
    public void Execute();
    public void Exit();
}
