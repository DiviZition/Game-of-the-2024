using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAnimal : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _homeCenter;
    [SerializeField] private Transform _agentPosition;
    [SerializeField] private Transform _agressorPosition;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private float _pursuitDistance;
    [SerializeField] private float _screamDistance;

    [SerializeField] private float _speed;
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private float _resetDestinationTimer;

    private Vector3 _newDestination;
    private Vector3 _newRandom;
    private float _timer;
    private float _visibility;

    private bool _isHeAggressive = false;
    private bool _doesHeKnow;

    private void Start()
    {
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

    private void BeastVision()
    {
        RaycastHit hit;
        _timer = _resetDestinationTimer;
        _timer -= Time.deltaTime;

        if (_timer == 0)
        {
            _doesHeKnow = Physics.SphereCast(_agentPosition.position, _screamDistance, _agentPosition.forward, out hit, _layerMask);
            _timer = _resetDestinationTimer;
        }

        if (_doesHeKnow == true)
        {
            _visibility = Vector3.Dot(_agentPosition.forward, _agressorPosition.position);

            if (_visibility >= 0.50)
            {
                _isHeAggressive = true;
            }
        }

    }

    private void StateOfCalm()
    {
        _agent.speed = _speed;
        if (Vector3.Distance(_newDestination, _agentPosition.position) == 0)
        {
            _timer = _resetDestinationTimer;
            _timer -= Time.deltaTime;

            if (_timer == 0)
            {
                NewRandomTaget();
                _timer = _resetDestinationTimer;
            }
        }
    }

    private void StateAggressive()
    {
        _agent.SetDestination(_agressorPosition.position);
        _agent.speed = _speed * _speedMultiplier;

        if (Vector3.Distance(_agressorPosition.position, _homeCenter.position) >= _pursuitDistance)
        {
            _isHeAggressive = false;
        }
    }

    private void NewRandomTaget()
    {
        RaycastHit hit;
        _newRandom = Random.insideUnitCircle; 
        _newDestination = new Vector3( _agentPosition.position.x + _newRandom.x, 50 , _agentPosition.position.z + _newRandom.z);
         
        Physics.Raycast(_newDestination , -Vector3.up, out hit);
        
        _agent.SetDestination( hit.point);
        Debug.Log( hit.point);
    }
}
