using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public Button play;
    public bool playclicked;
    public float moveOverTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if( playclicked == true){
            moveOverTime += Time.deltaTime;
        moveOffscreen();
            }
    }
    public void activatePlay(){
        playclicked = true;
    }
    void moveOffscreen(){
       play.GetComponent<Transform>().position = Vector2.Lerp(play.GetComponent<Transform>().position,new Vector2(play.GetComponent<Transform>().position.x,-300), moveOverTime);
        
    }
}