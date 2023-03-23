using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("values")]
    public Vector2 facingDir;
    public Vector2 moveInput;
    public float moveSpeed;
    public bool interactInput;

    [Header("componets")]
    public Rigidbody2D rig;
    public SpriteRenderer spriteR;
    public LayerMask layerMask;

    [Header("debug")]
    public bool debug;




    // Called when object comes into game
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        spriteR = GetComponentInChildren<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void FixedUpdate()
    {
        rig.velocity = moveInput.normalized * moveSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        if(moveInput.magnitude !=0.0f)
        {
            facingDir = moveInput.normalized;
            spriteR.flipX = (moveInput.x == 0) ? spriteR.flipX : moveInput.x > 0;
        }

        if (interactInput)
        {
            tryInteractTile();
            interactInput = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    public void onMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void onInteractInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            interactInput = true;
        }
        
    }
    private void tryInteractTile()
    {
        if(debug)
        {
            print("Player Inreracted with tile");
        }
    }
}
