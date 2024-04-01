using UnityEngine;

public class BillBoardCanvas : MonoBehaviour
{
    private Transform _cameraTransform;
    private Transform _transform;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _transform = this.transform;
    }

    private void LateUpdate()
    {
        _transform.LookAt(_transform.position + Camera.main.transform.forward);
    }
}
