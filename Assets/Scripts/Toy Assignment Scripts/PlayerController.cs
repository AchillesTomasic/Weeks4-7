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
    public float limbRotSpeed; // speed the limbs move towards eachother
    public float limbMoveSpeed; // speed of the limbs

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
        followLimbLeader();
        
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
    void followLimbLeader(){
        for(int i = 0; i < limbObj.Count;i++)
        {
            float limbGradSpeed = Mathf.Lerp(10f, 1000f, i / limbObj.Count - 1); // changes the speed of the segments depending on the position, keeping the earlier segments of the body attached correctly
            //if its the first limb on the list, follow the head
            if(limbObj[i] == limbObj[0])
            {
                Vector3 targetRotDir = backHeadSegment.position - limbObj[i].GetComponent<Transform>().position; // target position that the front part of the limb will follow
                // to stop flickering rotation in  the limbs, check if the magnitude to see how far each segment is to one another
                if(targetRotDir.sqrMagnitude > 0.0001f)
                 {
                limbObj[i].GetComponent<Transform>().right = Vector3.Lerp(limbObj[i].GetComponent<Transform>().right, targetRotDir,limbGradSpeed * Time.deltaTime);  // rotates the limb towards the back of the heads direction
                 }     
                  limbObj[i].GetComponent<Transform>().position = Vector3.Lerp(limbObj[i].GetComponent<Transform>().position,backHeadSegment.position, 0.99f * Time.deltaTime); // follows the back of the head by moving towards its position

            }
            else{
                 Vector3 targetRotDir = limbObj[i - 1].GetComponent<Limb>().backSegment.position  - limbObj[i].GetComponent<Transform>().position; // target position that the front part of the limb will follow
                // to stop flickering rotation in  the limbs, check if the magnitude to see how far each segment is to one another
                if(targetRotDir.sqrMagnitude > 0.0001f)
                 {
                limbObj[i].GetComponent<Transform>().right = Vector3.Lerp(limbObj[i].GetComponent<Transform>().right, targetRotDir,limbGradSpeed* Time.deltaTime);  // rotates the limb towards the back of the previous direction
                 }
                 limbObj[i].GetComponent<Transform>().position = Vector3.Lerp(limbObj[i].GetComponent<Transform>().position,limbObj[i - 1].GetComponent<Limb>().backSegment.position, limbMoveSpeed  * Time.deltaTime); // follows the back of the previous limb by moving towards its position
            }
        }
    }

}