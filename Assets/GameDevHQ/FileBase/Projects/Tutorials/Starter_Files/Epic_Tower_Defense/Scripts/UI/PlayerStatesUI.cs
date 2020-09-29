using UnityEngine;



namespace GameDevHQ.UI
{
    [System.Serializable]
    public struct PlayerStatesUI
    {
        public enum PlayerStateType {
            Normal,
            Caution,
            Warning
        }

        [SerializeField]
        PlayerStateType PlayerState; 

        [SerializeField]
        public Sprite Armory;

        [SerializeField]
        public Sprite LivesWaves;

        [SerializeField]
        public Sprite Warfunds;

        [SerializeField]
        public Sprite Restart;

        [SerializeField]
        public Sprite PlaybackSpeed;
    }
}