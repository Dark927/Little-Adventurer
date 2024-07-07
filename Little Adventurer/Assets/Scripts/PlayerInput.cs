using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    private Character _character;

    private float _horizontalInput;
    private float _verticalInput;

    private bool isMouseButtonDown = false;

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        if (!isMouseButtonDown && (Time.timeScale != 0))
        {
            isMouseButtonDown = Input.GetMouseButtonDown(0);
        }

        if (isMouseButtonDown)
        {
            _character.SetState(StateType.State_attack);
            enabled = false;
            return;
        }

        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }


    private void OnDisable()
    {
        isMouseButtonDown = false;
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
