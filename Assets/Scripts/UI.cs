using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update'
    public Text ui_text;
    public GameObject power;
    public GameObject ball;
    public GameObject elevator;

    // Update is called once per frame
    void Update()
    {
        ui_text.text = "Ball Temperature : " + ball.GetComponent<Temperature>().GetTemp() + "\n" +
            "Power Source Temperature: " + power.GetComponent<Temperature>().GetTemp() + "\n" +
            "Elevator on? " + elevator.GetComponent<Elevator>().turnedOn;
    }
}
