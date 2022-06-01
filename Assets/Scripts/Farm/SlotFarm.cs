using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;
    [Header("Settings")]
    [SerializeField] private int digAmount; //Quantidade de "pazadas". 
    [SerializeField] private bool detecting;
    [SerializeField] private float waterAmount; 

    private float currentWater;
    private int initialDigAmount;
    private bool dughole;
    PlayerItens playerItens;

    // Start is called before the first frame update
    void Start()
    {
        initialDigAmount = digAmount;
        playerItens = FindObjectOfType<PlayerItens>();
    }

    void Update()
    {
        if(dughole)
        {
            if(detecting)
            {
                currentWater += 0.01f;
            }
            if(currentWater >= waterAmount)
            {
                spriteRenderer.sprite = carrot;

                if(Input.GetKeyDown(KeyCode.E))
                {
                    spriteRenderer.sprite = hole;
                    playerItens.totalCarrot +=1;
                    currentWater = 0f;
                }
            }
        }

    }

    public void OnHit()
    {
        digAmount--;

        if(digAmount <= initialDigAmount / 2)
        {
            spriteRenderer.sprite = hole;
            dughole = true;
        }

        // if(digAmount <= 0)
        // {
        //     spriteRenderer.sprite = carrot;
        // }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Shovel") )
        {
            OnHit();
        }
        if(collision.CompareTag("Water") )
        {
            detecting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Water") )
        {
            detecting = false;
        }
    }
}
