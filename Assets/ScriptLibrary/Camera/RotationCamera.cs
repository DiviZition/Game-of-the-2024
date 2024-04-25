using System.Collections;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform _transformPlayer;
    [SerializeField] private Transform _ownTransform;
    [SerializeField] private float _angleOfRotation;
    [SerializeField] private float _rotationSpeed;

    private float _strideLenght = 1;
    private bool _enabledInput = true;
    private Quaternion _rotationAxis;
    private Vector3 _currentAngle;

    private void Update()
    {
        _ownTransform.position = _transformPlayer.position;

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(RotationThisLeft(-_rotationSpeed));
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(RotationThisLeft(_rotationSpeed));
        }
    }

    private IEnumerator RotationThisLeft(float strideLengthMultiplier)
    {
        if (_enabledInput == true)
        {
            _enabledInput = false;

            for (int i = 0; i < _angleOfRotation / Mathf.Abs(strideLengthMultiplier); i++)
            {
                _rotationAxis.eulerAngles = new Vector3(0, _currentAngle.y + (_strideLenght * strideLengthMultiplier), 0);
                _ownTransform.rotation = _rotationAxis;
                _currentAngle = _ownTransform.rotation.eulerAngles;

                yield return null;
                //yield return new WaitForSeconds(_timeStep);
            }

            _enabledInput = true;
        }
    }
}