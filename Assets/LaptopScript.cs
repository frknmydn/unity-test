using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopScript : MonoBehaviour
{
    public GameObject player;
    public GameObject terminalPanel;
    public float interactionDistance = 8f;
    private bool isUISceneOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E) && !terminalPanel.activeSelf)
            {
                ToggleUIPanel();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && terminalPanel.activeSelf)
        {
            ToggleUIPanel();
        }

    }

    void ToggleUIPanel()
    {
        bool isActive = !terminalPanel.activeSelf;
        terminalPanel.SetActive(isActive);
        SetCursorVisibility(isActive);
    }


    void SetCursorVisibility(bool isVisible)
    {
        Cursor.visible = isVisible;
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }


}
