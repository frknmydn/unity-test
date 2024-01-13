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
                uiPanel.SetActive(!uiPanel.activeSelf);
            }
        }
    }
}
