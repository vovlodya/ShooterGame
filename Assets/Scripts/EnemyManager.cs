using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    public PlayerStats playerStats;
    public GameManager gameManager;
    public Animator enemyAnimator;
    public int damage = 20;
    public int health = 100;
    public float attackDelay = 2f;
    private string targetName = "FirstPersonController";
    public NavMeshAgent navMeshAgent;
    private float timeOfLastAttack = 0;

    public void Hit(int damage)
    {
        enemyAnimator.SetTrigger("getDamage");
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            gameManager.enemiesAlive--;
            gameManager.enemiesKilled++;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerStats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerStats.isDead)
        navMeshAgent.destination = player.transform.position;
        enemyAnimator.SetBool("isRunning", navMeshAgent.velocity.magnitude != 0);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name != targetName || playerStats.isDead) return;

        if (Time.time > timeOfLastAttack + attackDelay)
        {
            timeOfLastAttack = Time.time;
            Attack();
        }
    }
    
    private void Attack()
    {
        enemyAnimator.SetTrigger("attack");
        playerStats.Hit(damage);
    }
    
}
