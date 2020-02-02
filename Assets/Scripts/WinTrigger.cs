using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private int mcLayer;

    // Start is called before the first frame update
    void Start()
    {
        mcLayer = LayerMask.NameToLayer("mc");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == mcLayer)
        {
            FindObjectOfType<GameManager>().WinGame();
        }
    }
}
