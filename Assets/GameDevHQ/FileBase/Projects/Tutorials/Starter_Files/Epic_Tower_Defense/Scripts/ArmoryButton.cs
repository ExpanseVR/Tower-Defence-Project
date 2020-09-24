using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevHQ.Scripts.Managers;
using GameDevHQ.Scripts;
using UnityEngine.UI;

namespace GameDevHQ.UI
{
    public class ArmoryButton : MonoBehaviour
    {
        public enum TowerType
        {
            GattlingGun,
            MissileLauncher
        }

        [SerializeField]
        TowerType _towerType;

        private int _towerID;
        Tower _towerToSpawn;

        [SerializeField]
        Text _costText;
        int _cost;

        [SerializeField]
        Image _towerButton;
        
        Sprite _towerImage;

        private void Start()
        {
            switch (_towerType)
            {
                case TowerType.GattlingGun:
                    _towerID = 0;
                    break;
                case TowerType.MissileLauncher:
                    _towerID = 1;
                    break;
                default:
                    Debug.Log("No tower selected :: ArmoryButton");
                    break;
            }

            _towerToSpawn = GameManger.Instance.GetTowerType(_towerID);

            _cost = _towerToSpawn.GetWarFundsRequired();
            _costText.text = "$" + _cost.ToString();

            _towerImage = _towerToSpawn.GetButtonImage();
            _towerButton.sprite = _towerImage;
        }

        public void ButtonPressed ()
        {
            UIManager.Instance.ArmorButton(_towerToSpawn.gameObject);
        }
    }
}