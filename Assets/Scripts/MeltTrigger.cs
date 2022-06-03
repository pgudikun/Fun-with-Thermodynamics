using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltTrigger : MonoBehaviour
{
    #region Public Vars

    public GameObject myCube; // Can be assigned without Start() but requires inspector

    public GameObject mySteam;
    private ParticleSystem steamParticles;

    public Vector3 sizeChange; // Modify this in the Inspector
    // [HideInInspector]
    public ObjBaseTemp baseTemp;
    #endregion

    #region Private Vars
    // MatterState should be a private variable, but until testing is complete, it remains public and hidden in inspector.

    #endregion

    #region MonoBehaviorCallbacks

    private void Start()
    {
        myCube = this.gameObject; // Assigns the gameobject
        mySteam.SetActive(false);
        steamParticles = mySteam.GetComponent<ParticleSystem>();

        // Adding the temperature condition requires the playerTemp to be obtained here as a getComponent.
        // E.g. float playerTemp = GameObject.FindGameObjectWithTag("Player").GetComponent<Temperature>().GetTemp;
    }
    private void OnTriggerStay(Collider collision)
    {
        // Behavior: Regardless of tag, collision into a hotter object will melt the current object.
        if ((collision.tag == "Player" || collision.tag == "CanHasTemp") && baseTemp.GetBaseTemp() < collision.GetComponent<Temperature>().GetTemp())
        {
            mySteam.SetActive(true);
            var steamShape = steamParticles.shape;
            var steamMain = steamParticles.main;

            // Subtracts the scale Vector3 when the object is touched.
            myCube.transform.localScale = myCube.transform.localScale - sizeChange;
            steamShape.radius = steamShape.radius - (sizeChange.x / 2);

            // Since you cannot compare vectors using ==, you must calculate the distance instead.
            float tempDist = Vector3.Distance(myCube.transform.localScale, Vector3.zero);

            // Destroys the object if it becomes 0.5f size or smaller.
            if (tempDist <= 0.5f)
            {
                Destroy(myCube);
                steamMain.loop = false;
            }
        }
    }

    /*
    private void OnTriggerExit(Collider collision)
    {
        if ((collision.tag == "Player" || collision.tag == "CanHasTemp") && baseTemp.GetBaseTemp() < collision.GetComponent<Temperature>().GetTemp())
        {
            Invoke("Steam lasts for a little bit", 5.0f);
            mySteam.SetActive(false);
        }
    }
    */

    #endregion
}