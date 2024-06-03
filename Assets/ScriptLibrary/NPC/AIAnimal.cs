using UnityEngine;
using UnityEngine.AI;

public class AIAnimal : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private LayerMask _playerMask;

    [SerializeField] private float _pursuitDistance;
    [SerializeField] private float _screamDistance;

    [SerializeField] private float _speed;
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private float _checkForPlayerDelay = 1;
    [SerializeField] private float _walkDelay = 5;

    private Transform _transform;
    private Transform _homeCenter;
    private Transform _player;

    private Vector3 _newDestination;
    private Vector3 _newRandom;
    private float _visibility;

    private float _walkTimer;
    private float _checkPlayerTimer;

    private bool _isHeAggressive = false;
    private bool _doesHeKnow;

    private void Start()
    {
        _transform = this.transform;
        NewRandomTaget();
        _agent.speed = _speed;
    }

    private void Update()
    {
        BeastVision();

        if (_isHeAggressive == false)
        {
            StateOfCalm();
        }
        else
        {
            StateAggressive();
        }
    }

    public void CreateEnemy(PlayerComponents player, Transform homeZone)
    {
        _player = player.Transform;
        _homeCenter = homeZone;
    }

    private void BeastVision()
    {
        _checkPlayerTimer -= Time.deltaTime;

        if (_checkPlayerTimer <= 0)
        {
            Collider[] trash = Physics.OverlapSphere(_transform.position, _screamDistance, _playerMask);

            _doesHeKnow = trash.Length > 0;
            _checkPlayerTimer = _checkForPlayerDelay;
        }

        if (_doesHeKnow == true)
        {
            _visibility = Vector3.Dot(_transform.forward, (_player.position - _transform.position).normalized);

            if (_visibility >= 0.50)
            {
                _isHeAggressive = true;
            }
        }
    }

    private void StateOfCalm()
    {
        _walkTimer -= Time.deltaTime;
        _agent.speed = _speed;

        if (_walkTimer <= 0)
        {
            NewRandomTaget();
            _walkTimer = _walkDelay;
        }
    }

    private void StateAggressive()
    {
        _agent.SetDestination(_player.position);
        _agent.speed = _speed * _speedMultiplier;

        if (Vector3.Distance(_player.position, _homeCenter.position) >= _pursuitDistance)
        {
            _isHeAggressive = false;
            _agent.SetDestination(_homeCenter.position);
        }
    }

    private void NewRandomTaget()
    {
        _newRandom = Random.insideUnitCircle; 
        _newDestination = new Vector3(_newRandom.x, 0, _newRandom.y);
        _newDestination *= _pursuitDistance / 2;
        _newDestination.y = 10;
        _newDestination += _homeCenter.position;

        Physics.Raycast(_newDestination , Vector3.down * 10 * 2, out RaycastHit hit);
        
        _agent.SetDestination( hit.point);
    }
}
