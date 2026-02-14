using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Frogger : MonoBehaviour
{
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputs();
    }
    void inputs(){
        Vector2 pos = transform.position;
        if(Keyboard.current.upArrowKey.isPressed){
            pos.y += speed * Time.deltaTime;
        }
        if(Keyboard.current.downArrowKey.isPressed){
            pos.y -= speed * Time.deltaTime;
        }
        if(Keyboard.current.leftArrowKey.isPressed){
            pos.x -= speed * Time.deltaTime;
        }
        if(Keyboard.current.rightArrowKey.isPressed){
            pos.x += speed * Time.deltaTime;
        }
        transform.position = pos;
    }
}
