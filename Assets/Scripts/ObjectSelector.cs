using UnityEngine;
using UnityEngine.InputSystem;
public class ObjectSelector : MonoBehaviour
{
    private Camera cam;
    private PlayerInput input;
    private InputActionMap controls;
    private InputAction select;
    private InputAction scroll;
    [SerializeField] private int ownerID; //determines the player. Makes it so it can only interact with dice of the same ownerNum.
    private const string SelectAction = "Select";
    private const string ScrollAction = "Scroll";
    private IndicatorManager indicatorManager;
    public GameObject currentSelectedDie;
    public BasicRollManager currentRollManager;

    private void Awake() //triggers when the object awakens.
    {
        cam = Camera.main; //sets the Camera object to the camera that has the MainCamera tag.

        input = GetComponent<PlayerInput>();
       
        controls = input.actions.FindActionMap("Controls");
       
        select = controls.FindAction("Select");
        scroll = controls.FindAction("Scroll");

        indicatorManager = FindFirstObjectByType<IndicatorManager>();
        
    }

    //Enables/Disables controls based on if the object is enabled for not.
    private void OnEnable()
    {
        if (select != null)
            select.performed += OnSelect;
        
        if (scroll != null)
            scroll.performed += OnScroll;
    }


    private void OnDisable()
    {  
        if (select != null)
            select.performed += OnSelect;
        
        if (scroll != null)
            scroll.performed += OnScroll;
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

    private void OnScroll(InputAction.CallbackContext context)
    {
        float scrollValue = context.ReadValue<float>();

        if (currentRollManager != null)
            currentRollManager.AdjustElevation(scrollValue);
    }

    private void SelectAtMouse()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue(); //Gets the position of the mouse.
        Ray ray = cam.ScreenPointToRay(mousePos); //Create a Ray that goes from the current camera's position to the position of the mouse.

        

        if (Physics.Raycast(ray, out RaycastHit hit)) //If ray hits a collider.
        {
            if (currentRollManager != null) //If there is a currently selected die.
                currentRollManager.RollDice(hit.point); //Rolls the currently selected die.

            if (hit.collider.CompareTag("Dice") && hit.collider.gameObject.GetComponent<SelectionManager>().ownerID == this.ownerID) //if Collider is from a Dice that it owns.
            {
                if(currentSelectedDie != null)
                    currentSelectedDie.GetComponent<SelectionManager>().DeSelect(); //Deselects the currently selected die if there is one.
               
                Debug.Log("Selected");
                SelectionManager manager = hit.collider.gameObject.GetComponent<SelectionManager>();
                manager.Select();
                currentSelectedDie = manager.gameObject; //Set currently selected die to this.
                currentRollManager = indicatorManager.CreateSelectIndicator(); //Creates a select indicator on the selected Die.
            }
        }
    }

    private void ClearSelection()
    {
        Debug.Log("Deselected.");
        if(currentSelectedDie != null)
        {
            currentSelectedDie.GetComponent<SelectionManager>().DeSelect();
            currentSelectedDie = null; //Set currently selected die to null.
            indicatorManager.DestroySelectIndicator(); //Destroys the select indicator.
            currentRollManager = null; //Set currentRollManager to null.
        }
    }
}
