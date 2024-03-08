using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.PlayerInput;


public class Player : MonoBehaviour
{

    //Physics Stats
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float movementSpeed;
    [SerializeField] float boostSpeed;
    float currentSpeed;
    [SerializeField] float turnSpeed;

    //Controls
    [SerializeField] Vector2 inputVector;
    float isBoosting;

    //New Control System
    private PlayerInputs input = null;
    

    //Enemy Bot
    bool toggleEnemy = true;
    [SerializeField] GameObject enemy;


    //vars
    float accelInputs = 0;
    float steerInputs = 0;
    float rotationAngle = 0;

    //for multiplayer
    [SerializeField] int index;

    void Start()
    {   
        currentSpeed = movementSpeed;
        
    }


    private void OnEnable()
    {
        input = new PlayerInputs();
        input.Enable();

    }

    // Update is called once per frame
    void Update()
    {
        //old input system - can't find any documnetations for coop somehow so im switching to newer system
        //inputVector.x = Input.GetAxisRaw("Horizontal");
        //inputVector.y = Input.GetAxisRaw("Vertical");
        //isBoosting = Input.GetAxisRaw("Fire1");
        inputVector = input.Player.Move.ReadValue<Vector2>();
        isBoosting = input.Player.Boost.ReadValue<float>();
        SetInputs(inputVector);
        QuitGame();
    }

   

    void FixedUpdate()
    {
        AccelSystem();
        SteerSystem();
        Boost();
    }

    public int GetPlayerIndex()
    {
        return index;
    }

    void AccelSystem()
    {
        //transform.up = fwd //accel and steer inputs is 0/1 boolean dictating when a force is applied
        Vector2 engineForceVector = transform.up * accelInputs * currentSpeed;
        rb.AddForce(engineForceVector, ForceMode2D.Impulse);
    }
    void SteerSystem()
    {
      rotationAngle -= steerInputs * turnSpeed;
        rb.MoveRotation(rotationAngle);
    }

    void SetInputs(Vector2 inputVector)
    {
        steerInputs = inputVector.x;
        accelInputs = inputVector.y;
    }

    void OnorOff()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            toggleEnemy = !toggleEnemy;
            enemy.SetActive(toggleEnemy);

        }
    }

    void Boost()
    {
        if (isBoosting == 1)
        {
            currentSpeed = boostSpeed;
        }
        else
        {
            currentSpeed = movementSpeed; 
        }
    }

    void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Application.Quit();
            Debug.Log("Quitting game... ");
        }
    }


}
