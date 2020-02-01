using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterRole : Role
{
    public override void ReactToPickup(string objectPickedUp)
    {
        Debug.Log(string.Format("Hunter Reacting to {0}", objectPickedUp));
    }
}
