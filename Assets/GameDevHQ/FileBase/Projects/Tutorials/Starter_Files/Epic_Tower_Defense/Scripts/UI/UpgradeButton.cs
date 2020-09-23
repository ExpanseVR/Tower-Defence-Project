using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevHQ.UI
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField]
        Text _costText;
        int _cost;

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

        public void SetButtonImage (Sprite buttonImage)
        {
            _towerButton.sprite = buttonImage;
        }
    }
}