using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformToRight : MonoBehaviour
{
    // get player object
    public GameObject player;

    // godown platform reference
    public GameObject otherPlatform;

    // Exposed so you can tweak in the Inspector.
    // Increase this to make the platform move faster immediately.
    [SerializeField]
    private float platformMoveSpeed = 20f;

    // Optional: acceleration while player is near (units/sec^2). Set to 0 for no acceleration.
    [SerializeField]
    private float acceleration = 400f;

    // Optional: maximum speed when accelerating.
    [SerializeField]
    private float maxSpeed = 100f;

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
        return distance < 5f;
    }

    void FixedUpdate()
    {
        // if player is near platform, optionally accelerate and move platform to the right
        if (IsPlayerCloseToPlatform())
        {
            if (acceleration > 0f)
            {
                platformMoveSpeed = Mathf.Min(platformMoveSpeed + acceleration * Time.fixedDeltaTime, maxSpeed);
            }

            transform.Translate(Vector3.right * Time.fixedDeltaTime * this.platformMoveSpeed);

            // shjow otherPlatform
            otherPlatform.SetActive(true);
        }
    }
}
