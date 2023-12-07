using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour {

    public AudioSource soundEff;
	// Use this for initialization
	void Start () {
        soundEff.mute = !Until.onSound;
		Destroy (gameObject, 2f);
	}
}
