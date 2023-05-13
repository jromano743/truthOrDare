using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    [SerializeField] Vector3 initialPosition = new Vector3 (0, 20, 8);
    [SerializeField] bool isActive = false;
    [SerializeField] float movementSpeed = 10.0f;
    [SerializeField] string playerName = "NoName";
    [SerializeField] int points = 0;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        StartBall();
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            bool dropBall = Input.GetButtonDown("Jump");
            transform.position += Vector3.forward * horizontalInput  * movementSpeed * Time.deltaTime;
            if (dropBall) DropBall();
        }
    }

    void DropBall()
    {
        isActive = false;
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void StartBall()
    {
        transform.position = initialPosition;
        rb.isKinematic = true;
        isActive = true;
        rb.useGravity = false;
    }

    public void AddPoints()
    {

    }

    public void DeactivePlayer()
    {
        isActive = false;
        gameObject.SetActive(isActive);
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public void AddPoint()
    {
        points++;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public int GetPlayerPoints()
    {
        return points;
    }
}
