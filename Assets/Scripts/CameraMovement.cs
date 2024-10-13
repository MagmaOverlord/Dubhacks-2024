using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform[] targets;

    public float padding = 30f; // amount to pad in world units from screen edge

    Camera _camera;

    bool playersDead = false;

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void LateUpdate() // using LateUpdate() to ensure camera moves after everything else has
    {
        if (!playersDead) {
                Bounds bounds = FindBounds();

            // Calculate distance to keep bounds visible. Calculations from:
            //     "The Size of the Frustum at a Given Distance from the Camera": https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
            //     note: Camera.fieldOfView is the *vertical* field of view: https://docs.unity3d.com/ScriptReference/Camera-fieldOfView.html
            float desiredFrustumWidth = bounds.size.x + 2 * padding;
            float desiredFrustumHeight = bounds.size.z + 2 * padding;

            float distanceToFitHeight = desiredFrustumHeight * 0.5f / Mathf.Tan(_camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            float distanceToFitWidth = desiredFrustumWidth * 0.5f / Mathf.Tan(_camera.fieldOfView * _camera.aspect * 0.5f * Mathf.Deg2Rad);

            float resultDistance = Mathf.Max(distanceToFitWidth, distanceToFitHeight);

            // Set camera to center of bounds at exact distance to ensure targets are visible and padded from edge of screen 
            _camera.transform.position = bounds.center + Vector3.forward * -5f;// + Vector3.up * resultDistance;
        }
    }

    private Bounds FindBounds()
    {
        if (targets.Length == 0)
        {
            return new Bounds();
        }

        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (Transform target in targets)
        {
            if (target.gameObject.activeSelf) // if target not active
            {
                bounds.Encapsulate(target.position);
            }
        }
        return bounds;
    }

    
    void PlayerDeath() {
        playersDead = true;
    }
    
    
    
    
    /*public Transform t1;
    public Transform t2;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        //t1 = GameObject.Find("Angelfish").transform;
        //t2 = GameObject.Find("Sheepuff").transform;
        cam =  gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
                // How many units should we keep from the players
        float zoomFactor = 1.5f;
        float followTimeDelta = 1f;

        // Midpoint we're after
        Vector3 midpoint = (t1.position + t2.position) / 2f;

        // Distance between objects
        float distance = (t1.position - t2.position).magnitude;

        // Move camera a certain distance
        Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;

        // Adjust ortho size if we're using one of those
        if (cam.orthographic)
        {
            // The camera's forward vector is irrelevant, only this size will matter
            cam.orthographicSize = distance;
        }

        // You specified to use MoveTowards instead of Slerp
        cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);
            
        // Snap when close enough to prevent annoying slerp behavior
        if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
            cam.transform.position = cameraDestination;
    }*/
}
