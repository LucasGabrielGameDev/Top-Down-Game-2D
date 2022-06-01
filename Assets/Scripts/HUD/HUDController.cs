using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Itens")]
    [SerializeField] private Image fishBar;
    [SerializeField] private Image waterBar;
    [SerializeField] private Image carrotBar;
    [SerializeField] private Image woodBar;

    [Header("Tools")]
    // [SerializeField] private Image axeUI;
    // [SerializeField] private Image shovelUI;
    // [SerializeField] private Image bucketUI;
    public List<Image> toolsUI = new List<Image>();

    [SerializeField] private Color selectedColor;
    [SerializeField] private Color alphaColor;

    private PlayerItens playerItens;
    private Player player;

    void Awake()
    {
        playerItens = FindObjectOfType<PlayerItens>();
        player = playerItens.GetComponent<Player>();
    }

    void Start()
    {
        waterBar.fillAmount = 0f;
        carrotBar.fillAmount = 0f;
        woodBar.fillAmount = 0f;
    }

    void Update()
    {
        waterBar.fillAmount = playerItens.currentWater / playerItens.waterLimit;
        carrotBar.fillAmount = playerItens.totalCarrot / playerItens.carrotLimit;
        woodBar.fillAmount = playerItens.totalWood / playerItens.woodLimit;
        fishBar.fillAmount = playerItens.fishes / playerItens.fishesLimit;

        //toolsUI[player.handlingObj].color = selectedColor;

        for (int i = 0; i < toolsUI.Count; i++)
        {
            if(i == player.handlingObj)
            {
                toolsUI[i].color = selectedColor;
            }
            else
            {
                toolsUI[i].color = alphaColor;
            }
        }

    }
}
