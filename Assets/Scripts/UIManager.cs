using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameManager gameMan;
    public GameObject promptHandler;
    public GameObject inefficientHandler;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDefaults()
    {
        gameMan = FindObjectOfType<GameManager>();
        promptHandler = FindObjectOfType<PromptHandler>().gameObject;
        promptHandler.SetActive(false);
        inefficientHandler = FindObjectOfType<InefficientHandler>().gameObject;
        inefficientHandler.SetActive(false);
    }

    public void PromptRecruit(GameObject girl)
    {
        if (gameMan.partyMan.partyParts >= 2)
        {
            gameMan.partyMan.IsBusy = true;
            gameMan.partyMan.AddPendingGirl(girl);
            promptHandler.SetActive(true);
            promptHandler.GetComponent<PromptHandler>().ShowPrompt();
        }
        else
        {
            gameMan.partyMan.isBusy = true;
            inefficientHandler.SetActive(true);
            promptHandler.GetComponent<InefficientHandler>().ShowPrompt();
        }
    }
}
