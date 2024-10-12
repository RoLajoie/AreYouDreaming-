using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;  
    public Vector3 offset;    
    public float mouseSensitivity = 100f;  
    public float rotationSmoothTime = 0.1f; 
    public float heightOffset = 1.5f;
    public float pitchMin = -50f;
    public float pitchMax = 50f;   

    private float currentYaw = 0f; 
    private float currentPitch = 0f; 
    private Vector3 currentRotation; 
    private Vector3 rotationSmoothVelocity;

    void Start()
    {
        offset = new Vector3(0, 3, -4); 
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        currentYaw += mouseX;
        currentPitch -= mouseY;  //subtracting mouseY to invert the Y-axis 
        
        currentPitch = Mathf.Clamp(currentPitch, pitchMin, pitchMax);  

        //smoothly rotate the camera around both the X and Y axes
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(currentPitch, currentYaw, 0f), ref rotationSmoothVelocity, rotationSmoothTime);

        //rotation around the player with the updated offset
        transform.position = player.position + Quaternion.Euler(currentRotation) * offset;
        
        Vector3 targetPosition = player.position + Vector3.up * heightOffset;

        transform.LookAt(targetPosition);
    }


}
