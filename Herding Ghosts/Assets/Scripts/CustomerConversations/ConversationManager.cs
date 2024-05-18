using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationManager : MonoBehaviour
{
    [SerializeField]
    public GameObject panelButton;

    [SerializeField]
    private GameObject playerNameplate;
    [SerializeField]
    private GameObject customerNameplate;

    //image gameobject holders
    [SerializeField]
    private GameObject imageObjectOne;
    [SerializeField]
    private GameObject imageObjectTwo;
    //images
    [SerializeField]
    private Image playerIMG;
    [SerializeField]
    private Image customerIMG;
    [SerializeField]
    private Color colorChange;

    //textmesh pro GUI gameobject holders
    [SerializeField]
    private GameObject textOne;
    [SerializeField]
    private GameObject textTwo;
    [SerializeField]
    private GameObject textThree; 
    //textmesh pro GUIs
    [SerializeField]
    private TextMeshProUGUI playerNameText;
    [SerializeField]
    private TextMeshProUGUI customerNameText;
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    //Animator gameobject holders
    [SerializeField]
    private GameObject animOne;
    [SerializeField]
    private GameObject animTwo;
    [SerializeField]
    private GameObject animThree;
    //Animators
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private Animator customerAnimator;

    [SerializeField]
    private Queue<string> convSentences;

    [SerializeField]
    private Customer customer;
    [SerializeField]
    private Sprite customerSprite;
    [SerializeField]
    private string foodName;

    //public Button butn;

    [SerializeField]
    private GameObject chooseDialogue;
    [SerializeField]
    private ChooseDialogueSystem cDS;

    [SerializeField]
    private int sentenceCounter = 0;
    [SerializeField]
    public int[] playerTalking;
    [SerializeField]
    public int[] customerTalking;

    [SerializeField]
    public bool conversationActive;

    // Start is called before the first frame update
    void Awake()
    {
        playerTalking = new int[] {0, 1, 4};
        customerTalking = new int[] {2, 3};

        playerNameplate = GameObject.Find("PlayerName");
        customerNameplate = GameObject.Find("GhostName");
        panelButton = GameObject.Find("PanelBtn");

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

        Debug.Log("Food Sprite " + foodPrefab.GetComponent<FoodItem>().name);
        foodName = foodPrefab.GetComponent<FoodItem>().name;
    }

    public void SetCustomerSprite(Sprite cusImage)
    {
        customerSprite = cusImage;
        customerIMG.sprite = cusImage;
    }

    public void StartConversation(Conversations convisation)
    {

        conversationActive = true;

        CustomerManager mngr = customer.GetCustomerManager();
        mngr.GetCurrentCustomer().AmSpokenTo();

        //Debug.Log("Starting conversation between "+ player.name + "and " + customer.name);
        animator.SetBool("IsOpen", true);
        playerAnimator.SetBool("PlayerActive", true);
        customerAnimator.SetBool("CustomerActive", true);

        panelButton.SetActive(true);

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
        //sentence = sentence.Replace("FOODITEM","<b><color=blue>FOODITEM</color></b>"); // has glitch during text output
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

    public void EndConversation()
    {
        Debug.Log("End of conversation.");
        cDS.NCOff();
        animator.SetBool("IsOpen", false);
        playerAnimator.SetBool("PlayerActive", false);
        customerAnimator.SetBool("CustomerActive", false);
        FindObjectOfType<CanvasManager>().EnableUIElements();
        Time.timeScale = 1f;
        panelButton.SetActive(false);
        conversationActive = false;
    }

    public bool GetCustomerTalk()
    {
        return customer.isSpokenTo;
    }
}