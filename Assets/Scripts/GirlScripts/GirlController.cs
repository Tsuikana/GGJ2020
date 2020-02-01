using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlController : MonoBehaviour
{
    private GirlStats ownStats;
    private PartyManager partyManager;
    private MCManager mcManager;

    void Start()
    {
        SetDefaults();
    }

    // Recieve action done by MC (Called from Game Manager)
    public void ActionTaken()
    {
        switch (ownStats.faction)
        {
            case "Red":
                break;
            case "Green":
                break;
            case "Yellow":
                break;
            case "Blue":
                break;
            case "Purple":
                break;
            default:
                break;
        }

        UpdateHappiness(1);
    }

    void UpdateHappiness(float amount)
    {
        ownStats.currentHappiness -= amount;
        if (ownStats.currentHappiness <= 0)
        {
            partyManager.girlLeaveParty(this.gameObject);
        }
        // Play leave animation
        Destroy(this.gameObject);
    }

    void SetDefaults() 
    {
        partyManager = Camera.main.GetComponent<PartyManager>();
        ownStats = this.gameObject.GetComponent<GirlStats>();
        mcManager = FindObjectOfType<MCManager>();
    }
}
