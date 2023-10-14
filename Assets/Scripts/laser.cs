using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    public Transform gunBarrel;  // Reference to the gun barrel transform
    public float raycastLength = 10f;  // Length of the raycast
    public LineRenderer lineRenderer; // Reference to the LineRenderer component
    public Camera mainCamera; // Reference to the main camera

    private bool isFiring = false;

    void Start()
    {
        // Ensure the LineRenderer has the required settings configured in the Inspector.
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component not assigned.");
        }

        // If mainCamera is not assigned, try to find the main camera in the scene
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isFiring = true;
            lineRenderer.enabled = true;
        }

        if (isFiring)
        {
            FireRaycast();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isFiring = false;
            lineRenderer.enabled = false;
        }
    }

    void FireRaycast()
    {
        // Ensure the gunBarrel reference is assigned in the Inspector
        if (gunBarrel == null)
        {
            Debug.LogError("Gun barrel transform not assigned.");
            return;
        }

        // Get the position and direction of the gun barrel
        Vector2 gunBarrelPosition = gunBarrel.position;
        Vector2 gunBarrelDirection = gunBarrel.up;

        // Calculate the raycast end point
        Vector2 rayEndPoint = gunBarrelPosition + gunBarrelDirection * raycastLength;

        // Check if the ray intersects with the camera's viewport
        bool isIntersectingCamera = false;

        if (mainCamera != null)
        {
            Plane cameraPlane = new Plane(mainCamera.transform.forward, mainCamera.transform.position);
            Ray ray = new Ray(gunBarrelPosition, gunBarrelDirection);
            float hitDistance;
            isIntersectingCamera = cameraPlane.Raycast(ray, out hitDistance) && hitDistance < raycastLength;
        }

        RaycastHit2D enemyHit = new RaycastHit2D();

        // If the ray doesn't hit the camera, continue with further raycast logic
        if (!isIntersectingCamera)
        {
            enemyHit = Physics2D.Raycast(gunBarrelPosition, gunBarrelDirection, raycastLength);

            // Set LineRenderer positions to match the raycast line
            lineRenderer.SetPosition(0, gunBarrelPosition);
            lineRenderer.SetPosition(1, enemyHit.collider != null ? enemyHit.point : rayEndPoint);
        }
        else
        {
            // If the ray hits the camera, render up to the camera's viewport
            lineRenderer.SetPosition(0, gunBarrelPosition);
            lineRenderer.SetPosition(1, gunBarrelPosition + gunBarrelDirection * enemyHit.distance);
        }

        if (enemyHit.collider != null && enemyHit.collider.CompareTag("Enemy"))
        {
            Debug.Log("Raycast hit an enemy: " + enemyHit.collider.name);

            // Add further actions specific to hitting an enemy.
        }
    }
}