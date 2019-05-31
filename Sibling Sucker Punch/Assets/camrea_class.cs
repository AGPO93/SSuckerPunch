using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

// TO DO : FIX OFFSET TO BE LOCAL POSITION NOT WORLD POSITION

public class camrea_class : MonoBehaviour
{
    #region variables

    private Camera camrea;

    [SerializeField]
    private List<GameObject> camrea_targets;

    [SerializeField]
    private GameObject camrea_orbit_target;

    [SerializeField]
    private Transform camrea_follow_target;

    [SerializeField]
    private Vector3 camera_targets_centre;

    [SerializeField]
    private float camrea_minimum_fov = 120.0f;

    [SerializeField]
    private float camrea_maximum_fov = 10.0f;



    [SerializeField]
    private float camrea_offset_x = 0;

    [SerializeField]
    private float camrea_offset_y = 0;

    [SerializeField]
    private float camrea_offset_z = 0;

    private Vector3 camrea_offset;

    private Vector3 camrea_smooth = Vector3.zero;
    private float camrea_smooth_time = 0.1f;
    #endregion
       

  // bools
  [SerializeField]
    private bool camrea_use_targets_centre;

    // unity

    private void Start()
    {
        camrea = this.GetComponent<Camera>();
        CheckIfDefaultValues();
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        UpdateCamreaTargetsCentre();
        MoveWithAndLookAtTargetCentre();
        ScaleFoVToFitCamreaTargetList();
    }

    // methods
       
    private void UpdateCamreaTargetsCentre()
    {        
        // if there is only one camrea the camrea should focus just on that
        if (camrea_targets.Count == 1)
        {
            Debug.Log("Only one target being used for MultipleTargetCameraBehaviour.");
            camera_targets_centre = camrea_targets[0].transform.position;
            return;
        }
        
        // if there is more than one camrea the camrea should focus on the centre of those objects
        var bounds = new Bounds(camrea_targets[0].transform.position, Vector3.zero);

        for (int i = 0; i < camrea_targets.Count; i++)
        {
            bounds.Encapsulate(camrea_targets[i].transform.position);
            camera_targets_centre = bounds.center;
        }
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(camrea_targets[0].transform.position, Vector3.zero);

        for (int i = 0; i < camrea_targets.Count; i++)
        {
            bounds.Encapsulate(camrea_targets[i].transform.position);
        }

        return bounds.size.x;
    }

    private void MoveWithAndLookAtTargetCentre()
    {
        camrea_offset = new Vector3(camrea_offset_x, camrea_offset_y, camrea_offset_z);

        this.transform.position = Vector3.SmoothDamp(
            this.transform.position, camera_targets_centre + camrea_offset, 
            ref camrea_smooth, camrea_smooth_time);

        this.transform.LookAt(camera_targets_centre);
    }

    private void ScaleFoVToFitCamreaTargetList()
    {
        camrea.fieldOfView = Mathf.Lerp(camrea_maximum_fov, camrea_minimum_fov, GetGreatestDistance());
    }

    private void DrawDebugRaysToTargets()
    {
        foreach(GameObject obj in camrea_targets)
        {
            Debug.DrawLine(this.transform.position, obj.transform.position, Color.blue);
        }
    }

    private void CheckIfDefaultValues()
    {
        if ((camrea_offset_x == 0.0f) &&
            (camrea_offset_y == 0.0f) &&
            (camrea_offset_z == 0.0f))
        {
            Debug.Log("Camrea offset values defaulted to 0.");
        }

        if ((camrea_minimum_fov == 120.0f) &&
            (camrea_maximum_fov == 10.0f))
        {
            Debug.Log("Camrea min and max zoom values defaulted to 120 and 10.");
        }
    }
    
}
