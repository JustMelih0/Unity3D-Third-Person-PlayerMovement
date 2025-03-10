using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target/Cam Movement Settings")]
    [SerializeField]private Transform target; 
    [SerializeField]private float distance = 7f; 
    [SerializeField]private float xSpeed = 100f; 
    [SerializeField]private float ySpeed = 120f; 

    [Space]

    [Header("Smooth Settings")]
    [SerializeField]private float yMinLimit = -20f; 
    [SerializeField]private float yMaxLimit = 80f; 
    [SerializeField]private float smoothTime = 0.1f; 

    [Space]

    [Header("Distance Settings")]
    [SerializeField]private float distanceMin = 2f; 
    [SerializeField]private float distanceMax = 10f; 

    [SerializeField]private Vector3 camOffSet = new(0 ,1 ,0);

    private float x = 0f;
    private float y = 0f;

    private Vector3 currentVelocity = Vector3.zero; 


    private void Start()
    {
        Cursor.visible = false;
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    private void Update()
    {
        if (target != null)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            Vector3 targetPosition = (target.position + camOffSet) - rotation * Vector3.forward * distance;
            
            transform.SetPositionAndRotation(Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime), rotation);
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            distance = Mathf.Clamp(distance - scroll * 5f, distanceMin, distanceMax);
        }
    }
}
