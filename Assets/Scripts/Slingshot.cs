using UnityEngine;
using UnityEngine.Events;

public class Slingshot : MonoBehaviour
{
    private Vector2 startPosition;
    private bool isDragging = false; 
    private bool hasLaunched = false;

    [Header("Movement & Velocity")]
    [Space()]

    [Tooltip("Default is inverse, which is the opposite direction.")]
    public bool inverseVelocity = false;

    [Tooltip("The max dragging distance between player and slingshot")]
    public float maxDragDistance = 3f;

    [Tooltip("The Launch velocity, which also counts in the amount user is dragging back")]
    public float playerVelocity = 5f;

    [Tooltip("The start position of the slingshot, where user will click to drag")]
    public Transform anchorPoint; 

    [Tooltip("The player object, needs the Player.cs script attached")]
    public Player playerObject;

    [Header("Mode Settings")]
    [Space()]

    [Tooltip("Choose between Normal (free drag) and Slingshot (left-only) modes")]
    public SlingshotMode mode = SlingshotMode.Normal;

    [Tooltip("Enable tension to make dragging harder as distance increases")]
    public bool useTension = false;

    [Header("Visual Effects")]
    [Space()]

    [Tooltip("The prefab of the launch effect")]
    public GameObject launchEffectPrefab;

    [Tooltip("The launch effect spawn point (Anchor point)")]
    public Transform launchEffectSpawnPoint;

    [Header("Debugging")]
    [Space()]

    [Tooltip("Whether to debug the line between player and anchor point")]
    public bool debugAnchor = false;
    private LineRenderer slingshotDebugger;

    [Header("Events")]
    public UnityEvent onLaunchPlayer;
    public UnityEvent onStart;

    // Enum for mode selection
    public enum SlingshotMode
    {
        Normal,    // Old behavior: drag in any direction
        Slingshot  // New behavior: drag left only
    }

    void Start()
    {
        if(anchorPoint == null){
            anchorPoint = gameObject.transform;
        }

        if(debugAnchor){
            slingshotDebugger = setupDebugger();
        }

        if(playerObject == null){
            playerObject = GameObject.Find("Player").GetComponent<Player>();
        }

        onStart?.Invoke();
    }

    void Update()
    {
        if(hasLaunched){
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = getMousePos();
            if(Vector2.Distance(mousePosition, anchorPoint.position) < 0.5f)
            {
                isDragging = true;
                startPosition = anchorPoint.position;
            }
        }

        if(isDragging)
        {
            Vector2 mousePosition = getMousePos();
            Vector2 direction = getDirection(mousePosition);

            // Calculate distance and optionally apply tension
            float distance = direction.magnitude;
            if(distance > maxDragDistance)
            {
                distance = maxDragDistance;
                direction = direction.normalized * maxDragDistance; // Clamp direction
            }

            if(useTension)
            {
                float tensionFactor = 1 - (distance / maxDragDistance); // 1 at anchor, 0 at max
                direction *= tensionFactor;
            }

            // Calculate new position and apply mode-specific restrictions
            Vector2 newPosition = startPosition + direction;
            if(mode == SlingshotMode.Slingshot && newPosition.x > startPosition.x)
            {
                newPosition.x = startPosition.x;
                direction = newPosition - startPosition; // Update direction for launch
            }

            playerObject.transform.position = newPosition;
            handleDebugging(mousePosition);

            if(Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                Launch(direction);
                handleDebugging(mousePosition);
            }
        }
    }

    protected void Launch(Vector2 direction)
    {
        Rigidbody2D rb = playerObject.GetComponent<Rigidbody2D>();
        if(inverseVelocity){
            rb.linearVelocity = -direction * playerVelocity * direction.magnitude;
        } else {
            rb.linearVelocity = direction * playerVelocity * direction.magnitude;
        }

        if(launchEffectPrefab != null){
            Transform _launchEffectSpawnPoint = launchEffectSpawnPoint ?? playerObject.transform;
            Instantiate(launchEffectPrefab, _launchEffectSpawnPoint.position, _launchEffectSpawnPoint.rotation);
        }
        hasLaunched = true;
        onLaunchPlayer?.Invoke();
    }

    private Vector2 getMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private Vector2 getDirection(Vector2 mousePosition)
    {
        return mousePosition - startPosition;
    }

    private LineRenderer setupDebugger()
    {
        LineRenderer lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.green;
        return lineRenderer;
    }

    private void handleDebugging(Vector2 mousePosition)
    {
        if(!debugAnchor) return;

        if(isDragging)
        {
            if(slingshotDebugger.positionCount == 0) slingshotDebugger.positionCount = 2;
            slingshotDebugger.SetPosition(0, mousePosition);
            slingshotDebugger.SetPosition(1, anchorPoint.transform.position);
        }
        else
        {
            if(slingshotDebugger.positionCount == 2)
            {
                slingshotDebugger.positionCount = 0;
                Debug.Log("Launched Player Object");
            }
        }
    }
}