using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Richagi : MonoBehaviour
{
    private bool isActive = false;

    [SerializeField]
    private GameObject richagiEffect;

    [SerializeField]
    public GameObject player;

    [SerializeField]
    private GameObject guideText;

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
        return distance < 3f;
    }

    // when user clicks e, and is close to element, set richagiEffect active
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerCloseToPlatform())
        {
            isActive = !isActive;
            richagiEffect.SetActive(isActive);
            if (isActive)
            {
                transform.rotation *= Quaternion.Euler(40f, 0f, 0f);
            } else
            {
                transform.rotation *= Quaternion.Euler(-40f, 0f, 0f);
            }
        }
    }

    void FixedUpdate()
    {
        // if player is near platform, show guide text
        if (IsPlayerCloseToPlatform())
        {
            guideText.SetActive(true);
        }
        else
        {
            guideText.SetActive(false);
        }
    }
}
