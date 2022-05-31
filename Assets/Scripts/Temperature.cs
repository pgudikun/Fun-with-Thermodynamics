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

    public float ObjTemp; // The temperature of this object.

    public MatterState ObjState;

    #endregion

    #region MonoBehaviorCallbacks

    private void Start()
    {
        // ObjTemp = 50.0f; Left Commented as this can be set in the inspector
        // ObjState = GetComponent<MatterState>(); Left commmented out as this can be manually hooked to inspector.
    }
    private void Update()
    {
        // Can be used to call functions (e.g. Cooling)
        // Also should be where calls occur to Enviornment
        // EnivornmentCalculator();
        // ConvertState();
    }
    private void OnTriggerEnter(Collider other)
    {
        float tempOther = other.GetComponent<Temperature>().GetTemp(); // Get the temperature of the object.

        // Current Behavior: If temperature difference is greater than 25, heat twice as fast.
        if (other.tag == "CanHasTemp" && tempOther > ObjTemp)
        {
            if (Mathf.Abs(tempOther - ObjTemp) > 25f) // Modify these based on functions of decreasing temperature.   
            {
                Heating(2.0f);
            }
            else
            {
                Heating(1.0f);
            }
        }

        if (other.tag == "CanHasTemp" && tempOther < ObjTemp)
        {
            // Current Behavior: If temperature difference is greater than 25, cool twice as fast.
            if (Mathf.Abs(tempOther - ObjTemp) > 25f) // Modify these based on functions of decreasing temperature.   
            {
                Cooling(2.0f);
            }
            else
            {
                Cooling(1.0f);
            } 
        }
    }

    #endregion

    #region Public Functions
    public float GetTemp()
    {
        return ObjTemp;
    }
    #endregion

    #region Private Functions
    private void Cooling(float CoolingRate)
    {
        // Called when object is cooling. Decreases temperature.
        ObjTemp -= CoolingRate;
    }
    private void Heating(float HeatingRate)
    {
        // Called when object is heating up. Increases Temperature.
        ObjTemp += HeatingRate;
    }

    private void ConvertState()
    {
        // Changes the state of the object if a threshold is breached.

        // To remove this error, delete the line below. Replace with #warning instead.
        #warning Function assumes that temperature is the base threshold before conversion to next state.

        if (ObjTemp >= ObjState.TempGas) // Hotter than gas temp
        {
            ObjState.ConvertToGas();
        }
        else if (ObjTemp < ObjState.TempGas && ObjTemp > ObjState.TempLiquid) // 
        {
            ObjState.ConvertToLiquid();
        }
        else
        {
            ObjState.ConvertToSolid();
        }
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
            if (tempEnviron > ObjTemp)
            {
                if (Mathf.Abs(tempEnviron - ObjTemp) > 25f) // Modify these based on functions of decreasing temperature.   
                {
                    Heating(2.0f);
                }
                else
                {
                    Heating(1.0f);
                }
            }

            if (tempEnviron < ObjTemp)
            {
                // Current Behavior: If temperature difference is greater than 25, cool twice as fast.
                if (Mathf.Abs(tempEnviron - ObjTemp) > 25f) // Modify these based on functions of decreasing temperature.   
                {
                    Cooling(2.0f);
                }
                else
                {
                    Cooling(1.0f);
                }
            }

        }
        else
        {
            Debug.Log("No Environment, or cannot find environment.");
        }
    }

    #endregion
}
