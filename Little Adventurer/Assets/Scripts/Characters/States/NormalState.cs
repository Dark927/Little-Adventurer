using UnityEngine;

public class NormalState : MonoBehaviour, IState
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    private Character _character;
    private IState.TYPE _stateType = IState.TYPE.Normal;


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

    public IState.TYPE Type 
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
