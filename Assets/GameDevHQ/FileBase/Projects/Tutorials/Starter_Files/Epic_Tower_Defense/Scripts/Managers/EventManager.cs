using GameDevHQ.Scripts.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevHQ.Scripts.Managers
{
    public class EventManager : MonoSingleton<EventManager>
    {
        public enum Events
        {
            WarFundsChanged,
            NewWaveStarted,
            ActivateTowerZones,
            ResetTowerZones,
            PlaceTower,
            UIArmorySelected,
            UIUpgradeMenu,
            TestEvent
        }

        private static Dictionary<string, dynamic> _eventDictionary = new Dictionary<string, dynamic>();




        public static void Listen(string eventName, Action method)
        {
            if (!_eventDictionary.ContainsKey(eventName))
            {
                //create new key
                _eventDictionary.Add(eventName, method);
            }
            else
            {
                var currentAction = _eventDictionary[eventName] as Action;
                currentAction += method;
                _eventDictionary[eventName] = currentAction;
            }
        }

        public static void Listen<T>(string eventName, Action<T> method)
        {
            if (!_eventDictionary.ContainsKey(eventName))
            {
                _eventDictionary.Add(eventName, method);
            }
            else
            {
                Action<T> currentAction = _eventDictionary[eventName];
                currentAction += method;
                _eventDictionary[eventName] = currentAction;
            }
        }

        public static void Fire(string eventName)
        {
            var eventToRaise = _eventDictionary[eventName] as Action;
            eventToRaise?.Invoke();
        }

        public static void Fire<T>(string eventName, T parem)
        {
            Action<T> eventToRaise = _eventDictionary[eventName] as Action<T>;
            eventToRaise?.Invoke(parem);
        }

        public static void StopListening(string eventName, Action method)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                var currentSubrcribers = _eventDictionary[eventName] as Action;
                currentSubrcribers -= method;
                _eventDictionary[eventName] = currentSubrcribers;
            }
        }

        public static void StopListening<T>(string eventName, Action<T> method)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                var currentSubrcribers = _eventDictionary[eventName] as Action<T>;
                currentSubrcribers -= method;
                _eventDictionary[eventName] = currentSubrcribers;
            }
        }
    }
}