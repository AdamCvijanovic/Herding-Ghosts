using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject playerNameplate;
    public GameObject customerNameplate;

    //public Image playerIMG;
    public Image customerIMG;
    public Sprite grandmaImg;
    //public Color colorChange;

    //textmesh pro GUIs
    //public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI customerNameText;
    public TextMeshProUGUI dialogueText;

    //Animators
    public Animator animator;
    //public Animator playerAnimator;
    public Animator customerAnimator;

    private GameObject chooseDialogue;
    private ChooseDialogueSystem cDS;

    public Queue<string> sentences;
    private int sentenceCounter = 0;
    public int[] playerTalking;
    public int[] customerTalking;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        //colorChange.a = 1;
        chooseDialogue = GameObject.Find("DialogueElements");
        cDS = chooseDialogue.GetComponent<ChooseDialogueSystem>();

        //retrieve customer image from Canvas
        customerIMG = FindObjectOfType<CanvasManager>().customerIMG;
        
    }

    public void StartDialogue(Dialogue convisation)
    {
        
        customerIMG.sprite = grandmaImg;
        //Debug.Log("Starting conversation between "+ player.name + "and " + customer.name);
        animator.SetBool("IsOpen", true);
        
        //playerAnimator.SetBool("PlayerActive", true);
        customerAnimator.SetBool("CustomerActive", true);

        sentenceCounter = 0;
        //player name
        //playerNameText.text = convisation.playerName;
        //customer name
        customerNameText.text = convisation.customerName;

        sentences.Clear();
        foreach (string sentence in convisation.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
         
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        
        //Debug.Log(sentence);
        //dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
    }

    IEnumerator TypeSentence(string sentence)
    {
      for(int i = 0; i < customerTalking.Length; i++)
        {
           if(sentenceCounter == customerTalking[i])
           {
                Debug.Log("Customer");
                playerNameplate.SetActive(false);
                customerNameplate.SetActive(true);
                //playerIMG.color = colorChange;
                customerIMG.color = Color.white;
           }  
        }

        for(int j = 0; j < customerTalking.Length + 1; j++)
        {
           if(sentenceCounter == playerTalking[j])
           {
                Debug.Log("Player");
                playerNameplate.SetActive(false);
                customerNameplate.SetActive(true);
                customerIMG.color = Color.white;
                //playerIMG.color = Color.white;
           }
              
        }
       
        sentenceCounter++; 
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation.");
        cDS.NCOff();
        animator.SetBool("IsOpen", false);
        //playerAnimator.SetBool("PlayerActive", false);
        customerAnimator.SetBool("CustomerActive", false);
        Time.timeScale = 1f;
    }
}