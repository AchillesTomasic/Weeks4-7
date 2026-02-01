using UnityEngine;
using UnityEngine.InputSystem;

public class SpawningCodingGym : MonoBehaviour
{
    public GameObject prefabObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool pressToSpawn = Keyboard.current.anyKey.isPressed;
        if(pressToSpawn){
            randomPos();
        }
        
    }
    public void randomPos(){
        Vector3 worldPos = transform.position;
        worldPos = new Vector3(Random.Range(0,Screen.width),Random.Range(0,Screen.height),0);
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);
        worldPos.z = 0;
        GameObject spawnedPrefab = Instantiate(prefabObj,worldPos,Quaternion.identity); 
    }
}
