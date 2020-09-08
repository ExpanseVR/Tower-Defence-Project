using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    int _warFundCost;

    public int GetWarFundCost()
    {
        return _warFundCost;
    }
}
