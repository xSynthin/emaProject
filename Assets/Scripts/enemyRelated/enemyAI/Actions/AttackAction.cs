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
        RaycastHit hit;
        if (Physics.Raycast(controller.eyes.position, controller.eyes.forward, out hit, 1.5f) && hit.collider.CompareTag("Player"))
        {
            if (canAttack)
            {
                // THIS NEEDS SOME SERIOUS TWEAKING
                PlayerManager.instance.CallPlayerDamageTakenEvent(controller.EnemyStats.attackDamage);
                UIManager.instance.CallPlayerHpChangeEvent();
                canAttack = false;
                controller.StartCoroutine(ResetAttack(controller));
            }
        }
    }

    private IEnumerator ResetAttack(StateController controller)
    {
        yield return new WaitForSeconds(controller.EnemyStats.attackRate);
        canAttack = true;
    }
}
