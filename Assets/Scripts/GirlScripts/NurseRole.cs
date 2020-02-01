using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseRole : Role
{
    public override void ReactToPickup(string objectPickedUp)
    {
        Debug.Log(string.Format("Healer Reacting to {0}", objectPickedUp));
    }
}
