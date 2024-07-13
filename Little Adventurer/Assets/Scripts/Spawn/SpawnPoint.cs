using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [SerializeField] private GameObject _enemyPrefab;

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public GameObject EnemyToSpawn
    {
        get { return _enemyPrefab; }
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 cubeCenter = transform.position + new Vector3(0, 0.5f, 0);
        Gizmos.DrawWireCube(cubeCenter, Vector3.one);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(cubeCenter, cubeCenter + transform.forward * 2);
    }

    #endregion
}
