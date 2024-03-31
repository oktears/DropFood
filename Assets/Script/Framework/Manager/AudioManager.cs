using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Chengzi
{
    public class AudioManager : MonoSingleton<AudioManager>
    {

        ////主界面
        //public const int SOUND_BGM_MAIN_MENU = 1;
        //单机
        public const int SOUND_BGM_COMMON = 2;
        ////MSPO
        //public const int SOUND_BGM_MSPO = 3;


        //点击按钮
        public const int SOUND_SFX_BTN_CLICK = 101;
        //游戏中点击屏幕开始落下道具时
        public const int SOUND_SFX_ITEM_DROP_START = 102;
        //普通道具碰撞
        public const int SOUND_SFX_ITEM_COLL = 103;
        //板糖和消除道具出现时
        public const int SOUND_SFX_SPECIAL_ITEM_SHOW = 104;
        //摄像机上移
        public const int SOUND_SFX_CAMERA_MOVE_UP = 105;
        //失败
        public const int SOUND_SFX_FAIL = 106;
        //进入结算界面
        public const int SOUND_SFX_RESULT = 107;
        //加速甜点道具出现时
        public const int SOUND_SFX_ACC_SHOW = 108;
        //消除道具出现
        public const int SOUND_SFX_ELIMINATE_SHOW = 109;
        //获得金币
        public const int SOUND_SFX_GAIN_GOLD = 110;
        //披萨掉落
        public const int SOUND_SFX_PIZZA = 111;
        //被消除
        public const int SOUND_SFX_ELIMINATE = 112;
        //翻页
        public const int SOUND_SFX_BTN_PAGE = 113;
        //金币
        public const int SOUND_SFX_ITEM_GOLD = 114;
        //ReadyGo
        public const int SOUND_SFX_READY_GO = 115;
        //获得道具
        public const int SOUND_GAIN_ITEM = 116;

        private AudioSource audio;

        private List<AudioSource> _audioSources = new List<AudioSource>();
        private Dictionary<int, AudioSource> _soundMap;
        private Transform _audioRootTrans;
        private bool _bg_switch = true;
        private bool _effect_switch = true;

        //播放速度
        public float _pitch = 1.0f;

        private int _curBgm = -1;

        private vp_Timer.Handle _timer;
        private Tweener _bgmFadeTwener;

        public void init()
        {
            GameObject gameObj = GameObject.Find("AudioRoot");
            if (gameObj == null)
            {
                gameObj = new GameObject("AudioRoot");
                GameObject.DontDestroyOnLoad(gameObj);
            }
            _audioRootTrans = gameObj.transform;
            _soundMap = new Dictionary<int, AudioSource>();
            audio = _audioRootTrans.gameObject.AddComponent<AudioSource>();
            try
            {
                for (int i = 0; i < DaoManager.Instance._soundDao._soundList.Count; i++)
                {
                    SoundData data = DaoManager.Instance._soundDao._soundList[i];
                    add(data._soundId,
                        (AudioClip)Resources.Load(data._soundPath,
                        typeof(AudioClip)),
                        data._isLoop,
                        data._volume);
                }
            }
            catch (Exception ex)
            {
#if UNITY_EDITOR
                Debug.Log(ex.ToString());
#endif
            }
        }


        private bool isBgm(int soundId)
        {
            if (soundId == AudioManager.SOUND_BGM_COMMON)
            {
                return true;
            }
            return false;
        }

        public bool getBgSwitch()
        {
            return _bg_switch;
        }

        public bool getEffectSwitch()
        {
            return _effect_switch;
        }

        public void stopAll()
        {
            foreach (var item in _soundMap)
            {
                item.Value.Stop();
            }
        }

        public void playPause()
        {
            List<int> soundList = new List<int>(_soundMap.Keys);
            for (int i = 0; i < soundList.Count; i++)
            {
                if (_soundMap[soundList[i]] != null)
                {
                    _soundMap[soundList[i]].Pause();
                }
            }
        }

        public void updatePitch(float pitch)
        {
            foreach (var item in _soundMap)
            {
                this._pitch = pitch;
                item.Value.pitch = pitch;
            }
        }

        void add(int sfx, AudioClip clip, bool isLoop, float volume)
        {
            AudioSource audio = _audioRootTrans.gameObject.AddComponent<AudioSource>();
            audio.playOnAwake = false;
            audio.clip = clip;
            audio.loop = isLoop;
            audio.volume = volume;
            _soundMap.Add(sfx, audio);
        }

        public AudioSource getAudioSource(int sfx)
        {
            return _soundMap[sfx];
        }

        public void playDelay(int sfx, float delay)
        {
            AudioSource snd = null;

            do
            {
                if (isBgm(sfx)) break;

                if (!EntityManager.Instance._userEntity._isOpenSfx) break;
                if (_soundMap.TryGetValue(sfx, out snd) && snd != null)
                {
                    snd.pitch = _pitch;
                    snd.DOFade(_pitch, delay).OnComplete(() =>
                    {
                        snd.Play();
                    });
                }
                else
                {
#if UNITY_EDITOR
                    //Debug.Log("sound not found " + sfx);
#endif
                }

            } while (false);
        }

        /// <summary>
        /// 播放音效结束后播放背景音乐
        /// </summary>
        /// <param name="sfx"></param>
        /// <param name="bgm"></param>
        public void playAndBgm(int sfx, int bgm)
        {
            stopBGM();
            AudioSource snd = null;

            do
            {
                //if (isBgm(sfx)) break;

                if (!EntityManager.Instance._userEntity._isOpenSfx) break;
                if (_soundMap.TryGetValue(sfx, out snd) && snd != null)
                {
                    snd.pitch = _pitch;
                    snd.Play();
                    _timer = new vp_Timer.Handle();
                    vp_Timer.In(snd.clip.length, new vp_Timer.Callback(() =>
                    {
                        _timer.Cancel();
                        playBGM(bgm);
                    }), _timer);
                }
                else
                {
#if UNITY_EDITOR
                    //Debug.Log("sound not found " + sfx);
#endif
                }

            } while (false);
        }

        public void stopDelayBgm()
        {
            if (_timer != null)
            {
                _timer.Cancel();
            }
            if (_bgmFadeTwener != null)
            {
                _bgmFadeTwener.Kill();
            }
        }

        public void play(int sfx)
        {
            AudioSource snd = null;

            do
            {
                if (isBgm(sfx)) break;

                if (!EntityManager.Instance._userEntity._isOpenSfx) break;
                if (_soundMap.TryGetValue(sfx, out snd) && snd != null)
                {
                    snd.volume = 1.0f;
                    snd.pitch = _pitch;
                    snd.Play();
                }
                else
                {
#if UNITY_EDITOR
                    //Debug.Log("sound not found " + sfx);
#endif
                }

            } while (false);
        }

        //语音播放
        public void playOneShot(AudioClip sfx)
        {
            do
            {
                //if (!EntityManager.Instance._userEntity._isOpenSound) break;
                audio.PlayOneShot(sfx);
                audio.volume = 1;
            } while (false);
        }

        public void playOneShot(int sfx)
        {
            AudioSource snd = null;

            do
            {
                if (isBgm(sfx)) break;

                if (!EntityManager.Instance._userEntity._isOpenSfx) break;
                if (_soundMap.TryGetValue(sfx, out snd) && snd != null)
                {
                    snd.PlayOneShot(snd.clip, snd.volume);
                }
                else
                {
#if UNITY_EDITOR
                    //Debug.Log("sound not found " + sfx);
#endif
                }

            } while (false);
        }

        public void pauseBG(bool isPause)
        {
            List<int> soundList = new List<int>(_soundMap.Keys);
            for (int i = 0; i < soundList.Count; i++)
            {
                int soundId = soundList[i];
                AudioSource audio = _soundMap[soundId];
                if (audio != null && isBgm(soundId))
                {
                    if (isPause)
                    {
                        audio.Pause();
                    }
                    else
                    {
                        if (!EntityManager.Instance._userEntity._isOpenBgm) break;
                        audio.Play();
                    }
                }
            }
        }

        public void playBGM(int sfx)
        {
            stopBGM();
            AudioSource snd = null;

            do
            {
                if (!isBgm(sfx)) break;

                if (!EntityManager.Instance._userEntity._isOpenBgm) break;
                if (_soundMap.TryGetValue(sfx, out snd) && snd != null)
                {
                    _curBgm = sfx;
                    snd.volume = 0.5f;
                    snd.pitch = _pitch;
                    snd.Play();
                }
                else
                {
#if UNITY_EDITOR 
                    //Debug.Log("sound not found " + sfx);
#endif
                }

            } while (false);
        }

        public void stopByExit()
        {
            stopAll();
            stopDelayBgm();
        }

        public void stop(int sfx)
        {
            if (_soundMap[sfx] != null)
            {
                _soundMap[sfx].Stop();
            }
            else
            {
#if UNITY_EDITOR
                //Debug.Log("sound not found " + sfx);
#endif
            }
        }

        public void stopBGM()
        {
            List<int> soundList = new List<int>(_soundMap.Keys);
            for (int i = 0; i < soundList.Count; i++)
            {
                int soundId = soundList[i];
                AudioSource audio = _soundMap[soundId];
                if (_curBgm == soundId
                    && audio != null
                    && isBgm(soundId))
                {
                    audio.Stop();
                }
            }
            stopDelayBgm();
        }

        /// <summary>
        /// 減弱音效
        /// </summary>
        /// <param name="duration"></param>
        public void fadeOutSfx(float duration)
        {
            List<int> soundList = new List<int>(_soundMap.Keys);
            for (int i = 0; i < soundList.Count; i++)
            {
                int soundId = soundList[i];
                AudioSource audio = _soundMap[soundId];

                if (_curBgm == soundId
                    && audio != null)
                {
                    _bgmFadeTwener = audio.DOFade(0, duration).OnComplete(() =>
                    {
                        audio.Stop();
                    });
                }
            }
        }


        /// <summary>
        /// 渐弱背景音，后停止
        /// </summary>
        public void fadeInBgm(int sfx, float duration)
        {
            stopBGM();
            AudioSource snd = null;

            do
            {
                if (!isBgm(sfx)) break;

                if (!EntityManager.Instance._userEntity._isOpenBgm) break;
                if (_soundMap.TryGetValue(sfx, out snd) && snd != null)
                {
                    _curBgm = sfx;
                    snd.pitch = _pitch;
                    snd.volume = 0.0f;
                    snd.Play();

                    float endVolume = 0.7f;
                    _bgmFadeTwener = snd.DOFade(endVolume, duration).OnComplete(() =>
                   {
                       _bgmFadeTwener.Kill();
                   });
                }
                else
                {
#if UNITY_EDITOR 
                    //Debug.Log("sound not found " + sfx);
#endif
                }

            } while (false);


            List<int> soundList = new List<int>(_soundMap.Keys);
            for (int i = 0; i < soundList.Count; i++)
            {
                int soundId = soundList[i];
                AudioSource audio = _soundMap[soundId];

                if (_curBgm == soundId
                    && audio != null
                    && isBgm(soundId))
                {

                }
            }
        }

        /// <summary>
        /// 渐弱背景音，后停止
        /// </summary>
        public void fadeOutBg(float duration)
        {
            List<int> soundList = new List<int>(_soundMap.Keys);
            for (int i = 0; i < soundList.Count; i++)
            {
                int soundId = soundList[i];
                AudioSource audio = _soundMap[soundId];

                if (_curBgm == soundId
                    && audio != null
                    && isBgm(soundId))
                {
                    _bgmFadeTwener = audio.DOFade(0, duration).OnComplete(() =>
                    {
                        audio.Stop();
                    });
                }
            }
        }

    }
}
