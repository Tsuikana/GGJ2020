using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameManager gameMan;
    public GameObject promptHandler;

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
        promptHandler.gameObject.SetActive(false);
    }

    public void PromptRecruit(GameObject girl)
    {
        gameMan.partyMan.IsBusy = true;
        gameMan.partyMan.AddPendingGirl(girl);
        promptHandler.gameObject.SetActive(true);
        promptHandler.GetComponent<PromptHandler>().ShowPrompt();
    }
}
