using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform _transformPlayer;
    [SerializeField] private Transform _ownTransform;
    [SerializeField] private float _angleOfRotation;
    [SerializeField] private float _strideLenght;
    [SerializeField] private float _stepTime;

    private Quaternion _rotationAxis;
    private Vector3 _currentAngle;

    private void Start()
    {
        _currentAngle = _ownTransform.rotation.eulerAngles;
    }
    private void Update()
    {
        //Input.mouseScrollDelta

        _ownTransform.position = _transformPlayer.position;

        IEnumerator enumerator = RotationThisCamera();
        StartCoroutine(enumerator);
    }

    private IEnumerator RotationThisCamera()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < _angleOfRotation; i++)
            {
                _rotationAxis.eulerAngles = new Vector3(0, _currentAngle.y + _strideLenght, 0);
                _ownTransform.rotation = _rotationAxis;
                _currentAngle = _ownTransform.rotation.eulerAngles;

                yield return new WaitForSeconds(_stepTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < _angleOfRotation; i++)
            {
                _rotationAxis.eulerAngles = new Vector3(0, _currentAngle.y + (_strideLenght * -1), 0);
                _ownTransform.rotation = _rotationAxis;
                _currentAngle = _ownTransform.rotation.eulerAngles;

                yield return new WaitForSeconds(_stepTime);
            }
        }
    }
}
