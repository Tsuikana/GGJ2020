using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text valueTimer;
    public Text valueHunting;
    public Text valueGathering;
    public Text valueMobility;
    public Text valueScouting;
    public RectTransform progressHunger;
    public RectTransform progressThirst;
    public RectTransform progressWarmth;
    public Text valueParts;

    private GameManager gameMan;
    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    public void SetDefaults()
    {
        gameMan = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        var hunting = gameMan.partyMan.partyHunting;
        var gathering = gameMan.partyMan.partyGathering;
        var mobility = gameMan.partyMan.partyMobility;
        var scouting = gameMan.partyMan.partyScouting;
        var hunger = gameMan.partyMan.partyHunger / gameMan.partyMan.PartyHungerMax;
        var thirst = gameMan.partyMan.partyThirstiness / gameMan.partyMan.PartyThirstinessMax;
        var warmth = (float)gameMan.partyMan.partyWarmth / (float)gameMan.partyMan.PartyWarmthMax;
        var parts = gameMan.partyMan.partyParts;
        
        //Display in seconds for now
        valueTimer.text = elapsedTime.ToString() + "s";
        valueHunting.text = hunting.ToString();
        valueGathering.text = gathering.ToString();
        valueMobility.text = mobility.ToString();
        valueScouting.text = scouting.ToString();
        progressHunger.localScale = new Vector3(hunger, progressHunger.localScale.y, progressHunger.localScale.z);
        progressThirst.localScale = new Vector3(thirst, progressThirst.localScale.y, progressThirst.localScale.z);
        progressWarmth.localScale = new Vector3(warmth, progressWarmth.localScale.y, progressWarmth.localScale.z);
        valueParts.text = parts.ToString();
    }
}
