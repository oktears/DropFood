using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chengzi
{

    /// <summary>
    /// 圖集管理器
    /// </summary>
    public class TextureAtlasManager : Singleton<TextureAtlasManager>
    {

        public const string ATLAS_RACE_UI_PATH = "UI/Texture/RaceScene/RaceUI/raceUI";

        public const string PROP_PATH = "UI/Texture/Icon/Icon";

        public const string ATLAS_LADDER_PATH = "UI/Texture/MainScene/Ladder/manyPeople"; 

        public const string ATLAS_SIGNIN_UI_PATH = "UI/Texture/MainScene/Main/signIn";
        public const string ATLAS_LICENSE_UI_PATH = "UI/Texture/MainScene/License/license";
        public const string ATLAS_GARAGE_UI_PATH_MOD = "UI/Texture/MainScene/Garage/Mod/ModAtlas";

        public const string AILAS_ROLE_UI_PATH = "UI/Texture/MainScene/Role/RoleView";
        public const string AILAS_ROLE_PHOTO_PATH = "UI/Texture/MainScene/Role/role";

        public const string ATLAS_LEVEL_UI_PATH = "UI/Texture/MainScene/Level/chooseStageUI";
        public const string ATLAS_LEVEL_PIC = "UI/Texture/MainScene/Level/stage_pic";

        public const string ATLAS_MAIN_UI_ACHIEVEMENT = "UI/Texture/MainScene/Main/AchievementAtlas";

        public const string ATLAS_GARAGE_UI_PATH = "UI/Texture/MainScene/Garage/garageAtlas";

        public const string ATLAS_RACE_SMALLMAP = "UI/Texture/RaceScene/Base/smallMap";
        public const string ATLAS_MAIN_UI_MAIN_VIEW = "UI/Texture/MainScene/Main/mainViewUI";
        public const string ATLAS_GARAGE_DEBUG = "UI/Texture/MainScene/Garage/GarageDebugAtlas";
        public const string ATLAS_PAY = "UI/Texture/Pay/pay";
        public const string ATLAS_RACE_PVP = "UI/Texture/RaceScene/RacePVP/PVPRaceAtlas";

        public const string ATLAS_ROOM_VIEW = "UI/Texture/MainScene/RoomView/RoomView";
        //public static const string ATLAS_RACE = "..../";
        //public static const string ATLAS_RACE = "..../";

        private Dictionary<string, Sprite[]> _atlasDic = new Dictionary<string, Sprite[]>();

        public Sprite loadAtlasSprite(string atlasPath, string textureName)
        {
            Sprite spr = null;
            if (_atlasDic.ContainsKey(atlasPath))
            {
                spr = findSprite(atlasPath, textureName);
            }
            else
            {
                Sprite[] sprArr = Resources.LoadAll<Sprite>(atlasPath);
                _atlasDic.Add(atlasPath, sprArr);
                spr = spriteFormAtlas(sprArr, textureName);
            }

            return spr;
        }

        public Dictionary<string, Sprite> loadAtlasSprite(string atlasPath, List<string> textureName)
        {
            Dictionary<string, Sprite> map = new Dictionary<string, Sprite>();
            Sprite[] sprArr = Resources.LoadAll<Sprite>(atlasPath);
            for (int i = 0; i < textureName.Count; i++)
            {
                Sprite spr = findSprite(atlasPath, textureName[i]);
                if (spr == null)
                {
                    _atlasDic.Add(atlasPath, sprArr);
                    spr = spriteFormAtlas(sprArr, textureName[i]);
                }
                if (!map.ContainsKey(textureName[i]))
                    map.Add(textureName[i], spr);
            }
            return map;
        }

        public void deleteAtlas(string atlasPath)
        {
            if (_atlasDic.ContainsKey(atlasPath))
            {
                _atlasDic.Remove(atlasPath);
            }
        }

        public void clear()
        {
            _atlasDic.Clear();
        }

        private Sprite findSprite(string atlasPath, string textureName)
        {
            if (_atlasDic.ContainsKey(atlasPath))
            {
                Sprite[] sprArr = _atlasDic[atlasPath];
                Sprite spr = spriteFormAtlas(sprArr, textureName);
                return spr;
            }
            return null;
        }

        private Sprite spriteFormAtlas(Sprite[] sprArr, string textureName)
        {
            for (int i = 0; i < sprArr.Length; i++)
            {
                if (sprArr[i].GetType() == typeof(UnityEngine.Sprite))
                {
                    if (sprArr[i].name == textureName)
                    {
                        return sprArr[i];
                    }
                }
            }
            Debug.LogWarning("图片名:" + textureName + ";在图集中找不到");
            return null;
        }
    }

}