using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class CursorLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;  
    public Transform playerCamera;         
    public float rayDistance = 100f;       

    private Button currentButton;          

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        
        HandleMouseLook();

       
        HandleMouseInteraction();
    }

    
    public void HandleMouseLook()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }

   
    public void HandleMouseInteraction()
    {
        
        Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Button button = hit.collider.GetComponent<Button>();

            if (button != null)
            {
                if (button != currentButton)
                {
                    HighlightButtonVisuals(button);
                    currentButton = button;
                }

                if (Input.GetMouseButtonDown(0))
                    button.onClick.Invoke();
            }
            else
            {
                currentButton = null;
            }
        }
        else
        {
            currentButton = null;
        }
    }

    public void HighlightButtonVisuals(Button button)
    {
        Debug.Log("Changing button color to highlight.");
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = Color.yellow;  
        button.colors = colorBlock;
    }
}
