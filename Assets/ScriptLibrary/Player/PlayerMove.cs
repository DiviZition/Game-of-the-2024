using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform _transformPlayer;
    [SerializeField] private Rigidbody _rbPlayer;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _dashImpulse;
    [SerializeField] private float _gravityModifier;

    private Vector3 _rotationPlayer;
    private Vector3 _deshRotation;

    private void Update()
    {
        WalkingPlayer();
        PlayerDash();
        GravityModifier();
    }

    private void WalkingPlayer()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            _rbPlayer.velocity = new Vector3(Input.GetAxis("Horizontal") * _playerSpeed, _rbPlayer.velocity.y, Input.GetAxis("Vertical") * _playerSpeed);
            _rotationPlayer = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _transformPlayer.rotation = Quaternion.LookRotation(_rotationPlayer);
        }
    }

    private void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _deshRotation = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _rbPlayer.velocity = _deshRotation * _dashImpulse;
        }
    }

    private void GravityModifier()
    {
        _rbPlayer.velocity -= Vector3.up * Mathf.Abs(_gravityModifier) * Time.deltaTime;
    }
}
