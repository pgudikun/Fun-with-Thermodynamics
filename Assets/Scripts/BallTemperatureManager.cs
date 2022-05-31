using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTemperatureManager : MonoBehaviour
{
    [SerializeField] private float ballTemperature;
    private float roomTemperature = 298f;
    private float fireTemperature = 1900f;
    private bool coolingOff;

    // Start is called before the first frame update
    void Start()
    {
        ballTemperature = 298f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ballTemperature >= roomTemperature && coolingOff)
        {
            Debug.Log("Cooling off");
            ballTemperature -= 1f;
            if (ballTemperature == roomTemperature)
            {
                coolingOff = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Fire")
        {
            coolingOff = false;
            Debug.Log("Player character walked into a fire");
            if (ballTemperature <= fireTemperature)
            {
                ballTemperature += 2f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fire")
        {
            Debug.Log("Player character walked out of a fire");
            coolingOff = true;
        }
    }
}
