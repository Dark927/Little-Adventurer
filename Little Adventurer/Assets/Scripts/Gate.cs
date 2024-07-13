using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [SerializeField] private float _openDuration = 2f;
    [SerializeField] private float _targetPosY = -1.5f;
    [SerializeField] private GameObject _gateVisual;
    private Collider _gateCollider;

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        _gateCollider = GetComponent<Collider>();
    }

    private IEnumerator OpenGateRoutine()
    {
        Vector3 startPosition = _gateVisual.transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * _targetPosY;

        float elapsedTime = 0f;

        while(elapsedTime < _openDuration)
        {
            elapsedTime += Time.deltaTime;
            _gateVisual.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / _openDuration);

            yield return null;
        }

        _gateVisual.transform.position = targetPosition;
        _gateCollider.enabled = false;
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public void Open()
    {
        StartCoroutine(OpenGateRoutine());
    }

    #endregion
}
