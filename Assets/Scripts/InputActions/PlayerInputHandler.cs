using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction; //using static eliminates need to call type name everytime u wanna use its references (Math.Pow -> just Pow)


public class PlayerInputHandler : MonoBehaviour
{
    //this script converts input from InputActions into vectors for the Player script to use physics.
    //useful for the new input system becauyse that shit is dog ass
    private Player playerScript;
    [SerializeField] PlayerInputs playerInputs;
    private PlayerInput playerInput;

    private Vector2 boostVect;
    
    /*How this works:
      Each playerInput component (the menu u see on the Player Input object) has an index
      whenever we press buttons on a controller, the PlayerInputManager will instantiate a Player Input Prefab (the empty object w/ PlayerInput and this script attached)
      On instantiation, this input handler script will find and attach to each Player Object (the actual bots with physics)m depending on their index.
     */
    
    void Start()
    {
        /*alternative movement system, but since this call the input action directly, u cant seperate different input devices.
         * playerScript = GetComponent<Player>();
        playerInputs = new PlayerInputs();
        playerInputs.Enable();

        if (playerInputs != null) { 
            playerScript.Boost2(playerInputs.Movement.Boost.ReadValue<float>());
        }
        */
        playerInput = GetComponent<PlayerInput>();
        var players = FindObjectsOfType<Player>();
        var index = playerInput.playerIndex;
        playerScript = players.FirstOrDefault(m => m.GetPlayerIndex() == index);
    }
    /*this method is called by the Player Input component on the Player prefab. 
     *Basically used to listen to the Input Action bindings of Event Players
     *there's a bug where it wasnt registering Input Actions from KB if controller is set up
     *Fix was idk, add a keyboard scheme but dont use it??? remove and readd keyboard support in input settings??
     *
     */
    public void OnMove(InputValue context) 
    {
        if (playerScript != null) //check this if not null point ref idk
        {
            playerScript.SetInputs(context.Get<Vector2>());
        }
        
    }

    /*Currently only registering on perform - the moment its pressed, not registering hold...
     * okay workaround fix is that somehow, Vector2 gets polled constantly, so i just made the button act like a vector2...
     */
    public void OnBoost(InputValue context)
    {
        if (playerScript != null)
        {
            boostVect = context.Get<Vector2>();
            playerScript.Boost2(boostVect.y);
            //Debug.Log("pluh!");
        }

        
    }
}
