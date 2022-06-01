using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    private Player player;
    private Animator anim;
    private Casting cast;

    private bool isHitting;
    private float recoveryTime = 2f;
    private float timeCount;

    void Start()
    {
        player = GetComponent<Player>();   
        anim = GetComponent<Animator>();   
        cast = FindObjectOfType<Casting>();  
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnRun();

        if(isHitting)
        {
            timeCount += Time.deltaTime;
            if(timeCount >= recoveryTime)
            {
                isHitting = false;
                timeCount = 0f;
            }
        }
    }

    #region Movement

    void OnMove()
    {
        if(player.direction.sqrMagnitude > 0)
        {
            if(player.isRolling)
            {
                anim.SetTrigger("isRoll");
            }
            else
            {
                anim.SetInteger("Transition", 1);
            }
        }
        else
        {
            anim.SetInteger("Transition", 0);
        }

        if(player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2 (0, 0);
        }
        if(player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2 (0,180);
        }

        if(player.isCutting)
        {
            anim.SetInteger("Transition", 4);
        }

        if(player.isDigging)
        {
            anim.SetInteger("Transition", 5);
        }

        if(player.isWatering)
        {
            anim.SetInteger("Transition", 6);
        }
    }

    void OnRun()
    {
        if(player.isRunning)
        {
            anim.SetInteger("Transition", 2);
        }
    }

    #endregion

    #region Attack

    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if(hit != null)
        {
            //atacou o inimigo.
            hit.GetComponentInChildren<AnimationControl>().OnHit();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

    #endregion


    //é chamado quando jogador pressiona botão de ação na lagoa;
    public void OnCastingStarted()
    {
        anim.SetTrigger("isCasting");
        player.isPaused = true;
    }

    //Chamado quando termina a animação.
    public void OnCastingEnded()
    {
        cast.OnCasting();
        player.isPaused = false;
    }

    public void OnHammeringStarted()
    {
        anim.SetBool("hammering", true);
    }

    public void OnHammeringEnded()
    {
        anim.SetBool("hammering", false);
    }

    public void OnHit()
    {
        if(!isHitting)
        {
            anim.SetTrigger("hit");
            isHitting = true;
        }
    }
}
