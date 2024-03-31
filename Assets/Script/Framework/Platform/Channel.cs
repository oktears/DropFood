using UnityEngine;
using System.Collections;

namespace Chengzi
{

    /// <summary>
    /// 渠道Id
    /// </summary>
    public enum Channel
    {
        // 三网母包
        SANWANG_MUBAO = 1005,

        LIAN_TONG = 1000,
        DIAN_XIN = 1001,
        MI_GU = 1003,
        MM = 1002,
        PI_PA_WANG = 1007, // 琵琶网
        SHANG_HAI_2345 = 1008, // 上海二三四五 1008
        YOU_KU = 1009, // 优酷 1009
        QI_KE_CHUANG_XIANG = 1010, // 奇客创想 1010
        KOUDAI_BUS = 1011, // 口袋巴士 1011
        BEIJING_QIANCHI = 1012, // 北京千尺 1012
        BEIJING_ENDU = 1013, // 北京恩度 1013

        TENCENT_YIYIONG_SHICHANG = 1014, // 腾讯应用市场 1014
        TENCENT_GAME_CENTER = 1015, // 腾讯游戏中心 1015
        TENCENT_YINGYONG_BAO = 1016, // 腾讯应用宝 1016

        // 百度
        BAIDU_DUO_KU = 1017, // 多酷
        BAIDU_91 = 1018, // 91
        BAIDU_TIEBA = 1019, // 贴吧
        BAIDU_SHOUJI_ZHUSHOU = 1020, // 手机助手

        KUGOU = 1021, // 酷狗游戏 1021
        UC_JIU_YOU = 1022, // UC九游 1022

        NING_MENG = 1023, // 柠檬助手 1023
        YOU_YI = 1024, // 优亿市场 1024
        YI_JIE = 3000, // 易接 3000
        LE_SHI = 1025, // 乐视 1025
        // 10-16日 易接版本 3001
        JIN_LI = 1026, // 金立游戏大厅 1026
        LI_QU_MARKET = 1027, // 历趣市场 1027
        YING_YONG_HUI = 1028, // 应用汇 1028
        AN_ZHI = 1029, // 安智市场 1029
        BAO_RUAN_KE_JI = 1030, // 宝软科技 1030
        MU_ZHI_WAN = 1031, // 拇指玩

        Qi_HOO_360 = 1032, //360

        AI_QI_YI = 1033,	//爱奇艺
        HUA_WEI = 1034, 	//华为
        MEI_ZU = 1035,	//魅族
        Vivo = 1036,		//Vivo
        OPPO = 1037,		//OPPO
        XIAO_MI = 1038,	//小米
        LIAN_XIANG = 1039,//联想
        COOLPAD = 1040,//酷派
        SOUGOU_PC = 1041 ,//搜狗pc
	    SOUGOU_SOUSUO = 1042,//搜狗搜索
	    SOUGOU_ZHUSHOU=1043,//搜狗助手
	    SOUGOU_SHOUYOU=1044,//搜狗手游
	    ZTE=1045,//中兴
	    SAMSUNG=    1046,
	    M4399=1047, // 4399 
	    YOS=1048,//阿里云
	    LianXiang_JiTuan=1049,//联想集团
	    LianXiang_Adver=1050,//联想广告推送集团
	    ZHUO_YI=1051, // 卓易市场=1051,//卓易市场卓悠网络
	    ZhongChuan=1052,//卓易市场卓悠网络
	    Leshi_1=1053,//替代乐视的渠道1
	    Leshi_2=1054,//替代乐视的渠道2
	    WDJ=1055, //豌豆荚
	    TENCENT_YINGYONG_QQBrowser=1056,//腾讯QQ浏览器 
        //本地测试
        TEST = 2000,
        
        //AppStore
        IOS_APPSTORE = 3001,
        
        //线下
        OFFLINE_ANDROID = 4001,
    }
}