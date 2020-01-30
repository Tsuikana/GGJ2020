using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GirlStats : MonoBehaviour
{
    public float happiness;
    public float hunting;
    public float gathering;
    public float scouting;
    public float mobility;

    public string element;
    public string type;

    void Start()
    {
        switch (element)
        {
            case "Fire":
                hunting += Convert.ToSingle(hunting * 0.25);
                break;
            case "Wind":
                scouting += Convert.ToSingle(scouting * 0.25);
                break;
            case "Lightning":
                mobility += Convert.ToSingle(mobility * 0.25);
                break;
            case "Water":
                gathering += Convert.ToSingle(gathering * 0.25);
                break;
            case "Earth":
                hunting += Convert.ToSingle(hunting * 0.05);
                gathering += Convert.ToSingle(gathering * 0.05);
                mobility += Convert.ToSingle(mobility * 0.05);
                scouting += Convert.ToSingle(scouting * 0.05);
                break;
            default:
                break;
        }
    }

    public void decreaseHappiness(float lostHappiness)
    {
        happiness -= lostHappiness;
    }
}
