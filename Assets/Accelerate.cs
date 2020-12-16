using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accelerate : MonoBehaviour
{
    Toggle toggle;
    
    void Start()
    {
        toggle = gameObject.GetComponent<Toggle>();
        toggle.isOn = false;
    }

    public void accelerate()
        {
            if (toggle.isOn == true)
            {
                Time.timeScale = 2;
            }
            if (toggle.isOn == false)
            {
                Time.timeScale = 1;
            }
        }

}
