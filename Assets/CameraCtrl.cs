using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {

    public GrindPlayerManager PlayerManager;

	public void ZoomInFinished()
    {
        PlayerManager.ShowWeapons();
    }
}
