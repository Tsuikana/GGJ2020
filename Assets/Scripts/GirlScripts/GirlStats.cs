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
    public float hungerConsumed;
    public float thristinessConsumed;
    public bool pickedUp;

    public enum Roles { Nurse, Gatherer, Hunter, Scout, Witch };
    public Roles role;

    public enum Factions { Red, Blue, Green, Yellow, Purple };
    public Factions faction;

    void Start()
    {
        SetDefaults();
    }

    public void AddToParty()
    {
        pickedUp = true;
        this.gameObject.GetComponent<GirlController>().BecomeActive();
    }

    public void SetDefaults()
    {
        pickedUp = false;
        currentHappiness = maxHappiness;
        SetFaction();
        SetRole();
    }

    void SetFaction()
    {
        var currentFaction = this.gameObject.GetComponent<Faction>();
        if (currentFaction)
        {
            Destroy(currentFaction);
        }
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

    void SetRole()
    {
        var currentRole = this.gameObject.GetComponent<Role>();
        if (currentRole)
        {
            Destroy(currentRole);
        }
        switch (role.ToString())
        {
            case "Nurse":
                this.gameObject.AddComponent<NurseRole>();
                break;
            case "Gatherer":
                this.gameObject.AddComponent<GathererRole>();
                break;
            case "Hunter":
                this.gameObject.AddComponent<HunterRole>();
                break;
            case "Scouter":
                this.gameObject.AddComponent<ScouterRole>();
                break;
            case "Witch":
                this.gameObject.AddComponent<WitchRole>();
                break;
            default:
                break;
        }
    }
}
