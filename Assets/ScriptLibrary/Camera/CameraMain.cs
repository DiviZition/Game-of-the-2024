using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    [SerializeField] private Transform _transformCentralAxis;
    [SerializeField] private Transform _transformCamera;
    [SerializeField] private float _scrollScaller;
    [SerializeField] private Vector3 _startOffset;
    [SerializeField] private Vector3 _startRotateon;

    private Quaternion _cameraRotation;

    private void Start()
    {
        _cameraRotation.eulerAngles = _startRotateon;
        _transformCamera.rotation = _cameraRotation;
        _transformCamera.localPosition = _startOffset;
    }
    private void Update()
    {
        CameraDinamicOffset();
    }

    private void CameraDinamicOffset()
    {
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            float scrollDelta = Input.mouseScrollDelta.y;
            _transformCamera.localPosition += new Vector3(0, -_scrollScaller/2 * scrollDelta, _scrollScaller * scrollDelta);
        }
    }
}
