using Pathfinding;
using System;
using UnityEngine;

public class EnemyAI : StateMachine
{
    [Header("References")]
    [SerializeField] GFXBehaivior gfx;
    [SerializeField] public Transform staticTurret;
    [SerializeField] LayerMask visibleLayers;
    [SerializeField] LayerMask damagableLayers;
    [SerializeField] Transform hitPoint;
    [SerializeField] Transform hitPoint2;
    public Rigidbody2D rb;
    public AIDestinationSetter destinationSetter;
    public AIPath aiPath;
    public AIManager aiManager;
    [SerializeField] GameObject explosionPrefab;

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
    [SerializeField] public bool isRanged, isStatic, isExplosive;
    [SerializeField] float explosionRadius;

    [Header("Bullet")]
    [SerializeField] float bulletSpeed;
    [SerializeField] int bulletDamage;


    [SerializeField] GameObject bulletPrefab;
    bool gotHit;

    void Start()
    {
        if (aiManager == null)
            aiManager = transform.parent.GetComponent<AIManager>();

        if (isStatic)
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

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
        //print("Displaying 'GetHit' animation");
    }

    public void Attack()
    {
        if (isRanged)
            RangedAttack();
        else if (isExplosive)
            Explode();
        else
            MeleeAttack();
    }

    void Explode()
    {
        var hit = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damagableLayers);

        if (hit.Length > 0)
        {
            foreach(var coll in hit)
            {
                if (coll.TryGetComponent(out Health health)){
                    health.TakeDamage(bulletDamage);
                }
            }
        }
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void MeleeAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPoint.position, attackRadius, damagableLayers);

        foreach (var coll in colliders)
        {
            if (coll.TryGetComponent(out Health health))
            {
                health.TakeDamage(attackDamage);
            }
        }
    }

    Transform prevHitPoint;
    void RangedAttack()
    {
        if (!IsPlayerVisible())
            return;

        var shootPoint = hitPoint;

        if (hitPoint2 != null && prevHitPoint == hitPoint)
        {
            shootPoint = hitPoint2;
            prevHitPoint = hitPoint2;
        }
        else
        {
            prevHitPoint = hitPoint;
        }


        var go = Instantiate(bulletPrefab, shootPoint.position, hitPoint.rotation, null);
        var bullet = go.GetComponent<Bullet>();

        bullet.Init(gameObject, bulletDamage, bulletSpeed);
    }

    bool IsPlayerAvailable()
    {
        if (destinationSetter.target == null || aiPath.remainingDistance > runningDistance || !aiManager.canFollowPlayer)
            return false;
        return true;
    }

    bool IsPlayerVisible()
    {
        var ray = Physics2D.Raycast(transform.position, transform.Direction(target.position), combatDistance, visibleLayers);

        if (ray.collider != null && ray.collider.transform == target)
            return true;
        return false;
    }

    bool IsPlayerWithingCombatRange()
    {
        return aiPath.remainingDistance <= combatDistance;
    }

    public void OnDamage()
    {
        StartCoroutine(gfx.Blink());
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
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
