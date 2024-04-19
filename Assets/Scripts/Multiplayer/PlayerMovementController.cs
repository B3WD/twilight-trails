using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
public class PlayerMovementController : NetworkBehaviour
{
    [SerializeField] float Speed;
    public GameObject PlayerModel;

    private void Start()
    {
        // Disable player body while in Lobby
        PlayerModel.SetActive(false);
        Speed = 1f;
    }
    private void Update()
    {
        // Check if we are In Game Scene
        if(SceneManager.GetActiveScene().name == "Game")
        {
            // If player is disabled, unable it => if check is so this happens only once
            if(PlayerModel.activeSelf == false)
            {
                // Spawn player in a random place on the map and give body
                SetPosition();
                PlayerModel.SetActive(true);
            }
            // if this is your player, allow movement
            if(isOwned)
            {
                Movement();
            }
        }
    }
    public void SetPosition()
    {
        float xPosition = Random.Range(-3,3);
        float zPosition = 0.8f;
        float yPosition = Random.Range(-3,3);
        transform.position = new Vector3(xPosition, zPosition, yPosition);
    }
    public void Movement()
    {
        float xDirection = Input.GetAxis("Horizontal");
        float yDirection = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(xDirection, 0.0f, yDirection);

        transform.position += moveDirection * Speed;
    }
}
