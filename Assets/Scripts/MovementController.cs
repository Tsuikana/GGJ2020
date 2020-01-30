using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float travelDist = 0.0f;
    public bool hasNewTarget;
    public Vector2 newMoveTarget;

    public GameObject target;
    public GameManager gameMan;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetDefaults();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //If there's a target position to move to
        if (hasNewTarget)
        {
            //Checks if object should move to the target
            if (ShouldMoveToTarget(newMoveTarget))
            {
                //Calculate where to move
                var movePosition = (Vector2)transform.position +
                    (newMoveTarget - (Vector2)transform.position).normalized *
                    gameMan.charMan.TimeScaledMoveSpeed;
                travelDist += (movePosition - (Vector2)transform.position).magnitude;
                transform.position = movePosition;
            }
            //Object is close to target, so will snap to target
            else
            {
                //Snap to target
                travelDist += (newMoveTarget - (Vector2)transform.position).magnitude;
                transform.position = newMoveTarget;
                
                //Only stop chasing target if mouse button up
                if (!gameMan.cursMan.mouseDown)
                {
                    hasNewTarget = false;
                }
            }
        }
    }

    protected virtual void SetDefaults()
    {
        hasNewTarget = false;
        newMoveTarget = Vector2.zero;
        gameMan = Camera.main.GetComponent<GameManager>();
    }

    protected bool ShouldMoveToTarget(Vector2 moveTarget)
    {
        float dist = Vector2.Distance(transform.position, moveTarget);
        if ((dist - gameMan.charMan.TimeScaledMoveSpeed) > gameMan.charMan.minMoveThresh)
        {
            //the position after movement is greater than the minMoveThreshold
            return true;
        }
        return false;
    }
}
