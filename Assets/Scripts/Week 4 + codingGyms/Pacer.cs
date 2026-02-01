using UnityEngine;

public class Pacer : MonoBehaviour
{
    public float speed;
    private float direction = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }
    void movement()
    {
        transform.position += direction * transform.right * speed * Time.deltaTime;
        Vector3 screenPacerPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPacerPos.x > Screen.width)
        {
            direction *= -1;
            
        }
        if(screenPacerPos.x < 0)
        {
            direction *= -1;
            
        }
        
    }
}
