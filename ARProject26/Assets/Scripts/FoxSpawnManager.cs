using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FoxSpawnManager : MonoBehaviour
{
    public GameObject FoxPrefab;
    public RaycastBehaviour Reticle;
    public ARSessionOrigin ARSessionOrigin;

    public FoxBehaviour Fox;

    // Update is called once per frame
    void Update()
    {
        if (Fox == null && WasTapped() && Reticle.CurrentPlane != null)
        {
            // Spawn our car at the reticle location.
            var obj = GameObject.Instantiate(FoxPrefab);
            Fox = obj.GetComponent<FoxBehaviour>();
            Fox.Reticle = Reticle;
            Fox.transform.position = Reticle.transform.position;
            ARSessionOrigin.GetComponent<SpawningPlaneManager>().LockPlane(Reticle.CurrentPlane);
        }
    }

    private bool WasTapped()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        if (Input.touchCount == 0)
        {
            return false;
        }

        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
        {
            return false;
        }

        return true;
    }
}