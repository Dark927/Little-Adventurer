using UnityEngine;

public class DashState : MonoBehaviour, IState
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [SerializeField] private float _dashSpeed = 9f;
    private Character _character;
    private Animator _animator;
    private IState.TYPE _stateType = IState.TYPE.Dash;

    private Vector3 _floorNormal;


    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        _character = GetComponent<Character>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector3 dashVector = _character.transform.forward - Vector3.Dot(_character.transform.forward, _floorNormal) * _floorNormal;
        dashVector *= _dashSpeed * Time.deltaTime;

        _character.SetMovementVelocity(dashVector);
        _character.Move();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponent<Enemy>() == null)
        {
            _floorNormal = hit.normal;
        }
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
        _animator.SetTrigger("Dash");
    }

    public void Exit()
    {
        enabled = false;
    }

    #endregion
}

