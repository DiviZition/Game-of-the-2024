using UnityEngine;

public class NpcComponents : MonoBehaviour
{
    public Transform Transform { get; private set; }
    public Collider Collider { get; private set; }

    public NpcBrain Brain { get; private set; }
    public HealthComponent Health { get; private set; }
    public NpcRandomTextSpeaker RandomSpeaker { get; private set; }
    public NpcLookTarget Rotator { get; private set; }

    private void Awake()
    {
        Transform = this.transform;
        Collider = this.GetComponent<Collider>();

        Brain = this.gameObject.GetComponent<NpcBrain>();
        Health = this.gameObject.GetComponent<HealthComponent>();
        RandomSpeaker = this.gameObject.GetComponent<NpcRandomTextSpeaker>();
        Rotator = this.gameObject.GetComponent<NpcLookTarget>();
    }
}