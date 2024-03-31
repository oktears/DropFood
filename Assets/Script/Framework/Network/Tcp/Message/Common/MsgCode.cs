namespace Chengzi
{
    /// <summary>
    /// 消息协议号
    /// </summary>
    public enum MsgCode
    {
		/********************************【基础模块】*************************************/

		C2S_MSG_HEART				= 1000,//心跳包
		S2C_MSG_HEART				= 1001,//心跳包返回
		C2S_MSG_LOGIN 	        	= 1002,//登录
		S2C_MSG_LOGIN_REP			= 1003,//登录返回
		S2C_MSG_PLAYER_INFO			= 1011,//玩家信息

		S2C_MSG_UPDATE_ACC_VALUE	= 1013,//更新水晶

		S2C_MSG_RECONNECT			= 1014,//断线重连
		C2S_MSG_ENTER_SVR			= 1102,//进入游戏服务器
		S2C_MSG_ENTER_SVR_REP		= 1103,//进入游戏服务器返回
		/*********************************************************************************/


        
        /*********************************异步推送消息************************************/
        MSGCODE_CLIENT_NET_SUCCESS_RESP = 0x00,             //联网成功
        MSGCODE_CLIENT_NET_ERROR_RESP = 0x01,               //网络异常
        /********************************Host消息*****************************************/
        MSGCODE_PLAYER_CONNECT_REQ = 50001,
        MSGCODE_PLAYER_CONNECT_RESP = 50002,
        /*********************************************************************************/


    }
}
