using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private List<SpawnPoint> _spawnPointsList;
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
    }

    private void SpawnCharacters()
    {
        if (_hasSpawned)
            return;

        _hasSpawned = true;

        foreach(SpawnPoint spawnPoint in _spawnPointsList)
        {
            Instantiate(spawnPoint.EnemyToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if(player != null)
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
