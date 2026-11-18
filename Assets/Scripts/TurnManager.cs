using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private int turnID = 0;
    private ObjectSelector objectSelector;
    private TurnText turnText;

    void Awake()
    {
        objectSelector = Camera.main.GetComponent<ObjectSelector>();
        turnText = FindFirstObjectByType<TurnText>();
    }

    public void EndTurn() //Is called by pressing the End Turn button.
    {
        Debug.Log("End Turn.");

        objectSelector.ClearSelection();

        turnID = 1 - turnID; //flip-flops between 1 and 0. 

        objectSelector.ownerID = turnID; //makes ObjectSelector change which dice it can interact with.
        turnText.turnID = turnID; 
        turnText.ChangeText(); //Changes the text denoting player's turn.
    }
}
