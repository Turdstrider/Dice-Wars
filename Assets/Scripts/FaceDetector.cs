using UnityEngine;

public class FaceDetector : MonoBehaviour
{
    int faceLayerMask = 1 << 3; //convert the number into a bitmask.
    
    public void DetectFace() //Called when the Dice comes to rest. 
    {
        Ray ray = new Ray(transform.position + Vector3.up * 2f, Vector3.down); //Create a ray directly above the Dice, pointing straight downwards.

        if (Physics.Raycast(ray, out RaycastHit hit, 5f, faceLayerMask, QueryTriggerInteraction.Collide)) //Only hits colliders of layer 3, which are the colliders for each face.
        {
            FaceData face = hit.collider.GetComponent<FaceData>(); //get the component of the face.

            if (face != null)
            {
                Debug.Log("Dice rolled a " + face.faceValue); 
            }
        }
    }
}
