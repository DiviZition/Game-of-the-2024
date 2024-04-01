using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float _rangeOfInteraction;
    [SerializeField] private LayerMask _interactableLayer;

    private Camera _camera;
    private RaycastHit _hitinfo;
    private PlayerComponents _components;

    private void Start()
    {
        _camera = Camera.main;
        _components = this.GetComponent<PlayerComponents>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(cameraRay, out _hitinfo, 999, _interactableLayer) == true)
            {
                if(_hitinfo.transform.TryGetComponent(out IInteractable interactable) == true)
                {
                    if((interactable.Transform.position - _components.Transform.position).sqrMagnitude < Mathf.Pow(_rangeOfInteraction, 2))
                    {
                        interactable.Interact();
                    }
                }
            }
        }
    }
}
