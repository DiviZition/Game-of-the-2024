using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    [SerializeField] private Transform _transformCentralAxis;
    [SerializeField] private Transform _transformCamera;
    [SerializeField] private Vector3 _startOffset;
    [SerializeField] private Vector3 _startRotateon;

    private Quaternion _cameraRotation;

    private void Start()
    {
        _cameraRotation.eulerAngles = _startRotateon;
        _transformCamera.rotation = _cameraRotation;
    }
    private void Update()
    {
        CameraDinamicOffset();
    }

    private void CameraDinamicOffset()
    {
        _transformCamera.localPosition = _startOffset;
    }
}
