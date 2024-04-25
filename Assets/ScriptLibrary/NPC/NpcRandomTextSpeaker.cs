using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcRandomTextSpeaker : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;
    [SerializeField] private Image _textBackGround;

    [SerializeField] private RandomSpeaker _textsForPlayer;
    [SerializeField] private RandomSpeaker _textsForNpc;
    [SerializeField] private RandomSpeaker _textsForAnimals;
    [SerializeField] private RandomSpeaker _textsForEnemys;

    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _allThatApproachable;

    private NpcComponents _components;
    private Transform _transform => _components.Transform;

    private Collider[] _collidersStorage = new Collider[32];
    private Dictionary<CreatureType, RandomSpeaker> _speakers;
    private Dictionary<CreatureType, float> _speakersTimers;

    private float _speakTimer;
    private float _speakDelay = 2f;
    private float _randomSpeakOffset;

    private void Start()
    {
        _components = this.gameObject.GetComponent<NpcComponents>();

        _speakers = new Dictionary<CreatureType, RandomSpeaker>(4);

        _speakers.Add(CreatureType.Player, _textsForPlayer);
        _speakers.Add(CreatureType.Npc, _textsForNpc);
        _speakers.Add(CreatureType.Animal, _textsForAnimals);
        _speakers.Add(CreatureType.Enemy, _textsForEnemys);
       
        _speakersTimers = new Dictionary<CreatureType, float>(_speakers.Count);

        foreach (var item in _speakers)
        {
            item.Value.SetupTextArea(_textField, _textBackGround);
            _speakersTimers.Add(item.Key, 0);
        }
    }

    private void Update()
    {
        if (_speakTimer > Time.time)
            return;

        _speakTimer = Time.time + Random.Range(_speakDelay * 0.5f, _speakDelay * 1.5f);

        TryToSpeak();
    }

    public void TryToSpeak()
    {
        int foundApproachers = Physics.OverlapSphereNonAlloc
            (_transform.position, _checkRadius, _collidersStorage, _allThatApproachable);

        if (foundApproachers > 0)
        {
            for (int i = 0; i < foundApproachers; i++)
            {
                if (_collidersStorage[i] == _components.Collider)
                    continue;

                if (_collidersStorage[i].transform.gameObject.TryGetComponent(out ICreature creature))
                {
                    if (_speakersTimers[creature.CreatureType] > Time.time)
                        continue;

                    if ((_collidersStorage[i].transform.position - _transform.position).sqrMagnitude >
                        Mathf.Pow(_speakers[creature.CreatureType].SpeakRadius, 2))
                    {
                        continue;
                    }

                    LookAtCreature(creature.Transform);

                    ExecuteSpeakersLogic(creature.CreatureType);
                }
            }
        }
    }

    private void LookAtCreature(Transform creatureTransform)
    {
        _components.Rotator.TakeALookAtPosition(creatureTransform.position);
    }

    private void ExecuteSpeakersLogic(CreatureType creature)
    {
        _randomSpeakOffset = Random.Range(0f, 1f);

        if (_speakers[creature].PingSpeaker() == true)
            _speakersTimers[creature] = Time.time + _speakers[creature].SpeachCoolDown + _randomSpeakOffset;
        else
            _speakersTimers[creature] = Time.time + _speakers[creature].IgnorSpeakCoolDown + _randomSpeakOffset;
    }
}