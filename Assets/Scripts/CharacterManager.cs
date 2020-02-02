using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float moveSpeedMultiplier = 0.1f;
    public float minMoveThresh = 0.01f;
    private float defaultMoveSpeed = 2f;
    private GameManager gameMan;

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
        defaultMoveSpeed = moveSpeed;
        gameMan = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = defaultMoveSpeed + gameMan.partyMan.partyMobility * moveSpeedMultiplier;
    }
}
