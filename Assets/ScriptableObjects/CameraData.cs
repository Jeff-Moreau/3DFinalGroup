using UnityEngine;

[CreateAssetMenu(fileName = "CameraData", menuName = "ScriptableObjects/CameraData")]
public class CameraData : ScriptableObject
{
    // INSPECTOR VARIABLES
    [Header("Camera Data for Zoom")]
    [SerializeField, Range(10, 50)] private float mMaxZoomDistance;
    [SerializeField, Range(10, 50)] private float mMinZoomDistance;
    [SerializeField, Range(0.1f, 10)] private float mZoomSpeed;
    [SerializeField] private float mEdgeBuffer;
    [SerializeField] private float mEdgeScrollSpeed;

    // GETTERS
    public float GetMaxZoomDistance => mMaxZoomDistance;
    public float GetMinZoomDistance => mMinZoomDistance;
    public float GetZoomSpeed => mZoomSpeed;
    public float GetEdgeBuffer => mEdgeBuffer;
    public float GetEdgeScrollSpeed => mEdgeScrollSpeed;
}
