using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float lerpMultiplier = 2f;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
        //Find MC
        target = FindObjectOfType<MovementControllerMc>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = Vector2.Lerp((Vector2)transform.position, target.transform.position, lerpMultiplier * Time.deltaTime);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}
