using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseTemp", menuName = "ScriptableObjects/BaseTemperatureObject", order = 1)]
public class ObjBaseTemp : ScriptableObject
{
    /// <summary>
    /// This ScriptableObject holds the base temperature of the assigned object.
    /// </summary>
    /// 

    #region Public Vars


    public float baseTemperature; // The base temperature of this object.

    public bool FixedTemp = false;

    #endregion

    #region Public Functions

    public float GetBaseTemp()
    {
        return baseTemperature;
    }

    public bool IsFixedTemp()
    {
        return FixedTemp;
    }

    #endregion


}
