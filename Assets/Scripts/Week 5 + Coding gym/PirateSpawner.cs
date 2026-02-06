using JetBrains.Annotations;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PirateSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject pirateObj;
    public List<GameObject> pirateList;
    public int spawnAmount;
    public GameObject victor;
    void Start()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
           
            pirateList.Add(randomPos());
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject pirate in pirateList) 
        {
            if (pirate == null)
            {
                spawnAmount -= 1;
                pirateList.Remove(pirate);
               
            }
        checkState(pirate);

        }
        vicotry();
    }
    GameObject randomPos()
    {
        Vector3 pos = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
        worldPos.z = 0;
        GameObject obj = Instantiate(pirateObj, worldPos, Quaternion.identity);
        return obj;
    }
    void checkState(GameObject pir)
    {
        if (pir.GetComponent<Enemy>().health <= 0)
        {
            spawnAmount -= 1;
            pirateList.Remove(pir);
            Destroy(pir);
            
        }
    }
    void vicotry()
    {
        if (spawnAmount <= 0)
        {
           victor.SetActive(true);
        }
    }
}
