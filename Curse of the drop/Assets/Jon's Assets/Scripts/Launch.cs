using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public float launchForce = 6.0f;
    public float maxPullDistance = 4.0f;
    public float releaseSpeed = 30.0f;
    public float pullBackInterval = 0.5f;
    public Vector2 baseLaunch = new Vector2(2.0f, 2.0f);

    public Transform playerStorageLocation;
    private GameObject capturedPlayer;
    
    private Vector2 launchVelocity;
    private TreeManMovement movement;
    private AudioSource audio;

    private bool launching;
    private bool pulling;
    private bool holdingPullButton;
    private bool pullButtonRelease;

    void Start()
    {
        movement = GetComponent<TreeManMovement>();
        audio = GetComponent<AudioSource>();
        capturedPlayer = null;
        pulling = false;
        launching = false;
        holdingPullButton = false;
        pullButtonRelease = false;
    }

    void Update()
    {
        if (pulling)
        {
            holdingPullButton = Input.GetButton("Jump");
            pullButtonRelease = Input.GetButtonUp("Jump");
        }
    }

    void FixedUpdate()
    {
        if (pulling)
        {
            if (holdingPullButton)
            {
                Vector2 playerPos = capturedPlayer.transform.position;

                playerPos.x += Input.GetAxisRaw("Horizontal") * pullBackInterval;
                playerPos.y += Input.GetAxisRaw("Vertical") * pullBackInterval;

                if (transform.localScale.x > 0.0f && playerPos.x > playerStorageLocation.position.x)
                {
                    playerPos = new Vector2(playerStorageLocation.position.x, playerPos.y);
                }
                else if (transform.localScale.x < 0.0f && playerPos.x < playerStorageLocation.position.x)
                {
                    playerPos = new Vector2(playerStorageLocation.position.x, playerPos.y);
                }

                if (playerPos.y > playerStorageLocation.position.y)
                {
                    playerPos = new Vector2(playerPos.x, playerStorageLocation.position.y);
                }

                Vector2 storageToMouse = playerPos - (Vector2)playerStorageLocation.position;

                if (storageToMouse.magnitude > maxPullDistance)
                {
                    capturedPlayer.transform.position = (Vector2)playerStorageLocation.position + (storageToMouse.normalized * maxPullDistance);
                }
                else
                {
                    capturedPlayer.transform.position = playerPos;
                }
            }

            if (pullButtonRelease)
            {
                launchVelocity = baseLaunch + (Vector2)((playerStorageLocation.position - capturedPlayer.transform.position) * launchForce);
                Debug.Log("Launch Veclocity X: " + launchVelocity.x + " Y: " + launchVelocity.y);
                pulling = false;
                launching = true;
            }
        }

        if (launching)
        {
            audio.Play();
            capturedPlayer.transform.position = Vector2.MoveTowards(capturedPlayer.transform.position, playerStorageLocation.position, releaseSpeed * Time.deltaTime);

            if (transform.localScale.x > 0.0f)
            {
                if (capturedPlayer.transform.position.x >= playerStorageLocation.position.x
                    && capturedPlayer.transform.position.y >= playerStorageLocation.position.y)
                {
                    StartPlayerMovement();
                    capturedPlayer.GetComponent<Rigidbody2D>().velocity = launchVelocity;
                    Debug.Log("Veclocity Before Moving X: " + capturedPlayer.GetComponent<Rigidbody2D>().velocity.x + " Y: " + capturedPlayer.GetComponent<Rigidbody2D>().velocity.y);
                    EnablePlayerColliders();
                    movement.StartMoving();
                    Debug.Log("Veclocity After Moving X: " + capturedPlayer.GetComponent<Rigidbody2D>().velocity.x + " Y: " + capturedPlayer.GetComponent<Rigidbody2D>().velocity.y);
                    capturedPlayer = null;
                    launching = false;
                }
            }
            else if(transform.localScale.x < 0.0f)
            {
                if (capturedPlayer.transform.position.x <= playerStorageLocation.position.x
                    && capturedPlayer.transform.position.y >= playerStorageLocation.position.y)
                {
                    StartPlayerMovement();
                    capturedPlayer.GetComponent<Rigidbody2D>().velocity = launchVelocity;
                    Debug.Log("Veclocity Before Moving X: " + capturedPlayer.GetComponent<Rigidbody2D>().velocity.x + " Y: " + capturedPlayer.GetComponent<Rigidbody2D>().velocity.y);
                    EnablePlayerColliders();
                    movement.StartMoving();
                    Debug.Log("Veclocity After Moving X: " + capturedPlayer.GetComponent<Rigidbody2D>().velocity.x + " Y: " + capturedPlayer.GetComponent<Rigidbody2D>().velocity.y);
                    capturedPlayer = null;
                    launching = false;
                }
            }

            GetComponent<Animator>().SetTrigger("launch");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pulling && !launching && collision.gameObject.tag == "Player")
        {
            movement.StopMoving();
            capturedPlayer = collision.gameObject;
            StopPlayerMovment();
            DisablePlayerColliders();
            capturedPlayer.transform.position = playerStorageLocation.position;
            pulling = true;
        }
    }

    private void DisablePlayerColliders()
    {
        capturedPlayer.GetComponent<Collider2D>().enabled = false;

        Collider2D[] childColliders = capturedPlayer.GetComponentsInChildren<Collider2D>();

        foreach (var collider in childColliders)
        {
            collider.enabled = false;
        }
    }

    private void EnablePlayerColliders()
    {
        capturedPlayer.GetComponent<Collider2D>().enabled = true;

        Collider2D[] childColliders = capturedPlayer.GetComponentsInChildren<Collider2D>();

        foreach (var collider in childColliders)
        {
            collider.enabled = true;
        }
    }

    private void StopPlayerMovment()
    {
        capturedPlayer.GetComponent<Rigidbody2D>().isKinematic = true;
        capturedPlayer.GetComponent<Rigidbody2D>().Sleep();
        capturedPlayer.GetComponent<PlayerInput>().stopMoving();
    }

    private void StartPlayerMovement()
    {
        capturedPlayer.GetComponent<Rigidbody2D>().isKinematic = false;
        capturedPlayer.GetComponent<Rigidbody2D>().WakeUp();
        capturedPlayer.GetComponent<PlayerInput>().startMoving();
        capturedPlayer.GetComponent<PlayerInput>().Launch();
    }
}
