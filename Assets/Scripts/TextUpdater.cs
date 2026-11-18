using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class TextUpdater : MonoBehaviour //abstract classes cannot be used directly. It acts as a 'base' class for other classes to add onto it.
{
    protected TextMeshProUGUI textMesh;

    protected virtual void Awake() //allows child classes to override the base Awake() function.
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    //Abstract function. Is called by ending your turn..
    public abstract void ChangeText();
}