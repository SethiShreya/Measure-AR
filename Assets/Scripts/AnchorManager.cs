using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AnchorManager : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    private ARAnchorManager arAnchorManager;
    private ARPlaneManager arPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private List<ARAnchor> anchorList= new List<ARAnchor>();
    [SerializeField]
    private TextMeshProUGUI debugText;
    [SerializeField]
    private LineManager _lineManager;
    
    void Start()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Camera)) { }
        else
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
        

        Debug.Log("Working");
        arRaycastManager = GetComponent<ARRaycastManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
        arAnchorManager = GetComponent<ARAnchorManager>();

        if (arRaycastManager == null)
        {
            Debug.Log("Ar raycast manager not found");
        }
        if (arAnchorManager == null)
        {
            Debug.Log("Ar Anchor manager not found");
        }
        if (arPlaneManager == null)
        {
            Debug.Log("Ar Plane manager not found");
        }
    }

    [System.Obsolete]
    void Update()
    {

        if (Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            return;
        }


        if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;

            ARAnchor anchor = arAnchorManager.AddAnchor(pose);

            if (anchor == null)
            {
                Debug.Log("Error occurred while creating anchor");
            }
            else
            {
                Debug.Log("Anchor created successfully");
                debugText.text += $"Anchor created successfully at {anchor.transform.position}";
                anchorList.Add(anchor);
                _lineManager.DrawLine(anchor.transform);
            }
        }

        //if (anchorList.Count >= 2)
        //{
        //    lineManager.DrawLine(anchorList);
        //}
    }
}
