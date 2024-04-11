using UnityEngine;

public class IsGroundedChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector3 _checkerStartPointOffset;
    [SerializeField] private float _checkerLength;

    [SerializeField] private bool _isGroundedForDebug;

    private Transform _transform;

    public bool IsGrounded { get; private set; }

    private void Start()
    {
        _transform = this.transform;
    }

    private void LateUpdate()
    {
        IsGrounded = Physics.Raycast
            (new Ray(_transform.position + _checkerStartPointOffset, Vector3.down), _checkerLength, _groundLayer);

        _isGroundedForDebug = IsGrounded;
    }

    private void OnDrawGizmos()
    {
        if (_transform == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_transform.position + _checkerStartPointOffset, Vector3.down * _checkerLength);
    }
}
