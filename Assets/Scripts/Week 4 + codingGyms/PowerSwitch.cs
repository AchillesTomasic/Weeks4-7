using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
    public float speed; // used for the speed
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = transform.eulerAngles;
        movement.z += speed * Time.deltaTime;
        transform.eulerAngles = movement;
    }
    public void StartSpin(){
        speed = 100;
    }
    public void StopSpin(){
        speed = 0;
    }
    
}
