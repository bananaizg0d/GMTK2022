using Pathfinding;
using System;
using UnityEngine;

public class EnemyAI : StateMachine
{
    [Header("References")]
    [SerializeField] LayerMask damagableLayers;
    [SerializeField] Transform hitPoint;
    public Rigidbody2D rb;
    public AIDestinationSetter destinationSetter;
    public AIPath aiPath;
    public AIManager aiManager;

    Animator animator;

    [HideInInspector]
    public Transform target;
    [Header("Stats")]
    [SerializeField] int attackDamage;
    [SerializeField] float attackRadius;
    [SerializeField] float runningDistance;
    [SerializeField] float combatDistance;
    [SerializeField] float stunOnDamageTime;
    [SerializeField] public float delayBetweenAttacks;
    [SerializeField] bool isRanged, isStatic;

    [Header("Bullet")]
    [SerializeField] float bulletSpeed;
    [SerializeField] int bulletDamage;


    [SerializeField] GameObject bulletPrefab;
    bool gotHit;

    void Start()
    {
        if (destinationSetter.target != null)
            target = destinationSetter.target;

        var searching = new Searching(this);
        var running = new Running(this);
        var combat = new Combat(this);
        var getHit = new GetHit(this);

        searching.AddTransition(running, IsPlayerAvailable);
        running.AddTransition(combat, IsPlayerWithingCombatRange);
        running.AddTransition(searching, () => !IsPlayerAvailable());
        getHit.AddTransition(searching, () => !IsPlayingGetHitAnimation(), new TimeCondition(0.1f).HasTimePassed);
        combat.AddTransition(running, () => !IsPlayerWithingCombatRange(), () => !IsPlayingAttackAnimation());
        combat.AddTransition(searching, IsTargetDead);

        AddAnyTransition(getHit, () => gotHit).AddTransitionCallBack(() => gotHit = false);

        SetState(searching);
    }

    public void SetTarget(Transform target)
    {
        destinationSetter.target = target;
        this.target = target;
    }

    public bool IsTargetDead()
    {
        return aiManager.target.isDead;
    }

    private bool IsPlayingGetHitAnimation()
    {
        return false;
        //add later
    }

    bool IsPlayingAttackAnimation()
    {
        return false;
        //add later
    }

    public void PlayHitAnimation()
    {
        print("Displaying 'GetHit' animation");
    }

    public void Attack()
    {
        if (isRanged)
            RangedAttack();
        else
            MeleeAttack();
    }

    void MeleeAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPoint.position, attackRadius, damagableLayers);

        foreach (var coll in colliders)
        {
            if (coll.TryGetComponent(out Health health))
            {
                health.TakeDamage(attackDamage);
                print("Dealt damage to: " + health.name + ". Damage amount: " + attackDamage + ". Current health: " + health.currentHealth);
            }
        }
    }

    void RangedAttack()
    {
        var go = Instantiate(bulletPrefab, hitPoint.position, hitPoint.rotation, null);
        var bullet = go.GetComponent<Bullet>();

        bullet.Init(gameObject, bulletDamage, bulletSpeed);
    }

    bool IsPlayerAvailable()
    {
        if (destinationSetter.target == null || aiPath.remainingDistance > runningDistance || !aiManager.canFollowPlayer)
            return false;
        return true;
    }

    bool IsPlayerWithingCombatRange()
    {
        return aiPath.remainingDistance <= combatDistance;
    }

    public void OnDamage()
    {
        gotHit = true;
        aiPath.canMove = false;
        Invoke(nameof(EnableMovement), stunOnDamageTime);
    }

    void EnableMovement()
    {
        aiPath.canMove = true;
    }

    public void OnDie()
    {
        Destroy(gameObject);
    }
}
