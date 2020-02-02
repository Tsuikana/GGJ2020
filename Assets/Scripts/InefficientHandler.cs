using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InefficientHandler : MonoBehaviour
{
    public Button closeButton;
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
        rectXform.localPosition = new Vector3(0f, 0f, rectXform.localPosition.z);

        transform.position.Set(0f, 0f, transform.position.z);
        gameMan = FindObjectOfType<GameManager>();
        closeButton.onClick.AddListener(OnClickClose);
    }

    public void ShowPrompt()
    {
        //SetDefaults();
    }

    public void ClosePrompt()
    {
        gameMan.partyMan.IsBusy = false;
        gameObject.SetActive(false);
    }

    void OnClickClose()
    {
        ClosePrompt();
    }
}
