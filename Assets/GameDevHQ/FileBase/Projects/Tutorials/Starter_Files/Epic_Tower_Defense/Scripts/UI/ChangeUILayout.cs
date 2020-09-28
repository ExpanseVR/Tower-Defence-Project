using UnityEngine;
using UnityEngine.UI;


namespace GameDevHQ.UI
{
    public class ChangeUILayout : MonoBehaviour
    {
        [SerializeField]
        Image _armory;

        [SerializeField]
        Image _livesWaves;

        [SerializeField]
        Image _warFunds;

        [SerializeField]
        Text _playerStatus;

        [SerializeField]
        PlayerStatesUI[] _playerStatesUI;

        public void UpdateUI (PlayerStatesUI.PlayerStateType playerStateType)
        {
            
            switch(playerStateType)
            {
                case PlayerStatesUI.PlayerStateType.Normal:
                    _armory.sprite = _playerStatesUI[0].Armory;
                    _livesWaves.sprite = _playerStatesUI[0].LivesWaves;
                    _warFunds.sprite = _playerStatesUI[0].Warfunds;
                    _playerStatus.text = "Good";
                    break;
                case PlayerStatesUI.PlayerStateType.Caution:
                    _armory.sprite = _playerStatesUI[1].Armory;
                    _livesWaves.sprite = _playerStatesUI[1].LivesWaves;
                    _warFunds.sprite = _playerStatesUI[1].Warfunds;
                    _playerStatus.text = "Average";
                    break;
                case PlayerStatesUI.PlayerStateType.Warning:
                    _armory.sprite = _playerStatesUI[2].Armory;
                    _livesWaves.sprite = _playerStatesUI[2].LivesWaves;
                    _warFunds.sprite = _playerStatesUI[2].Warfunds;
                    _playerStatus.text = "Bad";
                    break;
                default:
                    Debug.LogError("Invalid selection UpdateUI");
                    break;
            }    
        }
    }
}