using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawwnZone : MonoBehaviour
{
    [SerializeField] private AIAnimal _predatorPrefab;
    [SerializeField] private Transform _zoneCenter;

    [SerializeField] private PlayerComponents _player;
    
    private void Start()
    {
        _zoneCenter = GetComponent<Transform>();

        AIAnimal lion = Instantiate(_predatorPrefab, _zoneCenter.position , Quaternion.identity, this.transform);
        lion.CreateEnemy(_player, this.transform);
    }
}
