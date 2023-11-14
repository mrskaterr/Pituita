using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    public bool canSneak = false;
    
    [SerializeField] LocalCameraHandler cameraHandler;

    private Vector2 moveInput = Vector2.zero;
    private Vector2 viewInput = Vector2.zero;
    private bool jumpInput = false;
    private bool fireInput = false;//TODO: interact
    private float speedStep = 0;
    private bool sneakyInput = false;
    private bool dashInput= false;
    private bool sprintInput=false;
    private Vector3 sneakRot = Vector3.zero;

    private CharacterMovementHandler characterMovementHandler;
    private void Awake()
    {
        characterMovementHandler = GetComponent<CharacterMovementHandler>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//TOIMPROVE: Utils
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!characterMovementHandler.Object.HasInputAuthority) { return; }

        viewInput.x = Input.GetAxis("Mouse X");
        viewInput.y = Input.GetAxis("Mouse Y") * -1;

        moveInput.x = Input.GetAxis("Horizontal");//TODO: new input system
        moveInput.y = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
        }

        if (Input.GetButton("Fire1"))
        {
            fireInput = true;
        }

        if (Input.GetButtonDown("Fire2") && canSneak)
        {
            sneakRot = cameraHandler.transform.forward;
            sneakyInput = true;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            sneakyInput = false;
        }

        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            dashInput=true;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            Debug.Log("Sprint");
		    sprintInput=true;
	    }
        if(Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            Debug.Log("Sprint");
		    sprintInput=false;
	    }

        moveInput.y += speedStep;

        cameraHandler.SetViewInput(viewInput);
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        if (sneakyInput)
        {
            networkInputData.aimForwardVector = sneakRot;
        }
        else
        {
            networkInputData.aimForwardVector = cameraHandler.transform.forward;
        }

        networkInputData.movementInput = moveInput;

        networkInputData.isJumpPressed = jumpInput;

        networkInputData.isFirePressed = fireInput;

        networkInputData.isDashPressed = dashInput;

        networkInputData.isSprintPressed = sprintInput;
        jumpInput = false;
        fireInput = false;
        dashInput = false;
        return networkInputData;
    }
}