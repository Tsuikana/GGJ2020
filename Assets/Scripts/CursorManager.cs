using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public bool hasNewTarget;
    public bool mouseDown;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
        hasNewTarget = false;
        mouseDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Stores RMB target point
        if (Input.GetMouseButton(1))
        {
            mouseDown = true;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                //Debug.Log("Target Point: " + hit.point);
                transform.position = hit.point;
                hasNewTarget = true;
            }
        }
        else
        {
            mouseDown = false;
        }
    }
}
