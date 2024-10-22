using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private IsGroundedChecker groundedChecker;
    private Health playerHealth;

    private void Awake()
    {
        Debug.Log("Awake");
        animator = GetComponent<Animator>();
        groundedChecker = GetComponent<IsGroundedChecker>();
        playerHealth = GetComponent<Health>();

        //ações dos eventos
        playerHealth.OnHurt += PlayerHurtAnim;
        playerHealth.OnDead += PlayerDeadAnim;
    }

    private void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.inputManager != null){
            GameManager.Instance.inputManager.OnAttack += PlayAttackAnim;
        } else {
            Debug.LogError("GameManager or InputManager is not initialized.");
        }
    }

    private void Update()
    {
        bool isMoving = GameManager.Instance.inputManager.Movement != 0;
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isJumping", !groundedChecker.IsGrounded());
    }

    //animação de dano
    private void PlayerHurtAnim()
    {
        animator.SetTrigger("hurt");
    }

    //animação de morte
    private void PlayerDeadAnim()
    {
        animator.SetTrigger("dead");
    }

    //animação ataque
    private void PlayAttackAnim()
    {
        animator.SetTrigger("attack");
    }
}
