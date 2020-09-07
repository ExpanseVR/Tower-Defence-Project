using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField]
    GameObject[] _towers;
    
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    //prview towers when mouse is over object
    private void OnMouseOver()
    {
        _towers[0].SetActive(true);
    }

    private void OnMouseExit()
    {
        _towers[0].SetActive(false);
    }
}
