using System.Data.Common;
using NUnit.Framework.Constraints;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private int turnID = 0;
    private ObjectSelector objectSelector;
    private TurnText turnText;
    private SelectionManager[] dice; 

    void Awake()
    {
        objectSelector = Camera.main.GetComponent<ObjectSelector>();
        turnText = FindFirstObjectByType<TurnText>();
        dice = FindObjectsByType<SelectionManager>(FindObjectsSortMode.None); 

        foreach(SelectionManager die in dice) //goes through each Dice.
        {
            if(die.ownerID == turnID) //Gives 1 Basic Roll to each dice controlled by the other player.
            {
                die.basicRollCount = 1;
            }
        }
    }

    public void EndTurn() //Is called by pressing the End Turn button.
    {
        Debug.Log("End Turn.");

        objectSelector.ClearSelection();

        turnID = 1 - turnID; //flip-flops between 1 and 0. 

        objectSelector.ownerID = turnID; //makes ObjectSelector change which dice it can interact with.
        turnText.turnID = turnID; 
        turnText.ChangeText(); //Changes the text denoting player's turn.

        //Since there may be cases where the amount of Dice on each side can change,
        //object finding is done at the end of each turn, not when the script is awake.
        dice = FindObjectsByType<SelectionManager>(FindObjectsSortMode.None); 

        foreach(SelectionManager die in dice) //goes through each Dice.
        {
            if(die.ownerID == turnID) //Gives 1 Basic Roll to each dice controlled by the other player.
            {
                die.basicRollCount = 1;
            }
        }
    }
}
