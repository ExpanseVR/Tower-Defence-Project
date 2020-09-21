using GameDevHQ.Scripts.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevHQ.Scripts.Managers
{
    public class EventManager : MonoSingleton<EventManager>
    {
        private static Dictionary<string, dynamic> _eventDictionary = new Dictionary<string, dynamic>();

        public static void Listen(string eventName, Action method)
        {
            _eventDictionary.Add(eventName, method);
        }

        public static void Listen<T>(string eventName, Action<T> method)
        {
            _eventDictionary.Add(eventName, method);
        }

        public static void Fire(string eventName)
        {
            var eventToRaise = _eventDictionary[eventName] as Action;
            eventToRaise?.Invoke();
        }

        public static void Fire<T>(string eventName, T Parem)
        {
            var eventToRaise = _eventDictionary[eventName] as Action<T>;
            eventToRaise?.Invoke(Parem);
        }

        public static void StopListening(string eventName)
        {
            _eventDictionary.Remove(eventName);
        }
    }
}