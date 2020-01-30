﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterManager charMan;
    public CursorManager cursMan;
    public List<GameObject> girlList;
    public float partyHunting;
    public float partyGathering;
    public float partyScouting;
    public float partyMobility;
    public float hunger;
    public float thirstiness;
    public float warmth;
    public float THEME;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
        charMan = GetComponent<CharacterManager>();
        cursMan = FindObjectOfType<CursorManager>();
        girlList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addGirlToParty(GameObject newGirl)
    {
        girlList.Add(newGirl);
        var newGirlStats = newGirl.GetComponent<GirlStats>();
        if (newGirlStats)
        {
            partyGathering += newGirlStats.gathering;
            partyHunting += newGirlStats.hunting;
            partyScouting += newGirlStats.scouting;
            partyMobility += newGirlStats.mobility;
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
        }
    }
}
