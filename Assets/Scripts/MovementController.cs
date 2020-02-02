using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float travelDist = 0.0f;
    public float extMoveDist = 0.2f;
    public bool hasMoveTarget;
    public Vector2 newMoveTarget;

    public GameObject target;
    public GameManager gameMan;

    private int bgLayerId;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetDefaults();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //If there's a target position to move to
        if (hasMoveTarget)
        {
            //Is there ground or any objects in the way of our path
            bool traversable = IsTraversable(newMoveTarget);
            if (traversable)
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
                    if (!gameMan.cursMan.rMouseDown)
                    {
                        hasMoveTarget = false;
                    }
                }
            }
            else
            {
                hasMoveTarget = false;
            }
        }
    }

    protected virtual void SetDefaults()
    {
        hasMoveTarget = false;
        newMoveTarget = Vector2.zero;
        bgLayerId = LayerMask.NameToLayer("background");
        gameMan = Camera.main.GetComponent<GameManager>();
    }

    protected bool ShouldMoveToTarget(Vector2 moveTarget)
    {
        float dist = Vector2.Distance(transform.position, moveTarget);
        //the position after movement is greater than the minMoveThreshold
        if ((dist - gameMan.charMan.TimeScaledMoveSpeed) > gameMan.charMan.minMoveThresh)
        {
            return true;
        }
        return false;
    }

    //Check if the target vector can be actually moved to
    protected bool IsTraversable(Vector2 moveTarget)
    {
        //Raycast to next move spot to make sure it's still on the ground
        Vector3 scaledTargetOrigin = (Vector2)transform.position + (moveTarget - (Vector2)transform.position).normalized * gameMan.charMan.TimeScaledMoveSpeed;
        var hits = Physics2D.RaycastAll(scaledTargetOrigin, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.layer == bgLayerId)
            {
                return true;
            }
        }

        //Check if the character is lost in space
        hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject != gameObject && hit.collider.gameObject.layer != bgLayerId)
            {
                return false;
            }
        }

        //Attempt extended range movement
        Vector3 extendedTargetOrigin = (Vector2)transform.position + (moveTarget - (Vector2)transform.position).normalized * extMoveDist;
        hits = Physics2D.RaycastAll(extendedTargetOrigin, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.layer == bgLayerId)
            {
                return true;
            }
        }
        return false;
    }
}
