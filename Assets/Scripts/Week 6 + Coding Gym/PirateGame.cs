using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class PirateGame : MonoBehaviour
{


    public List<GameObject> knifes = new List<GameObject>();
    public bool gameOver;
    public List<bool> killer = new List<bool>();
    public GameObject knifePrefab;
    public SpriteRenderer barrelSPR;
    public List<GameObject> conditionUI;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
