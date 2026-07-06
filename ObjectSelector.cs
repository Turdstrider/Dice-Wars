using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class ObjectSelector : MonoBehaviour
{
    private Camera cam;
    private PlayerInput input;
    private InputAction select;
    [SerializeField] private int ownerNum; //determines the player. Makes it so it can only interact with dice of the same ownerNum.

    private void Awake() //triggers when the object awakens.
    {
        cam = Camera.main; //sets the Camera object to the camera that has the MainCamera tag.
        input = GetComponent<PlayerInput>(); //gets the components.

        //Retrieved from the PlayerInput asset map.
        select = input.actions["Select"];
        
    }

    //Enables/Disables controls base on if the object is enabled for not.
    private void OnEnable()
    {
        select.performed += OnSelect;     
    }

    private void OnDisable()
    {
        select.performed -= OnSelect;
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        float axisValue = context.ReadValue<float>(); //reads the 1D axis as a float value.

        // positive value triggers selection
        if (axisValue > 0.1f)
        {
            SelectAtMouse();
        }
        // negative value triggers de-selection
        else if(axisValue < -0.1f)
        {
            ClearSelection();
        }
          
    }

    private void SelectAtMouse()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue(); //Gets the position of the mouse.
        Ray ray = cam.ScreenPointToRay(mousePos); //Create a Ray that goes from the current camera's position to the position of the mouse.

        if(Physics.Raycast(ray, out RaycastHit hit)) //If ray hits a collider.
        {
            if(hit.collider.tag == "Dice" && hit.collider.gameObject.GetComponent<SelectionManager>().ownerNum == this.ownerNum) //if Collider is from a Dice that it owns.
            {
                ClearSelection();
                Debug.Log("Selected");
                SelectionManager manager = hit.collider.gameObject.GetComponent<SelectionManager>();
                manager.isSelected = true;
            }
        }
    }

    private void ClearSelection()
    {
        Debug.Log("Deselected.");
        GameObject[] dice = GameObject.FindGameObjectsWithTag("Dice"); //finds every Dice on the scene.
        foreach (GameObject die in dice)
        {
            if( die.GetComponent<SelectionManager>().ownerNum == this.ownerNum) //only interacts with dice it owns. 
            {
                die.GetComponent<SelectionManager>().isSelected = false;
            }
        }
    }
}
}
