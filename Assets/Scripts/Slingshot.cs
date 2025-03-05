using UnityEditor;
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

    [Range(1f,5f)]
    [Tooltip("The max dragging distance between player and slingshot")]
    public float maxDragDistance = 3f;

    [Tooltip("The Launch velocity, which also counts in the amount user is dragging back")]
    public float playerVelocity = 5f;

    [Tooltip("The start position of the slingshot, where user will click to drag")]
    public Transform anchorPoint; 

    [Tooltip("The player object, needs the Player.cs script attached")]
    public Player playerObject;

    [Header("Visual Effects")]
    [Space()]

    [Tooltip("The prefab of the launch effect")]
    public GameObject launchEffectPrefab;

    [Tooltip("The launch effect spawn point(Anchor point)")]
    public Transform launchEffectSpawnPoint;

    [Header("Debugging")]
    [Space()]

    [Tooltip("Wether to debug the line between player and anchor point")]
    public bool debugAnchor = false;
    private LineRenderer slingshotDebugger;

    [Header("Events")]
    public UnityEvent onLaunchPlayer;

 




    // Start is called before the first frame update
    void Start()
    {

        // If no anchor point is added, use this gameobject
        if(anchorPoint == null){
            anchorPoint = gameObject.transform;
        }

        // If debug is enabled, setup the debugger
        if(debugAnchor){
            slingshotDebugger = setupDebugger();
        }

        if(playerObject == null){
            playerObject = GameObject.Find("Player").GetComponent<Player>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        // Has launched already, return.
        if(hasLaunched == true){
            return;
        }

        // Is mouse button currently down
        if( Input.GetMouseButtonDown(0))
        {

            Vector2 mousePosition = getMousePos();
            if (Vector2.Distance(mousePosition, anchorPoint.position) < 0.5f) // If the mouse click is futher away than 0.5f from anchor point
            {
                isDragging = true;
                startPosition = anchorPoint.position;
            }
        }

        // Is dragging the player
        if(isDragging)
        {
           Vector2 mousePosition = getMousePos();
           Vector2 direction = getDirection(mousePosition);
           handleDebugging(mousePosition);


           // Limit the distance
           if (direction.magnitude > maxDragDistance)
           {
             direction = direction.normalized * maxDragDistance;
           }    

           playerObject.transform.position = startPosition + direction;

           if(Input.GetMouseButtonUp(0)){
            isDragging = false;
            Launch(direction);
            handleDebugging(mousePosition);
           }
        }
    }


    /// <summary>
    /// Launches the player object, if inverse it launches in the opposite direction.
    /// </summary>
    /// <param name="direction">The direction to launch the player</param>
    
    protected void Launch(Vector2 direction)
    {
        Rigidbody2D rb = playerObject.GetComponent<Rigidbody2D>();
        if(inverseVelocity){
        rb.velocity = -direction * playerVelocity * direction.magnitude;
        } else {
        rb.velocity = direction * playerVelocity * direction.magnitude;
        }

        // Spawn a launch effect prefab if it exists
        if(launchEffectPrefab != null){
            Transform _launchEffectSpawnPoint = playerObject.transform;
            if(launchEffectSpawnPoint != null) { _launchEffectSpawnPoint = launchEffectSpawnPoint; }
        Instantiate(launchEffectPrefab, _launchEffectSpawnPoint.position , _launchEffectSpawnPoint.rotation);
        }
        hasLaunched = true;
        onLaunchPlayer?.Invoke();

    }


    /// <summary>
    /// Gets the current mouse position.
    /// </summary>
    /// <returns>The current mouse position as Vector2</returns>
    
    private Vector2 getMousePos(){
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    /// <summary>
    /// Gets the current direction
    /// </summary>
    /// <param name="mousePosition">The current mouse position.</param>
    /// <returns>Direction as Vector2</returns>
    
    private Vector2 getDirection(Vector2 mousePosition){
        return mousePosition - startPosition;
    }


    /// <summary>
    /// Used to setup linerenderer to render the debug line
    /// </summary>
    /// <returns>LineRenderer object</returns>
    
    private LineRenderer setupDebugger(){

        LineRenderer lineRenderer = GetComponentInChildren<LineRenderer>();

        // Line Renderer settings
        lineRenderer.positionCount = 2; 
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); 
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.green;

        return lineRenderer;
    }


    /// <summary>
    /// Handles the debugging line, saves resources by looping over and over again.
    /// </summary>
    /// <param name="mousePosition"></param>
    
    private void handleDebugging(Vector2 mousePosition){
        if(!debugAnchor){ return; } // If debug is off, return.

        if(isDragging){
             // Update LineRenderer positions
             if(slingshotDebugger.positionCount == 0){ slingshotDebugger.positionCount = 2; }

             slingshotDebugger.SetPosition(0, mousePosition);
             slingshotDebugger.SetPosition(1, anchorPoint.transform.position);
             
        } else {
            if(slingshotDebugger.positionCount == 2){ 
                slingshotDebugger.positionCount = 0; 
                Debug.Log("Launched Player Object");
            }
        }
    }
}
