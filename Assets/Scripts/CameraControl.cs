using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Inspector member objects
    public Transform m_firstObject;
    public Transform m_secondObject;
    public float m_baseDistanceToAct = 10.0f;

    // Points for distance observation
    private Vector3 middlePoint;
    private float distanceFromMiddlePoint;
    private float distanceBetweenObjects;

    // Camera properties
    private float cameraDistance;
    private float fieldOfView;
    private float fieldOfViewTan;
    private float aspectRatio;

    void Start()
    {
        CameraSetup();
    }

    void Update()
    {

    }

    /**
     * Camera Settings methods.
     * */

    private void CameraSetup() {
        //Finding the aspect ratio (16:9, 16:10, 4:3...)
        aspectRatio = Screen.width / Screen.height;

        //Getting the field of view (FOV) tangent
        fieldOfViewTan = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);
    }
}