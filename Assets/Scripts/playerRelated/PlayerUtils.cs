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
    [HideInInspector] public int ammoMax;
    [HideInInspector] public float defaultReloadTime;
    public bool debug;
    private void Awake()
    {
        foreach (var kvp in SceneSpawningPointList)
            SceneSpawningPointListDict[kvp.key] = kvp.value;
    }

    private void Start()
    {
        PlayerManager.instance.playerDamageTakenEvent += TakeDamage;
        defaultReloadTime = reloadTime;
        ammoMax = ammo;
        // this should be moved to on level change event 
        handlePlayerSpawn();
    }

    private void Update()
    {
        PlayerDeath();
        if (debug)
        {
            DebugSpeed();
            DebugEnemy();
        }
    }

    private void PlayerDeath()
    {
        if (hp <= 0)
        {
            PlayerManager.instance.CallPlayerDeathEvent();
            // TODO DEATH HANDLING DESIGN
            //Destroy(gameObject);
        }
    }
    public void handlePlayerSpawn()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(SceneSpawningPointList.Count > 0)
        {
            transform.position = SceneSpawningPointListDict[scene.name].position;
        }
    }

    public void LevelPositionChange(string nextLevel)
    {
        transform.position = SceneSpawningPointListDict[nextLevel].position;
    }
    public void TakeDamage(float dmgToTake)
    {
        hp -= dmgToTake;
        UIManager.instance.CallPlayerHpChangeEvent();
    }
    public void decreaseAmmo(int ammoToDecrease) => ammo -= ammoToDecrease;
    public void DebugSpeed()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            EntitiesManager.instance.CallEnemyDeathEvent();
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
