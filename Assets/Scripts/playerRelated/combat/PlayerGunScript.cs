using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.PackageManager;
using UnityEditor.Scripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerGunScript : MonoBehaviour
{
    // refs
    [Header("Assignable")]
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private GameObject gunPrefab;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] private ParticleSystem shotImpactParticleSystem;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private LineRenderer gunTracer;
    [SerializeField] private PlayerUtils playerUtils;
    // gun opts
    [Header("Gun Vars")]
    [SerializeField] private int shotDistance;
    [SerializeField] private float fireCooldown;
    [SerializeField] private int attackDmg;
    void Start()
    {
        Instantiate(gunPrefab, weaponPosition.transform);
        StartCoroutine(ShootDelay());
    }

    private void Shoot()
    {
        RaycastHit hit;
        muzzleFlash.Play();
        AnimationManager.instance.CallPlayerShotEvent("playerGunShot");
        playerUtils.decreaseAmmo(1);
        //ScoreSystem.instance.UpdateAmmoUI();
        if (Physics.Raycast(shootPosition.position, shootPosition.forward, out hit, shotDistance))
        {
            GameObject impact = Instantiate(shotImpactParticleSystem, hit.point, Quaternion.LookRotation(hit.normal)).gameObject;
            SpawnBulletTrail(hit.point);
            // if (hit.transform.CompareTag("Enemy"))
            // {
            //     hit.transform.gameObject.GetComponent<EnemyScript>()?.TakeDamage(attackDmg);
            // }
            Destroy(impact, shotImpactParticleSystem.time + 0.5f);
        }
    }

    private IEnumerator ShootDelay()
    {
        while (true)
        {
            if (playerUtils.ammo <= 0)
            {
                AnimationManager.instance.CallPlayerReloadEvent("ReloadGun");
                yield return new WaitForSeconds(playerUtils.reloadTime);
                playerUtils.ammo = playerUtils.ammoMax;
                //ScoreSystem.instance.UpdateAmmoUI();
            }
            else
            {
                if (Input.GetKey(shootKey))
                {
                    Shoot();
                    yield return new WaitForSeconds(fireCooldown);
                }
                else yield return null;
            } 
        }
    }

    private void SpawnBulletTrail(Vector3 point)
    {
        GameObject bulletTrail = Instantiate(gunTracer.gameObject, shootPosition.position, Quaternion.identity);
        LineRenderer lineTrail = bulletTrail.GetComponent<LineRenderer>();
        lineTrail.SetPosition(0, shootPosition.position);
        lineTrail.SetPosition(1, point);
        Destroy(bulletTrail, 0.2f);
    }
}