using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoinController : MonoBehaviour
{
    int score = 0;
    public int winScore = 4;
    public GameObject winText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);

            score++;

            if (score >= winScore)
            {
                winText.SetActive(true);
            }
        }
    }
}
