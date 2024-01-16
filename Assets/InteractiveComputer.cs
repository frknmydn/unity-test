using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveComputer : MonoBehaviour
{
    public GameObject player;
    public GameObject uiPanel;
    public float interactionDistance = 8f;
    private bool isUISceneOpen = false; // UIScene'in durumunu takip etmek için deðiþken

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                bool isActive = !uiPanel.activeSelf;
                uiPanel.SetActive(isActive);
                SetCursorVisibility(isActive);
            }
        }
    }

    void SetCursorVisibility(bool isVisible)
    {
        Cursor.visible = isVisible;
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
