using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerTrigger : MonoBehaviour {
    public AdaptiveMusic mixerScript;
    void Start() {
        mixerScript = GameObject.Find("AudioManager").GetComponent<AdaptiveMusic>();
    }

    private void OnTriggerEnter(Collider other) {
        mixerScript.SwitchMusicLoop();
    }
}
