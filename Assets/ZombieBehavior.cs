using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : IEnemyBehavior
{
    private Animator animator;
    private RateLimiter attackRateLimiter;

    public AnimationClip dieAnimation;
    public float attackInterval;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        attackRateLimiter = new RateLimiter(attackInterval, FireImpl);
    }

    public override void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, dieAnimation.length);
    }

    private void FireImpl()
    {
        animator.SetTrigger("Attack");
    }

    public override void Fire()
    {
        attackRateLimiter.Invoke();
    }

    public override void OnChase()
    {
        animator.SetBool("Chase", true);
    }

    public override void OnStopChase()
    {
        animator.SetBool("Chase", false);
    }
}
