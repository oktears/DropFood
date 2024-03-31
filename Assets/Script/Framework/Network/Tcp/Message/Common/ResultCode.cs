namespace Chengzi
{
    public enum ResultCode
    {
        /// <summary>请求失败</summary>
        RESULT_CODE_FAIL = 0,
        /// <summary>请求成功</summary>
        RESULT_CODE_SUCCESS = 1,
        /// <summary></summary>
        RESULT_CODE_NOVIP = 2,
        /// <summary></summary>
        RESULT_CODE_CION_ERROR = 3,
        /// <summary></summary>
        RESULT_CODE_PASSWD_ERROR = 4,
        /// <summary></summary>
        RESULT_CODE_NEED_INLOBBY = 5,
        /// <summary></summary>
        RESULT_CODE_REPEAT_GET = 6,
        /// <summary></summary>
        RESULT_CODE_NOT_COND = 7,
        /// <summary></summary>
        RESULT_CODE_ERROR_PARAM = 8,
        /// <summary></summary>
        RESULT_CODE_NOT_TABLE = 9,
        /// <summary></summary>
        RESULT_CODE_NOT_OWER = 10,
        /// <summary></summary>
        RESULT_CODE_BLACKLIST = 11,
        /// <summary></summary>
        RESULT_CODE_NOT_DIAMOND = 12,
        /// <summary></summary>
        RESULT_CODE_ERROR_PLAYERID = 13,
        /// <summary></summary>
        RESULT_CODE_TABLE_FULL = 14,
        /// <summary></summary>
        RESULT_CODE_GAMEING = 15,
        /// <summary></summary>
        RESULT_CODE_ERROR_STATE = 16,
        /// <summary></summary>
        RESULT_CODE_LOGIN_OTHER = 17,
        /// <summary></summary>
        RESULT_CODE_SVR_REPAIR = 18,
        /// <summary></summary>
        RESULT_CODE_CDING = 19,
        /// <summary>已经有桌子了</summary>
        RESULT_CODE_HAVE_TABLE = 20,
        /// <summary>已经准备好了</summary>
        RESULT_CODE_IS_READY = 21,
        /// <summary></summary>
        RESULT_CODE_HAVE_CHANGED = 22
    }
}
