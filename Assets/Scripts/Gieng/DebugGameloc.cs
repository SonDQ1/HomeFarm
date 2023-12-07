using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGameloc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        public void addGem(int g)
    {
        PlayerPrefsController.instance.addGem(1000);
    }
    public void addDiamod(int g)
    {
        PlayerPrefsController.instance.addDiamod(1000);
    }
    public void addExp(int g)
    {
        PlayerPrefsController.instance.addExp(1000);
    }
}
