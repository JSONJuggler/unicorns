﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace unicorn
{
    public class SessionManager : MonoBehaviour
    {

        public static SessionManager singleton;
        public delegate void OnSceneLoaded();
        public OnSceneLoaded onSceneLoaded;

        private void Awake()
        {

            if (singleton == null)
            {
                singleton = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void LoadGameLevel(OnSceneLoaded callback)
        {
            onSceneLoaded = callback;
            StartCoroutine(LoadLevel("Main"));
        }

        public void LoadMenu()
        {
            StartCoroutine("Menu");
        }

        IEnumerator LoadLevel(string level)
        {
            yield return SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);

            if (onSceneLoaded != null)
            {
                onSceneLoaded();
                onSceneLoaded = null;
            }
        }
    }
}
