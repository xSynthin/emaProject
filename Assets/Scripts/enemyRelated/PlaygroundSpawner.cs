using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaygroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objToSpawn;
    private GameObject objRef;
    void Start()
    {
        objRef = spawnObj();
        StartCoroutine(RespawnObj());
    }

    GameObject spawnObj()
    {
        return Instantiate(objToSpawn, transform.position, Quaternion.identity);
    }

    IEnumerator RespawnObj()
    {
        while (true)
        {
            if (objRef == null)
            {
                yield return new WaitForSeconds(1f);
                objRef = spawnObj();
            }

            yield return null;
        }
    }
}
