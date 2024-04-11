using UnityEngine;

public class PlayerMovier : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _dashImpulse;
    [SerializeField] private float _rotateSpeed;

    [SerializeField] private Transform _cameraTransform;

    private PlayerComponents _components;

    private Vector3 _deshRotation;
    private Vector3 _inputs;
    private Vector3 _cameraBasedForward;

    private void Start()
    {
        _components = this.GetComponent<PlayerComponents>();
    }

    private void Update()
    {
        _inputs = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (_inputs != Vector3.zero)
        {
            _cameraBasedForward = CalculateCameraBasedForward();

            MovePlayer();
            RotatePlayer();
        }

        PlayerDash();
    }

    private void MovePlayer()
    {
        Vector3 playersForward = _cameraBasedForward;
        playersForward *= _playerSpeed;
        playersForward.y = _components.RigidBody.velocity.y;

        _components.RigidBody.velocity = playersForward;
    }

    private void RotatePlayer()
    {
        _components.Transform.rotation = Quaternion.Lerp
            (_components.Transform.rotation, Quaternion.LookRotation(_cameraBasedForward), _rotateSpeed);
    }

    private Vector3 CalculateCameraBasedForward()
    {
        Vector3 cameraBasedForward = _cameraTransform.right * _inputs.x + _cameraTransform.forward * _inputs.z;
        cameraBasedForward.Normalize();

        return cameraBasedForward;
    }

    private void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _deshRotation = new Vector3(_inputs.x, 0, _inputs.z);
            _components.RigidBody.velocity = _deshRotation * _dashImpulse;
        }
    }
}
