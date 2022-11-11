using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class KeyValuePair
{
    public String key;
    public Transform value;
}
public class PlayerUtils : MonoBehaviour
{
    [SerializeField] public float hp;
    [SerializeField] public int ammo;
    [SerializeField] public float reloadTime;
    public List<KeyValuePair> SceneSpawningPointList;
    [SerializeField] private Dictionary<String, Transform> SceneSpawningPointListDict = new Dictionary<string, Transform>();
    [HideInInspector] public Dictionary<float, List<float>> chrysalisAttackRanges = new Dictionary<float, List<float>>();
    [HideInInspector] public int ammoMax;
    [HideInInspector] public float defaultReloadTime;
    public bool debug;
    public bool playerOneShot = false;
    private Vector3 currSceneSpawnTransform = new Vector3();
    [HideInInspector] public bool isDead = false;
    private void Awake()
    {
        foreach (var kvp in SceneSpawningPointList)
            SceneSpawningPointListDict[kvp.key] = kvp.value;
    }

    private void Start()
    {
        defaultReloadTime = reloadTime;
        ammoMax = ammo;
        // this should be moved to on level change event 
        handlePlayerSpawn();
        chrysalisAttackRanges = new Dictionary<float, List<float>>()
        {
            {40, new List<float>(){0.75f, 20}},
            {30, new List<float>(){0.60f, 25}},
            {20, new List<float>(){0.3f, 30}},
            {7, new List<float>(){0.001f, 300}},
            {0, new List<float>(){0, 500}},
        };
    }

    private void Update()
    {
        PlayerDeath();
        if (debug)
        {
            DebugSpeed();
            DebugEnemy();
            DebugRestart();
        }
    }

    private void PlayerDeath()
    {
        if (hp <= 0)
        {
            //PlayerManager.instance.CallPlayerDeathEvent();
            GManager.instance.CallGameLostEvent();
            // TODO DEATH HANDLING DESIGN
            print("DEAD");
        }
        else if(playerOneShot)
            if (isDead)
            {
                GManager.instance.CallGameLostEvent();
                ammo = ammoMax;
                UIManager.instance.UpdateAmmoUI();
            }
    }
    public void handlePlayerSpawn()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(SceneSpawningPointList.Count > 0)
        {
            for(int i = 0; i < SceneManager.sceneCount; i++)
                foreach (var element in SceneSpawningPointListDict)
                {
                    if (SceneManager.GetSceneAt(i) == SceneManager.GetSceneByName(element.Key))
                    {
                        transform.position = element.Value.position;
                        currSceneSpawnTransform = element.Value.position;
                    }
                }
        }
    }
    public void LevelPositionChange(string nextLevel)
    {
        transform.position = SceneSpawningPointListDict[nextLevel].position;
    }
    public void TakeDamage(float dmgToTake)
    {
        hp -= dmgToTake;
        //UIManager.instance.CallPlayerHpChangeEvent();
    }
    public void decreaseAmmo(int ammoToDecrease) => ammo -= ammoToDecrease;
    public void DebugSpeed()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            EntitiesManager.instance.CallEnemyDeathEvent();
        }
    }

    public void DebugRestart()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            transform.position = currSceneSpawnTransform;
        }
    }
    public void DebugEnemy()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            if (Physics.Raycast(GetComponent<PlayerGunScript>().shootPosition.position, GetComponent<PlayerGunScript>().shootPosition.forward, out hit, 100) && !hit.collider.CompareTag("Hide"))
            {
                Instantiate(EntitiesManager.instance.mothManPrefab, hit.point, Quaternion.identity);
            }
        }
    }
}
