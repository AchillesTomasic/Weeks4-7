using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // used for the boundaires of the gameworld
    public GameObject boundingBox; // used as gameobject to determine the size of the level
    // used for the player click visual
    public GameObject visualLastclick; // gameobject that visually shows the players last click
    // manages the head segment
    public GameObject headSegment; // obtains a refrence of the players head segment GameObject
    private Transform headTransform; // heads transform for the object
    private Vector3 clickPos; // saves the players current click position
    private float rotSpeedHead = 3; // speed of rotation for head segment;
    public Transform backHeadSegment; // head segments back section
    public float playerSpeed;// sets speed for player to move by
    private float speedChanger; // changes the speed overTime
    public float dragF; // drag for the players velocity
    public float MaxSpeed; // max Speed the player can reach
    //manages the limb segments //
    public List<GameObject> limbObj = new List<GameObject>(); // list of limb objects
    public int numberOfLimbs; // sets the number of limbs
    private int limbCounter; // seperatly counts the limbs to cleanly add and subtract number
    public GameObject limbPrefab; // prefab of the limb
    public float limbRotSpeed; // speed the limbs move towards eachother
    public float limbMoveSpeed; // speed of the limbs
    // tail segment
    public Transform tailTransform; // transform for the tail
    // mouth segments
    public Slider mouthRot; //slider used for the mouth
    public Transform mouthTransform; // mouth for the transform
    public GameObject mouthRadius; // radius for food to be picked up
    public int foodInMouth; // checks if food is in mouth, 0 for no, 1 for yes

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        headTransform = headSegment.GetComponent<Transform>(); // sets the heads transform
    }

    // Update is called once per frame
    void Update()
    {
        rotateToClickPos(); // rotates the head component to the direction of the click
        limbAdditionManager(); // manages the limbs of the player
        followLimbLeader(); // allows each limb to follow their leader component
        MovePlayer();// moves the player in the direciton of the last click
        CameraPos(); // moves the camera on top of the player when active
        setTailSegment();// sets the tail segment to follow the player
        MouthSlider();// acts as a slider that rotates the players mouth
        FlipThelimbs();// flips the direction that the limbs are facing depending on how the player moves
        boundaries(); // sets the boundaries of the worldspace for the player
        clickVisual();// sets a gameobject for the pointer to visually show the players clicks
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
                 // if the limbs begin to disconnect from one another, push them in the targets direction faster to ensure they connect
                 if(targetRotDir.sqrMagnitude > 0.3f){
                    limbObj[i].GetComponent<Transform>().position += (targetRotDir  * 12) * Time.deltaTime ; // adds to the limbs direction by a smaller amount than the target position 
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
                 // if the limbs begin to disconnect from one another, push them in the targets direction faster to ensure they connect
                 if(targetRotDir.sqrMagnitude > 1f){
                    limbObj[i].GetComponent<Transform>().position += (targetRotDir  * 10) * Time.deltaTime;// adds to the limbs direction by a smaller amount than the target position 
                 }
                 limbObj[i].GetComponent<Transform>().position = Vector3.Lerp(limbObj[i].GetComponent<Transform>().position,limbObj[i - 1].GetComponent<Limb>().backSegment.position, limbMoveSpeed  * Time.deltaTime); // follows the back of the previous limb by moving towards its position
            }
        }
    }
    // function moves the head of the fish in the direction that the player clicked
    void MovePlayer()
    {
        // if any key is pressed, push the player forwards
        if (Keyboard.current.anyKey.wasPressedThisFrame)
    {
        speedChanger += playerSpeed * Time.deltaTime; // incriments the player speed with each click
    }
    speedChanger -= Time.deltaTime / dragF; // slows down the speed overtime by time and the drag
    speedChanger = Mathf.Clamp(speedChanger,0,MaxSpeed);// makes sure player can hit max speed and min speed
    headTransform.position += (- headTransform.right) * speedChanger * Time.deltaTime; // moves the player based on their head rotation by speed and time
    }
    // sets the cameras pos
    void CameraPos(){
        Camera.main.GetComponent<Transform>().position = new Vector3(headTransform.position.x,headTransform.position.y,Camera.main.GetComponent<Transform>().position.z);

    }
    // sets the tail limb segment
    void setTailSegment(){
        Vector3 targetRotDir = limbObj[limbObj.Count - 1].GetComponent<Transform>().position - tailTransform.position; // target position that the front part of the tail will follow
                // to stop flickering rotation in  the tail, check if the magnitude to see how far the tail is from the back
                if(targetRotDir.sqrMagnitude > 0.0001f)
                 {
                tailTransform.right = Vector3.Lerp(tailTransform.right, targetRotDir, 10* Time.deltaTime);  // rotates the tail towards the back of the previous direction
                 }
                 // if the tail begins to disconnect from one another, push them in the targets direction faster to ensure they connect
                 if(targetRotDir.sqrMagnitude > 1f){
                    tailTransform.position += (targetRotDir  * 10) * Time.deltaTime;// adds to the tails direction by a smaller amount than the target position 
                 }
                 tailTransform.position = Vector3.Lerp(tailTransform.position,limbObj[limbObj.Count - 1].GetComponent<Limb>().backSegment.position, limbMoveSpeed  * Time.deltaTime); // follows the back of the previous limb by moving towards its position
    }
    //function for the mouth slider
    void MouthSlider(){
        Vector3 mouthCurrentRot = mouthTransform.localEulerAngles; // obtains the angle of the mouth hinge
        mouthCurrentRot.z = Map(mouthRot.value,0, 100,0, 120); // maps the sliders values to the rotation needed, then sets that value to the z of the mouth rotation obtained
        mouthTransform.localEulerAngles = mouthCurrentRot;// sets the new mouth rotation
        

    }
    // flips the orientation of the limbs if they need to turn
    void FlipThelimbs()
    {
        // if the players head is pointing to the right, flip the scale to a negative
        if(headTransform.localEulerAngles.z > 90 && headTransform.localEulerAngles.z < 270){
            headTransform.localScale = new Vector3(headTransform.localScale.x, -1,headTransform.localScale.z); // flips the local scale to a negative value
        }
        // flip the players head back to the original direction if it is facing left
        else{
             headTransform.localScale = new Vector3(headTransform.localScale.x, 1,headTransform.localScale.z);// flips the local scale back to its original value
        }
        // loops over every limb, then flips the sprite renderer of each object
        foreach(GameObject limbFlip in limbObj){
            // checks if the limbs rotation is point right
            if(limbFlip.GetComponent<Transform>().localEulerAngles.z > 90 && limbFlip.GetComponent<Transform>().localEulerAngles.z < 270){
            // flips the sprite renderer
            limbFlip.GetComponent<SpriteRenderer>().flipX = true; 
            limbFlip.GetComponent<SpriteRenderer>().flipY = true;
            }
            // if the limbs rotation is pointing to the left
            else{
                // flips the sprite renderer
                limbFlip.GetComponent<SpriteRenderer>().flipX = false;
                limbFlip.GetComponent<SpriteRenderer>().flipY = false;
            }
        }
        // if the players tail is pointing to the right, flip the scale to a negative
        if(tailTransform.localEulerAngles.z > 90 && tailTransform.localEulerAngles.z < 270){
            tailTransform.localScale = new Vector3(tailTransform.localScale.x, -1,tailTransform.localScale.z); // flips the local scale to a negative value
        }
        // flip the players tail back to the original direction if it is facing left
        else{
             tailTransform.localScale = new Vector3(tailTransform.localScale.x, 1,tailTransform.localScale.z);// flips the local scale back to its original value
        }

    }
    // function created to work similarly to the map function in processing, calculation was used from the refrence in the miro board
    float Map(float value, float prevMin, float prevMax, float newMin,float newMax)
    {
        return newMin + (newMax - newMin) * ((value-prevMin)/(prevMax-prevMin)); // calculates the new translated values
        
    }
    // used for controlling the button
    public void mouthButtonToggle(){
        bool activeStat = true; // used so that it can flick between both states
        // checks if slider is active
        if(mouthRot.interactable && activeStat){
            mouthRot.interactable = false; // deactivates the slider
            activeStat = false; // stops any futher command from happening
        }
        // checks if slider is not active
        if(mouthRot.interactable == false && activeStat){
            mouthRot.interactable = true;// activates the slider
            activeStat = false; // stops any futher command from happening

        }
    }
    // sets the boundaries of the world
    void boundaries(){
        Transform boundsTran = boundingBox.GetComponent<Transform>();
        // used to constrict the downward position of the map
        if(headTransform.position.y < -boundsTran.localScale.y / 2){
            headTransform.position = new Vector2(headTransform.position.x,-boundsTran.localScale.y / 2); // sets the new position of the player
        }
        // used to constrict the upwards position of the map
        if(headTransform.position.y > boundsTran.localScale.y / 2){
            headTransform.position = new Vector2(headTransform.position.x,boundsTran.localScale.y / 2);// sets the new position of the player
        }
        // teleports to the right side of the map
        if(headTransform.position.x < -boundsTran.localScale.x / 2){
            headTransform.position = new Vector2(boundsTran.localScale.x / 2,headTransform.position.y);// sets the new position of the player
        }
        // teleports to the left side of the map
        if(headTransform.position.x > boundsTran.localScale.x / 2){
            headTransform.position = new Vector2(-boundsTran.localScale.x / 2,headTransform.position.y);// sets the new position of the player
        }
    }
    // sets the locaiton of the player click
    void clickVisual()
    {
        visualLastclick.GetComponent<Transform>().position = clickPos; // sets the click redicule to the position of the players last click
    }
}