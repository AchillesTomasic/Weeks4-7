using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    // manages the head segment
    public GameObject headSegment; // obtains a refrence of the players head segment GameObject
    private Transform headTransform; // heads transform for the object
    private Vector3 clickPos; // saves the players current click position
    private float rotSpeedHead = 3; // speed of rotation for head segment;
    public Transform backHeadSegment; // head segments back section
    //manages the limb segments //
    public List<GameObject> limbObj = new List<GameObject>(); // list of limb objects
    public int numberOfLimbs; // sets the number of limbs
    private int limbCounter; // seperatly counts the limbs to cleanly add and subtract number
    public GameObject limbPrefab; // prefab of the limb
    public float limbMoveSpeed; // speed the limbs move towards eachother

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        headTransform = headSegment.GetComponent<Transform>(); // sets the heads transform
    }

    // Update is called once per frame
    void Update()
    {
        rotateToClickPos();
        limbAdditionManager();
        //limbMover();
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
    // function attaches the limbss to one another
    void limbAdditionManager(){
        // if the limb amount is smaller than the needed number of limbs
          if(limbCounter < numberOfLimbs){
            GameObject limbRef = Instantiate(limbPrefab); // creates a new limb object
            limbObj.Insert(0,limbRef);// placed it at the first index closest to the head
            limbRef.GetComponent<Transform>().position += backHeadSegment.position - limbRef.GetComponent<Limb>().frontSegment.position;// sets its position exactly on the head
            limbRef.GetComponent<Transform>().eulerAngles = backHeadSegment.eulerAngles;
            limbCounter += 1;// adds to the limb counter
          }
          // if the limb count is greater than the needed number of limbs
          if(limbCounter > numberOfLimbs){
            Destroy(limbObj[limbObj.Count - 1]); // destroys the last limb
            limbObj.Remove(limbObj[limbObj.Count - 1]); // removes the limb from the list
            limbCounter -= 1;//subtracts from the counter
          }
    }
    /*
    // rework this
    // moves the limbs
    void limbMover(){
        for(int i = 0; i < limbObj.Count; i++){
            // if its the first on the list, attach to head
            if(i == 0){
               limbObj[i].GetComponent<Transform>().position = moveTowardsLimb(
               limbObj[i].GetComponent<Limb>().frontSegment.position,
               backHeadSegment.position,
               limbMoveSpeed);
            }
            else{
               limbObj[i].GetComponent<Transform>().position = moveTowardsLimb(
               limbObj[i].GetComponent<Limb>().frontSegment.position,
               limbObj[i - 1].GetComponent<Limb>().backSegment.position,
               limbMoveSpeed); 
            }
        }
    }
    // rework this 
    // function to move a limb towards a target, was not sure if we could use moveTo so i made the functionality myself
    Vector3 moveTowardsLimb(Vector3 currentPos,Vector3 endPos,float speed){
        Vector3 posDif = endPos - currentPos; // gets the difference between the two positions
        float dif = posDif.magnitude; // makes the posdif into a single value, length of a line between a and b
        // checks if there is no difference
        if(dif <= speed * Time.deltaTime){
            return endPos;// doesnt change position if its already equal or is close enough, stops flickering
        }
        return currentPos + posDif / dif * speed * Time.deltaTime; //gets the current difference between points, then divides the distance multiplies by the speed to move in correct direction
    }
}
*/
}