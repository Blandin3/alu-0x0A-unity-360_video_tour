using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButtonScript : MonoBehaviour
{
    public GameObject infoBox;          // Reference to the InfoBox panel
    public Button infoButton;           // Reference to the InfoButton

    void Start()
    {
        // Ensure the info box is initially inactive
        infoBox.SetActive(false);

        // Assign button functionality
        infoButton.onClick.AddListener(ToggleInfoBox);
    }

    // Method to toggle the info box visibility
    public void ToggleInfoBox()
    {
        infoBox.SetActive(!infoBox.activeSelf);
    }
}
