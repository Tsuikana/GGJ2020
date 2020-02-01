using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    GameManager gameMan;
    int girlsLayerId;
    int gatherLayerId;
    int huntLayerId;
    int collectLayerId;

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
        collectLayerId = LayerMask.NameToLayer("collect");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        var targetLayer = gameMan.cursMan.clickTarget.layer;
        if (targetLayer == girlsLayerId)
        {
            gameMan.uiMan.PromptRecruit(gameMan.cursMan.clickTarget);
        }
        else if (targetLayer == gatherLayerId)
        {
            var pickUps = gameMan.cursMan.clickTarget.GetComponent<PickUps>();
            pickUps.Use();
            //gameMan.partyMan.gather(pickUps.thirstUp, pickUps.hungerUp);
        }
        else if (targetLayer == huntLayerId)
        {
            var pickUps = gameMan.cursMan.clickTarget.GetComponent<PickUps>();
            pickUps.Use();
            //gameMan.partyMan.hunt(pickUps.thirstUp, pickUps.hungerUp);
        }
        else if (targetLayer == collectLayerId)
        {
            var pickUps = gameMan.cursMan.clickTarget.GetComponent<PickUps>();
            pickUps.Use();
            //gameMan.partyMan.collect(pickUps.partsUp);
        }
    }
}
