using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 用户业务逻辑
    /// </summary>
    public class UserBiz
    {

        /// <summary>
        /// 是否为首次游戏
        /// </summary>
        /// <returns></returns>
        public bool isUnFirstGame()
        {
            //DaoManager.Instance._userDao.loadFisrtGame();
            return EntityManager.Instance._userEntity._isOpenedGame;
        }

        public void openedGame()
        {
            //EntityManager.Instance._userEntity._isOpenedGame = true;
            DaoManager.Instance._userDao.saveOpenedGame(true);
        }

        /// <summary> 
        /// 是否拥有该收藏品
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public bool isOwnCollection(int cid)
        {
            return EntityManager.Instance._userEntity._ownCollection.Contains(cid);
        }

        /// <summary>
        /// 增加收藏品
        /// </summary>
        /// <param name="cid"></param>
        public void addCollection(int cid)
        {
            if (!isOwnCollection(cid))
            {
                EntityManager.Instance._userEntity._ownCollection.Add(cid);
            }
        }

        /// <summary>
        /// 更新历史分数
        /// </summary>
        public void updateHistoryScore(int score)
        {
            if (score > EntityManager.Instance._userEntity._gameScore)
            {
                //记录新记录
                EntityManager.Instance._userEntity._gameScore = score;
                BusinessManager.Instance._gameBiz._isNewRecord = true;
                DaoManager.Instance._userDao.saveGameScore();
            }
        }

        /// <summary>
        /// 更新金币数
        /// </summary>
        /// <param name="gold"></param>
        public void addGold(int gold)
        {
            EntityManager.Instance._userEntity._goldCount += gold;
            DaoManager.Instance._userDao.saveGoldCount();
        }

        /// <summary>
        /// 更新收藏品
        /// </summary>
        public void updateCollection(Dictionary<int, bool> collDict)
        {
            foreach (var item in collDict)
            {
                if (item.Value)
                {
                    addCollection(item.Key);
                }
            }
            DaoManager.Instance._userDao.saveCollection();
        }

        /// <summary>
        /// 更新引导状态
        /// </summary>
        public void updateGuide()
        {
            EntityManager.Instance._userEntity._isCheckGuide = false;
            DaoManager.Instance._userDao.saveGuide();
        }

        /// <summary>
        /// 重置游戏，清理存档
        /// </summary>
        public void resetGame()
        {
            DaoManager.Instance._userDao.reset();
        }


        /// <summary>
        /// 获取Tip的I18N字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string getTipI18N(TipData data)
        {
            string text = "";
            switch (EntityManager.Instance._userEntity._curLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    text = data._tipCN;
                    break;
                case SystemLanguage.ChineseTraditional:
                    text = data._tipTW;
                    break;
                case SystemLanguage.English:
                    text = data._tipEN;
                    break;
                default:
                    text = data._tipEN;
                    break;
            }
            return text;
        }

        /// <summary>
        /// 获取商品名的I18N字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string getPayInfoNameI18N(PayInfoData data)
        {
            string text = "";
            switch (EntityManager.Instance._userEntity._curLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    text = data._name;
                    break;
                case SystemLanguage.ChineseTraditional:
                    text = data._nameTW;
                    break;
                case SystemLanguage.English:
                    text = data._nameEN;
                    break;
                default:
                    text = data._nameEN;
                    break;
            }
            return text;
        }

        /// <summary>
        /// 获取商品描述的I18N字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string getPayInfoDescI18N(PayInfoData data)
        {
            string text = "";
            switch (EntityManager.Instance._userEntity._curLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    text = data._desc;
                    break;
                case SystemLanguage.ChineseTraditional:
                    text = data._descTW;
                    break;
                case SystemLanguage.English:
                    text = data._descEN;
                    break;
                default:
                    text = data._descEN;
                    break;
            }
            return text;
        }

        /// <summary>
        /// 获取道具名的I18N字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string getItemNameI18N(ItemData data)
        {
            string text = "";
            switch (EntityManager.Instance._userEntity._curLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    text = data._itemNameCN;
                    break;
                case SystemLanguage.ChineseTraditional:
                    text = data._itemNameTW;
                    break;
                case SystemLanguage.English:
                    text = data._itemNameEN;
                    break;
                default:
                    text = data._itemNameEN;
                    break;
            }
            return text;
        }

        /// <summary>
        /// 获取道具描述的I18N字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string getItemDescI18N(ItemData data)
        {
            string text = "";
            switch (EntityManager.Instance._userEntity._curLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    text = data._descCN;
                    break;
                case SystemLanguage.ChineseTraditional:
                    text = data._descTW;
                    break;
                case SystemLanguage.English:
                    text = data._descEN;
                    break;
                default:
                    text = data._descEN;
                    break;
            }
            return text;
        }

        /// <summary>
        /// 获取国际化字符串
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string getTextI18N(int id)
        {
            I18NData data = DaoManager.Instance._i18NDao._i18NDict[id];
            string text = "";
            switch (EntityManager.Instance._userEntity._curLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    text = data._simplifiedChinese;
                    break;
                case SystemLanguage.ChineseTraditional:
                    text = data._traditionalChinese;
                    break;
                case SystemLanguage.English:
                    text = data._english;
                    break;
                default:
                    text = data._english;
                    break;
            }
            return text;
        }

        /// <summary>
        /// 是否为汉语
        /// </summary>
        /// <returns></returns>
        public bool isChinese()
        {

            if (EntityManager.Instance._userEntity._curLanguage == SystemLanguage.Chinese
                || EntityManager.Instance._userEntity._curLanguage == SystemLanguage.ChineseSimplified
                || EntityManager.Instance._userEntity._curLanguage == SystemLanguage.ChineseTraditional)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
