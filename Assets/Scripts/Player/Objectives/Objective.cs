using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objective : MonoBehaviour
{
    public string description;
    public GameObject reward;
    public abstract bool CheckObjectiveStatus();
}
