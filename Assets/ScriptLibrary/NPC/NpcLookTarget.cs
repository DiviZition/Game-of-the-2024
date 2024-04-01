using Polytope;
using UnityEngine;

public class NpcLookTarget : MonoBehaviour
{
    [SerializeField] private float _turningSpeed = 4;

    private Transform _transform;

    private Vector3 _lookAtPosition;
    private Transform _lookAtTarget;
    private Vector3 _directionToTarget;
    private Quaternion _startRotation;

    private bool _haveToFallow;
    private float _turningProgress;

    private const float MAGIC_OFFSET = 1.1f;

    private void Start()
    {
        _transform = this.transform;
    }

    private void Update()
    {
        if (_lookAtTarget != null)
            LookAtTarget(_lookAtTarget.position);
        else if (_lookAtPosition != Vector3.zero)
            LookAtTarget(_lookAtPosition);
    }

    private void LookAtTarget(Vector3 targetPosition)
    {
        if (_turningProgress > 1 && _haveToFallow == false)
        {
            _lookAtPosition = Vector3.zero;
            _lookAtTarget = null;
            return;
        }

        _directionToTarget = (NoYVector3(targetPosition) - NoYVector3(_transform.position)).normalized;

        _transform.rotation = Quaternion.Slerp
            (_startRotation, Quaternion.LookRotation(_directionToTarget), _turningProgress);

        _turningProgress += Time.deltaTime * _turningSpeed * (MAGIC_OFFSET - _turningProgress);
    }

    private Vector3 NoYVector3(Vector3 vector)
    {
        vector.y = 0;
        return vector;
    }

    public void TakeALookAtPosition(Vector3 position)
    {
        _lookAtPosition = position;
        _startRotation = _transform.rotation;
        _turningProgress = 0;
        _haveToFallow = false;
    }

    public void StartFollowTarget(Transform targetToLookAt)
    {
        _lookAtTarget = targetToLookAt;
        _startRotation = _transform.rotation;
        _turningProgress = 0;
        _haveToFallow = true;
    }

    public void StopFoolowingTarget()
    {
        _haveToFallow = false;
    }
}
