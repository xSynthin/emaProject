using System.Collections;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class PlayerGunScript : MonoBehaviour
{
    // TODO RECOIL
    // refs
    [Header("Assignable")]
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private GameObject gunPrefab;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode reloadKey = KeyCode.R;
    [SerializeField] private ParticleSystem shotImpactParticleSystem;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private LineRenderer gunTracer;
    [SerializeField] private PlayerUtils playerUtils;
    // gun opts
    [Header("Gun Vars")]
    [SerializeField] private int shotDistance;
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
        if (Physics.Raycast(shootPosition.position, shootPosition.forward, out hit, shotDistance))
        {
            if(hit.transform.CompareTag("Enemy"))
                hit.transform.gameObject.GetComponent<EnemyUtils>()?.TakeDamage(attackDmg);
            GameObject impact = Instantiate(shotImpactParticleSystem, hit.point, Quaternion.LookRotation(hit.normal)).gameObject;
            SpawnBulletTrail(hit.point);
            Destroy(impact, shotImpactParticleSystem.time + 0.5f);
        }
    }
    private IEnumerator ShootDelay()
    {
        while (true)
        {
            if (Input.GetKeyDown(reloadKey) && (playerUtils.ammo < playerUtils.ammoMax))
            {
                AnimationManager.instance.CallPlayerReloadEvent("ReloadGun");
                yield return new WaitForSeconds(playerUtils.reloadTime);
                playerUtils.ammo = playerUtils.ammoMax;
                UIManager.instance.CallPlayerAmmoChangeEvent();
            }
            else
            {
                if (Input.GetKeyDown(shootKey) && playerUtils.ammo > 0)
                {
                    Shoot();
                    UIManager.instance.CallPlayerAmmoChangeEvent();
                    yield return null;
                }
                else yield return null;
            } 
        }
    }
    private void SpawnBulletTrail(Vector3 point)
    {
        Vector3 position = shootPosition.position;
        GameObject bulletTrail = Instantiate(gunTracer.gameObject, position, Quaternion.identity);
        LineRenderer lineTrail = bulletTrail.GetComponent<LineRenderer>();
        lineTrail.SetPosition(0, position);
        lineTrail.SetPosition(1, point);
        Destroy(bulletTrail, 0.2f);
    }
}