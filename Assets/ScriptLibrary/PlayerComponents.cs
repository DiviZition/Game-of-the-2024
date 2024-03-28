using UnityEngine;
using UnityEngine.AI;

public class PlayerComponents : MonoBehaviour
{
    public Transform Transform { get; private set; }
    public Collider Collider { get; private set; }

    public PlayerMove Move { get; private set; }

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
        Move = this.GetComponent<PlayerMove>();
    }
}
