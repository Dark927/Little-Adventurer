using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        Vector3 rotation = new Vector3(0f, _speed * Time.deltaTime, 0f);
        transform.Rotate(rotation);
    }
}
