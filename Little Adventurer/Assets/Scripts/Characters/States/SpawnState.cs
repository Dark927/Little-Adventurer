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

    private IState.TYPE _stateType = IState.TYPE.Spawn;

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
            _character.SetState(IState.TYPE.Normal);
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
        _character.BecomeInvincible(_spawnDuration);
        _character.DissolveMaterial(dissolveMaterialStart, dissolveMaterialEnd, dissolveDuration, startDelay);
    }

    public void Exit()
    {
        enabled = false;
    }

    #endregion
}
