using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    public AudioClip[] clips;

    public void playClip(int id)
    {
        AudioSource.PlayClipAtPoint(clips[id], Vector3.zero);
    }

}
