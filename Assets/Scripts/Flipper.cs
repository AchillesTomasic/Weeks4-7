using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Flipper : MonoBehaviour
{
    public float direction;
    public float speed;
    public bool moving;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            movement();
        }
    }
    void movement()
    {
        
        transform.position += transform.right * direction * speed * Time.deltaTime;
    }
    public void onMoveClick()
    {
        moving = true;
    }
    public void onStopClick()
    {
        moving=false;
    }
    public void onFlipClick()
    {
        direction *= -1;
    }
}
