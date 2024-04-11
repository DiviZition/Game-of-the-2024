using UnityEngine;

public class TestFPSLocker : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate;

    void Start()
    {
        Application.targetFrameRate = _targetFrameRate;
    }
}
