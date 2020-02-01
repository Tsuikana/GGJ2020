using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GirlStats : MonoBehaviour
{
    public float maxHappiness;
    public float currentHappiness;
    public float hunting;
    public float gathering;
    public float scouting;
    public float mobility;
    public Boolean pickedUp;

    public string faction;
    public string role;

    void Start()
    {
        SetDefaults();
        SetFaction();
    }

    public void AddToParty()
    {
        pickedUp = true;
    }

    void SetDefaults()
    {
        pickedUp = false;
        currentHappiness = maxHappiness;
    }

    void SetFaction()
    {
        // Probably colour the girl to the faction's colour here as well
        switch (faction)
        {
            case "Red":
                hunting += Convert.ToSingle(hunting * 0.25);
                break;
            case "Green":
                scouting += Convert.ToSingle(scouting * 0.25);
                break;
            case "Yellow":
                mobility += Convert.ToSingle(mobility * 0.25);
                break;
            case "Blue":
                gathering += Convert.ToSingle(gathering * 0.25);
                break;
            case "Purple":
                hunting += Convert.ToSingle(hunting * 0.05);
                gathering += Convert.ToSingle(gathering * 0.05);
                mobility += Convert.ToSingle(mobility * 0.05);
                scouting += Convert.ToSingle(scouting * 0.05);
                break;
            default:
                break;
        }
    }
}
