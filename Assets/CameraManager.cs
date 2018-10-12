using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance = null;
    public float lerpSpeed;

    private bool following = false;
    private GameObject target = null;
    private Camera cam;

    private IEnumerator followLoop = null;
    private Action onRelease = null;
    private float originalZPos;

    private bool busy = false;


    // Use this for initialization
    void Start()
    {
        //SINGLETON
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject, 0f); }

        cam = GetComponent<Camera>();
        originalZPos = transform.position.z;
    }

    public bool isBusy() { return busy; }
    public bool isFollowing() { return (followLoop != null); }

    public void follow(GameObject obj)
    {
        if (!busy)
        {
            busy = true;
            target = obj;
            followLoop = followAsync();
            StartCoroutine(followLoop);
        }
    }

    IEnumerator followAsync()
    {
        while (true)
        {
            if (target == null) { release(); break; }
            Vector3 targetPos = target.transform.position;
            Vector3 lerpTo = new Vector3(targetPos.x, targetPos.y, originalZPos);

            transform.position = Vector3.Lerp(transform.position, lerpTo, lerpSpeed);

            yield return null;
        }
    }

    public void release()
    {
        StopCoroutine(followLoop);
        busy = false;
        followLoop = null;
        target = null;
    }

    public void lookAt(Vector3 pos, bool instant)
    {
        busy = true;
        Vector3 endPos = new Vector3(pos.x, pos.y, originalZPos);
        if (instant)
        {
            transform.position = endPos;
            return;
        }
        else
        {
            StartCoroutine(lerpTo(endPos));
        }

    }

    IEnumerator lerpTo(Vector3 endPos)
    {
        while ((transform.position - endPos).magnitude < 0.02f)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, lerpSpeed);
            yield return null;
        }

        busy = false;
    }

}
