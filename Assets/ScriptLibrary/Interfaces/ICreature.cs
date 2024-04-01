using UnityEngine;

public interface ICreature
{
    public Transform Transform { get; }
    public CreatureType CreatureType { get; }
}
