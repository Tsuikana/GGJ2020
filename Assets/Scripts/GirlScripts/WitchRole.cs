using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchRole : Role
{
    public override void ReactToPickup(string objectPickedUp)
    {
        Debug.Log(string.Format("Strike Witch Reacting to {0}", objectPickedUp));
    }
}
