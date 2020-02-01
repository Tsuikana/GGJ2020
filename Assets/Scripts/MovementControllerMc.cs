using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerMc : MovementController
{
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
        //Don't update if Party is interacting with something
        if (gameMan.partyMan.isBusy)
        {
            return;
        }

        //Set move target as CursorTarget game object's position
        if (gameMan.cursMan.hasMoveTarget)
        {
            gameMan.cursMan.hasMoveTarget = false;

            //If the Cursor has clicked on a target and they are within interactable distance, prompt to collect
            if (gameMan.cursMan.clickTarget != null && 
                CanInteract(gameMan.cursMan.clickTarget) && 
                !IsPickedUp(gameMan.cursMan.clickTarget))
            {
                gameMan.partyMan.isBusy = true;
                gameMan.uiMan.PromptRecruit(gameMan.cursMan.clickTarget);
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

        //Update previous position list when girls are following
        if (travelDist > prevPosDistThresh && gameMan.partyMan.girlList.Count > 0)
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
        gameMan.partyMan.isBusy = false;
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

    private bool IsPickedUp(GameObject targetObject)
    {
        var girlStats = targetObject.GetComponent<GirlStats>();
        if (girlStats && girlStats.pickedUp)
        {
            return true;
        }
        return false;
    }
}
