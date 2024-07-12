using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    public GameObject EnemyToSpawn
    {
        get { return _enemyPrefab; }
    }
}
