using UnityEngine;

public class CreatureIdentifier : MonoBehaviour, ICreature
{
    [SerializeField] private CreatureType _characterType;

    public CreatureType CreatureType => _characterType;

    public Transform Transform { get; private set; }

    private void Start()
    {
        Transform = this.transform;
    }
}
