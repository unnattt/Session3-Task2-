using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    float speed = 5f;

    Vector2 mover;
    public FixedJoystick fixedJoystick;
    public InputPlayer player;
    Vector2 touchPoistion;
    public GameObject bullet;
    bool flag = false;

    void Awake()
    {
        player = new InputPlayer();
    }

    void Update()
    {
        transform.Translate(mover * speed * Time.deltaTime);
        PlayerMovementJoyStick();

    }
    public void OnEnable()
    {
        player.Enable();
        player.PlayerMovement.Movements.started += _ => movement();
        player.PlayerMovement.Movements.canceled+= _ => movement();
        player.PlayerMovement.Movements.performed += _ => movement();
        player.PlayerMovement.Shoot.performed += _ => shootBullet();
        player.PlayerMovement.Mouse.performed += _ => MouseRotater();
        player.PlayerMovement.Touch.started += _ => TouchRotater();
        player.PlayerMovement.Touch.performed += _ => TouchRotater();
        player.PlayerMovement.Touch.canceled += _ => TouchRotater();




    }
    public void OnDisable()
    {
        player.Disable();
        player.PlayerMovement.Movements.started += _ => movement();
        player.PlayerMovement.Movements.performed -= _ => movement();
        player.PlayerMovement.Movements.canceled += _ => movement();
        player.PlayerMovement.Shoot.performed -= _ => shootBullet();
        player.PlayerMovement.Mouse.performed -= _ => MouseRotater();
        player.PlayerMovement.Touch.started -= _ => TouchRotater();
        player.PlayerMovement.Touch.performed -= _ => TouchRotater();
        player.PlayerMovement.Touch.canceled -= _ => TouchRotater();



    }

    void movement()
    {
        mover = player.PlayerMovement.Movements.ReadValue<Vector2>();
    }

    void PlayerMovementJoyStick()
    {
        if (fixedJoystick.Vertical != 0 && fixedJoystick.Horizontal != 0)
        {
            flag = true;
            float verticalDirection = fixedJoystick.Vertical;
            float horizontalDirection = fixedJoystick.Horizontal;

            Vector2 direction = new Vector2(horizontalDirection, verticalDirection);

            transform.Translate(direction * Time.deltaTime * speed);
        }
        else
            flag = false;
    }

    public void shootBullet()
    {
        Instantiate(bullet, transform.position, transform.rotation);

    }

    void MouseRotater()
    {
       
        

            Vector2 MousePosition = player.PlayerMovement.Mouse.ReadValue<Vector2>();
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(MousePosition);

            Vector3 targetDirection = mouseWorldPosition - transform.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        

        //transform.up = mouseWorldPosition;
    }

    void TouchRotater()
    {
        if (flag == false)
        {
            touchPoistion = player.PlayerMovement.Touch.ReadValue<Vector2>();

            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPoistion);
            Vector3 targetDirection = touchWorldPosition - transform.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
    // an
    //transform.up = touchWorldPosition;
}


