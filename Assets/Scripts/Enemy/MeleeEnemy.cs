using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    [SerializeField] private Transform detectPosition;
    [SerializeField] private Vector2 detectBoxSize;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackCooldown;//resfiramento do ataque

    //tempo de espera do ataque
    private float cooldownTimer;

    protected override void Awake()
    {
        base.Awake();
        base.enemyHealth.OnHurt += EnemyHurtSound;
        base.enemyHealth.OnDead += EnemyDeadSound;
    }

    protected override void Update()
    {
        cooldownTimer += Time.deltaTime;
        VerifyCanAttack();
    }

    //sons
    private void EnemyHurtSound()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.EnemyHurt);
    }

    private void EnemyDeadSound()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.EnemyDead);
    }

    //ataque
    private void VerifyCanAttack()
    {
        if (cooldownTimer < attackCooldown || canAttack == false) return;
        if (PlayerInSight())
        {
            animator.SetTrigger("attack");
            AttackPlayer();//chama o ataque
        }
    }

    private void AttackPlayer()
    {
        cooldownTimer = 0;
        if (CheckPlayerInDetectArea().TryGetComponent(out Health playerHealth))
        {
            print("Making player take damage");
            playerHealth.TakeDamage();
        }
    }

    private Collider2D CheckPlayerInDetectArea()
    {
        return Physics2D.OverlapBox(detectPosition.position, detectBoxSize, 0f, playerLayer);
    }

    private bool PlayerInSight()
    {
        Collider2D playerCollider = CheckPlayerInDetectArea();
        return playerCollider != null;
    }

    private void OnDrawGizmos()
    {
        if (detectPosition == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectPosition.position, detectBoxSize);
    }
}
