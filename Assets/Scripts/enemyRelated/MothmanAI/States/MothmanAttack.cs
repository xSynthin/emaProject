using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MothmanAttack : IState
{
    private readonly mothmanController _mothmanController;
    private readonly NavMeshAgent _navMeshAgent;
    private bool canAttack = true;

    public MothmanAttack(mothmanController mothmanController, NavMeshAgent navMeshAgent)
    {
        _mothmanController = mothmanController;
        _navMeshAgent = navMeshAgent;
    }
    public void Tick() {}
    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
        if (canAttack)
        {
            if (PlayerManager.instance.playerUtils.playerOneShot)
                PlayerManager.instance.playerUtils.isDead = true;
            else{
                PlayerManager.instance.playerUtils.TakeDamage(3);
                canAttack = false;
                _mothmanController.StartCoroutine(ResetAttack());
            }
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(2);
        canAttack = true;
    }
    public void OnExit()
    {
        _navMeshAgent.enabled = false;
    }
}
