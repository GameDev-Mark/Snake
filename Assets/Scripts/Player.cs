using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public List<Transform> snakeGrowth = new List<Transform>();
    public Transform pickUp;

    public int initialSize = 4;

    public Text scoreText;
    private int scoreCounter = 0;

    public GameObject mapEnviro;

    private float screenHeight;
    private float screenWidth;
    private Vector2 resolution;

    //
    private void Start()
    {
        NewGameState();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (resolution.x == Screen.width && resolution.y == Screen.height) return;
        {
            screenHeight = Camera.main.orthographicSize * 2;
            screenWidth = screenHeight * Screen.width / Screen.height;
            mapEnviro.transform.localScale = new Vector3(screenWidth / 10, mapEnviro.transform.localScale.y, screenHeight / 10);

            resolution.x = Screen.width;
            resolution.y = Screen.height;
        }
    }

    //
    public void FixedUpdate()
    {
        for (int i = snakeGrowth.Count - 1; i > 0; i--)
        {
            snakeGrowth[i].position = snakeGrowth[i - 1].position;
        }
    }

    //
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            PlayerGrowth();
        }
        if (other.CompareTag("Environment"))
        {
            NewGameState();
        }
    }

    // 
    public void PlayerGrowth()
    {
        Transform _pickup = Instantiate(pickUp);
        _pickup.position = snakeGrowth[snakeGrowth.Count - 1].position;
        snakeGrowth.Add(_pickup);
        scoreCounter++;
        scoreText.text = "Score: " + scoreCounter;
    }

    // 
    public void NewGameState()
    {
        scoreCounter = 0;
        scoreText.text = "Score: " + scoreCounter;

        transform.position = new Vector3(0f, 0.25f, 0f);

        for (int i = 1; i < snakeGrowth.Count; i++)
        {
            Destroy(snakeGrowth[i].gameObject);
        }

        snakeGrowth.Clear();
        snakeGrowth.Add(transform);

        for (int i = 0; i < initialSize - 4; i++)
        {
            PlayerGrowth();
        }
    }

    // 1cubic per second block movement
    public void ConstantMoving()
    {
        transform.position += transform.forward * 1f;
    }

    //
    public void Movement()
    {
        // continously moving forward
        transform.position += transform.forward * Time.deltaTime * 4f;

        if (transform.forward == Vector3.forward)
        {
            if (Input.GetKeyDown(KeyCode.A)) // turn right
            {
                transform.Rotate(0f, -90f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.D)) // turn left
            {
                transform.Rotate(0f, 90f, 0f);
            }
        }

        if (transform.forward == Vector3.back)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.Rotate(0f, 90f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.Rotate(0f, -90f, 0f);
            }
        }

        if (transform.forward == Vector3.right)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.Rotate(0f, -90f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.Rotate(0f, 90f, 0f);
            }
        }

        if (transform.forward == Vector3.left)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.Rotate(0f, 90f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.Rotate(0f, -90f, 0f);
            }
        }
    }
}