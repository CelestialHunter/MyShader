using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject playerCamera;

    /* Interaction */
    public float minInteractDistance = 3.0f;
    Interactable currentGazeTarget;

    bool interactionEnabled = true;

    void Start()
    {
        //playerCamera = transform.Find("PlayerCamera").gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!interactionEnabled) return;
        look();
        interact();
        minimap();
        pauseGame();
    }


    void look()
    {
        RaycastHit target;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out target, 100) && target.distance <= minInteractDistance)
        {
            Interactable targetObj = target.collider.gameObject.GetComponent<Interactable>();
            if (targetObj != null && targetObj != currentGazeTarget)
            {
                if (currentGazeTarget != null) currentGazeTarget.unhighlight();
                currentGazeTarget = targetObj;
                currentGazeTarget.highlight();
                //ui.showMessage(targetObj.interactMessage);
            }
        }
        else
        {
            if (currentGazeTarget != null) currentGazeTarget.unhighlight();
            currentGazeTarget = null;
            //ui.hideMessages();
        }
    }

    void interact()
    {
        // daca jucatorul apasa tasta de interactiune in timp ce se uita la un obiect interactabil, lanseaza actiunea

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentGazeTarget != null)
            {
                currentGazeTarget.interact();
            }
        }
    }

    public void switchInteraction()
    {
        interactionEnabled = !interactionEnabled;
    }

    void minimap()
    {
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Tab))
        {
            //ui.toggleMinimap();
        }
    }

    void pauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //ui.enablePause();
        }
    }
}
