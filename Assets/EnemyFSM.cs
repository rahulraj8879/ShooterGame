using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
     Transform baseTransform;
    public Sight sightSensor;
    public float baseAttackDistance;
    public float playerAttackDistance;
    NavMeshAgent navMeshAgent;
    public GameObject Bullet;
    public float FireRate;
    float lastShootTime = 0;
    Animator animator;
    private void Awake()
    {
        baseTransform = GameObject.Find("PlayerBase").transform;
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponentInParent<Animator>();
    }
    public enum EnemyState
    {
        GoToBase,AttackBase,ChasePlayer,AttackPlayer
    }

    public EnemyState currentState;


   
    void Start()
    {
        
    }

   
    void Update()
    {
        if (currentState == EnemyState.GoToBase)
            GoToBase();
        else if (currentState == EnemyState.AttackBase)
            AttackBase();
        else if (currentState == EnemyState.ChasePlayer)
            ChasePlayer();
        else if (currentState == EnemyState.AttackPlayer)
            AttackPlayer();

    }

    private void AttackPlayer()
    {
        navMeshAgent.isStopped = true;
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }
        LookTO(sightSensor.detectedObject.transform.position);

        Shoot();
        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer > playerAttackDistance *1.1f)
        {
            currentState = EnemyState.ChasePlayer;
        }
    }

    private void ChasePlayer()
    {
        animator.SetBool("Shooting", false);
        navMeshAgent.isStopped = false;
        if(sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }
        navMeshAgent.SetDestination(sightSensor.detectedObject.transform.position);

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if(distanceToPlayer<= playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
        }
    }

    private void AttackBase()
    {
        navMeshAgent.isStopped = true;
        LookTO(baseTransform.position);

        Shoot();
        
    }

    private void GoToBase()
    {
        animator.SetBool("Shooting", false);

        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(baseTransform.position);

        if(sightSensor.detectedObject != null)
        {
            currentState = EnemyState.ChasePlayer;
        }

        float distanceToBase = Vector3.Distance(transform.position, baseTransform.position);
        if(distanceToBase <= baseAttackDistance)
        {
            currentState = EnemyState.AttackBase;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, baseAttackDistance);

    }
    void Shoot()
    {
        animator.SetBool("Shooting", true);

        if (Time.timeScale>0)
        {
            
            var timeSinceLost = Time.time - lastShootTime;
            if (timeSinceLost < FireRate)
                return;

            lastShootTime = Time.time;
            Instantiate(Bullet, transform.position, transform.rotation);

        }
    }
    void LookTO(Vector3 targetPosition)
    {
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.parent.position);
        directionToPosition.y = 0;
        transform.parent.forward = directionToPosition;
    }
}
