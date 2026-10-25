using UnityEngine;

public class BasicRollManager : MonoBehaviour
{
    private Camera cam;
    private ObjectSelector selector;

    void Awake()
    {
        cam = Camera.main;
        selector = Camera.main.GetComponent<ObjectSelector>();
    }

    public void RollDice(Vector3 targetPos) //Placeholder function.
    {
        Rigidbody rb = selector.currentSelectedDie.GetComponent<Rigidbody>(); //get the rigidbody of the selected die.

        Vector3 direction = (targetPos - rb.position).normalized; //calculate the direction from the die to the target position.

        rb.AddForce(direction * 10f, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 10f, ForceMode.Impulse); //adds a random torque to the die to make it roll.
    }

    private void Update()
    {
        transform.position = selector.currentSelectedDie.transform.position; //makes the indicator follow the selected die.
    }
}
