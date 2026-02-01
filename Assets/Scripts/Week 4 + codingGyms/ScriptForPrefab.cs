using UnityEngine;
using UnityEngine.InputSystem;

public class ScriptForPrefab : MonoBehaviour
{
    private SpriteRenderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        checkForInput();
    }
    void checkForInput(){
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(mousePos);
        mouseInWorld.z = 0;
        bool mousePressedSpawn = Mouse.current.leftButton.isPressed;
        if(mousePressedSpawn && sprite.bounds.Contains(mouseInWorld)){
            Destroy(gameObject);
            Debug.Log("works");
            
        }

    }
}
