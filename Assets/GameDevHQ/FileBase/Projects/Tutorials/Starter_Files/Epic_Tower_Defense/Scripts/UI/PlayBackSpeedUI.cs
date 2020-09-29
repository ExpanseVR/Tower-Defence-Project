using GameDevHQ.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevHQ.UI
{

    public class PlayBackSpeedUI : MonoBehaviour
    {      
        [SerializeField]
        Image _pauseImage;

        [SerializeField]
        Image _playImage;

        [SerializeField]
        Image _fastForwardImage;

        private bool _paused = false;
        private bool _playing = false;
        private bool _fastForward = false;
        private bool _stageStarted = false;


        public void PauseGame()
        {
            //check if stage has started and not already paused
            if (!_stageStarted || _paused)
                return;

            //pause or unpause game
            UpdateButtons(true, false, false);
            GameManger.Instance.SetGameState(GameManger.GameState.pause);
        }

        public void PlayGame()
        {
            //check if not already playing
            if (_playing)
                return;
            //check if the stage is started
            if (!_stageStarted)
            {
                //begin countdown
                //start first wave
                _stageStarted = true;
                GameManger.Instance.StartLevel();
            }

            //play the game
            UpdateButtons(false, true, false);
            GameManger.Instance.SetGameState(GameManger.GameState.play);
        }

        public void FastForward()
        {
            //check if stage has started and not already fast forwarding
            if (!_stageStarted || _fastForward)
                return;

            //fast forward game
            UpdateButtons(false, false, true);
            GameManger.Instance.SetGameState(GameManger.GameState.fastforward);
        }

        private void UpdateButtons(bool pause, bool play,  bool fastworward)
        {
            _paused = pause;
            _pauseImage.enabled = pause;

            _playing = play;
            _playImage.enabled = play;
            
            _fastForward = fastworward;
            _fastForwardImage.enabled = fastworward;
        }
    }
}