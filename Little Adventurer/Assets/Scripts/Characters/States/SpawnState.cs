using UnityEngine;
using System.Collections;

public class SpawnState : MonoBehaviour, IState
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [Space]
    [Header("Spawn Settings")]
    [Space]

    [SerializeField] private float _spawnDuration = 2f;
    private float _currentSpawnTime;


    [Space]
    [Header("Dissolve material Settings")]
    [Space]

    [SerializeField] private float dissolveMaterialStart = -10f;
    [SerializeField] private float dissolveMaterialEnd = 20f;
    [SerializeField] private float dissolveDuration = 2f;
    [SerializeField] private float startDelay = 0f;

    Character _character;
    CharacterController _characterController;

    private StateType _stateType = StateType.State_spawn;

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
        _currentSpawnTime += Time.deltaTime;

        if(_currentSpawnTime > _spawnDuration)
        {
            _character.SetState(StateType.State_normal);
        }
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
        _character.BecomeInvincible(_spawnDuration);
        _character.DissolveMaterial(dissolveMaterialStart, dissolveMaterialEnd, dissolveDuration, startDelay);
    }

    public void Exit()
    {
        enabled = false;
    }

    #endregion
}
