using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    bool hasNewTarget;
    GameManager gameMan;
    Vector2 newMoveTarget = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
        hasNewTarget = false;
        newMoveTarget = Vector2.zero;
        gameMan = Camera.main.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMan.cursMan.hasNewTarget)
        {
            hasNewTarget = true;
            gameMan.cursMan.hasNewTarget = false;
            newMoveTarget = gameMan.cursMan.newCursorTarget;
        }

        if (hasNewTarget)
        {
            if (ShouldMoveToTarget(newMoveTarget))
            {
                transform.position = (Vector2)transform.position +
                    (newMoveTarget - (Vector2)transform.position).normalized *
                    gameMan.charMan.TimeScaledMoveSpeed;
            }
            else
            {
                transform.position = newMoveTarget;
                hasNewTarget = false;
            }
        }
    }

    bool ShouldMoveToTarget(Vector2 moveTarget)
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
