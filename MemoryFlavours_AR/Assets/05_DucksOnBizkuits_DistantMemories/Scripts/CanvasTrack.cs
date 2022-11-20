/*
 * Author: Chao Hao
 * Last Updated: 18/11/2022 
 * Description: Gets world space canvas to always face player 
 */

using UnityEngine;

/// <summary>
/// Gets canvas to always face the player
/// </summary>
public class CanvasTrack : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject responseChat;

    // code to set the player's camera as the default camera
    private void Start()
    {
        playerCamera = Camera.main;
    }
    // Canvas faces the player at all times
    private void Update()
    {
        transform.rotation = Quaternion.Euler(playerCamera.transform.rotation.eulerAngles.y * Vector3.up);
    }
}
