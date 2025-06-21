
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class ObjectToggler : UdonSharpBehaviour
{
    [Header("Object default state")]
    [Tooltip("If true, the objects will be active by default. If false, they will be inactive.")]
    public bool defaultState = true;

    [Header("List of objects to toggle")]
    public GameObject[] objects;

    [Header("Reference to the checkbox")]
    public Toggle checkbox;

    [NonSerialized, UdonSynced] public bool _isOn = true;


    void Start()
    {
        // Debug.Log(Networking.InstanceOwner.displayName);
        if (Networking.InstanceOwner != null && Networking.InstanceOwner.displayName == Networking.LocalPlayer.displayName)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        _isOn = defaultState;
        foreach (GameObject obj in objects)
        {
            obj.SetActive(_isOn);
        }
        if (checkbox != null)
        {
            checkbox.isOn = _isOn;
        }
        if (Networking.IsOwner(Networking.LocalPlayer, gameObject))
        {
            RequestSerialization();
        }
    }

    public override void OnDeserialization()
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(_isOn);
        }

        if (checkbox != null)
        {
            checkbox.isOn = _isOn;
        }
    }

    public void ToggleState()
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        _isOn = !_isOn;
        foreach (GameObject obj in objects)
        {
            obj.SetActive(_isOn);
        }

        if (checkbox != null)
        {
            checkbox.isOn = _isOn;
        }

        RequestSerialization();
    }
}
