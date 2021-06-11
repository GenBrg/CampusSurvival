using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static volatile InputManager _instance;
    private static object syncRoot = new object();

    private bool _jump;
    private bool _crouch;
    private bool _sprint;

    private bool jumpReleased = true;
    public static InputManager Instance
    {
        get => _instance;
    }

    public bool Jump
    {
        get => _jump;
    }

    public bool Crouch
    {
        get => _crouch;
    }

    public bool Sprint
    {
        get => _sprint;
    }

    private void Start()
    {
        _instance = this;
    }

    public void JumpApplied()
    {
        _jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO DEBUG ONLY
        _instance = this;

        _crouch = Input.GetButton("Crouch");
        _sprint = Input.GetButton("Sprint");
        _jump = Input.GetButtonDown("Jump");
    }
}
