using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameObject player;                                  // referinta la GameObject-ul caruia ii este atasat acest script

    [SerializeField]
    private GameObject playerCamera;                            // referinta la GameObject-ul care contine camera
    CharacterController controller;                     // referinta la componenta ChracterController atasata GameObject-ului Player

    /* PlayerMovement */
    public float movementSpeed = 10.0f;                 // viteza de deplasare
    public float runningSpeed = 15.0f;                  // viteza de alergare
    public float currentSpeed;                          // viteza la un moment dat - se utilizeaza pentru tranzitia intre mers si alergare
    public float jumpHeigth = 2.0f;                     // inaltimea saltului

    public float gravity = -9.81f;                      // constanta gravitationala - simulam forta gravitationala

    public Transform groundCheck;                       // referinta la un GameObject care este folosit pentru a verifica daca jucatorul atinge solul
    public float groundDistance = 0.4f;                 // distanta de la player la sol - utilizata in verificarea daca jucatorul atinge solul
    public LayerMask groundMask;                        // masca care contine toate obiectele care sunt considerate sol
    bool isGrounded = true;                             // fanion care indica daca jucatorul atinge solul

    Vector3 velocity;                                   // vectorul de viteza - utilizat pentru implementarea saltului si a simula forta gravitationala

    /* Camera */
    public float cameraVerticalRotation = 0.0f;         // rotatia pe axa verticala a camerei
    public float verticalRotationLimit = 60.0f;         // limita de rotatie pe axa verticala a camerei
    public float cameraSensitivity = 100f;              // sensibilitatea camerei

    bool movementEnabled = true;                        // fanion care indica daca jucatorul poate sa se miste - util in momentul in care jucatorul interactioneaza cu un obiect

    void Start()
    {
        player = gameObject;
        //playerCamera = player.transform.Find("PlayerCamera").gameObject;
        controller = player.GetComponent<CharacterController>();

        currentSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!movementEnabled) return;
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2.0f;

        playerMovement();
        playerRotation();        
    }
    void playerMovement()
    {
        if (!movementEnabled) return;

        // sprint/run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = Mathf.Clamp(currentSpeed + .1f, movementSpeed, runningSpeed);
        }
        else
        {
            currentSpeed = Mathf.Clamp(currentSpeed - .1f, movementSpeed, runningSpeed);
        }        

        // movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = player.transform.right * moveHorizontal + player.transform.forward * moveVertical;
        controller.Move(movement * currentSpeed * Time.deltaTime);
        

        // jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            velocity.y = Mathf.Sqrt(jumpHeigth * -2.0f * gravity);
        }

        // gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void playerRotation()
    {
        if (!movementEnabled) return;

        // miscarea verticala a mouse-ului roteste camera pe axa verticala, intre limitele specificate
        float mouseVertical = Input.GetAxisRaw("Mouse Y") * cameraSensitivity * Time.deltaTime;
        float newRotation = cameraVerticalRotation - mouseVertical;
        cameraVerticalRotation = Mathf.Clamp(newRotation, -verticalRotationLimit, verticalRotationLimit);
        playerCamera.transform.localEulerAngles = new Vector3(cameraVerticalRotation, 0, 0);

        // miscarea orizontala a mouse-ului roteste player-ul pe axa orizontala
        float mouseHorizontal = Input.GetAxisRaw("Mouse X") * cameraSensitivity * Time.deltaTime;
        player.transform.Rotate(Vector3.up * mouseHorizontal, Space.World);
    }

    public void switchMovement()
    {
        movementEnabled = !movementEnabled;
        if (movementEnabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
