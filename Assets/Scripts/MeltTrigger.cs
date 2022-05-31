using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltTrigger : MonoBehaviour
{
    #region Public Vars

    public GameObject myCube; // Can be assigned without Start() but requires inspector
    public Vector3 sizeChange; // Modify this in the Inspector
    // [HideInInspector]
    public MatterState thisState;

    #endregion

    #region Private Vars
    // MatterState should be a private variable, but until testing is complete, it remains public and hidden in inspector.

    #endregion

    #region MonoBehaviorCallbacks

    private void Start()
    {
        myCube = this.gameObject; // Assigns the gameobject
        thisState = GetComponent<MatterState>();

        // Adding the temperature condition requires the playerTemp to be obtained here as a getComponent.
        // E.g. float playerTemp = GameObject.FindGameObjectWithTag("Player").GetComponent<Temperature>().GetTemp;
    }
    private void OnTriggerEnter(Collider collision)
    {
        // Behavior: Currently checks if the current state is solid, then melts.
        if (collision.tag == "Player" && thisState.State == "SOLID")
        {
            // Add the temperature condition here. (e.g. if collision.GetComponent<Temperature>().GetTemp() > this.GetComponent<Temperature>().GetTemp())


            // Subtracts the scale Vector3 when the object is touched.
            myCube.transform.localScale = myCube.transform.localScale - sizeChange;

            // Since you cannot compare vectors using ==, you must calculate the distance instead.
            float tempDist = Vector3.Distance(myCube.transform.localScale, Vector3.zero);

            // Destroys the object if it becomes 0.5f size or smaller.
            if (tempDist <= 0.5f)
            {
                Destroy(myCube);
            }
        }
    }

    #endregion
}
