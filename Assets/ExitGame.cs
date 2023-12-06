using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Exit();
    }

    // Update is called once per frame
    void Update()
    {
        Exit();
    }

    public void Exit()
    {
        if(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 3)
        {
            Debug.Log("Exit");
            Application.Quit();
        }
    }
}