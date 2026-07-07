using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class ObjectSelector : MonoBehaviour
{
    private Camera cam;
    private PlayerInput input;
    private InputAction select;
    [SerializeField] private int ownerID; //determines the player. Makes it so it can only interact with dice of the same ownerNum.
    private const string SelectAction = "Select";
    public GameObject CurrentSelectedDie;

    private void Awake() //triggers when the object awakens.
    {
        cam = Camera.main; //sets the Camera object to the camera that has the MainCamera tag.

        input = GetComponent<PlayerInput>(); //gets the components.

        //Retrieved from the PlayerInput asset map.
        select = input.actions[SelectAction];
        
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
            if(hit.collider.CompareTag("Dice") && hit.collider.gameObject.GetComponent<SelectionManager>().ownerID == this.ownerID) //if Collider is from a Dice that it owns.
            {
                ClearSelection();
                Debug.Log("Selected");
                SelectionManager manager = hit.collider.gameObject.GetComponent<SelectionManager>();
                manager.Select();
                CurrentSelectedDie = manager.gameObject; //Set currently selected die to this.
            }
        }
    }

    private void ClearSelection()
    {
        Debug.Log("Deselected.");
        if(CurrentSelectedDie != null)
        {
            CurrentSelectedDie.GetComponent<SelectionManager>().DeSelect();
            CurrentSelectedDie = null; //Set currently selected die to null.
        }
    }
}
}
