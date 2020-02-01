using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public GameObject pendingPartyGirl;
    public List<GameObject> girlList;
    public float partyHunting;
    public float partyGathering;
    public float partyScouting;
    public float partyMobility;

    public float partyHungerTotal;
    public float partyHungerConsumed;
    public float partyThirstinessTotal;
    public float partyThirstinessConsumed;

    public int partyWarmth;
    public int partyParts;
    public bool isBusy;
    public bool isWarming; //Only sent to true when in collider of fire

    private float survivalDegenInterval;
    public int hungerDegenPerTick;
    public int thirstinessDegenPerTick;
    public int warmthDegenPerTick;
    private float survivalDegenCurrentTime;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
        isBusy = false;
        isWarming = false;
        pendingPartyGirl = null;    
        girlList = new List<GameObject>();
        survivalDegenInterval = 5;
        survivalDegenCurrentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (survivalDegenCurrentTime >= survivalDegenInterval)
        {
            partyHungerTotal -= hungerDegenPerTick + partyHungerConsumed;
            Debug.Log("Lose Hunger, current hunger: " + partyHungerTotal);
            partyThirstinessTotal -= thirstinessDegenPerTick + partyThirstinessConsumed;
            Debug.Log("Lose Thirstiness, current thirstiness: " + partyThirstinessTotal);
            if (!isWarming)
            {
                partyWarmth -= warmthDegenPerTick;
                Debug.Log("Lose Warmth, current warmth: " + partyWarmth);
            }
            survivalDegenCurrentTime = 0;
        }
        else {
            survivalDegenCurrentTime += Time.deltaTime;
        }
    }

    public void AddPendingGirl(GameObject newGirl) { pendingPartyGirl = newGirl; }
    public void RemovePendingGirl() { pendingPartyGirl = null; }
    public bool IsBusy {
        get { return isBusy; }
        set { isBusy = value; }
    }

    public void AddGirlToParty()
    {
        if (girlList.Contains(pendingPartyGirl)) { return; }
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
            partyHungerConsumed += newGirlStats.hungerConsumed;
            partyThirstinessConsumed += newGirlStats.thristinessConsumed;
        }
    }

    public void GirlLeaveParty(GameObject byeGirl)
    {
        var byeGirlStats = byeGirl.GetComponent<GirlStats>();
        if (byeGirlStats)
        {
            partyGathering -= byeGirlStats.gathering;
            partyHunting -= byeGirlStats.hunting;
            partyScouting -= byeGirlStats.scouting;
            partyMobility -= byeGirlStats.mobility;
            partyHungerConsumed += byeGirlStats.hungerConsumed;
            partyThirstinessConsumed += byeGirlStats.thristinessConsumed;
        }
        girlList.Remove(byeGirl);
        byeGirl.GetComponent<GirlController>().DestroyGirl();
    }

    public void PickUp(int thirstUp, int hungerUp, int partsUp, PickUps.PickUpType environmentType)
    {
        bool safeEnvironment = true;
        if (environmentType.ToString() == "Cactus")
        {
            if (partyGathering < 50)
            {
                safeEnvironment = false;
            }
        }

        if (safeEnvironment)
        {
            partyThirstinessTotal += thirstUp;
            partyHungerTotal += hungerUp;
            partyParts += partsUp;

            foreach (var girl in girlList)
            {
                girl.GetComponent<GirlController>().EnvironmentPickup(environmentType);
            }
        }
        else
        {
            foreach (var girl in girlList)
            {
                girl.GetComponent<GirlController>().UpdateHappiness(-1);
            }
        }
    }
}
