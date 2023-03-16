using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
   float speed = 5f;
    Vector2 mover;
    public InputPlayer player;
       
    void Awake()
    {
        player = new InputPlayer();
    }

    
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * mover);
    }
    public void OnEnable()
    {
        player.PlayerMovement.Movements.Enable();
        player.PlayerMovement.Movements.performed += movement;
        player.PlayerMovement.Movements.canceled += movement;
    }
    public void OnDisable()
    {
        player.PlayerMovement.Movements.performed -= movement;
        player.PlayerMovement.Movements.canceled -= movement;
    }

    void movement(InputAction.CallbackContext context)
    {
        mover = player.PlayerMovement.Movements.ReadValue<Vector2>();
    }
    void Rotater()
    {
        
    }
}
