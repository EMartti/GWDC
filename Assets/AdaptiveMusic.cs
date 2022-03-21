using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AdaptiveMusic : MonoBehaviour {

    public AudioMixer audioMixer;

    public AudioMixerSnapshot SnapshotA;
    public AudioMixerSnapshot SnapshotB;

    public bool SnapshotBIsActive = false;

    public void SwitchMusicLoop() {
        if (SnapshotBIsActive == false) {
            SnapshotBIsActive = true;
            SnapshotB.TransitionTo(1f);
        } else {
            SnapshotBIsActive = false;
            SnapshotA.TransitionTo(1f);
        }
    }
}
