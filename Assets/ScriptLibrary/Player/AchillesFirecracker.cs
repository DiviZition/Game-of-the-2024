using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchillesFirecracker : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _attackMaker;
    [SerializeField] private Transform _handTransform;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _spawnBulletTransform;
    [SerializeField] private float _ballisticsAngle;

    [SerializeField] private GameObject _petard;
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private MeshRenderer _petardVisual;
    [SerializeField] private float _radiusAffectedArea;
    [SerializeField] private float _bindingTime;

    private float _throwForce;
    private float _graviti = Physics.gravity.y;
    private Vector3 _attackPoint;
    private bool _active = false;

    private void Start()
    {
        _spawnBulletTransform.rotation = Quaternion.EulerRotation(new Vector3(_ballisticsAngle, 0, 0));
        _collider.enabled = false;
        _petardVisual.enabled = false;
        _attackMaker.active = false;
    }

    private void Update()
    {
        _spawnBulletTransform.position = _handTransform.position;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _active = true;
        }

        AttackMode(_active);
    }

    private void AttackMode(bool modActive)
    {
        if (modActive == true)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            _petard.transform.position = _spawnBulletTransform.position;
            _petardVisual.enabled = true;

            Physics.Raycast(ray, out RaycastHit hitInfo);
            _attackPoint = hitInfo.point;
            _attackMaker.transform.position = _attackPoint;
            _attackMaker.active = true;

            Vector3 fromTo = _attackPoint - _playerTransform.position;
            Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

            _playerTransform.rotation = Quaternion.LookRotation(fromToXZ,Vector3.up);

            if (Input.GetButtonDown("Fire1"))
            {
                float x = fromToXZ.magnitude;
                float y = fromTo.y;

                float angleOfRadians = _ballisticsAngle * Mathf.PI / 180;

                float throwForce2 = (_graviti * x * x) / (2 * (y - Mathf.Tan(angleOfRadians) * x) * Mathf.Pow(Mathf.Cos(angleOfRadians), 2));

                _throwForce = Mathf.Sqrt(Mathf.Abs(throwForce2));

                _petard.GetComponent<Rigidbody>().velocity = _spawnBulletTransform.forward * _throwForce;

                _active = false;
            }
        }
    }
}
