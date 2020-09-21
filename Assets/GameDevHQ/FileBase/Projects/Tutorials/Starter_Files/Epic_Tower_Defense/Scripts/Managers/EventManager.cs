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
            MouseOverTowerZone,
            PlaceTower,
            UIArmorySelected
        }

        private static Dictionary<string, List<dynamic>> _eventDictionary = new Dictionary<string, List<dynamic>>();

        public static void Listen(string eventName, Action method)
        {
            if (!_eventDictionary.ContainsKey(eventName))
            {
                //create new key and List
                _eventDictionary.Add(eventName, new List<dynamic>());
            }
            //add to list
            _eventDictionary[eventName].Add(method);
        }

        public static void Listen<T>(string eventName, Action<T> method)
        {
            if (!_eventDictionary.ContainsKey(eventName))
            {
                //create new key and List
                _eventDictionary.Add(eventName, new List<dynamic>());
            }
            //add to list
            _eventDictionary[eventName].Add(method);
        }

        public static void Fire(string eventName)
        {
            if (_eventDictionary.ContainsKey(eventName)) //Jon <--- I think this is needed
            {
                int numMethods = _eventDictionary[eventName].Count;
                int i = 0;
                while (i < numMethods)
                {
                    var eventToRaise = _eventDictionary[eventName][i] as Action;
                    eventToRaise?.Invoke();
                    i++;
                }
            }
            else
            {
                Debug.LogError("EventManager :: Fire - Dictionary Key for event called doesnt exist");
            }
        }

        public static void Fire<T>(string eventName, T Parem)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                int numMethods = _eventDictionary[eventName].Count;
                int i = 0;
                while (i < numMethods)
                {
                    var eventToRaise = _eventDictionary[eventName][i] as Action<T>;
                    eventToRaise?.Invoke(Parem);
                    i++;
                }
            }
            else
            {
                Debug.LogError("EventManager :: Fire - Dictionary Key for event called doesnt exist");
            }
        }

        public static void StopListening(string eventName, Action method)
        {
            if (_eventDictionary.ContainsKey(eventName))
                _eventDictionary[eventName].Remove(method);
        }
    }
}