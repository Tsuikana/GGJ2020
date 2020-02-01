using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public GameObject pendingPartyGirl;
    public List<GameObject> girlList;
    public float baseInteractDuration = 2f;
    public float partyHunting;
    public float partyGathering;
    public float partyScouting;
    public float partyMobility;
    public float partyHunger;
    public float partyThirstiness;
    public float warmth;
    public float parts;
    public bool isBusy;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
        isBusy = false;
        pendingPartyGirl = null;    
        girlList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPendingGirl(GameObject newGirl) { pendingPartyGirl = newGirl; }
    public void RemovePendingGirl() { pendingPartyGirl = null; }
    public bool IsBusy {
        get { return isBusy; }
        set { isBusy = value; }
    }

    public void addGirlToParty()
    {
        pendingPartyGirl.GetComponent<MovementControllerGirls>().FollowMc();

        foreach (var girl in girlList){
            girl.GetComponent<GirlController>().GirlAdded(pendingPartyGirl);
        }
        girlList.Add(pendingPartyGirl);
        var newGirlStats = pendingPartyGirl.GetComponent<GirlStats>();

        if (newGirlStats)
        {
            newGirlStats.AddToParty();
            partyGathering += newGirlStats.gathering;
            partyHunting += newGirlStats.hunting;
            partyScouting += newGirlStats.scouting;
            partyMobility += newGirlStats.mobility;
            partyHunger += newGirlStats.hunger;
            partyThirstiness += newGirlStats.thristiness;
        }
    }

    public void girlLeaveParty(GameObject byeGirl)
    {
        var byeGirlStats = byeGirl.GetComponent<GirlStats>();
        if (byeGirlStats)
        {
            partyGathering -= byeGirlStats.gathering;
            partyHunting -= byeGirlStats.hunting;
            partyScouting -= byeGirlStats.scouting;
            partyMobility -= byeGirlStats.mobility;
            partyHunger += byeGirlStats.hunger;
            partyThirstiness += byeGirlStats.thristiness;
        }
        girlList.Remove(byeGirl);
        byeGirl.GetComponent<GirlController>().DestroyGirl();
    }

    public void gather(GameObject target)
    {
        Debug.Log("Gathering " + target.name);
        Destroy(target);
        
        foreach (var girl in girlList)
        {
            girl.GetComponent<GirlController>().EnvironmentPickup("Gather");
        }
    }

    public void hunt(GameObject target)
    {
        Debug.Log("Hunting " + target.name);
        Destroy(target);
		
        foreach (var girl in girlList)
        {
            girl.GetComponent<GirlController>().EnvironmentPickup("Hunt");
        }
    }
}
