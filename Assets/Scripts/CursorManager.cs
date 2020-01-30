using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public bool hasNewTarget;
    public Vector2 newCursorTarget;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
        hasNewTarget = false;
        newCursorTarget = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                //Debug.Log("Target Point: " + hit.point);
                newCursorTarget = hit.point;
                hasNewTarget = true;
            }
        }
    }
}
