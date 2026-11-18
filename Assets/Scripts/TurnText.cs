using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnText : TextUpdater
{   
    public int turnID = 0;

    protected override void Awake()
    {
        base.Awake(); //execute base class' Awake function.
        ChangeText(); //immediately update text when object awakens.
    }

    //overrides previous behaviour of TextUpdater (which did nothing before but I digress).
    public override void ChangeText() //Is called by ending your turn..
    {
        textMesh.text = "Player " + turnID + "'s Turn.";
    }
}