using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

public class TouchMgr : MonoBehaviour
{
    private Camera ARCamera;
    public GameObject placeObject;
    public Text measureLable;
    private Anchor prevAnchor;
    void Start()
    {
        ARCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
        TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (touch.phase == TouchPhase.Began
        && Frame.Raycast(touch.position.x, 
        touch.position.y, 
        raycastFilter, 
        out hit))
        {
            var anchor = hit.Trackable.CreateAnchor(hit.Pose);

            if(prevAnchor == null){
                prevAnchor = anchor;
            }

            float dist = Vector3.Distance(prevAnchor.transform.position, 
            anchor.transform.position);
            measureLable.text = dist.ToString("#0.00m");
            prevAnchor = anchor;

            GameObject obj = Instantiate(placeObject, 
            hit.Pose.position, 
            Quaternion.identity, 
            anchor.transform);

            var rot = Quaternion.LookRotation(ARCamera.transform.position
            - hit.Pose.position);

            obj.transform.rotation = Quaternion.Euler(ARCamera.transform.position.x, 
            rot.eulerAngles.y, 
            ARCamera.transform.position.z);
        }
    }
}
