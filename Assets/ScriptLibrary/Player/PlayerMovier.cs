using UnityEngine;

public class PlayerMovier : MonoBehaviour
{
    [SerializeField] private Transform _transformPlayer;
    [SerializeField] private Rigidbody _rbPlayer;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _dashImpulse;
    [SerializeField] private float _rotateSpeed;

    private Vector3 _deshRotation;

    private Vector3 _inputs;

    private void Update()
    {
        _inputs = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (_inputs != Vector3.zero)
        {
            MovePlayer();
            RotatePlayer();
        }

        PlayerDash();
    }

    private void MovePlayer()
    {
        _rbPlayer.velocity = new Vector3(_inputs.x * _playerSpeed, _rbPlayer.velocity.y, _inputs.z * _playerSpeed);
    }

    private void RotatePlayer()
    {
        _transformPlayer.rotation = Quaternion.Lerp
            (_transformPlayer.rotation, Quaternion.LookRotation(_inputs), _rotateSpeed * Time.deltaTime);
    }

    private void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _deshRotation = new Vector3(_inputs.x, 0, _inputs.z);
            _rbPlayer.velocity = _deshRotation * _dashImpulse;
        }
    }
}
