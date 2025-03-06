using UnityEditor.Callbacks;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    private bool _isAired = false; // Is the player in air
    private bool _isGrounded = false; // Is the player on the ground
    private bool _hasLaunched = false; // have player launched from slingshot
    private Rigidbody2D _rb; 
    private GameObject trailInstance; // The Trail instance, if enabled
    private AnimationManager _animationManager; // Used for playing animations with animator

    //Sliding
    private bool _isSliding = false; // is player currently sliding
    private BoxCollider2D _collider; // Default collider for player
    private Vector2 _originalColliderSize; // Original collider settings, for resetting after slide
    private Vector2 _originalColliderOffset; 
    public int Total_Coins = 0;


    [Header("Movement Settings")]
    [Space()]

    public float moveSpeed = 5f; // current movement speed, change maxSpeed instead.

    [Tooltip("The minimum speed, when stamina is lowest")]
    public float minSpeed = 0.5f;

    [Tooltip("Max speed of player, when stamina is max")]
    public float maxSpeed = 10f;


    [Header("Stamina Settings")] 
    public float maxStamina = 100f;
    public float stamina = 100f;

    [Tooltip("The drain rate of the samina, reduced every tick when moving")]
    public float staminaDrainRate = 5f;


    [Header("Jump Settings")]

    [Tooltip("How much force to add on jump")]
    public float jumpForce = 10f;

    [Tooltip("Check to allow jumping in even when in air")]
    public bool allowJumpInAir = false;

    [Tooltip("Default gravity, applied AFTER the player hits the ground")]
    public float playerGravity = 1f; // To change first gravity when launched , check slingshot event in inspector 


    [Header("Slide Settings")]
    public float slideSpeedMultiplier = 1.5f;

    [Tooltip("How long to slide")]
    public float slideDuration = 1f;

    [Tooltip("The size of the collider during sliding")]    
    public Vector2 slideColliderSize = new Vector2(9.31f, 5.03f);

    [Tooltip("The offset for the collider, change to make it look smoother")]
    public Vector2 slideColliderOffset = new Vector2(0.2639856f, -0.1319928f); // Pause game and edit in scene view for best results

    
    [Header("Visual Effects")]
    [Space()]

    [Tooltip("Show the trail when player is in air aswell")]
    public bool showTrailInAir = false;
    [Tooltip("The prefab for the trial effect used when player is on the ground")]
    public GameObject groundTrailPrefab; // If left empty, script will skip

    [Tooltip("The Anchor point where to spawn the trial effect")]
    public Transform groundTrailAnchorPoint;

    [Tooltip("The prefab for when player hits the ground")]
    public GameObject groundHitEffectPrefab; // If left empty, script will skip

    [Tooltip("The ground hit effect prefab anchor point")]
    public Transform groundHitEffectAnchorPoint;

    [Header("Layers")]
    [Space()]
    [Tooltip("The ground layer to check if grounded")]
    public LayerMask groundLayer; // Layer of which the player moves on top of

    [Header("Events")]
    // Events available in the Inspector, for ease of use.
    // Can for example simply add the player object to the event IsMoving and Set Animation using AnimationManager directly in inspector.

    public UnityEvent OnCollideWithEnemy;
    public UnityEvent IsGrounded;
    public UnityEvent IsMoving;
    public UnityEvent OnJump;
    public UnityEvent IsFalling;
    public UnityEvent OnSlide;
    public UnityEvent OnStopSlide;

    public UnityEvent OnStartUp;

    // Start is called before the first frame update
    void Start()
    {

        // Trail Effect
        if(groundTrailPrefab != null){
        trailInstance = Instantiate(groundTrailPrefab, transform);
        trailInstance.transform.localPosition = groundTrailAnchorPoint.transform.localPosition;
        }

        // Ground Hit Effect
        if(groundHitEffectPrefab != null && groundHitEffectAnchorPoint == null){
            groundHitEffectAnchorPoint = transform;
        }

        // Setup Rigid Body, Animation Manager
        if(_rb == null) { _rb = GetComponent<Rigidbody2D>(); }
        if(_animationManager == null) 
        {
             _animationManager = GetComponent<AnimationManager>();
             }
        OnStartUp?.Invoke(); // Invoke Event On Start in inspector

        // Cache collider for slide and unslide
        _collider = GetComponent<BoxCollider2D>();
        if(_collider != null){
            // cache original values
            _originalColliderSize = _collider.size;
            _originalColliderOffset = _collider.offset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded(); // Handles checking if player is on the ground, also checks if player is falling
        UpdateGroundTrailEffect(); // Handles trail effect
        MoveForward(); // Handles movement when grounded
        HandleStamina(); // Handles all the stamina for the player
        HandleSlide(); // Handles the sliding mechanics


        // Trail instance update position
        if (trailInstance != null){ trailInstance.transform.position = groundTrailAnchorPoint.transform.position; }
        // Jump Function
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")){
            Jump(jumpForce);
        }
    }

    /// <summary>
    /// Sets the gravity of the players rigidbody
    /// </summary>
    /// <param name="gravity">Float value</param>
    public void SetGravity(float gravity)
    {
        playerGravity = gravity;
        _rb.gravityScale = playerGravity;
    }

    /// <summary>
    /// Sets the player isGrounded state
    /// </summary>
    /// <param name="isGrounded">True or False</param>
    public void SetGrounded(bool isGrounded){
        _isGrounded = isGrounded;
    }

    /// <summary>
    /// Sets the player isAir state
    /// </summary>
    /// <param name="isAired">True or False</param>
    public void SetAired(bool isAired){
        _isAired = isAired;
    }

    /// <summary>
    /// Handles the trail effect, if showTrailInAir is true, it skips this
    /// </summary>
    private void UpdateGroundTrailEffect()
    {
    
    if(groundTrailPrefab == null) { return ; }

    // Check if grounded and activate/deactivate the trail
    if (_isGrounded && !trailInstance.activeSelf && showTrailInAir == false) 
    {   
        trailInstance.SetActive(true); 
    }
    else if (!_isGrounded && trailInstance.activeSelf && showTrailInAir == false) 
    {
        trailInstance.SetActive(false);
    }

    }

    /// <summary>
    /// Checks if player is grounded using raycast, 
    // aswell as if the player is falling, invokes the events for 
    // easy handling like playing animations.
    /// </summary>
    private void CheckIfGrounded()
    {
        float rayLength = 0.6f;
        // Cast ray from player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);

        if(hit.collider != null){
            IsGrounded?.Invoke(); // Invoke for Unity Event isGrounded
            if(_isGrounded == false && groundHitEffectPrefab != null) { 
                // Spawn ground hit effect
                Instantiate(groundHitEffectPrefab, groundHitEffectAnchorPoint.position , groundHitEffectAnchorPoint.rotation);
                }

            // Sets the gravity after hitting the ground, to change launch gravity check Slingshot even in inspector    
            if(_hasLaunched == false && _isGrounded == true) 
            { 
                SetLaunched(true); // First hit on the ground we set have launched to true
                _animationManager.SetAnimationTrigger("DropOnGround");
                Debug.Log("Player Landed first time");
            }  

                // Set State
            SetGrounded(true);
            SetAired(false);
        } else {

            // Check if falling and invoke event
            if(_hasLaunched == true && _rb.linearVelocity.y < -0.1f){
                IsFalling?.Invoke();
            }

            SetGrounded(false);
            SetAired(true);
        }

        // Gravity Check after launch
        if(_hasLaunched == true &&_rb.gravityScale != playerGravity){ SetGravity(playerGravity); }
    }

    /// <summary>
    /// The Jump Function, will add force to players rigidbody based on values
    /// </summary>
    /// <param name="jumpForce">Float value</param>
    public void Jump(float jumpForce)
    {
        if(allowJumpInAir == false) {
        if(_isGrounded){
            // If grounded, jump
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Jump 
            SetGrounded(false);
            SetAired(true);
            OnJump?.Invoke(); // Invokes On Jump in inspector
        } 
        } else {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            SetGrounded(false);
            SetAired(true);
            OnJump?.Invoke(); 
        }
    }

    /// <summary>
    /// Moves the player forward and invokes respective event
    /// </summary>
    public void MoveForward()
    {
        if(_isGrounded){
        _rb.linearVelocity = new Vector2(moveSpeed, _rb.linearVelocity.y);
        IsMoving?.Invoke(); // Event: Is Moving
        }
    }

    /// <summary>
    /// Handles the stamina/energy, movement speed is based
    /// on stamina value etc.
    /// </summary>
    private void HandleStamina()
    {
        if( stamina > maxStamina) { stamina = maxStamina; } // Set stamina to max if exceed max
        if(_isGrounded && stamina > 0){
            stamina -= staminaDrainRate * Time.deltaTime; // Reduce stamina over time
            moveSpeed = Mathf.Lerp(minSpeed, maxSpeed, stamina / 100f); // Smooth movment using lerp
        }
        else if (stamina <= 0)
        {
            moveSpeed = minSpeed; // When no stamina, move at slowest speed
        }
    }

    /// <summary>
    /// Handles the player sliding mechanic
    /// </summary>
    private void HandleSlide()
    {
        if ( Input.GetKeyDown(KeyCode.LeftControl))
        {
            Slide();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl)){
            StopSlide();
        }
    }

    /// <summary>
    /// Sets sliding state
    /// </summary>
    /// <param name="isSliding">True or False</param>
    public void SetSliding(bool isSliding){
        _isSliding = isSliding;
    }

    /// <summary>
    /// Slide function, if grounded will slide and invoke method on slide.
    /// Changes the collider size and offset, temporary
    /// </summary>
    public void Slide()
    {
        if (_isGrounded && !_isSliding)
        {
            SetSliding(true);
            moveSpeed *= slideSpeedMultiplier;
            _collider.size = slideColliderSize;
            _collider.offset = slideColliderOffset;
            OnSlide?.Invoke(); // invoke method On Slide

            // Stop sliding after delay
            Invoke(nameof(StopSlide), slideDuration);
        }
    }

    /// <summary>
    /// Stops the slide function and resets the collider values to default
    /// </summary>
    public void StopSlide()
    {
        if(_isSliding)
        {
            OnStopSlide?.Invoke();
            SetSliding(false);
            moveSpeed /= slideSpeedMultiplier;
            _collider.size = _originalColliderSize;
            _collider.offset = _originalColliderOffset;
        }
    }

    /// <summary>
    /// Removes amount of stamina
    /// </summary>
    /// <param name="amount">float value</param>
    public void RemoveStamina(float amount)
    {
        stamina -= amount;
    }

    /// <summary>
    /// Adds to stamina
    /// </summary>
    /// <param name="amount">float value</param>
    public void AddStamina(float amount)
    {
        stamina += amount;
    }

    /// <summary>
    /// Simply sets the private variable so we can check if player has launched
    /// from slingshot. Not really used more than from UnityEvent in slingshot.cs
    /// </summary>
    /// <param name="hasLaunched">True or False : Leave empty for True</param>
    public void SetLaunched(bool hasLaunched = true)
    {
        _hasLaunched = hasLaunched;
    }

    /// <summary>
    /// For debugging only
    /// </summary>
    private void OnDrawGizmoz()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")){
            OnCollideWithEnemy?.Invoke();
        }
    }


}
