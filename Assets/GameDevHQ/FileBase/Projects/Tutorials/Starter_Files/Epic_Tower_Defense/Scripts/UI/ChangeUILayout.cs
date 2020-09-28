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
        Image _restart;

        [SerializeField]
        Text _playerStatus;

        [SerializeField]
        PlayerStatesUI[] _playerStatesUI;

        public void UpdateUI (PlayerStatesUI.PlayerStateType playerStateType)
        {
            int PlayerStateID = 0;

            //Check for player state and set ID
            switch(playerStateType)
            {
                case PlayerStatesUI.PlayerStateType.Normal:
                    PlayerStateID = 0;
                    break;
                case PlayerStatesUI.PlayerStateType.Caution:
                    PlayerStateID = 1;
                    break;
                case PlayerStatesUI.PlayerStateType.Warning:
                    PlayerStateID = 2;
                    break;
                default:
                    Debug.LogError("Invalid selection UpdateUI");
                    break;
            }

            //update player state UI Images based on ID
            _armory.sprite = _playerStatesUI[PlayerStateID].Armory;
            _livesWaves.sprite = _playerStatesUI[PlayerStateID].LivesWaves;
            _warFunds.sprite = _playerStatesUI[PlayerStateID].Warfunds;
            _restart.sprite = _playerStatesUI[PlayerStateID].Restart;
            _playerStatus.text = playerStateType.ToString();
        }
    }
}