using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Application.LoadLevel("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetKeyDown());
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Debug.Log(this.parent.GetChild(i - 1));
            Application.LoadLevel("SampleScene");
        }
    }
}
