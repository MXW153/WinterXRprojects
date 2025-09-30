using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem.XR;
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
    public GameObject OBJPrefab; //obj to spawn
    public Transform SpawnPoint; //spawnpoint
    public float cooldown = 1.0f; // need Coroutine
    public XRNode controllerNode = XRNode.RightHand;
    private bool canSpawn = true; //Time in seconds between spawns

    // Update is called once per frame
    void Update()
    {
        if(canSpawn && BtnPressed()) { StartCoroutine(SpawnObjWithCd()); }
        if (cooldown > 0) { cooldown -= Time.deltaTime; }
    }

    bool BtnPressed()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        bool ButtonPressed = false;
        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out ButtonPressed) && ButtonPressed) //primary btn is a or x
        {
            return true;
        }
        return false;
    }

    IEnumerator SpawnObjWithCd()
    {
        canSpawn = false; // prevent immediate repawn
        SpawnObj();
        yield return new WaitForSeconds(cooldown);
        canSpawn = true; // allows spawning
    }

    void SpawnObj()
    {
        if(OBJPrefab != null && SpawnPoint != null) 
        {
            GameObject SpawnedObj = Instantiate(OBJPrefab, SpawnPoint.position, SpawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Assign OBJPrefab and Spawnpoint in inspecter");
        }
    }
}
