using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj; //Janela do diálogo
    public Image profileSprite; //Sprite do perfil
    public Text speechText; //Texto da fala
    public Text actorNameText; //Nome do NPC

    [Header("Settings")]
    public float typingSpeed; //velocidade da fala;

    //Variáveis de controle
    private bool _isShowing; // Se a janela está visível;
    private int index; // index das sentenças.
    private string[] sentences;

    public static DialogueControl instance;

    public bool isShowing {get => _isShowing; set => _isShowing = value;}

    //awake é chamado antes de todos os Start() na hierarquia de execução de scripts;
    private void Awake()
    {
        instance = this;
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    //pular para a próxima frase/fala;
    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            if(index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else //quando terminam os textos.
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                _isShowing = false;
            }
        }
    }

    //chamar a fala do npc
    public void Speech(string[] txt)
    {
        if(!_isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            _isShowing = true;
        }
    }
}
