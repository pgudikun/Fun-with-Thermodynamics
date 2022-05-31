using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatterState", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]

public class MatterState : ScriptableObject
{
    /// <summary>
    /// This is a scriptable object to designate the state at which the attached object will convert (i.e. reaction) at temperature.
    /// This should be placed on all objects the player can interact with.
    /// The player may have this object in the future (such that reducing the player to gas results in death/loss)
    /// </summary>


    #region Public Vars

    public string State = "NULL"; // Can only ever be three types as of now - Solid, Liquid, Gas. Plasma does not exist here.
    public Temperature thisTemp;

    // In reality, we will only ever be in Gas/Solid.

    public float TempGas;
    public float TempLiquid;
    public float TempSolid;

    #endregion

    #region Public Functions
    public string GetState()
    {
        // Returns a string with the current state.
        return State;
    }

    public void ConvertToGas()
    {
        // If temperature is critical, state becomes gaseous.
        State = "GAS";
    }
    public void ConvertToLiquid()
    {
        // If temperature is critical, state becomes liquid.
        State = "LIQUID";
    }
    public void ConvertToSolid()
    {
        // If temperature is critical, state beccomes solid.
        State = "SOLID";
    }

    #endregion

}
