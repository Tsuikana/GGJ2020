using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlController : MonoBehaviour
{
    private GirlStats ownStats;
    private PartyManager partyManager;
    private Faction ownFaction;

    void Start()
    {
        SetDefaults();
    }

    public void UpdateHappiness(float amount)
    {
        ownStats.currentHappiness -= amount;
        if (ownStats.currentHappiness <= 0)
        {
            partyManager.girlLeaveParty(this.gameObject);
        }
        // Play leave animation
        Destroy(this.gameObject);
    }

    public void GirlAdded(GameObject girl)
    {
        if (gameObject == girl)
            return;

        var newGirlStats = girl.GetComponent<GirlStats>();
        if (newGirlStats)
        {
            ownFaction.ReactToGirl(newGirlStats.faction);
        }
    }

    void SetDefaults() 
    {
        partyManager = Camera.main.GetComponent<PartyManager>();
        ownStats = this.gameObject.GetComponent<GirlStats>();
        ownFaction = this.gameObject.GetComponent<Faction>();
    }
}
