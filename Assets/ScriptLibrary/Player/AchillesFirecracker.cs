using UnityEngine;

public class AchillesFirecracker : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _attackMaker;
    [SerializeField] private Transform _handTransform;
    [SerializeField] private Transform _spawnBulletTransform;
    [SerializeField] private float _ballisticsAngle;

    [SerializeField] private MeshRenderer _petardVisual;
    [SerializeField] private Rigidbody _petardPrefab;

    private Transform _playerTransform;

    private float _throwForce;
    private float _graviti = Physics.gravity.y;
    private Vector3 _attackPoint;
    private bool _active = false;

    private void Start()
    {
        _playerTransform = this.transform;
        _spawnBulletTransform.rotation = Quaternion.Euler(new Vector3(_ballisticsAngle, 0, 0));
        SwitchActive(false);
    }

    private void Update()
    {
        _spawnBulletTransform.position = _handTransform.position;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _active = !_active;
            SwitchActive(_active);
        }

        if (_active == true)
        {
            GetMarkerPosition();
            RotatePlayer();

            VisualizePetard();
            VisualizeCrossHair();

            ThrowPetard();
        }
    }

    private void GetMarkerPosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out RaycastHit hitInfo);
        _attackPoint = hitInfo.point;
    }

    private void SwitchActive(bool isActive)
    {
        _petardVisual.enabled = isActive;
        _attackMaker.SetActive(false);
    }

    private void VisualizePetard()
    {
        _petardVisual.enabled = true;
    }

    private void VisualizeCrossHair()
    {
        _attackMaker.transform.position = _attackPoint;
        _attackMaker.SetActive(true);
    }

    private void RotatePlayer()
    {
        Vector3 loockRotation = _attackPoint - _spawnBulletTransform.position;
        loockRotation.y = 0;

        _playerTransform.rotation = Quaternion.LookRotation(loockRotation, Vector3.up);
    }

    private void ThrowPetard()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {
            Transform petardVisualTransform = _petardVisual.transform;
            Rigidbody newPetard = Instantiate(_petardPrefab, petardVisualTransform.position, petardVisualTransform.rotation);

            newPetard.velocity = CalculateThrowDirection();

            _active = false;
            SwitchActive(false);
        }
    }

    private Vector3 CalculateThrowDirection()
    {
        Vector3 fromTo = _attackPoint - _spawnBulletTransform.position;
        float directionY = fromTo.y;
        fromTo.y = 0;

        float x = fromTo.magnitude;
        float y = directionY;

        float angleOfRadians = _ballisticsAngle * Mathf.PI / 180;

        float throwForce2 = (_graviti * x * x) / (2 * (y - Mathf.Tan(angleOfRadians) * x) * Mathf.Pow(Mathf.Cos(angleOfRadians), 2));

        _throwForce = Mathf.Sqrt(Mathf.Abs(throwForce2));

        return _spawnBulletTransform.forward * _throwForce;
    }
}
