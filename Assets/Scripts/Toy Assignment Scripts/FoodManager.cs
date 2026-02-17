using UnityEngine;
using System.Collections.Generic;
public class FoodManager : MonoBehaviour
{
    private float timer; // timer for food to spawn
    public int maxTimer; // sets time it takes for food to spawn
    private int foodInScene; // used to spawn food
    public int maxFoodInScene; // maximum food that spawns at once

    public PlayerController player;// grabs the player controller
    public ScoreManager score;// grabs the score for the game
    public GameObject foodPrefab; // gets the prefab for the food
    public List<GameObject> foodList = new List<GameObject>(); // list of food objects

    // Update is called once per frame
    void Update()
    {
        destroyFood();
        SpawnTimer();
    }
    // spawns the food over a timer
    void SpawnTimer()
    {
        if(foodInScene < maxFoodInScene){
            timer += Time.deltaTime; // adds to the timer
            if(timer >= maxTimer){
                spawn(); // spawns a food obj
                timer = 0;// resets the timer
            }
        }
    }
 
    void spawn()
    {
        GameObject foodObj = Instantiate(foodPrefab, new Vector3(5,0,0), Quaternion.identity);// spawns in the food prefab
        foodObj.GetComponent<FoodController>().scoreCode = score; // sets the score script for the food
        foodObj.GetComponent<FoodController>().playerScript = player;// sets the player script for the food
        foodList.Add(foodObj);// adds food object to the list
        foodInScene += 1;// adds to the number of food in the scene
    }
    void destroyFood(){
        for(int i = 0; i< foodList.Count; i++){
            // checks if the object should be destroyed
            if(foodList[i].GetComponent<FoodController>().destroyThisFood == true){
                foodInScene -= 1;// lowers the number of food in the scene
                GameObject foodTemp = foodList[i]; // saves the value for deletion
                foodList.Remove(foodTemp); // removes the object from the list
                Destroy(foodTemp);// destorys the object
            }
        }
    }
}
