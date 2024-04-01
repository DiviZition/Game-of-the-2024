using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RandomSpeaker
{
    [SerializeField] private float _speakRadius;
    [SerializeField] private float _speachCoolDown;
    [SerializeField] private float _ignorSpeakCoolDown;
    [Range(0, 100)][SerializeField] private int _chanseToSpeak;
    [TextArea][SerializeField] private string[] _randomTexts;

    private TMP_Text _text;
    private Image _backGround;

    private readonly int _textAppearsForMilliseconds = 5000;
    private readonly int _testDessapearOffset = 100;

    public float SpeakRadius => _speakRadius;
    public float SpeachCoolDown => _speachCoolDown;
    public float IgnorSpeakCoolDown => _ignorSpeakCoolDown;

    public void SetupTextArea(TMP_Text text, Image backGround)
    {
        _text = text;
        _backGround = backGround;
    }

    public bool PingSpeaker()
    {
        int speakProbability = Random.Range(0, 100);

        if (speakProbability > _chanseToSpeak)
        {
            return false;
        }

        int randomText = Random.Range(0, _randomTexts.Length);

        _text.text = _randomTexts[randomText];
        _text.ForceMeshUpdate();

        AppearTextForTime();

        return true;
    }

    private async void AppearTextForTime()
    {
        _backGround.transform.gameObject.SetActive(true);
        _text.transform.gameObject.SetActive(true);

        if((int)(_speachCoolDown * 1000) < _textAppearsForMilliseconds)
            await Task.Delay((int)(_speachCoolDown * 1000) - _testDessapearOffset);
        else
            await Task.Delay(_textAppearsForMilliseconds);

        if (_backGround == null || _text == null)
            return;

        _backGround.transform.gameObject.SetActive(false);
        _text.transform.gameObject.SetActive(false);
    }
}