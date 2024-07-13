using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [Space]
    [Header("Keys Settings")]
    [Space]

    [SerializeField] private KeyCode _attackKeyCode = KeyCode.Mouse0;
    [SerializeField] private KeyCode _dashKeyCode = KeyCode.Space;

    [Space]
    [Header("Time delay Settings")]
    [Space]

    [SerializeField] private float _attackTimeDelay = 0.5f;
    [SerializeField] private float _dashTimeDelay = 2f;


    [Space]
    [Header("States Settings")]
    [Space]

    [SerializeField] private List<IState.TYPE> _blockInputStates;

    private bool _isAttackReloaded = true;
    private bool _isDashReloaded = true;


    private Character _character;
    private CharacterController _characterController;

    private float _horizontalInput;
    private float _verticalInput;

    [HideInInspector] public bool IsAttackButtonDown = false;
    [HideInInspector] public bool IsDashButtonDown = false;

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
        HandleInputState(_attackKeyCode, ref IsAttackButtonDown, IState.TYPE.Attack, ref _isAttackReloaded, _attackTimeDelay);
        HandleInputState(_dashKeyCode, ref IsDashButtonDown, IState.TYPE.Dash, ref _isDashReloaded, _dashTimeDelay);

        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void HandleInputState(KeyCode stateKey, ref bool stateCondition, IState.TYPE stateToApply, ref bool isReloaded, float reloadTime = 0f)
    {
        if (!isReloaded)
            return;


        if (!stateCondition && (Time.timeScale != 0))
        {
            stateCondition = Input.GetKeyDown(stateKey);
        }


        // Check current player state and set attack state if it is possible 

        if (stateCondition && _characterController.isGrounded)
        {
            IState.TYPE currentStateType = _character.CurrentState.Type;

            if (!IsInputBlocked(currentStateType))
            {
                _character.SetState(stateToApply);

                isReloaded = false;
                StartCoroutine(ReloadStateRoutine(stateToApply, reloadTime));
            }
            stateCondition = false;
        }
    }

    private bool IsInputBlocked(IState.TYPE currentStateType)
    {
        foreach (IState.TYPE blockType in _blockInputStates)
        {
            if (currentStateType == blockType)
            {
                return true;
            }
        }

        return false;
    }

    private IEnumerator ReloadStateRoutine(IState.TYPE reloadState, float reloadTime)
    {
        // Reload time delay 

        yield return new WaitForSeconds(reloadTime);


        switch (reloadState)
        {
            default:
                {
                    break;
                }

            case IState.TYPE.Attack:
                {
                    _isAttackReloaded = true;
                    break;
                }

            case IState.TYPE.Dash:
                {
                    _isDashReloaded = true;
                    break;
                }
        }
    }

    private void OnDisable()
    {
        ClearCache();
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

    public void ClearCache()
    {
        IsAttackButtonDown = false;
        IsDashButtonDown = false;
        _horizontalInput = 0f;
        _verticalInput = 0f;
    }

    #endregion
}
