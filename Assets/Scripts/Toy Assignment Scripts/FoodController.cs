using UnityEngine;
using UnityEngine.UI;

public class FoodController : MonoBehaviour
{
    public PlayerController playerScript; // script for the player controller
    public int health;// health of the food
    public int stateInMouth; // checks if the food is in the mouth 0 for no 1 for yes
    int sliderPos; // saves the previous slider pos
    // Update is called once per frame
    void Update()
    {
        
        // checks if the mouth is eathing this food obj
        if(stateInMouth == 1){
        beingEaten(playerScript.mouthRot);
        }
        Transform mouthTran = playerScript.mouthRadius.GetComponent<Transform>(); // gets the transform of the mouth
        capturedFood(playerScript.foodInMouth,mouthTran.position,mouthTran.localScale);
    death(); // destroys the food if 0 health
    }
    // sets if the food is captured or not
    void capturedFood(int foodInMouth, Vector2 mouthPos,Vector2 mouthRad)
    {
        float magFoodToMouth = Vector2.Distance(transform.position,mouthPos);// distance between food and the mouth
        // checks if the food is close enough, and if there is an avalible food spot to occupy
        if(stateInMouth == 0 && foodInMouth == 0 &&  magFoodToMouth < mouthRad.x){
            transform.position = mouthPos; // moves the food into the players mouth
            stateInMouth = 1; // sets the state to active
            playerScript.foodInMouth = 1; // sets the food in mouth to occupied
        }
        if(stateInMouth == 1 && foodInMouth == 1){
            transform.position = mouthPos; // moves the food into the players mouth
        }
    }
    // lowers heatlh if the food is being eaten
    void beingEaten(Slider mouthSlide){
        int difInValue = (int)Mathf.Abs(sliderPos - mouthSlide.value); // gets the difference in the sliders positon and translates to a positive value
        health -= difInValue;// lowers the health by the difference
        sliderPos = (int)mouthSlide.value;// sets the new state of the slider
    }
    // kills the food
    void death(){
        // if health is equal to 0, destory food
        if(health <= 0)
        {
            playerScript.foodInMouth = 0; // removes the food from mouth
            // add points to score
            Destroy(gameObject); // destroys itself
            
        }
    }
}
