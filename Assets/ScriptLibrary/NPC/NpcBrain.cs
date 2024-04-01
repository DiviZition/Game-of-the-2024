using UnityEngine;

public class NpcBrain : MonoBehaviour, IInteractable
{
    private NpcComponents _components;

    public Transform Transform => _components.Transform;

    private void Start()
    {
        _components = this.gameObject.GetComponent<NpcComponents>();
    }

    public void Interact()
    {
        Debug.Log("Interacting");
    }
}