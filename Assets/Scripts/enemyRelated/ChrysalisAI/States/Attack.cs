using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : IState
{
    private readonly chrysalisController _chrysalisController;
    public Attack(chrysalisController chrysalisController)
    {
        _chrysalisController = chrysalisController;
    }
    public void OnEnter()
    {
        _chrysalisController.StartCoroutine(shootPlayer());
        _chrysalisController.GetComponent<Renderer>().material.color = Color.magenta;
    }

    public void OnExit()
    {
        _chrysalisController.StopAllCoroutines();
    }
    public void Tick(){}
    private IEnumerator shootPlayer()
    {
        yield return new WaitForSeconds(1f);
        RaycastHit hit;
        Vector3 dir = (PlayerManager.instance.playerUtils.transform.position - _chrysalisController.shootPosition.position);
        _chrysalisController.RotateToDirection(dir.normalized);
        if(Physics.Raycast(_chrysalisController.shootPosition.position, dir.normalized, out hit, 100) && !hit.collider.CompareTag("Hide"))
        {
            _chrysalisController.GetComponent<Renderer>().material.color = Color.blue;
            yield return new WaitForSeconds(_chrysalisController.shotCooldownMultiplier * dir.magnitude);
            _chrysalisController.SpawnBulletTrail(hit.point);
            if(Vector3.Distance(hit.point, PlayerManager.instance.playerUtils.transform.position) <= 0.7f)
                PlayerManager.instance.playerUtils.TakeDamage(2);
            _chrysalisController.GetComponent<Renderer>().material.color = Color.magenta;
        }
        _chrysalisController.StartCoroutine(shootPlayer());
    }
}
