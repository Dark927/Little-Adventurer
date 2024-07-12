using UnityEngine;

public class NormalState : MonoBehaviour, IState
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    private Character _character;
    private StateType _stateType = StateType.State_normal;


    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void FixedUpdate()
    {
        _character.ConfigureMovement();
        _character.Move();
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public StateType CurrentStateType 
    { 
        get { return _stateType; } 
    }

    public void Execute()
    {
        enabled = true;
    }

    public void Exit()
    {
        enabled = false;
    }

    #endregion
}
