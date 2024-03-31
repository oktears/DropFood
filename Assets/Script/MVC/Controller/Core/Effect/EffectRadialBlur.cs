using UnityEngine;

using Chengzi;


/// <summary>
/// 特效：动态模糊
/// </summary>
public class EffectRadialBlur : EffectBase
{
    private RadialBlurFilter radialBlur;
    float strength = 0;
    bool is_nos_state = false;
    //float timer = 0;
    private int playCount = 0;

    private bool _isStopEffect = false;

    public EffectRadialBlur(Transform trans)
        : base(trans)
    {
        radialBlur = Camera.main.GetComponentInChildren<RadialBlurFilter>();
        // radialBlur.setEnable(QualityManager.Instance._isOpenFilter);
    }

    public override bool playParticle(bool isPlay, EffectPlayMode playMode)
    {

        if (_isStopEffect)
            return false;

        // if (!QualityManager.Instance._isOpenFilter)
        // {
        //     return false;
        // }

        if (radialBlur == null)
        {
            radialBlur = Camera.main.GetComponentInChildren<RadialBlurFilter>();
        }
        playCount += isPlay ? 1 : -1;
        if (!isPlay && playCount != 0)
        {
            return false;
        }
        if (isPlay)
        {
            is_nos_state = true;
            radialBlur.setEnable(is_nos_state);
        } 
        else
        {
            is_nos_state = false;
        }
        //timer = 1;

        return true;
    }

    public override bool playParticleImmediately(bool isPlay)
    {

        if (_isStopEffect)
            return false;

        // if (!QualityManager.Instance._isOpenFilter)
        // {
        //     return false;
        // }

        isPlay = false;
        if (radialBlur == null)
        {
            radialBlur = Camera.main.GetComponentInChildren<RadialBlurFilter>();
        }
        if (isPlay)
        {
            is_nos_state = true;
            radialBlur.setEnable(is_nos_state);
        }
        else
        {
            is_nos_state = false;
        }
        //timer = 1;

        return true;
    }

    public override bool playParticleNormal(bool isPlay)
    {
        if (_isStopEffect)
            return false;

        // if (!QualityManager.Instance._isOpenFilter)
        // {
        //     return false;
        // }

        isPlay = false;
        if (radialBlur == null)
        {
            radialBlur = Camera.main.GetComponentInChildren<RadialBlurFilter>();
        }
        if (isPlay)
        {
            is_nos_state = true;
            radialBlur.setEnable(is_nos_state);
        }
        else
        {
            is_nos_state = false;
        }
        //timer = 1;

        return true;
    }

    public override void loop(float dt)
    {
        if (_isStopEffect)
            return;

        // if (!QualityManager.Instance._isOpenFilter)
        // {
        //     return;
        // }

        //if (timer > 0)
        //{
        if (radialBlur == null)
        {
            radialBlur = Camera.main.GetComponentInChildren<RadialBlurFilter>();
        }
        //timer -= dt;
        if (is_nos_state)
        {
            strength = Mathf.Lerp(strength, 1.2f, dt);
            radialBlur.setStrength(strength);
        }
        else
        {
            strength = Mathf.Lerp(strength, 0, dt);
            radialBlur.setStrength(strength);
        }

        if (strength == 0)
        {
            radialBlur.setEnable(false);
        }

        //if (timer <= 0)
        //{
        //    radialBlur.setEnable(is_nos_state);
        //}
        //}
    }

    public override void stopFilter()
    {
        base.stopFilter();
        is_nos_state = false;
    }

}
