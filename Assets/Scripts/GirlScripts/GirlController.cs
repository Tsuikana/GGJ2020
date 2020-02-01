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
        //SetDefaults();
    }

    public void UpdateHappiness(float amount)
    {
        ownStats.currentHappiness -= amount;
        if (ownStats.currentHappiness <= 0)
        {
            partyManager.girlLeaveParty(this.gameObject);
        }
    }

    public void DestroyGirl()
    {
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
            if (ownFaction)
            {
                ownFaction.ReactToGirl(newGirlStats.faction);
            }
            else
            {
                Debug.Log("Cant find faction.");
            }
        }
    }

    public void EnvironmentPickup(GameObject pickedUpObject)
    {
        // Do things depending on what object was picked up
    }

    public void EnvironmentEffect()
    {

    }

    public void SetDefaults() 
    {
        partyManager = Camera.main.GetComponent<PartyManager>();
        ownStats = this.gameObject.GetComponent<GirlStats>();
        ownStats.SetDefaults();
        ownFaction = this.gameObject.GetComponent<Faction>();
    }
}
