using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BreathEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool setAllCoroutineNullOnDisable = false;
    public bool m_OwnAnim = false;
    public bool m_StartBreath = false;
    public bool m_CanClick = true;
    public bool scaleXYOnSameValue = true;
    public bool m_NeedBreath = false;
    public bool m_HasChild = false; // child not be affected by parent
    public float m_DelayTime = 2.0f;
    public float m_LoopInterval = 1.0f;
    public float m_Duration = 0.3f;
    public AnimationCurve pressCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public AnimationCurve shakeCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public AnimationCurve shakeCurve1 = AnimationCurve.Linear(0, 0, 1, 1);
    public bool m_LoopAnim = false;

    public bool CanResumeBreath = true;
    //呼吸动画执行状态(保持  停止)
    [NonSerialized]
    public bool isBreathing = false;
    public bool breathing
    {
        get { return m_breathing; }
        set
        {
            m_breathing = value;
        }
    }
    private bool m_breathing = false;
    private Coroutine nextShakeCoroutine;
    private Coroutine delayBreathCoroutine;
    private bool ownAnimLoop = false;
    public bool rotateLeft = false;
    public float rotateSpeed = 5.0f;
    private bool m_CanRotate = false;
    private bool isBreathingInterval = false; //breathing 为true后，到真正执行ShakeAnim有一帧间隙

    public void CopyTo(BreathEffect be)
    {
        be.m_OwnAnim = this.m_OwnAnim;
        be.m_StartBreath = this.m_StartBreath;
        be.m_CanClick = this.m_CanClick;
        be.scaleXYOnSameValue = this.scaleXYOnSameValue;
        be.m_NeedBreath = this.m_NeedBreath;
        be.m_HasChild = this.m_HasChild;
        be.m_DelayTime = this.m_DelayTime;
        be.m_LoopInterval = this.m_LoopInterval;
        be.m_Duration = this.m_Duration;
        be.pressCurve = this.pressCurve;
        be.shakeCurve = this.shakeCurve;
        be.shakeCurve1 = this.shakeCurve1;
        be.btnPressScale = this.btnPressScale;
        be.CanResumeBreath = this.CanResumeBreath;
    }
    public enum OwnAnimType
    {
        TagOn,
        Box
    }

    public OwnAnimType ownAnimType = OwnAnimType.TagOn;


    void Start()
    {
        if (m_NeedBreath)
            delayBreathCoroutine = StartCoroutine(DelayEnableBreath(m_DelayTime));
    }

    public void OnValidate()
    {
        transform.localScale = btnNormalScale * Vector3.one;
    }

    private IEnumerator DelayEnableBreath(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        breathing = true;
        delayBreathCoroutine = null;
    }

    public float btnNormalScale = 1;
    public float btnPressScale = 1.1f;
    public GameObject scaleOnPressObj;
    public float scaleTime = 0.08f;
    private Tweener scaleTween = null;
    private bool BreathStoped = false;

    private bool OrigBreathState = false; // false : 先前未呼吸；true : 先前在呼吸
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsInteractable())
            return;

        if (!m_CanClick)
            return;
        if (!OrigBreathState)
            OrigBreathState = ((nextShakeCoroutine != null) || (resumeCor != null));// || isBreathingInterval);
        StopBreath();
        GameObject go = (scaleOnPressObj != null) ? scaleOnPressObj : gameObject;

        if (scaleOnPressObj != null)
        {
            scaleOnPressObj.transform.localScale = btnNormalScale * Vector3.one;
            scaleTween = scaleOnPressObj.transform.DOScale(Vector3.one * btnPressScale, scaleTime).SetEase(pressCurve).OnComplete(scaleTweenCallBack);
        }
        else
        {
            transform.localScale = btnNormalScale * Vector3.one;
            scaleTween = transform.DOScale(Vector3.one * btnPressScale, scaleTime).SetEase(pressCurve).OnComplete(scaleTweenCallBack);
        }

        //StartCoroutine(ShakeAnim(false));
    }

    private void scaleTweenCallBack()
    {
        scaleTween = null;
    }
    public void SetBreathAnim(bool breath = false)
    {
        isBreathing = breath;
        breathing = true;
    }

    Coroutine resumeCor = null;

    public void PlayOnce()
    {
        scaleTween = transform.DOScale(Vector3.one * btnPressScale, scaleTime).SetEase(pressCurve).OnComplete(() =>
        { transform.DOScale(Vector3.one, scaleTime).SetEase(pressCurve).OnComplete(scaleTweenCallBack); });
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!IsInteractable())
            return;

        if (!m_CanClick)
            return;

        GameObject go = (scaleOnPressObj != null) ? scaleOnPressObj : gameObject;
        TweenCallback callback = () =>
        {
            scaleTween = go.transform.DOScale(btnNormalScale * Vector3.one, scaleTime).SetEase(pressCurve).OnComplete(scaleTweenCallBack);
        };

        if (scaleTween != null)
            scaleTween.OnComplete(callback);
        else
            callback();
        //AY_Tween.ScaleTo(go, AY_Tween.Hash(
        //        "time", scaleTime,
        //        "x", 1,
        //        "y", 1,
        //        "islocal", true));
        if ((OrigBreathState && CanResumeBreath))
        {
            resumeCor = StartCoroutine(ResumeCor());
            OrigBreathState = false;
        }
    }

    IEnumerator ResumeCor()
    {
        yield return new WaitForSeconds(m_LoopInterval);
        breathing = true; //在下一帧，才会执行到Update中的 if (breathing)语句

        yield return null; //等两帧，使resumeCor和nextShakeCoroutine两个协程中间的一帧不同时为 null。保证 OrigBreathState的正确性。
        yield return null;
        resumeCor = null;
    }


    private bool IsInteractable()
    {
        Button button = gameObject.GetComponent<Button>();
        bool ret = false;
        if (button != null)
        {
            ret = button.enabled;
        }
        else
            ret = true;

        return ret;
    }


    private void LateUpdate()
    {
        if (m_CanRotate)
        {
            if (rotateLeft)
                gameObject.transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            else
                gameObject.transform.Rotate(-Vector3.forward, rotateSpeed * Time.deltaTime);

        }

        if (breathing)
        {
            breathing = false;
            nextShakeCoroutine = StartCoroutine(ShakeAnim(scaleXYOnSameValue));
        }

        if (m_StartBreath)
        {
            m_StartBreath = false;
            //StartBreath(false);
            StartBreath(m_LoopAnim, null, 0); 
        }

    }

    private IEnumerator ShakeAnim(bool scaleXYOnSameValue = true, bool loop = true, UnityAction completeCallBack = null, int tag = 0)
    {
        float time = 0;
        float scaleOnCurve;
        Vector2 origSize = (transform as RectTransform).sizeDelta;
        AnimationCurve curve = GetShakeCurve(tag);
        while (time < m_Duration)
        {
            scaleOnCurve = curve.Evaluate(time / m_Duration);

            if (!m_HasChild)
            {
                if (scaleXYOnSameValue)
                    transform.localScale = new Vector2(scaleOnCurve, scaleOnCurve) * btnNormalScale;
                else
                    transform.localScale = new Vector2(scaleOnCurve, 1 / scaleOnCurve) * btnNormalScale;
            }
            else
            {
                if (scaleXYOnSameValue)
                    (transform as RectTransform).sizeDelta = origSize * scaleOnCurve;
                else
                    (transform as RectTransform).sizeDelta = new Vector2(origSize.x * scaleOnCurve, origSize.y / scaleOnCurve);
            }


            time += Time.deltaTime;
            yield return null;
        }

        if (completeCallBack != null)
            completeCallBack.Invoke();
        yield return new WaitForSeconds(m_LoopInterval);

        if (loop)
        {
            breathing = true;
            //StartCoroutine(SetBreathingIntervalState());
        }
        //yield return null;// 同ResumeCor()中的等两帧
        //yield return null;
        nextShakeCoroutine = null;

    }

    IEnumerator SetBreathingIntervalState()
    {
        //Debug.LogError("SetBreathingIntervalState => framecount = " + Time.frameCount);
        isBreathingInterval = true;
        yield return null;
        yield return null;
        isBreathingInterval = false;
    }

    private AnimationCurve GetShakeCurve(int tag = 0)
    {
        if (tag == 0)
            return shakeCurve;
        else
            return shakeCurve1;
    }
    public void StopBreath(bool reset = true)
    {
        breathing = false;
        m_NeedBreath = false;
        m_OwnAnim = false;
        if (nextShakeCoroutine != null)
        {
            StopCoroutine(nextShakeCoroutine);
            //if (button != null)
            //    button.interactable = true;
            nextShakeCoroutine = null;
        }
        if (delayBreathCoroutine != null)
        {
            StopCoroutine(delayBreathCoroutine);
            delayBreathCoroutine = null;
        }

        if (resumeCor != null)
        {
            StopCoroutine(resumeCor);
            resumeCor = null;
        }
        if (reset)
            transform.localScale = Vector3.one;
    }
    public void StopAnimontinu(bool ContinBreath)
    {
        isBreathing = ContinBreath;
        StopBreath(true);
    }
    public void StopOwnAnim()
    {
        m_OwnAnim = false;
        ownAnimLoop = false;
    }
    public void StartBreath(bool needLoop = false, UnityAction completeCallBack = null, int tag = 0)
    {
        nextShakeCoroutine = StartCoroutine(ShakeAnim(scaleXYOnSameValue, needLoop, completeCallBack, tag));
    }

    public bool InBreath { get { return (delayBreathCoroutine != null || resumeCor != null || nextShakeCoroutine != null); } }
    public void SetAllCoroutineNull()
    {
        delayBreathCoroutine = null;
        resumeCor = null;
        nextShakeCoroutine = null;
    }
    void OnDisable()
    {
        if (setAllCoroutineNullOnDisable)
        {
            SetAllCoroutineNull();
        }
    }
}
