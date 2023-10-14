using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Toggle : MonoBehaviour
{
    public GameObject objectToToggle; // The GameObject you want to activate or deactivate 

    private bool isActive = true;      // Initial state of the GameObject


    // Function to toggle the GameObject's active state
    void ToggleObject()
    {
        isActive = !isActive; // Toggle the active state

        // Activate or deactivate the GameObject based on the new state
        objectToToggle.SetActive(isActive);
        this.GetComponent<Shooting>().enabled = isActive;
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            ToggleObject();
        }
    }
}
