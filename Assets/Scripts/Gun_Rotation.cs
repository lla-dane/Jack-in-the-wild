using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Rotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePos = Input.mousePosition;

        // Convert the mouse position to world space
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - Camera.main.transform.position.z));

        // Calculate the direction from the GameObject to the mouse position
        Vector3 direction = mouseWorldPos - transform.position;

        // Calculate the angle in radians
        float angle = Mathf.Atan2(direction.y, direction.x);

        // Convert the angle to degrees and rotate the GameObject
        transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
    }
}