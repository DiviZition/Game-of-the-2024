using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Camera _Camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private NavMeshAgent _agent;

    private Ray _ray;
    private float _distanceRay = 1000;
    void Update()
    {
        LeadingPoint();
    }

    private void LeadingPoint()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(_ray, out hit, _distanceRay, _layerMask);
            _agent.destination = hit.point;
        }
    }
}
