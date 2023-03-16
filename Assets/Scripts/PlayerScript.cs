using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
   float speed = 5f;
   float rotateSpeed = 2f;
    Vector2 mover;
    public InputPlayer player;
     Vector3 rotator;

    void Awake()
    {
        player = new InputPlayer();
    }

    void Update()
    {
        transform.Translate(mover * speed * Time.deltaTime );

        
    }
    public void OnEnable()
    {
        player.PlayerMovement.Movements.Enable();
        player.PlayerMovement.Movements.performed += movement;
        player.PlayerMovement.Movements.canceled += movement;
        player.PlayerMovement.Rotater.Enable();
        player.PlayerMovement.Rotater.performed += Rotater;
        
       
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


    void Rotater(InputAction.CallbackContext context)
    {
        rotator = player.PlayerMovement.Rotater.ReadValue<Vector2>();
        float rotation = rotator.x * rotateSpeed; // change rotation speed according to your preference
        transform.rotation *= Quaternion.Euler(0f, 0f, rotation);
    }
}
