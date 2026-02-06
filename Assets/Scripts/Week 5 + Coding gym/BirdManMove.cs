using NUnit.Framework;
using NUnit.Framework.Internal.Filters;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BirdManMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject pirate;
    public GameObject reaction;
    public GameObject reactCanvas;
    public List<Sprite> ReactionTypes;
    public bool InRange = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        detectionRange();
    }
    void detectionRange()
    {
        float dist = Vector2.Distance(transform.position, pirate.transform.position);
        
            if (dist < 2)
            {
            reactCanvas.SetActive(true);
            if (!InRange)
            {
                int select = (int)Random.Range(0, 4);
                
                if (select == 0)
                {
                    reaction.GetComponent<Image>().sprite = ReactionTypes[0];
                }
                if (select == 1)
                {
                    reaction.GetComponent<Image>().sprite = ReactionTypes[1];
                }
                if (select == 2)
                {
                    reaction.GetComponent<Image>().sprite = ReactionTypes[2];
                }
                if(select == 3) 
                {
                    reaction.GetComponent<Image>().sprite = ReactionTypes[3];
                }
                InRange = true;
            }
        }
        else
        {
            InRange = false;
            reactCanvas.SetActive(false);
        }

    }

    void movement()
    {
        Vector2 pos = transform.position;
        if (Keyboard.current.wKey.isPressed)
        {
            pos.y += 1 * Time.deltaTime;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            pos.y -= 1 * Time.deltaTime;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            pos.x -= 1 * Time.deltaTime;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            pos.x += 1 * Time.deltaTime;
        }
        transform.position = pos;
    }
}
