using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public float scoutingMultiplier = 0.2f;
    public float minCameraSize = 4f;

    private float refreshElapsedTime = 0f;
    private float refreshMaxTimeDelta = 1f;
    private Camera mmCam;
    private GameManager gameMan;
    

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    public void SetDefaults()
    {
        gameMan = FindObjectOfType<GameManager>();
        mmCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Don't want to refresh minimap properties every frame
        if (refreshElapsedTime >= refreshMaxTimeDelta)
        {
            refreshElapsedTime = 0f;

            // Find all MiniMap items in scene
            var mmItems = GameObject.FindGameObjectsWithTag("MiniMap");
            // Calculate new camera size based on Scouting value
            var newCameraSize = minCameraSize + gameMan.partyMan.partyScouting * scoutingMultiplier;

            //Set the scaled sizes
            mmCam.orthographicSize = newCameraSize;
            foreach (GameObject mmItem in mmItems)
            {
                mmItem.transform.localScale = new Vector3(newCameraSize, newCameraSize, 1f);
            }
        }
        
        refreshElapsedTime += Time.deltaTime;
    }
}
