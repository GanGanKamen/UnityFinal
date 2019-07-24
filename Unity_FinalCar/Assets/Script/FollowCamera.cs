using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    GameObject AimLookAt;
    GameObject AimPosition;

    Camera currentCamera;
    float time;

    // Use this for initialization
    void Start ()
    {
        currentCamera = GetComponent<Camera> ();
        time = 0.0f;
    }

    // Update is called once per frame
    void Update ()
    {

        if (AimLookAt == null || AimPosition == null) {
            return;
        }

        // 現在位置から目的位置へ補間
        time += Time.deltaTime * 0.5f;
        Vector3 pos = transform.position;
        pos = Vector3.Lerp (pos, AimPosition.transform.position, time);
        transform.position = pos;

        transform.LookAt (AimLookAt.transform.position, Vector3.up);
    }

    public void SetAim (GameObject lookat, GameObject position)
    {
        AimLookAt = lookat;
        AimPosition = position;
        time = 0.0f;
    }
}
