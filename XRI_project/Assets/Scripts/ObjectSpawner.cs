using UnityEngine;
using UnityEngine.XR;
//-------------------------------------------------------------------------------------\\
/*
Select Object
Cooldown
Input - btn
Where to Spawn
HAND
*/
//-------------------------------------------------------------------------------------\\


public class ObjectSpawner : MonoBehaviour
{
    public GameObject OBJPrefab;
    public Transform SpawnPoint;
    public float cooldown = 1.0f; // need Coroutine
    public XRNode controllerNode = XRNode.RightHand;
    private bool canSpawn = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0) { cooldown -= Time.deltaTime; }
    }
}
