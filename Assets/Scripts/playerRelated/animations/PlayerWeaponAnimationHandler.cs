using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerWeaponAnimationHandler : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float animationSpeedBoost = 1.15f;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        AnimationManager.instance.PlayerShootEvent += PlayerShootAnimation;
        AnimationManager.instance.PlayerReloadEvent += PlayerReloadingAnimation;
        // EntitiesManager.instance.EnemyDeathEvent += SpeedUpAnimation;
        // PlayerManager.instance.OnBoostStopEvent += ResetAnimationSpeed;
    }
    void PlayerShootAnimation(string aName)
    {
        animator.Play(aName, 0, 0.0f);
    }

    void SpeedUpAnimation()
    {
        animator.speed *= animationSpeedBoost;
        PlayerManager.instance.playerUtils.reloadTime *= (1-animationSpeedBoost);
    }
    void ResetAnimationSpeed()
    {
        animator.speed = 1f;
        PlayerManager.instance.playerUtils.reloadTime = PlayerManager.instance.playerUtils.defaultReloadTime;
    }

    void PlayerReloadingAnimation(string aName) => animator.Play(aName, 0, 0.0f);
}
