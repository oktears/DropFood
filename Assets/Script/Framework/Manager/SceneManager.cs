using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Chengzi
{
    public delegate void SceneLoadDelegate(SceneType type);

    public class SceneManager : MonoSingleton<SceneManager>
    {
        public static SceneManager GetInstance()
        {
            return SceneManager.Instance;
        }

        private SceneLoadDelegate enter;
        private SceneLoadDelegate exit;

        public static bool isLaunch = true;
        public static SceneType curSceneType = SceneType.UNKNOWN;

        private SceneType m_loadingSceneType = SceneType.UNKNOWN;

        public AsyncOperation m_sceneAsync = null;
        private bool m_isLoadWillFinish = false;


        private float m_progressCounter = 0f;

        private bool m_uiCanClick = false;
        public bool UICanClick
        {
            get { return m_uiCanClick; }
        }
        private float m_loadSceneProgress = -1f;
        public float LoadSceneProgress
        {
            get { return m_loadSceneProgress; }
        }

        private bool m_isShowProgress;
        public bool IsShowProgress
        {
            get { return m_isShowProgress; }
        }
        private bool m_isLoadingScene = false;
        public bool IsLoadingScene
        {
            get { return m_isLoadingScene; }
        }

        public void ResetSceneProgress()
        {
            m_loadSceneProgress = -1f;
        }

        public void LoadScene(SceneType type)
        {
            LoadScene(type, false);
        }

        public void setDelegate(SceneLoadDelegate enter, SceneLoadDelegate exit)
        {
            this.enter = enter;
            this.exit = exit;
        }

        public void LoadScene(SceneType type, bool isShowProgress)
        {
            m_isShowProgress = isShowProgress;
            StartCoroutine(LoadSceneAsync(type));
            //LoadSceneAsync(type);
        }

        //private void LoadSceneAsync(SceneType type)

        private IEnumerator LoadSceneAsync(SceneType type)
        {
            if (exit != null) exit(type);
            m_loadingSceneType = type;

            m_uiCanClick = false;
            m_loadSceneProgress = 0f;
            m_progressCounter = 0f;
            m_isLoadingScene = true;
            m_isLoadWillFinish = false;

            int levelIndex = (int)type;

            //清理颜色缓冲区，防止花屏
            //GL.Clear(true, true, Color.black);
            Time.timeScale = 1;
            //m_sceneAsync = Application.LoadLevelAsync(levelIndex);\
            //UnityEngine.SceneManagement.LoadScene(levelIndex)
#if UNITY_5
            Application.LoadLevel(levelIndex);
#else
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex);
#endif
            //m_sceneAsync.allowSceneActivation = true;
            yield return null;

            curSceneType = m_loadingSceneType;
            m_loadSceneProgress = 1f;
            //GL.Clear(true, true, Color.black);

            PrefabPool.Instance.Clear();
            TextureAtlasManager.Instance.clear();

            //yield return Resources.UnloadUnusedAssets();
            Resources.UnloadUnusedAssets();
            DOTween.ClearCachedTweens();

            //GL.Clear(true, true, Color.black);
            if (enter != null) enter(type);

            m_isLoadingScene = false;
            m_uiCanClick = true;
        }

        void Update()
        {
            if (m_isLoadingScene)
            {
                if (m_sceneAsync != null && !m_sceneAsync.isDone)
                {
                    float factor = 0.3f;
                    float loadPauseFlag = 0.9f;
                    //float progressInterval = 1f/(Common.frameRate * 0.5f);
                    float progressInterval = 1f / (60 * 0.5f);
                    if (m_sceneAsync.progress >= loadPauseFlag)
                    {
                        if (m_isShowProgress)
                        {
                            m_progressCounter = Mathf.Clamp01(m_progressCounter + progressInterval);
                            m_loadSceneProgress = factor * loadPauseFlag + (1 - factor) * m_progressCounter;
                        }
                        else
                        {
                            m_progressCounter = 1.1f;
                            m_loadSceneProgress = m_sceneAsync.progress;
                        }

                        if (m_progressCounter >= 1f)
                        {
                            m_isLoadWillFinish = true;
                        }
                    }
                    else
                    {
                        m_loadSceneProgress = m_loadSceneProgress * factor;
                    }
                }
            }
        }
    }
}