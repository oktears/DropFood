using Chengzi;
using System.Collections.Generic;
using UnityEngine;

public class RacingViewName
{
    private static RacingViewName instance;
    public static RacingViewName Instance()
    {
        if (instance == null)
            instance = new RacingViewName();
        return instance;
    }

    Dictionary<string, List<Sprite>> m_map; 
    public RacingViewName()
    {
        string m_ui_path = string.Empty;
        m_map = new Dictionary<string, List<Sprite>>();
        List<Sprite> m_ui_name = new List<Sprite>();
        for (int i = 0; i < 10; i++)
        {
            Sprite m_ui_pre;
            m_ui_path = "UITexture/RaceScene/Base/RaceTextures/Start-number-"+i;
            m_ui_pre = PrefabPool.Instance.getPrefab<Sprite>(m_ui_path); 
            m_ui_name.Add(m_ui_pre);
        }
        m_map.Add("common", m_ui_name);

    }

    public List<Sprite> getNameList(string key)
    {
        if (m_map.ContainsKey(key))
            return m_map[key];
        else
            return null;
    }
}
