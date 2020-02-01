using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFaction : Faction
{
    // Start is called before the first frame update
    public override void ReactToGirl(GirlStats.Factions newFaction)
    {
        if (newFaction.ToString().Equals("Yellow")) { this.gameObject.GetComponent<GirlController>().UpdateHappiness(-1); }
    }
}
