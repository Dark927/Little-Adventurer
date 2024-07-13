using UnityEngine;
using System.Collections;

public class DeadState : MonoBehaviour, IState
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [Space]
    [Header("Dissolve material Settings")]
    [Space]

    [SerializeField] private float dissolveMaterialStart = 20f;
    [SerializeField] private float dissolveMaterialEnd = -10f;
    [SerializeField] private float dissolveDuration = 2f;
    [SerializeField] private float startDelay = 2f;


    Character _character;
    Animator _animator;

    private IState.TYPE _stateType = IState.TYPE.Dead;

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
        _animator.SetTrigger("Dead");
        _character.DissolveMaterialDeath(dissolveMaterialStart, dissolveMaterialEnd, dissolveDuration, startDelay);
    }

    public void Exit()
    {
        enabled = false;
    }

    #endregion
}
