using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public Button play;
    public bool playclicked;
    public float moveOverTime;

    // Update is called once per frame
    void Update()
    {
        // checks if the play button should move off screen
        if( playclicked == true){
            moveOverTime += Time.deltaTime; // moves the variable for the lerp overtime
        moveOffscreen(); // moves the menu off screen when true
            }
    }
    // used as a custom function when the player presses the play button
    public void activatePlay(){
        playclicked = true;// sets the game to active
    }
    // moves the play off screen
    void moveOffscreen(){
       play.GetComponent<Transform>().position = Vector2.Lerp(play.GetComponent<Transform>().position,new Vector2(play.GetComponent<Transform>().position.x,-300), moveOverTime); // larps the position of the play button offscreen
        
    }
}