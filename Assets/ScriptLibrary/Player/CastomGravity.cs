using UnityEngine;

[RequireComponent(typeof(IsGroundedChecker))]
public class CastomGravity : MonoBehaviour
{
    [SerializeField] private float _flyGravity;
    [SerializeField] private float _groundedGravity;

    private IsGroundedChecker _groundChecker;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _groundChecker = this.GetComponent<IsGroundedChecker>();
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        Vector3 gravityVelocity = _rigidbody.velocity;

        if (_groundChecker.IsGrounded == true)
            gravityVelocity.y = _rigidbody.velocity.y - Mathf.Abs(_groundedGravity);
        else
            gravityVelocity.y = _rigidbody.velocity.y - Mathf.Abs(_flyGravity);

        _rigidbody.velocity = gravityVelocity;
    }
}
