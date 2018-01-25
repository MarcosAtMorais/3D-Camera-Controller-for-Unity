using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Inspector member objects
    public Transform m_firstObject;
    public Transform m_secondObject;
    public float m_baseDistanceToAct = 10.0f;

    // Points for distance observation
    private Vector3 middlePoint;
    private Vector3 vectorBetweenObjects;
    private float distanceFromMiddlePoint;
    private float distanceBetweenObjects;

    // Camera properties
    private float cameraDistance;
    private float fieldOfView;
    private float fieldOfViewTan;
    private float aspectRatio;

    private const float NORMALIZE_FACTOR = 0.5f;

    private Camera currentCamera;

    void Start() {
        CameraSetup();
    }

    void Update() {
        CenterCamera();
        GetMiddlePointBetween(firstObject: m_firstObject, secondObject: m_secondObject);
        GetNewCameraDistance();
        GetNewCameraPosition();
    }

    /**
     * Camera Settings methods.
     * */

    private void CameraSetup() {
        // Finding the aspect ratio (16:9, 16:10, 4:3...)
        aspectRatio = Screen.width / Screen.height;

        // Getting the field of view (FOV) tangent
        fieldOfViewTan = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);

        // Gets the current camera
        currentCamera = Camera.main;
    }

    /**
     * Method that positions the camera in the right center position every frame per sec
     */
    private void CenterCamera() {
        // Gets the now used camera's current position
        Vector3 newCameraPosition = currentCamera.transform.position;

        newCameraPosition.x = middlePoint.x;
        currentCamera.transform.position = newCameraPosition;
    }

    /**
     * Get current middle point between two objects using their positions based on Transforms
     */

    private void GetMiddlePointBetween(Transform firstObject, Transform secondObject) {
        vectorBetweenObjects = secondObject.position - firstObject.position;
        middlePoint = firstObject.position + NORMALIZE_FACTOR * vectorBetweenObjects;
    }

    /**
     * Calculates the new camera distance, which is going to be based on the FOV.
     */

    private void GetNewCameraDistance() {
        distanceBetweenObjects = vectorBetweenObjects.magnitude;
        cameraDistance = (distanceBetweenObjects / 2.0f/ aspectRatio) / fieldOfViewTan;
    }

    /*
     * Normalizes the middle point // camera current position and 
     * smoothifies the action based on the camera distance and the 
     * Base Distance to Act.
     */
    private void GetNewCameraPosition() {
        Vector3 normalizedPositionPoint = (currentCamera.transform.position - middlePoint).normalized;
        currentCamera.transform.position = middlePoint + normalizedPositionPoint * (cameraDistance + m_baseDistanceToAct);
    }

}