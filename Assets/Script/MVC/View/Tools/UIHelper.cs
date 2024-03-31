using Chengzi;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public static class UIHelper
{
    /// <summary>归零</summary>
    public static void setParent(this RectTransform rectTransform, Transform father)
    {
        rectTransform.SetParent(father);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.sizeDelta = new Vector2(1, 1);
        rectTransform.localScale = Vector3.one;
    }

    public static void setParent(this RectTransform rectTransform, RectTransform father)
    {
        rectTransform.SetParent(father);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.sizeDelta = new Vector2(1, 1);
        rectTransform.localScale = Vector3.one;
    }

    /// <summary>归零</summary>
    public static void setParent(this Transform rectTransform, Transform father)
    {
        rectTransform.SetParent(father);
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
        rectTransform.localRotation = Quaternion.identity;
    }
    /// <summary>归零</summary>

    public static void Reset(this RectTransform rectTransform, Transform father)
    {
        rectTransform.SetParent(father);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.localScale = new Vector3(1, 1, 1);
    }

    public static void ResetTransform(this Transform transform, Transform father)
    {
        transform.SetParent(father);
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }


    /// <summary>把子物体加到父物体上</summary>
    static public GameObject addChild(Transform parent, string prefabPath)
    {
        GameObject go = PrefabPool.Instance.getObject(prefabPath);

        if (go != null && parent != null)
        {
            Transform t = go.transform;
            t.transform.setParent(parent.transform);
            go.layer = parent.gameObject.layer;
        }
        return go;
    }

    public static GameObject addChild(GameObject parent, GameObject prefab)
    {
        GameObject go = PrefabPool.Instance.getObject(prefab);

        if (go != null && parent != null)
        {
            Transform t = go.transform;
            t.transform.setParent(parent.transform);
            go.layer = parent.gameObject.layer;
        }
        return go;
    }

    /// <summary>
    /// Instantiate an object and add it to the specified parent.
    /// </summary>
    static public GameObject addChild(this GameObject parent, string prefabPath)
    {
        GameObject go = PrefabPool.Instance.getObject(prefabPath);
#if UNITY_EDITOR
        UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
#endif
        if (go != null && parent != null)
        {
            Transform t = go.transform;
            RectTransform rt = t.GetComponent<RectTransform>();

            if (rt != null)
            {
                t.GetComponent<RectTransform>().setParent(parent.GetComponent<RectTransform>());
            }
            else
            {
                t.setParent(parent.transform);
            }
            go.layer = parent.layer;
        }
        return go;
    }

    public static void BindButtonEvent(this Button button, EventTriggerType type, UnityAction<BaseEventData> callback)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            button.gameObject.AddComponent<EventTrigger>();
            trigger = button.gameObject.GetComponent<EventTrigger>();
        }
        if (trigger.triggers == null)
        {
            trigger.triggers = new List<EventTrigger.Entry>();
        }

        EventTrigger.Entry entry = null;
        for (int i = 0; i < trigger.triggers.Count; i++)
        {
            if (trigger.triggers[i].eventID == type)
            {
                entry = trigger.triggers[i];
                break;
            }
        }
        if (entry == null)
        {
            entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback = new EventTrigger.TriggerEvent();
            trigger.triggers.Add(entry);
        }

        entry.callback.AddListener(callback);
    }

    public static void RemoveButtonEvent(this Button button, EventTriggerType type, UnityAction<BaseEventData> callback)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        do
        {
            if (trigger == null) break;
            for (int i = 0; i < trigger.triggers.Count; i++)
            {
                if (trigger.triggers[i].eventID == type)
                {
                    trigger.triggers[i].callback.RemoveListener(callback);
                    break;
                }
            }
        } while (false);
    }

    public static bool bindEvent(this Button obj, EventTriggerType type, _pointEvent callback)
    {
        bool ret = false;
        OnEventTrigger eventTri = obj.GetComponent<OnEventTrigger>();
        if (eventTri == null) eventTri = obj.gameObject.AddComponent<OnEventTrigger>();

        switch (type)
        {
            case EventTriggerType.PointerEnter:
                eventTri._onPointerEnter += callback;
                ret = true;
                break;
            case EventTriggerType.PointerExit:
                eventTri._onPointerExit += callback;
                ret = true;
                break;
            case EventTriggerType.PointerDown:
                eventTri._onPointerDown += callback;
                ret = true;
                break;
            case EventTriggerType.PointerUp:
                eventTri._onPointerUp += callback;
                ret = true;
                break;
            case EventTriggerType.PointerClick:
                eventTri._onPointerClick += callback;
                ret = true;
                break;
            case EventTriggerType.Scroll:
                eventTri._onScroll += callback;
                ret = true;
                break;
            case EventTriggerType.InitializePotentialDrag:
                eventTri._onInitializePotentialDrag += callback;
                ret = true;
                break;
            case EventTriggerType.BeginDrag:
                eventTri._onBeginDrag += callback;
                ret = true;
                break;
            case EventTriggerType.EndDrag:
                eventTri._onEndDrag += callback;
                ret = true;
                break;
            case EventTriggerType.Drag:
                eventTri._onDrag += callback;
                ret = true;
                break;
            case EventTriggerType.Drop:
                eventTri._onDrop += callback;
                ret = true;
                break;
        }
        return ret;
    }

    public static bool bindEvent(this Button obj, EventTriggerType type, _baseEvent callback)
    {
        bool ret = false;
        OnEventTrigger eventTri = obj.GetComponent<OnEventTrigger>();
        if (eventTri == null) eventTri = obj.gameObject.AddComponent<OnEventTrigger>();

        switch (type)
        {
            case EventTriggerType.UpdateSelected:
                eventTri._onUpdateSelected += callback;
                ret = true;
                break;
            case EventTriggerType.Select:
                eventTri._onSelect += callback;
                ret = true;
                break;
            case EventTriggerType.Submit:
                eventTri._onSubmit += callback;
                ret = true;
                break;
            case EventTriggerType.Deselect:
                eventTri._onDeselect += callback;
                ret = true;
                break;
            case EventTriggerType.Cancel:
                eventTri._onCancel += callback;
                ret = true;
                break;
        }
        return ret;
    }

    public static bool bindEvent(this Button obj, EventTriggerType type, _axisEvent callback)
    {
        bool ret = false;
        OnEventTrigger eventTri = obj.GetComponent<OnEventTrigger>();
        if (eventTri == null) eventTri = obj.gameObject.AddComponent<OnEventTrigger>();

        switch (type)
        {
            case EventTriggerType.Move:
                eventTri._onMove += callback;
                ret = true;
                break;
        }
        return ret;
    }

    public static bool unBindEvent(this Button obj, EventTriggerType type, _pointEvent callback)
    {
        bool ret = false;
        OnEventTrigger eventTri = obj.GetComponent<OnEventTrigger>();
        if (eventTri == null) eventTri = obj.gameObject.AddComponent<OnEventTrigger>();

        switch (type)
        {
            case EventTriggerType.PointerEnter:
                eventTri._onPointerEnter -= callback;
                ret = true;
                break;
            case EventTriggerType.PointerExit:
                eventTri._onPointerExit -= callback;
                ret = true;
                break;
            case EventTriggerType.PointerDown:
                eventTri._onPointerDown -= callback;
                ret = true;
                break;
            case EventTriggerType.PointerUp:
                eventTri._onPointerUp -= callback;
                ret = true;
                break;
            case EventTriggerType.PointerClick:
                eventTri._onPointerClick -= callback;
                ret = true;
                break;
            case EventTriggerType.Scroll:
                eventTri._onScroll -= callback;
                ret = true;
                break;
            case EventTriggerType.InitializePotentialDrag:
                eventTri._onInitializePotentialDrag -= callback;
                ret = true;
                break;
            case EventTriggerType.BeginDrag:
                eventTri._onBeginDrag -= callback;
                ret = true;
                break;
            case EventTriggerType.EndDrag:
                eventTri._onEndDrag -= callback;
                ret = true;
                break;
            case EventTriggerType.Drag:
                eventTri._onDrag -= callback;
                ret = true;
                break;
            case EventTriggerType.Drop:
                eventTri._onDrop -= callback;
                ret = true;
                break;
        }
        return ret;
    }

    public static bool unBindEvent(this Button obj, EventTriggerType type, _baseEvent callback)
    {
        bool ret = false;
        OnEventTrigger eventTri = obj.GetComponent<OnEventTrigger>();
        if (eventTri == null) eventTri = obj.gameObject.AddComponent<OnEventTrigger>();

        switch (type)
        {
            case EventTriggerType.UpdateSelected:
                eventTri._onUpdateSelected -= callback;
                ret = true;
                break;
            case EventTriggerType.Select:
                eventTri._onSelect -= callback;
                ret = true;
                break;
            case EventTriggerType.Submit:
                eventTri._onSubmit -= callback;
                ret = true;
                break;
            case EventTriggerType.Deselect:
                eventTri._onDeselect -= callback;
                ret = true;
                break;
            case EventTriggerType.Cancel:
                eventTri._onCancel -= callback;
                ret = true;
                break;
        }
        return ret;
    }

    public static bool unBindEvent(this Button obj, EventTriggerType type, _axisEvent callback)
    {
        bool ret = false;
        OnEventTrigger eventTri = obj.GetComponent<OnEventTrigger>();
        if (eventTri == null) eventTri = obj.gameObject.AddComponent<OnEventTrigger>();

        switch (type)
        {
            case EventTriggerType.Move:
                eventTri._onMove -= callback;
                ret = true;
                break;
        }
        return ret;
    }



    public static bool bindEvent(this GameObject obj, EventTriggerType type, _pointEvent callback)
    {
        bool ret = false;
        OnEventTrigger eventTri = obj.GetComponent<OnEventTrigger>();
        if (eventTri == null) eventTri = obj.gameObject.AddComponent<OnEventTrigger>();

        switch (type)
        {
            case EventTriggerType.PointerEnter:
                eventTri._onPointerEnter += callback;
                ret = true;
                break;
            case EventTriggerType.PointerExit:
                eventTri._onPointerExit += callback;
                ret = true;
                break;
            case EventTriggerType.PointerDown:
                eventTri._onPointerDown += callback;
                ret = true;
                break;
            case EventTriggerType.PointerUp:
                eventTri._onPointerUp += callback;
                ret = true;
                break;
            case EventTriggerType.PointerClick:
                eventTri._onPointerClick += callback;
                ret = true;
                break;
            case EventTriggerType.Scroll:
                eventTri._onScroll += callback;
                ret = true;
                break;
            case EventTriggerType.InitializePotentialDrag:
                eventTri._onInitializePotentialDrag += callback;
                ret = true;
                break;
            case EventTriggerType.BeginDrag:
                eventTri._onBeginDrag += callback;
                ret = true;
                break;
            case EventTriggerType.EndDrag:
                eventTri._onEndDrag += callback;
                ret = true;
                break;
            case EventTriggerType.Drag:
                eventTri._onDrag += callback;
                ret = true;
                break;
            case EventTriggerType.Drop:
                eventTri._onDrop += callback;
                ret = true;
                break;
        }
        return ret;
    }


    /// <summary>
    /// 用于显示UI的相关函数从左侧填满
    /// </summary>
    /// <param name="num">需要输出的数字</param>
    /// <param name="Objs">输出的对象（从个位开始）</param>
    /// <param name="sprites">需要的sprite(0-9的排序)</param>
    public static void PrintUILabelLeft(int num, List<Image> images, List<Sprite> sprites)
    {
        Color fullColor = new Color(1, 1, 1, 1);
        Color noColor = new Color(1, 1, 1, 0);
        int length = 1;
        while (true)
        {
            if (num < Digit(length + 1))
                break;
            length++;
        }

        if (num < 10)
        {
            for (int i = 0; i < images.Count; i++)
            {
                if (i == images.Count - 1)
                {
                    images[i].sprite = sprites[num / 1];
                    if (images[i].enabled == false)
                    {
                        images[i].enabled = true;
                    }
                }
                else
                {
                    if (images[i].enabled == true)
                    {
                        images[i].enabled = false;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < images.Count; i++)
            {
                if (i < length)
                {
                    images[i + (images.Count - length)].sprite = sprites[(num % Digit(i + 2) - num % Digit(i + 1)) / Digit(i + 1)];
                    if (images[i + (images.Count - length)].enabled == false)
                    {
                        images[i + (images.Count - length)].enabled = true;
                    }
                }
                else
                {
                    if (images[i - length].enabled == true)
                    {
                        images[i - length].enabled = false;
                    }
                }

            }
        }
    }

    /// <summary>
    /// 用于显示UI的相关函数从右侧填满
    /// </summary>
    /// <param name="num">需要输出的数字</param>
    /// <param name="Objs">输出的对象（从个位开始）</param>
    /// <param name="sprites">需要的sprite(0-9的排序)</param>
    public static void PrintUILabelRight(int num, List<Image> images, List<Sprite> sprites)
    {
        Color fullColor = new Color(1, 1, 1, 1);
        Color noColor = new Color(1, 1, 1, 0);
        int length = 1;
        while (true)
        {
            if (num < Digit(length + 1))
                break;
            length++;
        }

        if (num < 10)
        {
            for (int i = 0; i < images.Count; i++)
            {
                if (i == 0)
                {
                    images[i].sprite = sprites[num / 1];
                    if (images[i].enabled == false)
                    {
                        images[i].enabled = true;
                    }
                }
                else
                {
                    if (images[i].enabled == true)
                    {
                        images[i].enabled = false;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < images.Count; i++)
            {
                if (i < length)
                {
                    images[i].sprite = sprites[(num % Digit(i + 2) - num % Digit(i + 1)) / Digit(i + 1)];
                    if (images[i].enabled == false)
                    {
                        images[i].enabled = true;
                    }

                }
                else
                {
                    if (images[i].enabled == true)
                    {
                        images[i].enabled = false;
                    }
                }

            }
        }
    }

    /// <summary>
    /// get int length
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static int Digit(int length)
    {
        int value = 1;
        for (int i = 1; i < length; i++)
        {
            value *= 10;
        }
        return value;
    }

    /// <summary>
    /// 为按钮添加音效音
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="act"></param>
    public static void addListener(this Button btn, UnityAction act)
    {
        btn.onClick.AddListener(() => { AudioManager.Instance.play(AudioManager.SOUND_SFX_BTN_CLICK); });
        btn.onClick.AddListener(act);
    }


    /// <summary>
    /// 为按钮添加音效音
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="act"></param>
    public static void addListener(this Button btn, UnityAction act, int sfx)
    {
        btn.onClick.AddListener(() => { AudioManager.Instance.play(sfx); });
        btn.onClick.AddListener(act);
    }


    /// <summary>
    /// 获取text的长度
    /// </summary>
    /// <param name="des"></param>
    /// <param name="size"></param>
    /// <param name="font"></param>
    /// <returns></returns>
    public static float getTextWidth(string des, int size, Font font)
    {
        float width = 0;
        font.RequestCharactersInTexture(des, size, FontStyle.Normal);
        CharacterInfo characterInfo;
        for (int i = 0; i < des.Length; i++)
        {
            font.GetCharacterInfo(des[i], out characterInfo, size);
            width += characterInfo.advance;
        }
        return width;
    }

    /// <summary>
    /// 计算字符串在指定text控件中的长度
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static float getTextWidth(Text text)
    {
        float totalLength = 0;
        Font font = Font.CreateDynamicFontFromOSFont(text.font.name, text.fontSize);
        font.RequestCharactersInTexture(text.text, text.fontSize, text.fontStyle);
        CharacterInfo characterInfo;
        char[] arr = text.text.ToCharArray();
        foreach (char c in arr)
        {
            font.GetCharacterInfo(c, out characterInfo);
            totalLength += characterInfo.advance;
        }
        return totalLength;
    }


    public static int getMaxLineCount(Text text)
    {
        var textGenerator = new TextGenerator();
        var generationSettings = text.GetGenerationSettings(text.rectTransform.rect.size);
        var lineCount = 0;
        var s = new StringBuilder();
        while (true)
        {
            textGenerator.Populate(s.ToString(), generationSettings);
            var nextLineCount = textGenerator.lineCount;
            if (lineCount == nextLineCount) break;
            lineCount = nextLineCount;
        }
        return lineCount;
    }

    public static string getAwardStringById(UserConstant.PropType type)
    {
        string ret = string.Empty;
        switch (type)
        {
            case UserConstant.PropType.UNKNOWN:
                break;
            case UserConstant.PropType.GOLD:
                break;
            case UserConstant.PropType.MOD:
                break;
            case UserConstant.PropType.CLIP:
                break;
            case UserConstant.PropType.EXTRACT_SCROLL:
                break;
            case UserConstant.PropType.WHEEL_SCROLL:
                break;
            case UserConstant.PropType.CAR_GTR:
                break;
            case UserConstant.PropType.CAR_LBG:
                break;
            case UserConstant.PropType.CAR_BNFLN:
                break;
            case UserConstant.PropType.CAR_DZ:
                break;
            case UserConstant.PropType.CAR_BJD:
                break;
            case UserConstant.PropType.CAR_AE86:
                break;
            case UserConstant.PropType.CAR_BWM:
                break;
            case UserConstant.PropType.ROLE_SHL:
                break;
            case UserConstant.PropType.ROLE_ZYX:
                break;
            case UserConstant.PropType.ROLE_GIRL:
                break;
            case UserConstant.PropType.ROLE_LZX:
                break;
            case UserConstant.PropType.CAR_SCROLL:
                break;
            case UserConstant.PropType.WEEK_CARD:
                break;
            case UserConstant.PropType.MONTH_CARD:
                break;
        }

        return string.Format("<quad name={0} size=30 width=1 />", ret);
    }

    /// <summary>
    /// 获取富文本颜色串
    /// </summary>
    /// <param name="str"></param>
    /// <param name="colorHex"></param>
    /// <returns></returns>
    public static string getColorString(string str, string colorHex)
    {
        return "<color=" + colorHex + ">" + str + "</color>";
    }

    /// <summary>
    /// 获取富文本颜色串
    /// </summary>
    /// <param name="str"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string getColorString(string str, Color color)
    {
        return "<color=" + getHexColor(color) + ">" + str + "</color>";
    }

    /// <summary>
    /// 获取颜色十六进制格式字符串
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string getHexColor(Color color)
    {
        string R = Convert.ToString(Convert.ToInt32(color.r * 255), 16);
        if (R == "0")
            R = "00";
        string G = Convert.ToString(Convert.ToInt32(color.g * 255), 16);
        if (G == "0")
            G = "00";
        string B = Convert.ToString(Convert.ToInt32(color.b * 255), 16);
        if (B == "0")
            B = "00";
        string hexColor = "#" + R + G + B;
        return hexColor;
    }

}