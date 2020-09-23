using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellButton : MonoBehaviour
{
    [SerializeField]
    Text _refundText;
    int _refund;

    [SerializeField]
    Image _towerButton;

    // Start is called before the first frame update
    void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetButtonImage(Sprite buttonImage)
    {
        _towerButton.sprite = buttonImage;
    }
}
