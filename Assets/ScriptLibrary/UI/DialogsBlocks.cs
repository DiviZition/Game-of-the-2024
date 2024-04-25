using System.Collections.Generic;
using UnityEngine;

public class DialogsBlocks : MonoBehaviour
{
    [SerializeField] private float _showDialogDistance;

    [SerializeField] private GameObject _dialogBlockPrefab;
    [SerializeField] private Transform _dialogsHolder;

    private List<GameObject> _dialogBlocks;

    private void Start()
    {
        _dialogBlocks = new List<GameObject>(16);
        for (int i = 0; i < _dialogBlocks.Count; i++)
        {
            GameObject newBlock = Instantiate(_dialogBlockPrefab,  _dialogsHolder);
            _dialogBlocks.Add(newBlock);
        }
    }
}
