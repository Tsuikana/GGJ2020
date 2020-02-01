using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScouterRole : Role
{
    public override void ReactToPickup(string objectPickedUp)
    {
        Debug.Log(string.Format("Scouter Reacting to {0}", objectPickedUp));
    }
}
