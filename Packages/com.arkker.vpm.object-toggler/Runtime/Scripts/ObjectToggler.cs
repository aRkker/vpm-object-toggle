
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class ObjectToggler : UdonSharpBehaviour
{
    [Header("List of objects to toggle")]
    public GameObject[] objects;

    [Header("Reference to the checkbox")]
    public Toggle checkbox;

    [NonSerialized, UdonSynced] public bool _isOn = true;


    void Start()
    {

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
