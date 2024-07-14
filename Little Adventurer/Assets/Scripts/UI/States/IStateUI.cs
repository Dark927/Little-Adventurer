
public interface IStateUI
{
    public enum TYPE
    { 
        Gameplay = 0,
        Pause,
        GameOver,
        GameFinished
    }

    public TYPE Type { get; }
    public void Execute();
    public void Exit();
}
