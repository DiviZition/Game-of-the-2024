using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    public Transform Transform { get; private set; }
    public Rigidbody RigidBody { get; private set; }
    public Collider Collider { get; private set; }

    public PlayerMovier Move { get; private set; }
    public HealthComponent Health { get; private set; }
    public PlayerInteractor Interactor { get; private set; }
    public CreatureIdentifier CreatureIdentifier { get; private set; }
    public IsGroundedChecker GroundChecker { get; private set; }
    public CastomGravity Gravity { get; private set; }

    private void Awake()
    {
        GetUnityComponents();
        GetScripts();
    }

    private void GetUnityComponents()
    {
        Transform = this.transform;
        RigidBody = this.GetComponent<Rigidbody>();
        Collider = this.GetComponent<Collider>();
    }

    private void GetScripts()
    {
        Move = this.GetComponent<PlayerMovier>();
        Health = this.GetComponent<HealthComponent>();
        Interactor = this.GetComponent<PlayerInteractor>();
        CreatureIdentifier = this.GetComponent<CreatureIdentifier>();
        GroundChecker = this.GetComponent<IsGroundedChecker>();
    }
}