using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxBehaviour : MonoBehaviour
{
    public RaycastBehaviour Reticle;
    public float Speed = 1.2f;

    // Update is called once per frame
    void Update()
    {
        var trackingPosition = Reticle.transform.position;
        if (Vector3.Distance(trackingPosition, transform.position) < 0.1)
        {
            return;
        }

        var lookRotation = Quaternion.LookRotation(trackingPosition - transform.position);
        transform.rotation =
            Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        transform.position =
            Vector3.MoveTowards(transform.position, trackingPosition, Speed * Time.deltaTime);
    }
}
