using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawningPrefab;
    public float waitDuration;
    public float destroyDuration;
    private float waitProgress = 0f;
    public float pacerSpeed;
    public Color pacerColour;
    private float destroyProgress = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 originPos = Vector3.zero;
        Quaternion originRotation = Quaternion.identity;
        Instantiate(spawningPrefab,transform.position,Quaternion.identity);// spawns at positon
        //Instantiate(spawningPrefab,transform.position,transform.rotation);// spawns at positon
        // instantiate(spawningPrefab) spawns at origin of map
    }

    // Update is called once per frame
    void Update()
    {
        spawner();
    }
    void spawner()
    {
        waitProgress += Time.deltaTime;
        if (waitProgress > waitDuration)
        {

            //typeOfComponent variableName = variableObj.GetCommponent<TypeofComponent>();
            GameObject spawnedObj = Instantiate(spawningPrefab, transform.position, Quaternion.identity);
            Pacer pacerScript = spawnedObj.GetComponent<Pacer>();
            SpriteRenderer spriteRenderer = spawnedObj.GetComponent<SpriteRenderer>();
            spriteRenderer.color = pacerColour;
            pacerScript.speed = pacerSpeed;
            Destroy(spawnedObj, destroyDuration);
            waitProgress = 0;
        }
 
    }
    public void increasePacerSpeed()
    {
        pacerSpeed++;
    }
}
