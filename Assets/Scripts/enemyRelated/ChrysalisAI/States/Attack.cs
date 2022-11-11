using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : IState
{
    private readonly chrysalisController _chrysalisController;
    private Vector3 dir;
    private float accuracyDelayVal;
    public Attack(chrysalisController chrysalisController)
    {
        _chrysalisController = chrysalisController;
    }
    public void OnEnter()
    {
        _chrysalisController.StartCoroutine(shootPlayer());
        _chrysalisController.GetComponent<Renderer>().material.color = Color.magenta;
        getProperAccuracyDelayVal();
    }

    public void OnExit()
    {
        _chrysalisController.StopAllCoroutines();
    }

    public void Tick()
    {
        dir = (PlayerManager.instance.playerUtils.transform.position - _chrysalisController.shootPosition.position);
        _chrysalisController.RotateToDirection(dir.normalized);
    }

    public float getProperAccuracyDelayVal()
    {
        Dictionary<float, List<float>> attackRanges = PlayerManager.instance.playerUtils.chrysalisAttackRanges;
        float attackRangeOffset = 0;
        float index = 0;
        foreach (var element in attackRanges)
        {
            if(index++ == 0)
                attackRangeOffset = element.Key;
            else
            {
                if ((dir.magnitude < attackRangeOffset) && (dir.magnitude > element.Key))
                {
                    return attackRanges[attackRangeOffset][0];
                }
                attackRangeOffset = element.Key;
            }
        }
        return 1;
    }
    private IEnumerator shootPlayer()
    {
        yield return new WaitForSeconds(_chrysalisController.timeBetweenShots);
        RaycastHit hit;
        if (Physics.Raycast(_chrysalisController.shootPosition.position, dir.normalized, out hit, 1000)) //!hit.collider.CompareTag("Hide")
        {
            _chrysalisController.GetComponent<Renderer>().material.color = Color.blue;
            yield return new WaitForSeconds(getProperAccuracyDelayVal());
            _chrysalisController.PlayAttackSound(_chrysalisController.chrysalisSpit);
            _chrysalisController.SpawnBulletTrail(hit.point);
            if (Vector3.Distance(hit.point, PlayerManager.instance.playerUtils.transform.position) <= 0.7f){
                if (PlayerManager.instance.playerUtils.playerOneShot)
                    PlayerManager.instance.playerUtils.isDead = true;
                else
                    PlayerManager.instance.playerUtils.TakeDamage(2);
            }
            _chrysalisController.GetComponent<Renderer>().material.color = Color.magenta;
        }
        _chrysalisController.StartCoroutine(shootPlayer());
    }
}
