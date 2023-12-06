using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBgr : MonoBehaviour {

    float length, starpos;
    float lengthy, starposy;
    public GameObject cam;
    public float parallaxEff;
	void Start () {
        starpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        starposy = transform.position.y;
        lengthy = GetComponent<SpriteRenderer>().bounds.size.y;
	}

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEff));
        float dist = (cam.transform.position.x * parallaxEff);

        //lặp lại bg
        //if (temp > starpos + length) starpos += length;
        //else if (temp < starpos - length) starpos -= length;

        float dist1 = (cam.transform.position.y * (0.5f - parallaxEff));

        transform.position = new Vector3(starpos + dist, starposy + dist1, transform.position.z);

        //if (temp1 > starposy + lengthy) starposy += lengthy;
        //else if (temp1 < starposy - lengthy) starposy -= lengthy;
    }
}
