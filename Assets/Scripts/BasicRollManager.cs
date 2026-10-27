using UnityEngine;
using Random = UnityEngine.Random;

public class BasicRollManager : MonoBehaviour
{
    private Camera cam;
    private ObjectSelector selector;

    [SerializeField] float elevation = 30f; // Degrees of elevation; controlled by player input.
    [SerializeField] private float minPower = 2f; //Minimum power of roll
    [SerializeField] private float maxPower = 10f; //Maximum power of roll
    [SerializeField] private float maxDistance = 10f; //distance of raycast for max power.

    private const float ElevationStep = 1f; //amount of degrees per scroll notch.
    private const float MinElevation = 5f; 
    private const float MaxElevation = 85f;

    void Awake()
    {
        cam = Camera.main;
        selector = Camera.main.GetComponent<ObjectSelector>();
    }

    public void AdjustElevation(float scrollValue) //Called by ObjectSelector. Changes the elevation angle.
    {   
        //Clamps the elevation.
        if (scrollValue > 0f)
            elevation = Mathf.Clamp(elevation + ElevationStep, MinElevation, MaxElevation);
        else if (scrollValue < 0f)
            elevation = Mathf.Clamp(elevation - ElevationStep, MinElevation, MaxElevation);

        Debug.Log($"Launch elevation: {elevation}°");
    }

    public void RollDice(Vector3 targetPos) //Called by ObjectSelector.
    {

        Rigidbody rb = selector.currentSelectedDie.GetComponent<Rigidbody>(); //get the rigidbody of the selected die.

        //Prevent rolling if the die is already moving.
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
            return;

        Vector3 direction = targetPos - rb.position; //calculate the direction from the die to the target position.
        direction.y = 0f; //set y to 0, making the vector strictly horizontal.

        float distance = direction.magnitude; //Distance from the die to the target.
        direction.Normalize();

        float elevationRadians = elevation * Mathf.Deg2Rad; //convert elevation value to Radians.

        Vector3 launchDirection = direction * Mathf.Cos(elevationRadians) + Vector3.up * Mathf.Sin(elevationRadians); //Add elevation to direction.

        float powerRatio = Mathf.Clamp01(distance / maxDistance); //Clamp value as a number between 0 and 1. if distance = maxdistance, the value is 1.
        float power = Mathf.Lerp(minPower, maxPower, powerRatio); //create a power value between min and max power. Ratio of 0 returns minPower, Ratio of 1 returns maxPower

        rb.AddForce(launchDirection * power, ForceMode.Impulse); 
        Debug.Log("Power: " + power);
        rb.AddTorque(Random.insideUnitSphere * 1f, ForceMode.Impulse); //adds a random torque to the die to make it roll.
    }

    public float GetElevation()
    {
        return elevation;
    }

    public float[] GetPowerValues()
    {
        float[] powers = { minPower, maxPower, maxDistance };
        return powers;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void Update()
    {
        transform.position = selector.currentSelectedDie.transform.position; //makes the indicator follow the selected die.
    }
}
