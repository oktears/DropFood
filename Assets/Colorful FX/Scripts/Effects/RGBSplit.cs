// Colorful FX - Unity Asset
// Copyright (c) 2015 - Thomas Hourdel
// http://www.thomashourdel.com

namespace Colorful
{
    using UnityEngine;

    [HelpURL("http://www.thomashourdel.com/colorful/doc/camera-effects/rgb-split.html")]
    [ExecuteInEditMode]
    [AddComponentMenu("Colorful FX/Camera Effects/RGB Split")]
    public class RGBSplit : BaseEffect
    {
        [Tooltip("RGB shifting amount.")]
        public float Amount = 0f;

        [Tooltip("Shift direction in radians.")]
        public float Angle = 0f;

        protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (Amount == 0f)
            {
                Graphics.Blit(source, destination);
                return;
            }

            Material.SetVector("_Params", new Vector3(
                    Amount * 0.001f,
                    Mathf.Sin(Angle),
                    Mathf.Cos(Angle)
                ));

            Graphics.Blit(source, destination, Material);
        }

        public void setEnable(bool isEnable)
        {
            enabled = isEnable;
        }

        public void setAmount(float amount)
        {
            Amount = amount;
        }

        public void setAngle(float angle)
        {
            Angle = angle;
        }

        public bool isEnable()
        {
            return enabled;
        }

        public float getAmount()
        {
            return Amount;
        }
    }
}
