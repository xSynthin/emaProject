using System;
using System.Collections.Generic;
using JetBrains.Annotations;
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
    private void handlePlayerSpawn()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(SceneSpawningPointList.Count > 0)
        {
            transform.position = SceneSpawningPointListDict[scene.name].position;
        }
    }
    private void TakeDamage(float dmgToTake) => hp -= dmgToTake;
    public void decreaseAmmo(int ammoToDecrease) => ammo -= ammoToDecrease;
}
