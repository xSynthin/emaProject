using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "EnemyAI/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    // TODO CHANGE NAME TO MOTHMAN STATS
    public float lookRange = 40f;
    public float lookSphereCastRadius = 1f;
    public float attackRange = 1f;
    public float attackRate = 1f;
    public float attackDamage = 50f;
    public float health = 100f;
    public bool attacked;
}
