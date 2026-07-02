using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class ObjectSelector : MonoBehaviour
{
    private Camera cam;
    private PlayerInput input;
    private InputAction select;

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
        float AxisValue = context.ReadValue<float>(); //reads the 1D axis as a float value.

        // positive value triggers selection
        if (AxisValue > 0.1f)
        {
            SelectAtMouse();
        }
        // negative value triggers de-selection
        else if(AxisValue < -0.1f)
        {
            ClearSelection();
        }
          
    }

    private void SelectAtMouse()
    {
        Vector2 MousePos = Mouse.current.position.ReadValue(); //Gets the position of the mouse.
        Ray ray = cam.ScreenPointToRay(MousePos); //Create a Ray that goes from the current camera's position to the position of the mouse.

        if(Physics.Raycast(ray, out RaycastHit hit)) //If ray hits a collider.
        {
            Debug.Log($"Hit {hit.collider.name}");
        }
    }

    private void ClearSelection()
    {
        Debug.Log("Deselected.");
    }
}
