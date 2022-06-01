using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float treeHealth;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private int totalDropWood;
    [SerializeField] private ParticleSystem leafs;

    private bool isCut;

    public void OnHit()
    {
        treeHealth--;

        anim.SetTrigger("IsHit");
        leafs.Play();
        if(treeHealth <= 0)
        {
            //Cria o toco e instancia os drops.
            int i = Random.Range(0, totalDropWood);
            for (i = 0; i < totalDropWood; i++)
            {
                Instantiate(woodPrefab, transform.position + new Vector3(Random.Range(-1f, 1f),Random.Range(-1f, 1f),0f), Quaternion.identity);
            }
            anim.SetTrigger("Cut");

            isCut = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Axe") && !isCut)
        {
            OnHit();
        }
    }
}
