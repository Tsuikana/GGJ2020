using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptHandler : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;
    public GameManager gameMan;

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
        var rectXform = GetComponent<RectTransform>();
        rectXform.localPosition = new Vector3(rectXform.localPosition.x, 0f, rectXform.localPosition.z);

        transform.position.Set(transform.position.x, 0f, transform.position.z);
        gameMan = FindObjectOfType<GameManager>();
        yesButton.onClick.AddListener(OnClickYes);
        noButton.onClick.AddListener(OnClickNo);
    }

    public void ClosePrompt()
    {
        gameMan.partyMan.isBusy = false;
        gameObject.SetActive(false);
    }

    void OnClickYes()
    {
        gameMan.partyMan.addGirlToParty();
        ClosePrompt();
    }

    void OnClickNo()
    {
        ClosePrompt();
    }
}
