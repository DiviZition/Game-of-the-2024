using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogsBlocksRenderer : MonoBehaviour
{
    [SerializeField] private float _showDialogDistance;

    [SerializeField] private DialogText _dialogBlockPrefab;
    [SerializeField] private Transform _dialogsHolder;

    private Camera _camera;

    private List<DialogComponents> _dialogBlocks;
    private List<int> _activeDialogsIndexes;

    private void Start()
    {
        _camera = Camera.main;

        _dialogBlocks = new List<DialogComponents>(16);
        _activeDialogsIndexes = new List<int>(16);

        for (int i = 0; i < _dialogBlocks.Count; i++)
        {
            DialogText newBlock = Instantiate(_dialogBlockPrefab,  _dialogsHolder);
            _dialogBlocks.Add(new DialogComponents(newBlock));
        }
    }

    private void Update()
    {
        DisableOutDatedDialogs();
        UpdateDialogsPositions();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void StartShowingDilog()
    {

    }

    private void UpdateDialogsPositions()
    {
        for (int i = 0; i < _activeDialogsIndexes.Count; i++)
        {
            UpdateBlocksPosition(_activeDialogsIndexes[i]);
        }
    }

    private void DisableOutDatedDialogs()
    {
        for (int i = 0; i < _activeDialogsIndexes.Count; i++)
        {
            if(_dialogBlocks[_activeDialogsIndexes[i]].DisableTime > Time.time)
            {
                _dialogBlocks[_activeDialogsIndexes[i]].DeactivateDialog();

                _activeDialogsIndexes[i] = _activeDialogsIndexes[_activeDialogsIndexes.Count - 1];
                _activeDialogsIndexes.RemoveAt(_activeDialogsIndexes.Count - 1);
            }
        }
    }

    private int FindDisabledComponentsIndex()
    {
        for (int i = 0; i < _dialogBlocks.Count; i++)
        {
            if (_dialogBlocks[i].IsActive == false)
                return i;
        }

        return -1;
    }

    private void UpdateBlocksPosition(int blockIndex)
    {
        Vector3 newPosition = _dialogBlocks[blockIndex].SpeakerTransform.position;
        newPosition.y += _dialogBlocks[blockIndex].VerticalPositionOffset;
        newPosition = _camera.WorldToScreenPoint(newPosition);

        _dialogBlocks[blockIndex].TextTransform.position = newPosition;
    }
}


public struct DialogComponents
{
    private DialogText _textComponent;

    private const float SHOW_TEXTBOX_TIME = 5;


    public Transform TextTransform { get; private set; }
    public Transform SpeakerTransform { get; private set; }
    public float VerticalPositionOffset { get; private set; }
    public float DisableTime { get; private set; }
    public bool IsActive { get; private set; }

    public DialogComponents(DialogText text)
    {
        _textComponent = text;
        TextTransform = _textComponent.transform;
        SpeakerTransform = null;
        VerticalPositionOffset = 0;

        IsActive = false;
        DisableTime = 0;
    }

    public void DeactivateDialog()
    {
        IsActive = false;

        _textComponent.HideTextBox();
    }

    public void ActivateDialog(string text, Transform speaker, float dialogVerticalPositionOffset)
    {
        IsActive = true;
        DisableTime = Time.time + SHOW_TEXTBOX_TIME;

        SpeakerTransform = speaker;
        VerticalPositionOffset = dialogVerticalPositionOffset;

        _textComponent.ShowTextBox(text);
    }
}