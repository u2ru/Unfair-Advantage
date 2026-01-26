using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    private float bounceForce = 20f;

    private string playerTag = "Player";

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(playerTag))
            return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb == null)
            return;

        // Compute local position relative to pad (local X and Z)
        Vector3 localPos = transform.InverseTransformPoint(other.transform.position);

        float halfWidth = (_collider != null ? _collider.bounds.extents.x : 0.5f);

        float halfDepth = (_collider != null ? _collider.bounds.extents.z : 0.5f);

        // Ignore if contact is outside allowed X or Z half-size
        if (Mathf.Abs(localPos.x) > halfWidth || Mathf.Abs(localPos.z) > halfDepth)
            return;

        // Apply bounce: keep horizontal velocity, replace vertical velocity
        Vector3 v = rb.velocity;
        v.y = bounceForce;
        rb.velocity = v;
    }
}
