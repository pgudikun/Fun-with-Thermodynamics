using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] powerSource;
    public bool turnedOn;

    private int powered = 0;
    private Vector3 moveDirection;

    private void Start()
    {
        turnedOn = false;
        moveDirection = Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach (GameObject power in powerSource)
        {
            if (power.GetComponent<Temperature>().GetTemp() > 500)
            {
                count += 1;
            }
        }
        if (count == powerSource.Length)
        {
            turnedOn = true;
        }
        if (turnedOn)
        {
            Move();
        }
    }

    void Move()
    {
        if (transform.position.y >= 25)
        {
            moveDirection = Vector3.down;
        }
        else if (transform.position.y <= 9)
        {
            moveDirection = Vector3.up;
        }

        transform.Translate(moveDirection * Time.deltaTime * 5f);
    }
}
