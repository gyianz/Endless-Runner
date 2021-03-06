﻿using UnityEngine;



public class Runner : MonoBehaviour
{


    public float gameOverY = -6;

    public static float distanceTraveled;

    public float acceleration;

    private bool touchingPlatform;

    public Vector3 jumpVelocity;

    void Update()
    {
        if (touchingPlatform && Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().AddForce(jumpVelocity, ForceMode.VelocityChange);
            touchingPlatform = false;
        }
        distanceTraveled = transform.localPosition.x;

        if (transform.localPosition.y < gameOverY)
        {
            GameEventManager.TriggerGameOver();
        }
    }

    void FixedUpdate()
    {
        if (touchingPlatform)
        {
            GetComponent<Rigidbody>().AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
        }
    }

    void OnCollisionEnter()
    {
        touchingPlatform = true;
    }

    void OnCollisionExit()
    {
        touchingPlatform = false;
    }

    private Vector3 startPosition;

    void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        startPosition = transform.localPosition;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        enabled = false;
    }

    private void GameStart()
    {
        distanceTraveled = 0f;
        transform.localPosition = startPosition;
        GetComponent<Renderer>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        enabled = true;
    }

    private void GameOver()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        enabled = false;
    }
}