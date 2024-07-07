using UnityEngine;

public class AttackState : MonoBehaviour, IState
{
    public virtual void Execute()
    {
        enabled = true;
        Debug.Log("attack state");
    }

    public virtual void Exit()
    {
        enabled = false;
    }
}
