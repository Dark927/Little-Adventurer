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
    [SerializeField] private UnityEvent _event;

    private List<SpawnPoint> _spawnPointsList;
    private List<Enemy> _spawnedEnemiesList;
    private bool _hasSpawned = false;

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
        if (!_hasSpawned)
            return;

        for(int i = 0; i < _spawnedEnemiesList.Count; ++i)
        {
            if(_spawnedEnemiesList[i].GetCurrentStateType() == StateType.State_dead)
            {
                _spawnedEnemiesList.RemoveAt(i);
            }
        }

        if (_event != null && (_spawnedEnemiesList.Count == 0))
        {
            _event.Invoke();
        }
    }

    private void SpawnCharacters()
    {
        if (_hasSpawned)
            return;

        _hasSpawned = true;

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
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _triggerCollider.bounds.size);
    }

    #endregion
}
