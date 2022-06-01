using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    [SerializeField] private int percentage;
    [SerializeField] private GameObject fishPrefab;

    private PlayerItens player;
    private PlayerAnim playerAnim;
    private bool detectingPlayer;   
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerItens>();
        playerAnim = player.GetComponent<PlayerAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        if(detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            playerAnim.OnCastingStarted();
        }
    }

    public void OnCasting()
    {
        int randomValue = Random.Range(1, 100);
        if(randomValue <= percentage)
        {
            //Sucesso
            Instantiate(fishPrefab, player.transform.position + new Vector3(Random.Range(-2f, -1f), Random.Range(0f, 0.5f), 0f), Quaternion.identity);
            Debug.Log("Pescou!");
        }
        else
        {
            //Falha
            Debug.Log("Erroooooooooou!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}
