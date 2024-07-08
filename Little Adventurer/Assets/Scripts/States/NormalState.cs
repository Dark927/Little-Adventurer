using UnityEngine;

public class NormalState : MonoBehaviour, IState
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    Character _character;

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
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

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
