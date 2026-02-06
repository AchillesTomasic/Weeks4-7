using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int health;
    private SpriteRenderer sp;
    bool clicked = false;
    public Slider slider;
    // movement
    
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyClicked();
        slider.value = health;
    }
    
    public void enemyClicked()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 WorldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        if (sp.bounds.Contains(WorldMousePos) && Mouse.current.leftButton.IsPressed())
        {
            if (!clicked)
            {
                health -= 1;
                clicked = true;
            }
        }
        else
        {
            clicked = false;
        }
        void movement()
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 WorldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            
        }
    }
}  
