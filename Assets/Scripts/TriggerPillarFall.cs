using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPillarFall : MonoBehaviour
{
    public GameObject pillar;

    private bool hasFallen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FallPillar()
    {
        float duration = 1f; // duration of the fall
        float elapsed = 0f;
        Quaternion initialRotation = pillar.transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, 0f, 80f);
        while (elapsed < duration)
        {
            pillar.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        pillar.transform.rotation = targetRotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (pillar != null && !hasFallen)
            {
                hasFallen = true;

                StartCoroutine(FallPillar());
            }
        }
    }
}
