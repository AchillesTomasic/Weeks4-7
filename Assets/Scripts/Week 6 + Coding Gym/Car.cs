using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed;
    public Transform spawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Camera.main.WorldToScreenPoint(transform.position).x > Screen.width){
        transform.position = spawn.position;
        }
        Vector2 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
    }
}
