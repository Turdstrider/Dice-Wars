using UnityEngine;
using UnityEngine.InputSystem;

public class IndicatorManager: MonoBehaviour
{
    private Camera cam;
    [SerializeField] private GameObject hoverIndicator;
    [SerializeField] private GameObject selectIndicator;
    private GameObject hover;
    private GameObject select;
    private MeshRenderer hoverMesh;
    private Vector3 raycastPoint; //Stores vector3 of raycast hit.
    private bool isRaycastHit; 

    private void Awake()
    {
        cam = Camera.main;
        hover = Instantiate(hoverIndicator, transform.position, Quaternion.Euler(90, 0, 0)); //Creates a hover indicator. It only instantiates it once.
        hoverMesh = hover.GetComponent<MeshRenderer>(); //Gets the meshrenderer of the hover indicator.
        select = null; //This stays null until a Die is selected. The select indicator is instantiated when a Die is selected.
    }

    public BasicRollManager CreateSelectIndicator() //Is called by the ObjectSelector script when a Die is selected.
    {
        if(select != null)
        {
           Destroy(select); //Destroys the previous select indicator if there is one.
        }
        if (cam.TryGetComponent<ObjectSelector>(out ObjectSelector objectSelector) && objectSelector.currentSelectedDie != null) //If there is a Die already selected.
        {
           select = Instantiate(selectIndicator, objectSelector.currentSelectedDie.transform.position, Quaternion.identity * Quaternion.Euler(90, 0, 0)); //Creates a select indicator on the selected Die.
        }
        return select.GetComponent<BasicRollManager>(); //Returns the BasicRollManager of the selected Die.
    }

    public void DestroySelectIndicator() //Is called by the ObjectSelector script when a Die is deselected.
    {
        if(select != null)
        {
            Destroy(select); //Destroys the select indicator.
        }
    }

    public Vector3 GetRaycast()
    {
        return raycastPoint;
    }

    public bool GetisRaycastHit()
    {
        return isRaycastHit;
    }

    void Update() // Update is called once per frame
    {
        Vector2 mousePos = Mouse.current.position.ReadValue(); //Gets the position of the mouse.
        Ray ray = cam.ScreenPointToRay(mousePos); //Create a Ray that goes from the current camera's position to the position of the mouse.
        

        if (Physics.Raycast(ray, out RaycastHit hit)) //If ray hits a collider.
        {
            if (hit.collider.CompareTag("Dice")) //if Collider is from a Dice.
            {
                hover.transform.position = hit.collider.transform.position;
                hoverMesh.enabled = true;
                //make indicator appear on the Dice.
            }
            else //If there is no selected Die.
            {
                hoverMesh.enabled = false;
            }
            raycastPoint = hit.point; //store point of collision.
            isRaycastHit = true;
        }
        else
        {
            isRaycastHit = false;
        }
    }
}
