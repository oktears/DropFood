//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: error_code.proto
namespace Chengzi
{
    [global::ProtoBuf.ProtoContract(Name=@"RESULT_CODE")]
    public enum RESULT_CODE
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_FAIL", Value=0)]
      RESULT_CODE_FAIL = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_SUCCESS", Value=1)]
      RESULT_CODE_SUCCESS = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_NOVIP", Value=2)]
      RESULT_CODE_NOVIP = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_CION_ERROR", Value=3)]
      RESULT_CODE_CION_ERROR = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_PASSWD_ERROR", Value=4)]
      RESULT_CODE_PASSWD_ERROR = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_NEED_INLOBBY", Value=5)]
      RESULT_CODE_NEED_INLOBBY = 5,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_REPEAT_GET", Value=6)]
      RESULT_CODE_REPEAT_GET = 6,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_NOT_COND", Value=7)]
      RESULT_CODE_NOT_COND = 7,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_ERROR_PARAM", Value=8)]
      RESULT_CODE_ERROR_PARAM = 8,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_NOT_TABLE", Value=9)]
      RESULT_CODE_NOT_TABLE = 9,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_NOT_OWER", Value=10)]
      RESULT_CODE_NOT_OWER = 10,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_BLACKLIST", Value=11)]
      RESULT_CODE_BLACKLIST = 11,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_NOT_DIAMOND", Value=12)]
      RESULT_CODE_NOT_DIAMOND = 12,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_ERROR_PLAYERID", Value=13)]
      RESULT_CODE_ERROR_PLAYERID = 13,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_TABLE_FULL", Value=14)]
      RESULT_CODE_TABLE_FULL = 14,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_GAMEING", Value=15)]
      RESULT_CODE_GAMEING = 15,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_ERROR_STATE", Value=16)]
      RESULT_CODE_ERROR_STATE = 16,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_LOGIN_OTHER", Value=17)]
      RESULT_CODE_LOGIN_OTHER = 17,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_SVR_REPAIR", Value=18)]
      RESULT_CODE_SVR_REPAIR = 18,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_CDING", Value=19)]
      RESULT_CODE_CDING = 19,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_HAVE_TABLE", Value=20)]
      RESULT_CODE_HAVE_TABLE = 20,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_IS_READY", Value=21)]
      RESULT_CODE_IS_READY = 21,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RESULT_CODE_HAVE_CHANGED", Value=22)]
      RESULT_CODE_HAVE_CHANGED = 22
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"MISSION_TYPE")]
    public enum MISSION_TYPE
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"MISSION_TYPE_PLAY", Value=101)]
      MISSION_TYPE_PLAY = 101,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MISSION_TYPE_WIN", Value=102)]
      MISSION_TYPE_WIN = 102,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MISSION_TYPE_KILL", Value=103)]
      MISSION_TYPE_KILL = 103,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MISSION_TYPE_PRESS", Value=104)]
      MISSION_TYPE_PRESS = 104,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MISSION_TYPE_FEEWIN", Value=105)]
      MISSION_TYPE_FEEWIN = 105,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MISSION_TYPE_FEELOSE", Value=106)]
      MISSION_TYPE_FEELOSE = 106
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"MISSION_CYCLE_TYPE")]
    public enum MISSION_CYCLE_TYPE
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"MISSION_CYCLE_TYPE_DAY", Value=1)]
      MISSION_CYCLE_TYPE_DAY = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MISSION_CYCLE_TYPE_WEEK", Value=2)]
      MISSION_CYCLE_TYPE_WEEK = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MISSION_CYCLE_TYPE_MONTH", Value=3)]
      MISSION_CYCLE_TYPE_MONTH = 3
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"REWARD_FLAG")]
    public enum REWARD_FLAG
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"REWARD_CLOGIN", Value=1)]
      REWARD_CLOGIN = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"REWARD_ALOGIN1", Value=2)]
      REWARD_ALOGIN1 = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"REWARD_ALOGIN2", Value=3)]
      REWARD_ALOGIN2 = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"REWARD_ALOGIN3", Value=4)]
      REWARD_ALOGIN3 = 4
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"GAME_CATE_TYPE")]
    public enum GAME_CATE_TYPE
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_SX_KOUDIAN_CHALLENGECAR", Value=1)]
      GAME_SX_KOUDIAN_CHALLENGECAR = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_CATE_SHOWHAND", Value=2)]
      GAME_CATE_SHOWHAND = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_CATE_BULLFIGHT", Value=3)]
      GAME_CATE_BULLFIGHT = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_CATE_TEXAS", Value=4)]
      GAME_CATE_TEXAS = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_CATE_ZAJINHUA", Value=5)]
      GAME_CATE_ZAJINHUA = 5,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_CATE_NIUNIU", Value=6)]
      GAME_CATE_NIUNIU = 6,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_CATE_BACCARAT", Value=7)]
      GAME_CATE_BACCARAT = 7,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_CATE_SANGONG", Value=8)]
      GAME_CATE_SANGONG = 8,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_CATE_PAIJIU", Value=9)]
      GAME_CATE_PAIJIU = 9,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_CATE_THIRTEEN", Value=10)]
      GAME_CATE_THIRTEEN = 10,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_CATE_MAX_TYPE", Value=11)]
      GAME_CATE_MAX_TYPE = 11
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"GAME_SUB_TYPE")]
    public enum GAME_SUB_TYPE
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_SUB_COMMON", Value=1)]
      GAME_SUB_COMMON = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_SUB_MATCH", Value=2)]
      GAME_SUB_MATCH = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_SUB_PRIVATE", Value=3)]
      GAME_SUB_PRIVATE = 3
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"EXCHANGE_TYPE")]
    public enum EXCHANGE_TYPE
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"EXCHANGE_TYPE_SCORE", Value=1)]
      EXCHANGE_TYPE_SCORE = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"EXCHANGE_TYPE_COIN", Value=2)]
      EXCHANGE_TYPE_COIN = 2
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"ROOM_CONSUME_TYPE")]
    public enum ROOM_CONSUME_TYPE
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"ROOM_CONSUME_TYPE_SCORE", Value=1)]
      ROOM_CONSUME_TYPE_SCORE = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ROOM_CONSUME_TYPE_COIN", Value=2)]
      ROOM_CONSUME_TYPE_COIN = 2
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"ROOM_DEAL_TYPE")]
    public enum ROOM_DEAL_TYPE
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"ROOM_DEAL_TYPE_SOLO", Value=1)]
      ROOM_DEAL_TYPE_SOLO = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ROOM_DEAL_TYPE_THREE", Value=2)]
      ROOM_DEAL_TYPE_THREE = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ROOM_DEAL_TYPE_SEVEN", Value=3)]
      ROOM_DEAL_TYPE_SEVEN = 3
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"TABLE_FEE_TYPE")]
    public enum TABLE_FEE_TYPE
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"TABLE_FEE_TYPE_NO", Value=0)]
      TABLE_FEE_TYPE_NO = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"TABLE_FEE_TYPE_ALLBASE", Value=1)]
      TABLE_FEE_TYPE_ALLBASE = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"TABLE_FEE_TYPE_WIN", Value=2)]
      TABLE_FEE_TYPE_WIN = 2
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"TABLE_STATE")]
    public enum TABLE_STATE
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"TABLE_STATE_FREE", Value=1)]
      TABLE_STATE_FREE = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"TABLE_STATE_CALL", Value=2)]
      TABLE_STATE_CALL = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"TABLE_STATE_PLAY", Value=3)]
      TABLE_STATE_PLAY = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"TABLE_STATE_WAIT", Value=4)]
      TABLE_STATE_WAIT = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"TABLE_STATE_GAME_END", Value=5)]
      TABLE_STATE_GAME_END = 5
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"TABLE_STATE_NIUNIU")]
    public enum TABLE_STATE_NIUNIU
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"TABLE_STATE_NIUNIU_FREE", Value=1)]
      TABLE_STATE_NIUNIU_FREE = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"TABLE_STATE_NIUNIU_PLACE_JETTON", Value=2)]
      TABLE_STATE_NIUNIU_PLACE_JETTON = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"TABLE_STATE_NIUNIU_GAME_END", Value=3)]
      TABLE_STATE_NIUNIU_GAME_END = 3
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"EnumchallengecarTableState")]
    public enum EnumchallengecarTableState
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"CHALLENGECAR_TABLE_STATE_FREE", Value=1)]
      CHALLENGECAR_TABLE_STATE_FREE = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CHALLENGECAR_TABLE_STATE_RESET_SEAT", Value=2)]
      CHALLENGECAR_TABLE_STATE_RESET_SEAT = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CHALLENGECAR_TABLE_STATE_READY", Value=3)]
      CHALLENGECAR_TABLE_STATE_READY = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CHALLENGECAR_TABLE_STATE_GAMING", Value=4)]
      CHALLENGECAR_TABLE_STATE_GAMING = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CHALLENGECAR_TABLE_STATE_REACH", Value=5)]
      CHALLENGECAR_TABLE_STATE_REACH = 5,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CHALLENGECAR_TABLE_STATE_END", Value=6)]
      CHALLENGECAR_TABLE_STATE_END = 6
    }
  
}