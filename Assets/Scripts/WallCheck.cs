using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour //Check if L4 is touching a wall
{

    AI ParentScript;
    CircleCollider2D col;


    // Start is called before the first frame update
    void Start()
    {
        ParentScript = this.transform.parent.GetComponent<AI>();
        col = this.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        WallChecking();
    }

    void WallChecking()
    {

        if (col.IsTouchingLayers(LayerMask.GetMask("Walls")))
        {
            //Debug.Log("Detecting wall");
            ParentScript.SetL1TurnSpeed(6);
        }
        else
        {
            ParentScript.SetL1TurnSpeed(1f);
        }
    }
}
