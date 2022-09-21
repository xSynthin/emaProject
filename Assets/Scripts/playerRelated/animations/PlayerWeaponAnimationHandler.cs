using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerWeaponAnimationHandler : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        AnimationManager.instance.PlayerShootEvent += PlayerShootAnimation;
        AnimationManager.instance.PlayerReloadEvent += PlayerReloadingAnimation;
        PlayerManager.instance.playerSpeedBoostActivateEvent += SpeedUpAnimation;
        PlayerManager.instance.playerSpeedBoostDeactivateEvent += ResetAnimationSpeed;
    }
    void PlayerShootAnimation(string aName)
    {
        animator.Play(aName, 0, 0.0f);
    }

    void SpeedUpAnimation() => animator.speed *= 1.15f;
    void ResetAnimationSpeed() => animator.speed = 1f;
    void PlayerReloadingAnimation(string aName) => animator.Play(aName, 0, 0.0f);
}
