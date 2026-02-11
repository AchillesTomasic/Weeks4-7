using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject headSegment; // obtains a refrence of the players head segment GameObject
    private Transform headTransform; // heads transform for the object
    private Vector3 clickPos; // saves the players current click position
    private float rotSpeedHead = 3; // speed of rotation for head segment;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        headTransform = headSegment.GetComponent<Transform>(); // sets the heads transform
    }

    // Update is called once per frame
    void Update()
    {
        rotateToClickPos();
    }
    // rotates the head segment in the direction of the users mouse click
    void rotateToClickPos()
    {
        // obtains the location where the mouse has been pressed
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
        Vector3 mousePos = Mouse.current.position.ReadValue(); // obtains the mouses position
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos); // converts the value to screen space
        worldMousePos.z = 0; // used incase vector3 ever becomes activated
        clickPos = worldMousePos; // sets the click position
        }
        
        // rotates the players head towards the current input location smoothly
        Vector3 headRot = -clickPos + headTransform.position; // obtains the position the head should rotate to
        Vector3 lerpRot = Vector3.Lerp(headTransform.right,headRot,rotSpeedHead * Time.deltaTime); // lerps the rotation of the head towards the position to make the transition smooth
        headTransform.right = lerpRot;// sets the lerped rotation
    }
}
