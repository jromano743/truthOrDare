using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{

    [SerializeField] bool isTruth; //or dare

    private void OnTriggerEnter(Collider other)
    {
        HUD.Instance.ShowChallengePanel(isTruth);
    }
}
