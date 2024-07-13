using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [Space]
    [Header("Visual Settings")]
    [Space]

    [SerializeField] private GameObject _gateVisual;

    [Space]
    [Header("Move Settings")]
    [Space]

    [SerializeField] private float _offsetY = 2f;
    [SerializeField] private float _openDuration = 2f;
    [SerializeField] private float _closeDuration = 2f;

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

    private IEnumerator MoveGateRoutine(Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _gateVisual.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);

            yield return null;
        }

        _gateVisual.transform.position = targetPosition;
    }

    private IEnumerator ColliderStatusRoutine(float delay, bool enableStatus)
    {
        yield return new WaitForSeconds(delay);
        _gateCollider.enabled = enableStatus;
    }

    private Vector3 CalculateTargetPosition(float offsetX, float offsetY, float offsetZ)
    {
        return _gateVisual.transform.position + new Vector3(offsetX, offsetY, offsetZ);
    }

    private Vector3 CalculateTargetPosition(Vector3 offset)
    {
        return _gateVisual.transform.position + offset;
    }

    private Vector3 GetCurrentPosition()
    {
        return _gateVisual.transform.position;
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public void Open()
    {
        StartCoroutine(ColliderStatusRoutine(_openDuration, false));
        StartCoroutine(MoveGateRoutine(GetCurrentPosition(), CalculateTargetPosition(0, -_offsetY, 0), _openDuration));
    }


    public void Close()
    {
        StartCoroutine(ColliderStatusRoutine(0, true));
        StartCoroutine(MoveGateRoutine(GetCurrentPosition(), CalculateTargetPosition(0, _offsetY, 0), _closeDuration));
    }

    #endregion
}
