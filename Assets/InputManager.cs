using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Backpack backpack;

    public bool InGame
    {
        get
        {
            return !backpack.IsOpen;
        }
    }

    public bool Fire
    {
        get => InGame && Input.GetButton("Fire1");
    }

    public bool Jump
    {
        get => InGame && Input.GetButtonDown("Jump");
    }

    public float HorizontalInput
    {
        get => InGame ? Input.GetAxisRaw("Horizontal") : 0.0f;
    }

    public float VerticalInput
    {
        get => InGame ? Input.GetAxisRaw("Vertical") : 0.0f;
    }

    public bool Crouch
    {
        get => InGame && Input.GetButton("Crouch");
    }

    public bool Sprint
    {
        get =>InGame && Input.GetButton("Sprint");
    }

    // Start is called before the first frame update
    void Start()
    {
        backpack = FindObjectOfType<Backpack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
