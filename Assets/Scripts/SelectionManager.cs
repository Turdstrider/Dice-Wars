using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public bool isSelected = false;
    public int ownerID = 0; //determines who this die belongs to.
    public int basicRollCount = 0; //how many Basic Rolls the dice can perform.

    // Update is called once per frame
    public void Select() //Setter functions.
    {
        isSelected = true;
    }
    public void DeSelect()
    {
        isSelected = false;
    }
}
