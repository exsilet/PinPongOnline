using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Logic
{
    public class SceneLoader
    { 
        // private readonly ICoroutineRunner _coroutineRunner;

        private readonly List<SceneID> _loadedScenes = new();

        // public SceneLoader(ICoroutineRunner coroutineRunner)
        // {
        //   _coroutineRunner = coroutineRunner;
        // }

        public event Action<SceneID> SceneLoaded;
        public List<SceneID> LoadedScenes => _loadedScenes.ToList();
        public SceneID CurrentScene { get; private set; }

        public void Load(SceneID name, Action onLoaded = null)
        {
            if (name == SceneID.Unknown)
                throw new ArgumentException(nameof(name));

            //_coroutineRunner.StartCoroutine(LoadSceneAsync(name, onLoaded));

            LoadScene(SceneID.Empty, () => LoadScene(name, onLoaded));
        }

        // private IEnumerator LoadSceneAsync(SceneId nextScene, Action onLoaded)
        // {
        //   Debug.Log("Начал грузить сцену: " + nextScene);
        //
        //   AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene.ToString());
        //
        //   if (asyncOperation != null)
        //   {
        //     asyncOperation.allowSceneActivation = true;
        //
        //     while (asyncOperation.isDone == false)
        //     {
        //       yield return null;
        //     }
        //   }
        //
        //   _loadedScenes.Add(nextScene);
        //
        //   onLoaded?.Invoke();
        //   SceneLoaded?.Invoke(nextScene);
        //   CurrentScene = nextScene;
        // }

        private void LoadScene(SceneID nextScene, Action onLoaded)
        {
            SceneManager.LoadScene(nextScene.ToString());
            _loadedScenes.Add(nextScene);

            onLoaded?.Invoke();
            SceneLoaded?.Invoke(nextScene);
            CurrentScene = nextScene;
        }
    }
}