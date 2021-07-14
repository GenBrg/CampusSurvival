using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : IEnemyBehavior
{
    private Animator animator;
    private RateLimiter attackRateLimiter;
    private bool _isDead = false;

    public GameObject zombieAttack;
    public AnimationClip dieAnimation;
    public float attackInterval;

    public override bool IsDead
    {
        get => _isDead;
    }
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        attackRateLimiter = new RateLimiter(attackInterval, FireImpl);
        GetComponent<Health>().onDie += Die;
    }

    public override void Die()
    {
        --EnemySpawner.Instance.ZombieNum;
        animator.SetTrigger("Die");
        Destroy(gameObject, dieAnimation.length);
        _isDead = true;
    }

    private void FireImpl()
    {
        animator.SetTrigger("Attack");
        AOE attackAOE = Instantiate(zombieAttack, transform).GetComponent<AOE>();
        attackAOE.owner = gameObject;
    }

    public override void Fire()
    {
        attackRateLimiter.Invoke();
    }

    public override void OnChase()
    {
        
    }

    public override void OnStopChase()
    {
        
    }

    public override void OnMoving()
    {
        animator.SetBool("Chase", true);
    }

    public override void OnNotMoving()
    {
        animator.SetBool("Chase", false);
    }
}
