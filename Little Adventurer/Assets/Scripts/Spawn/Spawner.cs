using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [Space]
    [Header("Debug Settings")]
    [Space]

    [SerializeField] private BoxCollider _triggerCollider;

    [Space]

    [SerializeField] private UnityEvent _onTriggerEnterEvent;
    [SerializeField] private UnityEvent _onEnemiesDeadEvent;

    private List<SpawnPoint> _spawnPointsList;
    private List<Enemy> _spawnedEnemiesList;

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Start()
    {
        SpawnPoint[] spawnPointsArray = transform.parent.GetComponentsInChildren<SpawnPoint>();
        _spawnPointsList = new List<SpawnPoint>(spawnPointsArray);
        _spawnedEnemiesList = new List<Enemy>();
    }

    private void Update()
    {
        if (_spawnedEnemiesList.Count == 0)
            return;

        UpdateSpawnedEnemyList();
        RaiseOnEnemyDeadEvent();
    }

    private void RaiseOnEnemyDeadEvent()
    {
        if (_onEnemiesDeadEvent != null && (_spawnedEnemiesList.Count == 0))
        {
            _onEnemiesDeadEvent.Invoke();
        }
    }

    private void UpdateSpawnedEnemyList()
    {
        for (int i = 0; i < _spawnedEnemiesList.Count; ++i)
        {
            if (_spawnedEnemiesList[i].CurrentState.Type == IState.TYPE.Dead)
            {
                _spawnedEnemiesList.RemoveAt(i);
            }
        }
    }

    private void SpawnCharacters()
    {
        foreach (SpawnPoint spawnPoint in _spawnPointsList)
        {
            GameObject enemyObject = Instantiate(spawnPoint.EnemyToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation);

            _spawnedEnemiesList.Add(enemyObject.GetComponent<Enemy>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            SpawnCharacters();
            RaiseTriggerEnterEvent();

            _triggerCollider.enabled = false;
        }
    }

    private void RaiseTriggerEnterEvent()
    {
        if (_onTriggerEnterEvent != null)
        {
            _onTriggerEnterEvent.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _triggerCollider.bounds.size);
    }

    #endregion
}
