using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform _transformPlayer;
    [SerializeField] private Transform _ownTransform;
    [SerializeField] private float _angleOfRotation;
    [SerializeField] private float _stepTime;

    private float _timeScaler = 100;
    private float _strideLenght = 1;
    private bool _enabledInput = true;
    private Quaternion _rotationAxis;
    private Vector3 _currentAngle;

    private void Start()
    {
        _currentAngle = _ownTransform.rotation.eulerAngles;
    }
    private void Update()
    {
        _ownTransform.position = _transformPlayer.position;

        IEnumerator enumerator1 = RotationThisLeft();
        IEnumerator enumerator2 = RotationThisRight();

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(enumerator2);
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(enumerator1);
        }
    }

    private IEnumerator RotationThisLeft()
    {
        if (_enabledInput == true)
        {
            _enabledInput = false;
            for (int i = 0; i < _angleOfRotation; i++)
            {
                _rotationAxis.eulerAngles = new Vector3(0, _currentAngle.y + _strideLenght, 0);
                _ownTransform.rotation = _rotationAxis;
                _currentAngle = _ownTransform.rotation.eulerAngles;

                yield return new WaitForSeconds(_stepTime / _timeScaler);
            }
            _enabledInput = true;
        }
    }

    private IEnumerator RotationThisRight()
    {
        if (_enabledInput == true)
        {
            _enabledInput = false;
            for (int i = 0; i < _angleOfRotation; i++)
            {
                _rotationAxis.eulerAngles = new Vector3(0, _currentAngle.y + (_strideLenght * -1), 0);
                _ownTransform.rotation = _rotationAxis;
                _currentAngle = _ownTransform.rotation.eulerAngles;

                yield return new WaitForSeconds(_stepTime / _timeScaler);
            }
            _enabledInput=true;
        }
    }
}
