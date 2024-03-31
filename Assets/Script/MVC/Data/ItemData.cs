using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chengzi
{

    /// <summary>
    /// 道具的获得方式
    /// </summary>
    public enum ItemGainType
    {
        STAGE = 1,
        AD = 2,
        BUY = 3,
    }


    /// <summary>
    /// 道具数据
    /// </summary>
    public class ItemData
    {

        /// <summary>
        /// 道具Id
        /// </summary>
        public int _itemId { get; set; }

        /// <summary>
        /// 道具名，简体
        /// </summary>
        public string _itemNameCN { get; set; }

        /// <summary>
        /// 道具名，繁体
        /// </summary>
        public string _itemNameTW { get; set; }

        /// <summary>
        /// 道具名，英文
        /// </summary>
        public string _itemNameEN { get; set; }

        /// <summary>
        /// 道具类型
        /// </summary>
        public ItemType _itemType { get; set; }

        /// <summary>
        /// 预设
        /// </summary>
        public string _prefab { get; set; }

        /// <summary>
        /// 收藏品界面缩放
        /// </summary>
        public float _scaleInCollection { get; set; }

        /// <summary>
        /// 奖励金币
        /// </summary>
        public int _rewardGold { get; set; }

        /// <summary>
        /// 难度
        /// </summary>
        public int _star { get; set; }

        /// <summary>
        /// 道具获取方式
        /// </summary>
        public ItemGainType _gainType { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public int _price { get; set; }

        /// <summary>
        /// 道具描述，简体
        /// </summary>
        public string _descCN { get; set; }

        /// <summary>
        /// 道具描述，繁体
        /// </summary>
        public string _descTW { get; set; }

        /// <summary>
        /// 道具描述，英文
        /// </summary>
        public string _descEN { get; set; }

        /// <summary>
        /// 美食城Y偏移
        /// </summary>
        public float _offsetYInCollection { get; set; }
    }

}
