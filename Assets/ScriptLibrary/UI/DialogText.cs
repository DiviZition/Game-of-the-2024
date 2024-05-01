using TMPro;
using UnityEngine;

public class DialogText : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;

    private GameObject _gameObject;

    private void Start()
    {
        _gameObject = this.gameObject;

        HideTextBox();
    }

    public void ShowTextBox(string newText)
    {
        SwitchAllElements(true);

        _textField.text = newText;
    }

    public void HideTextBox()
    {
        SwitchAllElements(false);
    }

    private void SwitchAllElements(bool isEnabled)
    {
        _gameObject.SetActive(isEnabled);
    }
}
