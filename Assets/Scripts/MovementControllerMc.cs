using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerMc : MovementController
{
    public bool isBusy = false;
    public float minInteractDistance = 0.2f;
    public float prevPosDistThresh = 0.02f;
    public int maxPosListSize = 10;
    public List<Vector2> prevPosList;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //Don't update if MC is interacting with something
        //if (isBusy)
        //{
        //    return;
        //}

        //Set move target as CursorTarget game object's position
        if (gameMan.cursMan.hasMoveTarget)
        {
            gameMan.cursMan.hasMoveTarget = false;

            //If the Cursor has clicked on a target and they are within interactable distance, prompt to collect
            if (gameMan.cursMan.clickTarget != null && CanInteract(gameMan.cursMan.clickTarget))
            {
                isBusy = true;
                gameMan.PromptCollectGirl();
            }
            //Set values for movement
            else
            {
                hasMoveTarget = true;
                newMoveTarget = gameMan.cursMan.transform.position;
            }
        }

        //Move
        base.Update();

        //Update previous position list
        if (travelDist > prevPosDistThresh)
        {
            travelDist = 0.0f;
            prevPosList.Add(transform.position);

            //remove entries at end of list
            if (prevPosList.Count > maxPosListSize)
            {
                prevPosList.RemoveAt(0);
            }
        }
    }

    protected override void SetDefaults()
    {
        base.SetDefaults();
        isBusy = false;
        prevPosList = new List<Vector2>();
        target = gameMan.cursMan.gameObject;
    }

    private bool CanInteract(GameObject targetObject)
    {
        if (Vector2.Distance(transform.position, targetObject.transform.position) < minInteractDistance)
        {
            return true;
        }
        return false;
    }
}
