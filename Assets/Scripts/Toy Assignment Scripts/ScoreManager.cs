using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public int score = 2; // score for the player
    private int prevScore = 0; // previous score of the player for comparison
    public int requiredToGrowSize;// set the size requried to make it to the next limb
    public PlayerController player; // gets the player controller script
    public TextMeshProUGUI textForScoreUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setScoreText(); // sets the text for the players length
        addToSize();// adds to the size of the player so that they grow correctly
    }
    void addToSize(){
        // checks if the player has eaten enough food to grow, by comparing the score the the requred size of growth
        if(score >= prevScore + requiredToGrowSize){
            
        player.numberOfLimbs += 1; // sets the number of limbs to the score
        prevScore = score; // sets the new previous score
        }
    }
    void setScoreText(){
    textForScoreUI.text = "Length : " + score + "M"; // sets the length of the player
    }
}
