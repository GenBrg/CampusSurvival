using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Backpack backpack;
    private static InputManager instance;

    public static InputManager Instance
    {
        get => instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public bool InGame
    {
        get
        {
            return !backpack.IsOpen;
        }
    }

    public bool Reload
    {
        get
        {
            return InGame && Input.GetButton("Reload");
        }
    }

    public bool AutoFire1
    {
        get => InGame && Input.GetButton("Fire1");
    }

    public bool SemiFire1
    {
        get => InGame && Input.GetButtonDown("Fire1");
    }

    public bool Fire2
    {
        get => InGame && Input.GetButton("Fire2");
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
