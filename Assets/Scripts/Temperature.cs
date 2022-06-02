using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temperature : MonoBehaviour
{
    /// <summary>
    /// This is the temperature component. All objects within the scene which has temperature must have this component.
    /// This component manages all functions related to temperature calls.
    /// If animations are added for the ball to heat up, this is where the calls to the animator component will occur.
    /// </summary>


    #region Public Vars

    public float objTemp; // The temperature of this object.
    public GameObject changingTempObj;  // the object whose temperature is being changed

    public ObjBaseTemp baseTemp; // A base temperature ScriptableObject.
                                 // public MatterState objState;

    public bool revertTemp;
    #endregion

    #region MonoBehaviorCallbacks

    private void Start()
    {
        // ObjTemp = 50.0f; Left Commented as this can be set in the inspector
        // ObjState = GetComponent<MatterState>(); Left commmented out as this can be manually hooked to inspector.
        objTemp = baseTemp.GetBaseTemp();
        revertTemp = true; // Objects always attempt to return to the base temperature.
    }
    private void FixedUpdate()
    {
        // Can be used to call functions (e.g. Cooling)
        // Also should be where calls occur to Enviornment
        // EnivornmentCalculator();

        if (revertTemp)
        {
            RevertToBase();
        }   
    }
    private void OnTriggerStay(Collider other)
    {
        if (revertTemp == true) // Prevents constantly setting isCooling to false
            revertTemp = false;

        if ((other.tag == "CanHasTemp" || other.tag == "Player") && !baseTemp.IsFixedTemp())
        {
            //Debug.Log("Touching");
            float tempOther = other.GetComponent<Temperature>().GetTemp(); // Get the temperature of the object.

            if (tempOther > objTemp)
                Heating();

            if (tempOther < objTemp)
                Cooling();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        revertTemp = true;
    }

    #endregion

    #region Public Functions
    public float GetTemp()
    {
        return objTemp;
    }
    #endregion

    #region Private Functions
    private void RevertToBase()
    {
        if (baseTemp.GetBaseTemp() < objTemp)
            Cooling();
        else if (baseTemp.GetBaseTemp() > objTemp)
            Heating();
        else
            revertTemp = false;

    }

    private void EnvironmentCalculator()
    {
        // This function handles temperature calculations with environmental conditions (e.g. hot room).
        // Differs from Collisions as there is no trigger with an Environment.

        // This may be replaced/obsoleted with invisible gameobjects with triggers for "Hot/Cold" areas instead.
        GameObject Environment = GameObject.FindGameObjectWithTag("Environment");
        if (Environment != null)
        {
            float tempEnviron = Environment.GetComponent<Temperature>().GetTemp();
            if (tempEnviron > objTemp)
                Heating();

            if (tempEnviron < objTemp)
                Cooling();
        }
        else
        {
            Debug.Log("No Environment, or cannot find environment.");
        }
    }
    private void Cooling()
    {
        changingTempObj = this.gameObject;
        if (changingTempObj.tag == "Player")
        {
            //Debug.Log("Red Hot Metal");
            float tempDifference = objTemp - baseTemp.GetBaseTemp();
            tempDifference /= 1000;
            //Debug.Log(tempDifference);
            tempDifference = Mathf.Min(tempDifference, 1f);
            //Debug.Log(tempDifference);

            Material redGlow = changingTempObj.GetComponent<Renderer>().material;
            redGlow.EnableKeyword("_EMISSION");
            redGlow.SetColor("_EmissionColor", new Color(2.55f, 0f, 0f, 1.0f) * tempDifference);
        }
        objTemp -= 1f;
    }

    private void Heating()
    {
        changingTempObj = this.gameObject;
        if (changingTempObj.tag == "Player")
        {
            //Debug.Log("Red Hot Metal");
            float tempDifference = objTemp - baseTemp.GetBaseTemp();
            tempDifference /= 1000;
            //Debug.Log(tempDifference);
            tempDifference = Mathf.Min(tempDifference, 1f);
            //Debug.Log(tempDifference);

            Material redGlow = changingTempObj.GetComponent<Renderer>().material;
            redGlow.EnableKeyword("_EMISSION");
            redGlow.SetColor("_EmissionColor", new Color(2.55f, 0f, 0f, 1.0f) * tempDifference);
        }
        objTemp += 1f;
    }

    #endregion

    #region Coroutines
    #endregion
}
