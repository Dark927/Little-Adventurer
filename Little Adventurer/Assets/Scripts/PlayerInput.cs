using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    private Character _character;
    private CharacterController _characterController;

    private float _horizontalInput;
    private float _verticalInput;

    public bool IsMouseButtonDown = false;

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        _character = GetComponent<Character>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleAttackInput();
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void HandleAttackInput()
    {
        if (!IsMouseButtonDown && (Time.timeScale != 0))
        {
            IsMouseButtonDown = Input.GetMouseButtonDown(0);
        }


        // Check current player state and set attack state if it is possible 

        if (IsMouseButtonDown && _characterController.isGrounded)
        {
            StateType currentState = _character.GetCurrentStateType();

            if (!(currentState == StateType.State_attack))
            {
                _character.SetState(StateType.State_attack);
            }
            IsMouseButtonDown = false;
        }
    }

    private void OnDisable()
    {
        IsMouseButtonDown = false;
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
