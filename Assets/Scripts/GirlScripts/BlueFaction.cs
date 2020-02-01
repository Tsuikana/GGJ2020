using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFaction : Faction
{
    // Start is called before the first frame update
    public override void ReactToGirl(GirlStats.Factions newFaction)
    {
        if (newFaction.ToString().Equals("Purple")) { this.gameObject.GetComponent<GirlController>().UpdateHappiness(-1); }
    }
}
