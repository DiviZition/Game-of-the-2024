using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _groundMask;

    private PlayerComponents _components;
    private Ray _ray;
    private float _distanceRay = 1000;

    private void Start()
    {
        _components = this.gameObject.GetComponent<PlayerComponents>();
    }

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
            Physics.Raycast(_ray, out hit, _distanceRay, _groundMask);
            _components.NavMeshAgent.destination = hit.point;
        }
    }
}
