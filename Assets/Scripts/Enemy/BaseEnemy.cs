using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public abstract class BaseEnemy : MonoBehaviour
{
    protected Animator animator;

    protected Health enemyHealth;

    //pode atacar
    protected bool canAttack = true;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<Health>();

        //eventos de ação
        enemyHealth.OnHurt += HandleHurt;
        enemyHealth.OnDead += HandleDead;

    }

    protected abstract void Update();

    //ações dos eventos
    private void HandleHurt()
    {
        animator.SetTrigger("hurt");
    }

    private void HandleDead()
    {
        canAttack = false;
        GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("dead");
        StartCoroutine(DestroyEnemy(1));
    }

    private IEnumerator DestroyEnemy(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
