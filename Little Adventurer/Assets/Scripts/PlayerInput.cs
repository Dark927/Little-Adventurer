using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    private float _horizontalInput;
    private float _verticalInput;

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }


    private void OnDisable()
    {
        _horizontalInput = 0f;
        _verticalInput = 0f;
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public float HorizontalInput
    {
        get { return _horizontalInput; }
    }

    public float VerticalInput
    {
        get { return _verticalInput; }
    }

    #endregion
}
