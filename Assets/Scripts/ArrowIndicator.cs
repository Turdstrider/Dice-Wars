using UnityEngine;
using UnityEngine.UIElements;

public class ArrowIndicator : MonoBehaviour
{
    private BasicRollManager basicRollManager;
    private IndicatorManager indicatorManager;

    private float minPower; //These are automatically assigned from BasicRollManager.
    private float maxPower;
    private float maxDistance;

    private void Awake()
    {
        indicatorManager = FindFirstObjectByType<IndicatorManager>();
        basicRollManager = GetComponentInParent<BasicRollManager>();

        minPower = basicRollManager.GetPowerValues()[0];
        maxPower = basicRollManager.GetPowerValues()[1];
        maxDistance = basicRollManager.GetPowerValues()[2];
    }

    private void Update()
    {    
        Vector3 targetPos = indicatorManager.GetRaycast();
        float elevation = basicRollManager.GetElevation();

        Vector3 direction = targetPos - basicRollManager.GetPosition(); //calculate the direction from the pivot to the target position.
        direction.y = 0f; //set y to 0, making the vector strictly horizontal.

        float distance = direction.magnitude; //Distance from the die to the target.
        direction.Normalize();

        float powerRatio = Mathf.Clamp01(distance / maxDistance); //Clamp value as a number between 0 and 1. if distance = maxdistance, the value is 1.
        float power = Mathf.Lerp(minPower, maxPower, powerRatio); //create a power value between min and max power. Ratio of 0 returns minPower, Ratio of 1 returns maxPower

        float horizontalAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(-elevation, horizontalAngle, 0f);

        transform.localScale = new Vector3(1f,1f,power/2); //Set scale of arrow.
            
    }
}