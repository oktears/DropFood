
using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 用户数据访问层
    /// </summary>
    public class UserDao
    {

        //玩家实体对象
        private const string KEY_USER = "Key_UserEntity";
        //是否开启过游戏
        private const string KEY_OPENED_GAME = "Key_IsOpenedGame";
        //是否开启音效
        public const string KEY_IS_PlAY_SFX = "Key_IsPlaySfx";
        //是否开启背景音乐
        public const string KEY_IS_PlAY_BGM = "Key_IsPlayBgm";

        //收藏品
        public const string KEY_COLLECTION = "Key_Collection";
        //首次游戏的时间戳：秒
        public const string KEY_FIRST_GAME_TIME = "Key_FirstGameTime";
        //游戏分数
        public const string KEY_GAME_SCORE = "Key_GameScore";
        //金币数
        public const string KEY_GOLD_COUNT = "Key_GoldCount";
        //是否检测引导
        public const string KEY_CHECK_GUIDE = "Key_CheckGuide";
        //是否购买了去广告
        public const string KEY_IS_REMOVED_AD = "Key_IsRemovedAD";
        //当前语言
        public const string KEY_CUR_LANGUAGE = "Key_CurLanguage";

        public void loadUserData()
        {
            loadFisrtGame();
            if (!BusinessManager.Instance._userBiz.isUnFirstGame())
            {
                //首次游戏
                loadDefaultData();
            }
            else
            {
                //非首次游戏
                loadSfx();
                loadBgm();
                loadCollection();
                loadFirstGameTime();
                loadGameScore();
                loadGoldCount();
                loadGuide();
                loadRemovedAD();
                loadLanguage();
            }
        }

        /// <summary>
        /// 加载默认存档
        /// </summary>
        private void loadDefaultData()
        {
            EntityManager.Instance._userEntity._isOpenBgm = true;
            EntityManager.Instance._userEntity._isOpenSfx = true;
            EntityManager.Instance._userEntity._ownCollection = new List<int>();
            EntityManager.Instance._userEntity._firstGameTimestamp = DateTimeUtil.GetTimestampSecond();
            EntityManager.Instance._userEntity._gameScore = 0;
            EntityManager.Instance._userEntity._goldCount = 0;
            EntityManager.Instance._userEntity._isCheckGuide = true;
            EntityManager.Instance._userEntity._isRemovedAD = false;
            EntityManager.Instance._userEntity._curLanguage = Application.systemLanguage;

            saveSfx();
            saveBgm();
            saveCollection();
            saveFirstGameTime();
            saveGameScore();
            saveGoldCount();
            saveGuide();
            saveRemovedAD();
            saveLanguage();
            BusinessManager.Instance._userBiz.openedGame();
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void reset()
        {
            EntityManager.Instance._userEntity._firstGameTimestamp = DateTimeUtil.GetTimestampSecond();
            EntityManager.Instance._userEntity._ownCollection.Clear();

            saveCollection();
            saveFirstGameTime();
        }

        //保存第一次登陆游戏 
        public void saveOpenedGame(bool isOpenedGame)
        {
            PlayerPrefsHelper.SaveBool(KEY_OPENED_GAME, isOpenedGame);
        }

        //加载是否第一次登陆游戏
        public void loadFisrtGame()
        {
            EntityManager.Instance._userEntity._isOpenedGame = PlayerPrefsHelper.GetBool(KEY_OPENED_GAME);
        }

        //保存数据到本地
        public void saveUser()
        {
            PlayerPrefsHelper.Save(KEY_USER, EntityManager.Instance._userEntity);
        }

        public void loadProp()
        {
            EntityManager.Instance._userEntity = PlayerPrefsHelper.Get<UserEntity>(KEY_USER);
        }

        public void loadSfx()
        {
            EntityManager.Instance._userEntity._isOpenSfx = PlayerPrefsHelper.GetBool(KEY_IS_PlAY_SFX);
        }

        public void saveSfx()
        {
            PlayerPrefsHelper.SaveBool(KEY_IS_PlAY_SFX, EntityManager.Instance._userEntity._isOpenSfx);
        }

        public void loadBgm()
        {
            EntityManager.Instance._userEntity._isOpenBgm = PlayerPrefsHelper.GetBool(KEY_IS_PlAY_BGM);
        }

        public void saveBgm()
        {
            PlayerPrefsHelper.SaveBool(KEY_IS_PlAY_BGM, EntityManager.Instance._userEntity._isOpenBgm);
        }

        public void loadCollection()
        {
            EntityManager.Instance._userEntity._ownCollection = PlayerPrefsHelper.Get<List<int>>(KEY_COLLECTION);
        }

        public void saveCollection()
        {
            PlayerPrefsHelper.Save(KEY_COLLECTION, EntityManager.Instance._userEntity._ownCollection);
        }

        public void loadFirstGameTime()
        {
            EntityManager.Instance._userEntity._firstGameTimestamp = PlayerPrefsHelper.GetInt(KEY_FIRST_GAME_TIME);
        }

        public void saveFirstGameTime()
        {
            PlayerPrefsHelper.SaveInt(KEY_FIRST_GAME_TIME, (int)EntityManager.Instance._userEntity._firstGameTimestamp);
        }

        public void loadGameScore()
        {
            EntityManager.Instance._userEntity._gameScore = PlayerPrefsHelper.GetInt(KEY_GAME_SCORE);
        }

        public void saveGameScore()
        {
            PlayerPrefsHelper.SaveInt(KEY_GAME_SCORE, EntityManager.Instance._userEntity._gameScore);
        }
        public void loadGoldCount()
        {
            EntityManager.Instance._userEntity._goldCount = PlayerPrefsHelper.GetInt(KEY_GOLD_COUNT);
        }

        public void saveGoldCount()
        {
            PlayerPrefsHelper.SaveInt(KEY_GOLD_COUNT, EntityManager.Instance._userEntity._goldCount);
        }

        public void loadGuide()
        {
            EntityManager.Instance._userEntity._isCheckGuide = PlayerPrefsHelper.GetBool(KEY_CHECK_GUIDE);
        }

        public void saveGuide()
        {
            PlayerPrefsHelper.SaveBool(KEY_CHECK_GUIDE, EntityManager.Instance._userEntity._isCheckGuide);
        }

        public void loadRemovedAD()
        {
            EntityManager.Instance._userEntity._isRemovedAD = PlayerPrefsHelper.GetBool(KEY_IS_REMOVED_AD);
        }

        public void saveRemovedAD()
        {
            PlayerPrefsHelper.SaveBool(KEY_IS_REMOVED_AD, EntityManager.Instance._userEntity._isRemovedAD);
        }

        public void loadLanguage()
        {
            EntityManager.Instance._userEntity._curLanguage = (SystemLanguage)PlayerPrefsHelper.GetInt(KEY_CUR_LANGUAGE);
        }

        public void saveLanguage()
        {
            PlayerPrefsHelper.SaveInt(KEY_CUR_LANGUAGE, (int)EntityManager.Instance._userEntity._curLanguage);
        }
    }
}