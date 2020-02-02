using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlController : MonoBehaviour
{
    private GirlStats ownStats;
    private PartyManager partyManager;
    private Faction ownFaction;
    private Role ownRole;
    private Animator anim;

    void Start()
    {
        SetDefaults();
    }

    public void UpdateHappiness(float amount)
    {
        ownStats.currentHappiness -= amount;
        if (ownStats.currentHappiness <= 0)
        {
            partyManager.GirlLeaveParty(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        //anim.SetFloat("Speed", Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x));
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
    }

    public void EnvironmentPickup(PickUps.PickUpType environmentType)
    {
        ownRole.ReactToPickup(environmentType.ToString());
    }

    public void EnvironmentEffect()
    {

    }

    public void SetDefaults() 
    {
        partyManager = Camera.main.GetComponent<PartyManager>();
        ownStats = this.gameObject.GetComponent<GirlStats>();
        ownFaction = this.gameObject.GetComponent<Faction>();
        ownRole = this.gameObject.GetComponent<Role>();
        anim = this.gameObject.GetComponent<Animator>();
    }
}
