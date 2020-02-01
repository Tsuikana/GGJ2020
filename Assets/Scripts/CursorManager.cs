using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public bool hasMoveTarget;
    public bool rMouseDown;
    public GameObject clickTarget;

    private int bgLayerId;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
        hasMoveTarget = false;
        rMouseDown = false;
        clickTarget = null;
        bgLayerId = LayerMask.NameToLayer("background");
    }

    // Update is called once per frame
    void Update()
    {
        // LMB
        if (Input.GetMouseButtonDown(0))
        {

        }

        // Rest clickTarget on RMB down frame
        if (Input.GetMouseButtonDown(1))
        {
            clickTarget = null;
        }

        // Stores RMB click point and target object
        if (Input.GetMouseButton(1))
        {
            var hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                // Having a clickTarget means we've selected something, so we don't want to move on holding RMB
                if (hit.collider != null && clickTarget == null)
                {
                    // Checks for hitting non-BG objects on frame that RMB is clicked
                    if (!hit.collider.gameObject.layer.Equals(bgLayerId) && !rMouseDown)
                    {
                        clickTarget = hit.collider.gameObject;
                    }
                    SetMoveTarget(hit.point);
                }
            }
            rMouseDown = true;
        }
        else
        {
            rMouseDown = false;
        }
    }

    private void SetMoveTarget(Vector2 targetPoint)
    {
        //Debug.Log("Target Point: " + hit.point);
        transform.position = targetPoint;
        hasMoveTarget = true;
    }
}
