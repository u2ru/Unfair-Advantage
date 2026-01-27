using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingOnContact : MonoBehaviour
{
    // get player object
    public GameObject player;

    private float platformMoveSpeed = 100f;

    private bool hasFallen = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    bool IsPlayerCloseToPlatform()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        return distance < 2f;
    }

    void FixedUpdate()
    {
        // if player is near platform, optionally accelerate and move platform to the right
        if (IsPlayerCloseToPlatform() && !hasFallen)
        {
            hasFallen = true;

            transform.Translate(Vector3.down * Time.fixedDeltaTime * this.platformMoveSpeed);
        }
    }
}
