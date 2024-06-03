using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawwnZone : MonoBehaviour
{
    [SerializeField] private GameObject _predatorPrefab;
    [SerializeField] private Transform _zoneCenter;
    
    private void Start()
    {
        _zoneCenter = GetComponent<Transform>();
        Instantiate(_predatorPrefab, _zoneCenter.position , Quaternion.identity);
    }
}
