using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private float unitsInY;

    [SerializeField]
    private float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveElevator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator MoveElevator()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + new Vector3(0f, unitsInY, 0f);
        float journeyLength = Vector3.Distance(startPos, targetPos);

        // This loop makes it repeat forever without stopping
        while (true)
        {
            // MOVE UP
            float startTime = Time.time;
            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                float fraction = ((Time.time - startTime) * speed) / journeyLength;
                transform.position = Vector3.Lerp(startPos, targetPos, fraction);
                yield return null;
            }
            transform.position = targetPos;

            // MOVE DOWN
            startTime = Time.time;
            while (Vector3.Distance(transform.position, startPos) > 0.01f)
            {
                float fraction = ((Time.time - startTime) * speed) / journeyLength;
                transform.position = Vector3.Lerp(targetPos, startPos, fraction);
                yield return null;
            }
            transform.position = startPos;
        }
    }
}
