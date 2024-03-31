using Chengzi;
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 数据访问层管理器
    /// </summary>
    public class DaoManager : Singleton<DaoManager>
    {
        /// <summary> 用户数据访问层</summary>
        public UserDao _userDao { get; private set; }

        public GameDao _gameDao { get; private set; }

        public SoundDao _soundDao { get; private set; }

        // public DeviceDao _deviceDao { get; private set; }

        public LevelDao _levelDao { get; private set; }

        public I18NDao _i18NDao { get; private set; }

        public PayDao _payDao { get; private set; }

        public CommonDao _commonDao { get; private set; }


        public void init()
        {
            this._userDao = new UserDao();
            this._gameDao = new GameDao();
            this._soundDao = new SoundDao();
            this._i18NDao = new I18NDao();
            this._payDao = new PayDao();
            this._commonDao = new CommonDao();
// #if UNITY_ANDROID
//             this._deviceDao = new DeviceDao();
// #endif

            loadConfigData();
        }

        public void loadConfigData()
        {
            this._gameDao.loadItemData();
            this._gameDao.loadItemSeqData();
            //this._gameDao.loadMspoItemSeqData();
            this._soundDao.loadSoundData();
            this._userDao.loadUserData();
            this._i18NDao.loadI18NData();
            this._payDao.loadPayInfoData();
            this._commonDao.loadTipData();
// #if UNITY_ANDROID
//             this._deviceDao.loadDeviceData();
// #endif
        }

    }
}
