using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu (menuName = "EnemyAI/Actions/Attack")]
public class AttackAction : SAction
{
    private bool canAttack = true;
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        //RaycastHit hit;
        //this can be optimized through doing dist check manually
        if (canAttack)
        {
            // THIS NEEDS SOME SERIOUS TWEAKING
            PlayerManager.instance.CallPlayerDamageTakenEvent(controller.EnemyStats.attackDamage);
            UIManager.instance.CallPlayerHpChangeEvent();
            controller.transform.GetComponent<Renderer>().material.color = Color.red;
            canAttack = false;
            controller.StartCoroutine(ResetAttack(controller));
        }
    }
    private IEnumerator ResetAttack(StateController controller)
    {
        yield return new WaitForSeconds(controller.EnemyStats.attackRate);
        controller.transform.GetComponent<Renderer>().material.color = Color.black;
        canAttack = true;
    }
}
