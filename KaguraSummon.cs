using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaguraSummon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    void Start()
    {
        //Debug.Log("Started");
         if (SavesMain.KaguraSummoned)
        {
            //Debug.Log("Trigger");
            target.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
