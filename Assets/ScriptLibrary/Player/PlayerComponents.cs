using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    public Transform Transform { get; private set; }
    public Collider Collider { get; private set; }

    public PlayerMovier Move { get; private set; }
    public HealthComponent Health { get; private set; }
    public PlayerInteractor Interactor { get; private set; }

    private void Awake()
    {
        GetUnityComponents();
        GetScripts();
    }

    private void GetUnityComponents()
    {
        Transform = this.transform;
        Collider = this.GetComponent<Collider>();
    }

    private void GetScripts()
    {
        Move = this.GetComponent<PlayerMovier>();
        Health = this.GetComponent<HealthComponent>();
        Interactor = this.GetComponent<PlayerInteractor>();
    }
}