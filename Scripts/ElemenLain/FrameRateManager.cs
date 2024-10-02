using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class AdvancedCameraController : MonoBehaviour
{
    [SerializeField] GameObject alertUI;
    public Transform player;
    public Transform[] cameraPoints;
    public float switchDistance = 10f;
    public Vector3 cameraOffset;
    public CinemachineVirtualCamera cinemachine;
    public SFXManager sfx;
    public Camera mainCamera;
    public Button backButton;
    public Button exitButton;

    private bool isPaused = false;
    private Transform currentTarget;

    [System.Obsolete]
    void Start()
    {
        if (cinemachine != null)
        {
            Debug.Log("Cinemachine successful to use");
        }
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(() => Application.Quit());
        }
        
        // Set the target frame rate to the highest possible value 
        Application.targetFrameRate = Screen.currentResolution.refreshRate;

        // Disable V-Sync to allow the frame rate to go as high as possible
        QualitySettings.vSyncCount = 0;

        if (alertUI != null)
        {
            alertUI.SetActive(false);
        }

        currentTarget = player;
    }

    void Update()
    {
        if (isPaused) return;

        UpdateCameraTarget();
        MoveCameraToTarget();
        HandleEscapeKey();
    }

    void UpdateCameraTarget()
    {
        Vector3 playerPosition = player.position;
        Transform closestPoint = null;
        float closestDistance = float.MaxValue;
        
        foreach (Transform point in cameraPoints)
        {
            float distance = Vector3.Distance(playerPosition, point.position);
            if (distance < switchDistance && distance < closestDistance)
            {
                closestPoint = point;
                closestDistance = distance;
            }
        }
        
        currentTarget = (closestPoint != null) ? closestPoint : player;
        cinemachine.Follow = currentTarget;
    }

    void MoveCameraToTarget()
    {
        Vector3 desiredPosition = currentTarget.position + cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 5f * Time.deltaTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10f);

        if (currentTarget != player)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
        }
    }

    void HandleEscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (alertUI != null)
            {            
                Time.timeScale = 0f;
                alertUI.SetActive(true);
                isPaused = true;
            }
        }
    }

    public void Back()
    {                
        sfx.buttonSfxObject.SetActive(true);
        sfx.buttonSound.Play();
        Time.timeScale = 1f;
        isPaused = false;
        alertUI.SetActive(false);
    }
}