using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerGirls : MovementController
{
    public MovementControllerMc targetMoveCtrlMc;
    public MCManager mc;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (this.gameObject.GetComponent<GirlStats>().pickedUp)
        {
            //Set movement position target to index 0 of target object's previous position list
            if (targetMoveCtrlMc.hasMoveTarget && targetMoveCtrlMc.prevPosList.Count == targetMoveCtrlMc.maxPosListSize)
            {
                hasMoveTarget = true;
                newMoveTarget = targetMoveCtrlMc.prevPosList[0];
            }

            //Move
            base.Update();
        }
    }

    protected override void SetDefaults()
    {
        base.SetDefaults();
        targetMoveCtrlMc = FindObjectOfType<MovementControllerMc>();
        target = targetMoveCtrlMc.gameObject;
        mc = FindObjectOfType<MCManager>();
    }
}
