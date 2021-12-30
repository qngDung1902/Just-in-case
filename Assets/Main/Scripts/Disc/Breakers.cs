using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = System.Random;

namespace BreakersNQD
{
    public static class Tool
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = (rnd.Next(0, n) % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static IEnumerator Delay(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            callback?.Invoke();
        }

        public static void LoadResourceAsyn<T>(Action<ResourceRequest> callback)
        {
            Mono.Instance.StartCoroutine(LoadTask(typeof(T).ToString(),callback));
            IEnumerator LoadTask(string assetname,Action<ResourceRequest> action)
            {
                ResourceRequest req = Resources.LoadAsync(assetname,typeof(T));
                while (!req.isDone)
                {
                    //Debug.LogError($"Load {typeof(T)} - {req.progress *100}%");
                    yield return null;
                }
                action?.Invoke(req);
            }
        }

        public class Mono : MonoBehaviour
    {
        private static Mono instance;
        public static Mono Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<Mono>();
                    instance.name = "Mono";
                }
                return instance;
            }
        }

        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    }
}
