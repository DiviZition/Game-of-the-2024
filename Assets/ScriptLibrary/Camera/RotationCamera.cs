using System.Collections;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform _transformPlayer;
    [SerializeField] private Transform _ownTransform;
    [SerializeField] private float _angleOfRotation;
    [SerializeField] private float _rotationSpeed;

    private float _timeScaler = 0.5f;
    private float _strideLenght = 1;
    private bool _enabledInput = true;
    private Quaternion _rotationAxis;
    private Vector3 _currentAngle;

    private void Update()
    {
        _ownTransform.position = _transformPlayer.position;

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(RotationThisLeft(-1));
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(RotationThisLeft(1));
        }
    }

    private IEnumerator RotationThisLeft(float strideLengthMultiplier)
    {
        if (_enabledInput == true)
        {
            _enabledInput = false;

            for (int i = 0; i < _angleOfRotation; i++)
            {
                _rotationAxis.eulerAngles = new Vector3(0, _currentAngle.y + (_strideLenght * strideLengthMultiplier), 0);
                _ownTransform.rotation = _rotationAxis;
                _currentAngle = _ownTransform.rotation.eulerAngles;

                yield return new WaitForSeconds(_timeScaler / _rotationSpeed);
            }

            _enabledInput = true;
        }
    }
}