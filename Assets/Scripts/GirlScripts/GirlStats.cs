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
    // Add to party manager
    public float hunger;
    public float thristiness;
    public bool pickedUp;

    public enum Roles { Nurse, Gatherer, Hunter, Scouter, Plane };
    public Roles role;

    public enum Factions { Red, Blue, Green, Yellow, Purple };
    public Factions faction;

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
        switch (faction.ToString())
        {
            case "Red":
                this.gameObject.AddComponent<RedFaction>();
                hunting += Convert.ToSingle(hunting * 0.25);
                break;
            case "Green":
                this.gameObject.AddComponent<GreenFaction>();
                scouting += Convert.ToSingle(scouting * 0.25);
                break;
            case "Yellow":
                this.gameObject.AddComponent<YellowFaction>();
                mobility += Convert.ToSingle(mobility * 0.25);
                break;
            case "Blue":
                this.gameObject.AddComponent<BlueFaction>();
                gathering += Convert.ToSingle(gathering * 0.25);
                break;
            case "Purple":
                this.gameObject.AddComponent<PurpleFaction>();
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
