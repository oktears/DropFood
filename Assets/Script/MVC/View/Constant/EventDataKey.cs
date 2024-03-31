using UnityEngine;
using System.Collections;

namespace Chengzi
{
    public class EventDataKey
    {
        //加载进度
        public const string LAODING_PROGRESS = "loadingProgress";

        //关卡ID 
        public const string RACE_STAGE_ID = "stageId";

        //统计类
        public const string RACE_STATISTICS = "raceStatistics";
        public const string RACE_SCENELOADER = "SceneLoader";

        public const string RACE_CUTDOWN = "cutdown";
        public const string RACE_CUTDOWN_NORMAL = "cutdown_normal";
        public const string RACE_ELIMINATION_RANK = "elimination_rank";

        public const string LICENSE_SETTLE_VICTORY = "licenseSettleVictory";

        public const string RACE_COMPETE = "RACECOMPETE";

        public const string RACE_IS_RUNNING_OVER = "isrunningover";
        public const string RACE_KNOCKOUT_SHOW_VICTORY = "raceknockoutshowvictory";

        public const string RACE_SCENE_LOADER = "SceneLoader";
        public const string RACE_IS_WIN = "RaceIsWin";


        public const string ROOM_ID_STATE = "RoomIDState";
        public const string ROOM_CREATED_RESULT = "RoomCreatedResult";
        public const string ROOM_ID_ENTER_AFTER = "RoomIDEnterAfter";
        public const string ROOM_PLAYER_DATA = "RoomPlayerData";

        public const string ROOM_HEARTBEAT = "RoomHeartBeat";

    }
}