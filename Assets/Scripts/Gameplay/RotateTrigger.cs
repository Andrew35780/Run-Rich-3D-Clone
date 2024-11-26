using System.Collections;
using UnityEngine;

public class RotateTrigger : MonoBehaviour
{
    [SerializeField] private bool _isRightRotate;

    private float _speedTurn = 4f;
    private Quaternion _lookRotation;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 dir = _isRightRotate ? new Vector3(0, 90, 0) : new Vector3(0, 0, 0);
        _lookRotation = Quaternion.Euler(dir);

        StartCoroutine(RotateToTarget(other));
    }

    private IEnumerator RotateToTarget(Collider other)
    {
        while (Mathf.Abs(Quaternion.Angle(other.transform.rotation, _lookRotation)) > 0.01f)
        {
            Vector3 rotation = Quaternion.Lerp(other.gameObject.transform.rotation, _lookRotation, _speedTurn * Time.deltaTime).eulerAngles;
            other.gameObject.transform.rotation = Quaternion.Euler(0, rotation.y, 0);
            yield return null;
        }
    }
}
