using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class PirateGame : MonoBehaviour
{


    public List<GameObject> knifes = new List<GameObject>();
    public bool gameOver = true;
    public List<bool> killer = new List<bool>();
    public GameObject knifePrefab;
    public SpriteRenderer barrelSPR;
    public List<GameObject> conditionUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
        knifeSpawner();
        
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       for(int i = 0; i < knifes.Count; i++){
        clicked(i);   
       }
       if(knifes.Count == 1 && gameOver ==false){
        conditionUI[1].SetActive(true);
       }
    }
   
    void knifeSpawner()
    {
        int select = Random.Range(0,2);
        float x = 0;
        if(select == 0){
        float leftSide = transform.position.x - 2.5f;
        x = leftSide;
        }
        if(select == 1){
        float rightSide = transform.position.x + 2.5f;
        x = rightSide;
        }
        float y = Random.Range(- barrelSPR.bounds.size.y / 3, barrelSPR.bounds.size.y / 3);
        
            if(gameOver == true){
                GameObject kni = Instantiate(knifePrefab,new Vector2(x,y),Quaternion.identity);
                knifes.Add(kni);
                kni.GetComponent<Transform>().up = transform.position - kni.GetComponent<Transform>().position;
                killer.Add(true);
                gameOver = false;
            }
            else{
                GameObject kni = Instantiate(knifePrefab,new Vector2(x,y),Quaternion.identity);
                knifes.Add(kni);
                kni.GetComponent<Transform>().up = transform.position - kni.GetComponent<Transform>().position;
                killer.Add(false);
           
        }
        
        
    }
    void clicked(int sat){
        Vector2 mousepos = Mouse.current.position.ReadValue();
        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousepos);
        if(knifes[sat].GetComponent<SpriteRenderer>().bounds.Contains(worldMousePos) && Mouse.current.leftButton.wasPressedThisFrame){
           if(killer[sat] == true){
            conditionUI[0].SetActive(true);
           }
           Destroy(knifes[sat]);
            knifes.Remove(knifes[sat]);
            
            
        }
    }
}
