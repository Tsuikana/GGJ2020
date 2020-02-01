using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public bool hasMoveTarget;
    public bool rMouseDown;
    public GameObject clickTarget;

    private int bgLayerId;
    private int girlsLayerId;

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
        girlsLayerId = LayerMask.NameToLayer("girls");
    }

    // Update is called once per frame
    void Update()
    {
        // LMB
        if (Input.GetMouseButtonDown(0))
        {

        }

        // Stores RMB target point
        if (Input.GetMouseButton(1))
        {
            rMouseDown = true;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                clickTarget = null;
                if (!hit.collider.gameObject.layer.Equals(bgLayerId))
                {
                    clickTarget = hit.collider.gameObject;
                }
                SetMoveTarget(hit.point);
            }
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
