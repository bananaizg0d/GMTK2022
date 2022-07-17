using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] float InvulnerabilityAfterHit;
    public int maxHealth;
    public bool isStatic;
    [SerializeField] GameObject invBubble;
    public int currentHealth { get; private set; }

    public bool isDead { get; private set; }

    [Tooltip("Called even if target is dead")]
    public UnityEvent _OnRegisterHit;
    public UnityEvent<int> _OnDamage, _OnRestore;
    public UnityEvent _OnDie;

    bool invulnerable;
    float timeAfterFirstHit;
    bool startCountingTime;
    float damageModifier = 1;

    void OnEnable()
    {
        Init();
    }

    void Update()
    {
        if (startCountingTime)
        {
            if (timeAfterFirstHit >= InvulnerabilityAfterHit)
            {
                startCountingTime = false;
                invulnerable = false;
            }
            else
            {
                timeAfterFirstHit += Time.deltaTime;
            }
        }
    }

    public virtual void Init()
    {
        currentHealth = maxHealth;
        isDead = false;
    }
#if UNITY_EDITOR
    [ContextMenu("Take Damage")]
    public void TakeDamageTest()
    {
        TakeDamage(10);
    }

#endif
    public void Kill()
    {
        TakeDamage(maxHealth);
    }

    public void TakeDamage(int amount)
    {
        _OnRegisterHit?.Invoke();

        if (isDead) 
            return;

        if (invulnerable)
            return;

        timeAfterFirstHit = 0;
        startCountingTime = true;
        if (InvulnerabilityAfterHit > 0)
            invulnerable = true;

        currentHealth -= (int)(amount * damageModifier);

        if (currentHealth < 0) 
            currentHealth = 0;


        if (currentHealth == 0)
        {
            isDead = true;
            _OnDie?.Invoke();
            return;
            
        }

        _OnDamage?.Invoke(currentHealth);
    }

    public void Restore(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        _OnRestore?.Invoke(currentHealth);
    }

    public void BuffHealth(float modifier, float time)
    {
        damageModifier = modifier;
        Invoke(nameof(SetNormalHealth), time);
    }

    void SetNormalHealth()
    {
        damageModifier = 1;
    }

    public void MakeInvulnirable(float time)
    {
        invBubble.gameObject.SetActive(true);
        invulnerable = true;
        Invoke(nameof(ResetInvul), time);
    }

    void ResetInvul()
    {
        invulnerable = false;
        invBubble.gameObject.SetActive(false);
    }
}
