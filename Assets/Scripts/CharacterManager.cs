using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float minMoveThresh = 0.01f;

    public float TimeScaledMoveSpeed {
        get
        {
            return moveSpeed * Time.deltaTime;
        }
    }

    public float FixedTimeScaledMoveSpeed
    {
        get
        {
            return moveSpeed * Time.fixedDeltaTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
