using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererRole : Role
{
    public override void ReactToPickup(string objectPickedUp)
    {
        Debug.Log(string.Format("Gatherer Reacting to {0}", objectPickedUp));
    }
}
