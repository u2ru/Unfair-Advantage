using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private bool initialTransitionDone = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // if level is not 1, do nothing
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 1)
        {
            return;
        }
        // check if 'Player' is within 3 units of the flag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < 3f && !initialTransitionDone)
            {
                // move flag up by 10 units on z-axis
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10f);
                initialTransitionDone = true;
                MakeItemAppear.ShowByName("level_1_bomb");
            }
        }
    }
}
