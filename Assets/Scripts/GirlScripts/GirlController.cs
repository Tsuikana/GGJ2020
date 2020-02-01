using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlController : MonoBehaviour
{
    private GirlStats ownStats;
    private PartyManager partyManager;
    private Faction ownFaction;
    private Role ownRole;

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
                Debug.Log("Can't find faction.");
            }
        }
    }

    public void BecomeActive()
    {
        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Debug.Log(this.gameObject.GetComponent<Rigidbody2D>().constraints);
    }

    public void EnvironmentPickup(string pickedUpObject)
    {
        ownRole.ReactToPickup(pickedUpObject);
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
        ownRole = this.gameObject.GetComponent<Role>();
    }
}
