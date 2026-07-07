using UnityEngine;
using UnityEngine.InputSystem;

public class IndicatorMovement : MonoBehaviour
{
    private Camera cam;
    private MeshRenderer render;
    

    private void Awake()
    {
        cam = Camera.main;
        render = GetComponent<MeshRenderer>();
    }

    

    void Update() // Update is called once per frame
    {
        Vector2 mousePos = Mouse.current.position.ReadValue(); //Gets the position of the mouse.
        Ray ray = cam.ScreenPointToRay(mousePos); //Create a Ray that goes from the current camera's position to the position of the mouse.

        if (Physics.Raycast(ray, out RaycastHit hit)) //If ray hits a collider.
        {
            if (hit.collider.CompareTag("Dice")) //if Collider is from a Dice.
            {
                gameObject.transform.position = hit.collider.gameObject.transform.position;
                render.enabled = true; //make indicator appear
            }
            else if(cam.TryGetComponent<ObjectSelector>(out ObjectSelector select) && select.CurrentSelectedDie != null) //If there is a Die already selected.
            {
                gameObject.transform.position = select.CurrentSelectedDie.transform.position;
                render.enabled = true; //make indicator appear.
            }
            else //If there is no selected Die.
            {
                render.enabled = false; //make indicator dissappear
            }
        }
        else if (cam.TryGetComponent<ObjectSelector>(out ObjectSelector select) && select.CurrentSelectedDie != null) //If there is a Die already selected.
        {
            gameObject.transform.position = select.CurrentSelectedDie.transform.position;
            render.enabled = true; //make indicator appear.
        }
        else //If there is no selected Die.
        {
            render.enabled = false; //make indicator dissappear
        }
    }
}
