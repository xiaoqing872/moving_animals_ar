using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Linq;

public class RaycastBehaviour : MonoBehaviour
{
    public GameObject Child;
    public ARSessionOrigin ARSessionOrigin;

    public ARPlane CurrentPlane;

    // Start is called before the first frame update
    void Start()
    {
        if (Child == null)
        {
            Child = transform.GetChild(0).gameObject;
        }

        if (ARSessionOrigin == null)
        {
            ARSessionOrigin = GetComponent<ARSessionOrigin>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        ARSessionOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, TrackableType.PlaneWithinBounds);

        CurrentPlane = null;
        ARRaycastHit? hit = null;
        if (hits.Count > 0)
        {
            // If you don't have a locked plane already...
            var lockedPlane = ARSessionOrigin.GetComponent<SpawningPlaneManager>().LockedPlane;
            hit = lockedPlane == null
                // ... use the first hit in `hits`.
                ? hits[0]
                // Otherwise use the locked plane, if it's there.
                : hits.SingleOrDefault(x => x.trackableId == lockedPlane.trackableId);
        }

        if (hit.HasValue)
        {
            CurrentPlane = ARSessionOrigin.GetComponent<SpawningPlaneManager>().PlaneManager.GetPlane(hit.Value.trackableId);
            // Move this reticle to the location of the hit.
            transform.position = hit.Value.pose.position;
        }
        Child.SetActive(CurrentPlane != null);
    }
}