using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    GameManager gameMan;
    int girlsLayerId;
    int gatherLayerId;
    int huntLayerId;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
        gameMan = FindObjectOfType<GameManager>();
        girlsLayerId = LayerMask.NameToLayer("girls");
        gatherLayerId = LayerMask.NameToLayer("gather");
        huntLayerId = LayerMask.NameToLayer("hunt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        var targetLayer = gameMan.cursMan.clickTarget.layer;

        gameMan.partyMan.IsBusy = true;
        if (targetLayer == girlsLayerId)
        {
            gameMan.uiMan.PromptRecruit(gameMan.cursMan.clickTarget);
        }
        else if (targetLayer == gatherLayerId)
        {
            gameMan.partyMan.gather(gameMan.cursMan.clickTarget);
        }
        else if (targetLayer == huntLayerId)
        {
            gameMan.partyMan.hunt(gameMan.cursMan.clickTarget);
        }
    }
}
