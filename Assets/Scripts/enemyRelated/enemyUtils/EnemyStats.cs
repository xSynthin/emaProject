using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "EnemyAI/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float lookRange = 40f;
    public float attackRange = 1f;
    public float health = 100f;
    public float chaseSpeed = 10f;
}
