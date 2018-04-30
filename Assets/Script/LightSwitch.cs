using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : InteractabltObj {
    [SerializeField]
    private List<Light> lightSource;
    private bool isLightOn;
    private int isToggle;
	// Use this for initialization
	void Start () {
        foreach(Light l in lightSource)
        {
            l.enabled = enabled;
        }
        isLightOn = true;
        isToggle = -1;
	}
	
	// Update is called once per frame
	void Update () {
        if (isToggle == 0)
        {
            isToggle = 1;
            foreach (Light l in lightSource)
            {
                l.enabled = !l.enabled;
            }
        }

        if (isAction && isToggle == -1) {
            print("------------");
            isToggle = 0;
        }
        else if(!isAction)
        {
            isToggle = -1;
        }

	}
}
