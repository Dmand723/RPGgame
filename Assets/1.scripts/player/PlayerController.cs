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
    public LayerMask interactLayer;
    public Transform muzzlePos;
    public Muzzle muzzle;
    private SpriteRenderer muzzleSprite;

    [Header("debug")]
    public bool debug;




    // Called when object comes into game
    private void Awake()
    {
        muzzle = GetComponentInChildren<Muzzle>();
        muzzlePos = muzzle.transform;
        muzzleSprite = muzzle.getMuzzleSprite();
        muzzleSprite.enabled = false;
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
            muzzleSprite.enabled = true;
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            interactInput = true;
            muzzleSprite.enabled = false;
        }
        
    }
    private void tryInteractTile()
    {
        Collider2D hit = muzzle.GetComponent<Collider2D>();
        //RaycastHit2D hit = Physics2D.Raycast(((Vector2)transform.position+facingDir)-new Vector2(0,0.25f), new Vector3(1,0,0)*facingDir,.1f, interactLayer);
        
        
            if(hit.gameObject.CompareTag("Interact_FieldTile"))
            {
               FieldTile fieldTile = hit.gameObject.GetComponent<FieldTile>();
                fieldTile.interact();
            }


            if (debug)
            {
                print("Player Interacted with tile");
                
            }
        
    }
}
