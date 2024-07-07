using UnityEngine;

public class NormalState : MonoBehaviour, IState
{
    Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void FixedUpdate()
    {
        _character.ConfigureMovement();
    }

    public void Execute()
    {
        enabled = true;
    }

    public void Exit()
    {
        enabled = false;
    }
}
