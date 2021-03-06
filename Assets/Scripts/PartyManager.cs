﻿using System.Collections;
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

    public float partyHunger;
    public float partyHungerConsumed;
    public float partyThirstiness;
    public float partyThirstinessConsumed;

    public int partyWarmth;
    public int partyParts;
    public bool isBusy;
    public bool isWarming; //Only sent to true when in collider of fire
    
    public int hungerDegenPerTick = 1;
    public int thirstinessDegenPerTick = 1;
    public int warmthDegenPerTick = 1;
    public int warmthRegenPerTick = 10;

    private float survivalDegenInterval;
    private float survivalDegenCurrentTime;
    private float survivalRegenInterval;
    private float survivalRegenCurrentTime;
    private float partyHungerMax;
    private float partyThirstinessMax;
    private int partyWarmthMax;
    private int layerWarm;

    public float PartyHungerMax { get { return partyHungerMax; } }
    public float PartyThirstinessMax { get { return partyThirstinessMax; } }
    public int PartyWarmthMax { get { return partyWarmthMax; } }

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
        survivalRegenInterval = 1;
        survivalRegenCurrentTime = 0;
        partyHungerMax = partyHunger;
        partyThirstinessMax = partyThirstiness;
        partyWarmthMax = partyWarmth;
        layerWarm = LayerMask.NameToLayer("warm");
    }

    // Update is called once per frame
    void Update()
    {
        if (survivalDegenCurrentTime >= survivalDegenInterval)
        {
            partyHunger -= hungerDegenPerTick + partyHungerConsumed;
            if (partyHunger <= 0) 
            {
                Death("Hunger");
                Debug.Log("You ran out of food!"); 
            }
            Debug.Log("Lose Hunger, current hunger: " + partyHunger);
            partyThirstiness -= thirstinessDegenPerTick + partyThirstinessConsumed;
            if (partyThirstiness <= 0) 
            {
                Death("Thirstiness");
                Debug.Log("You ran out of food!"); 
            }
            Debug.Log("Lose Thirstiness, current thirstiness: " + partyThirstiness);
            if (!isWarming)
            {
                partyWarmth -= warmthDegenPerTick;
                Debug.Log("Lose Warmth, current warmth: " + partyWarmth);
                if (partyWarmth <= 0) 
                {
                    Death("Warmth");
                    Debug.Log("You've frozen!"); 
                }
            }
            survivalDegenCurrentTime = 0;
        }
        else {
            survivalDegenCurrentTime += Time.deltaTime;
        }

        if (survivalRegenCurrentTime >= survivalRegenInterval)
        {
            if (isWarming)
            {
                partyWarmth += warmthRegenPerTick;
                Debug.Log("Warming Up");
                if (partyWarmth >= PartyWarmthMax)
                {
                    partyWarmth = PartyWarmthMax;
                }
            }
            survivalRegenCurrentTime = 0;
        }
        else
        {
            survivalRegenCurrentTime += Time.deltaTime;
        }
    }

    void Death(string deathReason)
    {
        FindObjectOfType<GameManager>().LoseGame(deathReason);
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
            partyParts -= newGirlStats.costToRepair;
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
            partyThirstiness += thirstUp;
            partyHunger += hungerUp;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerWarm)
        {
            isWarming = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerWarm)
        {
            isWarming = false;
        }
    }
}
