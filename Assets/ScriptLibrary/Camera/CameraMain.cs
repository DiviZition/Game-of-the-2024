using UnityEngine;

public class CameraMain : MonoBehaviour
{
    [SerializeField] private Transform _transformCentralAxis;
    [SerializeField] private Transform _transformCamera;
    [SerializeField] private float _scrollScaller;
    [SerializeField] private Vector3 _startOffset;
    [SerializeField] private Vector3 _startRotateon;

    [SerializeField] private Vector2 _zoomLimits;

    private float _scrollDelta;
    private float _distanceToCamera;

    private void Start()
    {
        _transformCamera.rotation = Quaternion.Euler(_startRotateon);
        _transformCamera.localPosition = _startOffset;
    }

    private void LateUpdate()
    {
        _scrollDelta = Input.mouseScrollDelta.y;
        _distanceToCamera = FastSquareRoot((_transformCentralAxis.position - _transformCamera.position).sqrMagnitude);

        CameraDinamicOffset();
    }

    private void CameraDinamicOffset()
    {
        if (IsZoomingInLimitations() == true)
            return;

        float scrollDelta = Input.mouseScrollDelta.y;
        _transformCamera.localPosition += new Vector3(0, -_scrollScaller / 2 * scrollDelta, _scrollScaller * scrollDelta);
    }

    private bool IsZoomingInLimitations()
    {
        return 
            _scrollDelta == 0 ||
            _scrollDelta < 0 && _distanceToCamera > _zoomLimits.y ||
            _scrollDelta > 0 && _distanceToCamera < _zoomLimits.x;
    }

    private static float FastSquareRoot(float x)
    {
        if (x == 0)
            return 0;

        float estimate = x;
        float previous;

        do
        {
            previous = estimate;
            estimate = (estimate + x / estimate) / 2f;
        }
        while (previous != estimate);

        return
            estimate;
    }
}