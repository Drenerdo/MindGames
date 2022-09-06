using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using NextMind.NeuroTags;

public class Drag : MonoBehaviour
{
    private Vector3 offset;
    private float mZCoord;
    private NeuroTag _neuroTag;

    void Awake()
    {
        _neuroTag = this.GetComponent<NeuroTag>();
        _neuroTag.onConfidenceChanged.AddListener(OnConfidenceChanged);

        StartCoroutine(Upload());

    }

    IEnumerator Upload()
    {
        byte[] data = System.Text.Encoding.UTF8.GetBytes("This is EEG Sensor Data");
        UnityWebRequest www = UnityWebRequest.Put("", data);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload Complete!");
        }

    }

    private void OnConfidenceChanged(float val)
    {
        float percentage = val * 100;

        Debug.Log($"User is focusing on this NeuroTag at {percentage}");
    }

    private void OnDestroy()
    {
        _neuroTag.onConfidenceChanged.RemoveListener(OnConfidenceChanged);
    }

    // void OnMouseDown()
    // {
    //     mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
    //     offset = gameObject.transform.position - GetMouseWorldPos();
    // }
    //
    // private Vector3 GetMouseWorldPos()
    // {
    //     Vector3 mousePoint = Input.mousePosition;
    //
    //     mousePoint.z = mZCoord;
    //
    //     return Camera.main.WorldToScreenPoint(mousePoint);
    // }


    // private void OnMouseDrag()
    // {
    //     transform.position = GetMouseWorldPos() + offset;
    // }

    public void MoveJengaObj()
    {
        transform.position = offset.normalized * 100;

        // transform.TransformPoint(offset);
    }
}
