using UnityEngine;


public class MouseLookScript : MonoBehaviour
{

    [HideInInspector]
    public Transform myCamera;

    [SerializeField]
    private bool stopLooking;

    /*
	 * Hiding the cursor.
	 */
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        myCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        stopLooking = false;

    }

    
    void Update()
    {

        MouseInputMovement();

        if (Input.GetKeyDown(KeyCode.L))
        {
            Cursor.lockState = CursorLockMode.Locked;
            stopLooking = false; ;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            stopLooking = true;
        }
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        if (GetComponent<PlayerMovementScript>().currentSpeed > 1)
            HeadMovement();

    }

    [Header("Z Rotation Camera")]
    [HideInInspector] public float timer;
    [HideInInspector] public int int_timer;
    [HideInInspector] public float zRotation;
    [HideInInspector] public float wantedZ;
    [HideInInspector] public float timeSpeed = 2;

    [HideInInspector] public float timerToRotateZ;
    /*
	* Switching Z rotation and applying to camera in camera Rotation().
	*/
    void HeadMovement()
    {
        timer += timeSpeed * Time.deltaTime;
        int_timer = Mathf.RoundToInt(timer);
        if (int_timer % 2 == 0)
        {
            wantedZ = -1;
        }
        else
        {
            wantedZ = 1;
        }

        zRotation = Mathf.Lerp(zRotation, wantedZ, Time.deltaTime * timerToRotateZ);
    }

    public float mouseSensitvity = 0;
    [HideInInspector]
    public float mouseSensitvity_notAiming = 300;
    [HideInInspector]
    public float mouseSensitvity_aiming = 50;

    /*
    * FixedUpdate()
    * If aiming set the mouse sensitvity from our variables and vice versa.
    */
    void FixedUpdate()
    {

        /*
         * Reduxing mouse sensitvity if we are aiming.
         */
        if (Input.GetAxis("Fire2") != 0)
        {
            mouseSensitvity = mouseSensitvity_aiming;
        }
        else if (GetComponent<PlayerMovementScript>().Velocidad > 5)
        {
            mouseSensitvity = mouseSensitvity_notAiming;
        }
        else
        {
            mouseSensitvity = mouseSensitvity_notAiming;
        }

        ApplyingStuff();


    }


    private float rotationYVelocity, cameraXVelocity;

    public float yRotationSpeed, xCameraSpeed;

    [HideInInspector]
    public float wantedYRotation;
    [HideInInspector]
    public float currentYRotation;

    [HideInInspector]
    public float wantedCameraXRotation;
    [HideInInspector]
    public float currentCameraXRotation;

    [Tooltip("Top camera angle.")]
    public float topAngleView = 90;
    [Tooltip("Minimum camera angle.")]
    public float bottomAngleView = -90;
   
    void MouseInputMovement()
    {
        if (!stopLooking)
        {
            wantedYRotation += Input.GetAxis("Mouse X") * mouseSensitvity;

            wantedCameraXRotation -= Input.GetAxis("Mouse Y") * mouseSensitvity;

            wantedCameraXRotation = Mathf.Clamp(wantedCameraXRotation, bottomAngleView, topAngleView);
        }
    }

    
    void ApplyingStuff()
    {
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, yRotationSpeed);
        currentCameraXRotation = Mathf.SmoothDamp(currentCameraXRotation, wantedCameraXRotation, ref cameraXVelocity, xCameraSpeed);
        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        myCamera.localRotation = Quaternion.Euler(currentCameraXRotation, 0, zRotation);
    }


    [HideInInspector]
    public GameObject weapon;
    private GunScript gun;

    

    float deltaTime = 0.0f;
    [Tooltip("Shows FPS in top left corner.")]
    public bool showFps = true;
    /*
    * Shows fps if its set to true.
    */
    void OnGUI()
    {

        if (showFps)
        {
            FPSCounter();
        }

    }

    void FPSCounter()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = Color.white;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }

}
