using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationManager : MonoBehaviour
{
    private GameObject playerNameplate;
    private GameObject customerNameplate;

    //image gameobject holders
    private GameObject imageObjectOne;
    private GameObject imageObjectTwo;
    //images
    private Image playerIMG;
    private Image customerIMG;
    private Color colorChange;

    //textmesh pro GUI gameobject holders
    private GameObject textOne;
    private GameObject textTwo;
    private GameObject textThree; 
    //textmesh pro GUIs
    private TextMeshProUGUI playerNameText;
    private TextMeshProUGUI customerNameText;
    private TextMeshProUGUI dialogueText;

    //Animator gameobject holders
    private GameObject animOne;
    private GameObject animTwo;
    private GameObject animThree;
    //Animators
    private Animator animator;
    private Animator playerAnimator;
    private Animator customerAnimator;

    private Queue<string> convSentences;

    private Customer customer;
    private Sprite customerSprite;
    private string foodName;

    //public Button butn;

    private GameObject chooseDialogue;
    private ChooseDialogueSystem cDS;

    private int sentenceCounter = 0;
    public int[] playerTalking;
    public int[] customerTalking;

    // Start is called before the first frame update
    void Start()
    {
        playerTalking = new int[] {0, 1, 4};
        customerTalking = new int[] {2, 3};

        playerNameplate = GameObject.Find("PlayerName");
        customerNameplate = GameObject.Find("GhostName");

        imageObjectOne = GameObject.Find("PlayerDialogueImage");
        playerIMG = imageObjectOne.GetComponent<Image>();
        
        imageObjectTwo = GameObject.Find("CustomerDialogueImage");
        customerIMG = imageObjectTwo.GetComponent<Image>();

        textOne = GameObject.Find("PNameText");
        playerNameText = textOne.GetComponent<TextMeshProUGUI>();
        textTwo = GameObject.Find("GNameText");
        customerNameText = textTwo.GetComponent<TextMeshProUGUI>();
        textThree = GameObject.Find("DialogueText");
        dialogueText = textThree.GetComponent<TextMeshProUGUI>();
 
        animOne = GameObject.Find("DialogueElements");
        animator = animOne.GetComponent<Animator>();
        animTwo = GameObject.Find("PlayerDialogueImage");
        playerAnimator = animTwo.GetComponent<Animator>();
        animThree = GameObject.Find("CustomerDialogueImage");
        customerAnimator = animThree.GetComponent<Animator>();

        /* Button btn = butn.GetComponent<Button>();
        btn.onClick.AddListener(DisplayNextConversationSentence); */

        chooseDialogue = GameObject.Find("DialogueElements");
        cDS = chooseDialogue.GetComponent<ChooseDialogueSystem>();

        convSentences = new Queue<string>();
        colorChange.a = 1;
        colorChange = Color.grey;
    }

    public void SetCustomer(Customer _customer)
    {
        customer = _customer;
    }

    public void WhatFoodIsIt(FoodItem.FoodType foodtype)
    {
        CustomerManager mngr = customer.GetCustomerManager();
        GameObject foodPrefab = mngr.FindFoodFromEnum(foodtype);

        Debug.Log("Food Sprite " + foodPrefab.GetComponent<FoodItem>().foodSprite.name);
        foodName = foodPrefab.GetComponent<FoodItem>().foodSprite.name;
    }

    public void SetCustomerSprite(Sprite cusImage)
    {
        customerSprite = cusImage;
        customerIMG.sprite = cusImage;
    }

    public void StartConversation(Conversations convisation)
    {

        CustomerManager mngr = customer.GetCustomerManager();
        mngr.GetCurrentCustomer().AmSpokenTo();

        //Debug.Log("Starting conversation between "+ player.name + "and " + customer.name);
        animator.SetBool("IsOpen", true);
        playerAnimator.SetBool("PlayerActive", true);
        customerAnimator.SetBool("CustomerActive", true);

        sentenceCounter = 0;
        //player name
        playerNameText.text = convisation.playerName;
        //customer name
        customerNameText.text = convisation.customerName;
        convSentences.Clear();
        foreach (string sentence in convisation.convSentences)
        {
            convSentences.Enqueue(sentence);
        }
        DisplayNextConversationSentence();
         
    }

    public void DisplayNextConversationSentence()
    {
        if(convSentences.Count == 0)
        {
            EndConversation();
            return;
        }
        string sentence = convSentences.Dequeue();
        sentence = sentence.Replace("FOODITEM",foodName);
    

        //Debug.Log(sentence);
        //dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeConversation(sentence));
        
    }

    IEnumerator TypeConversation(string sentence)
    {
      for(int i = 0; i < customerTalking.Length; i++)
        {
           if(sentenceCounter == customerTalking[i])
           {
            
                Debug.Log("Customer");
                customerIMG.sprite = customerSprite;
                playerNameplate.SetActive(false);
                customerNameplate.SetActive(true);
                playerIMG.color = colorChange;
                customerIMG.color = Color.white;
           }  
        }

        for(int j = 0; j < playerTalking.Length; j++)
        {
           if(sentenceCounter == playerTalking[j])
           {
                Debug.Log("Player");
                customerIMG.sprite = customerSprite;
                playerNameplate.SetActive(true);
                customerNameplate.SetActive(false);
                customerIMG.color = colorChange;
                playerIMG.color = Color.white;
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

    void EndConversation()
    {
        Debug.Log("End of conversation.");
        cDS.NCOff();
        animator.SetBool("IsOpen", false);
        playerAnimator.SetBool("PlayerActive", false);
        customerAnimator.SetBool("CustomerActive", false);
        FindObjectOfType<CanvasManager>().EnableUIElements();

        Time.timeScale = 1f;
    }
}