
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{

    /// <summary>
    /// Shader管理器
    /// </summary>
    public class ShaderManager : Singleton<ShaderManager>
    {
        public delegate void TextureCallback(Texture texture);

        private Camera _photoCamera;
        private TextureCallback _texCallback;

        //是否开启泛光滤镜
        public bool _isOpenBloomFilter { get; set; }
        //是否开启黑角滤镜
        public bool _isOpenLomoFilter { get; set; }
        //是否开启移轴摄影滤镜
        public bool _isOpenTiltShiftFilter { get; set; }

        public enum GroundGlassType
        {
            GG_3D,
            GG_UI,
            //结算特殊处理
            GG_RESULT
        }

        /// <summary>
        /// 开启毛玻璃效果
        /// </summary>
        /// <param name="callback"></param>
        public void openGroundGlass(TextureCallback callback, GroundGlassType type)
        {
            if (type == GroundGlassType.GG_3D)
            {
                if (Camera.allCameras.Length > 0)
                {
                    for (int i = 0; i < Camera.allCameras.Length; i++)
                    {
                        Camera cam = Camera.allCameras[i];
                        IGroundGlassFilter ggf = cam.GetComponent<IGroundGlassFilter>();
                        if (cam.depth == -1)
                        {
                            //场景相机
                            IGroundGlassFilter filter = cam.GetComponent<IGroundGlassFilter>();
                            if (filter != null)
                            {
                                // if (!QualityManager.Instance._isOpenGroundGlass)
                                // {
                                //     _photoCamera = cam;
                                //     _texCallback = callback;
                                //     XTool.DelayCallEndOfFrame(captureByCamera);
                                // }
                                // else
                                // {
                                    filter.setEnable(true);
                                    filter.setCallback(callback);
                                // }
                            }
                        }
                    }

                }
            }
            else if (type == GroundGlassType.GG_UI)
            {
                float maxDepth = -1;
                IGroundGlassFilter filter = null;

                Camera camera = null;
                for (int i = 0; i < Camera.allCameras.Length; i++)
                {
                    Camera cam = Camera.allCameras[i];
                    IGroundGlassFilter ggf = cam.GetComponent<IGroundGlassFilter>();
                    if (ggf != null)
                    {
                        if (cam.depth >= maxDepth)
                        {
                            maxDepth = cam.depth;
                            filter = ggf;
                            camera = cam;
                        }
                    }
                }

                if (filter != null)
                {
                    // if (!QualityManager.Instance._isOpenGroundGlass)
                    // {
                    //     callback(null);
                    // }
                    // else
                    // {
                        filter.setEnable(true);
                        filter.setCallback(callback);
                    // }
                }
            }
            else if (type == GroundGlassType.GG_RESULT)
            {
                float maxDepth = -1;
                IGroundGlassFilter filter = null;
                Camera camera = null;
                Camera _3dCamera = null;

                for (int i = 0; i < Camera.allCameras.Length; i++)
                {
                    Camera cam = Camera.allCameras[i];
                    IGroundGlassFilter ggf = cam.GetComponent<IGroundGlassFilter>();
                    if (ggf != null)
                    {
                        if (cam.depth >= maxDepth)
                        {
                            maxDepth = cam.depth;
                            filter = ggf;
                            camera = cam;
                        }
                    }

                    if (cam.depth == -1)
                    {
                        _3dCamera = cam;
                    }
                }

                if (filter != null)
                {
                    // if (!QualityManager.Instance._isOpenGroundGlass)
                    // {
                    //     _photoCamera = _3dCamera;
                    //     _texCallback = callback;
                    //     XTool.DelayCallEndOfFrame(captureByCamera);
                    // }
                    // else
                    // {
                        filter.setEnable(true);
                        filter.setCallback(callback);
                    // }
                }
            }
        }

        private void captureByCamera()
        {
            if (_photoCamera == null)
                return;

            RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);

            RenderTexture currentRT = RenderTexture.active;

            _photoCamera.targetTexture = rt;
            _photoCamera.Render();

            RenderTexture.active = _photoCamera.targetTexture;
            Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(new Rect(0, 0, _photoCamera.targetTexture.width,
                _photoCamera.targetTexture.height), 0, 0);
            screenShot.Apply();

            _texCallback(screenShot);

            RenderTexture.active = currentRT;

            _photoCamera.targetTexture = null;
            RenderTexture.active = null;
            RenderTexture.Destroy(rt);
            _photoCamera = null;
            _texCallback = null;
            rt = null;
        }

        /// <summary>
        /// 开启压灰效果
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="isCascade">是否级联修改子节点</param>
        public void openGrayEffect(Transform transform, bool isCascade)
        {
            openGrayEffect(transform, isCascade, true);
        }

        /// <summary>
        /// 开启压灰效果
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="isCascade">是否级联修改子节点</param>
        public void openGrayEffect(Transform transform, bool isCascade, bool incldeText)
        {
            Material mt = Object.Instantiate(PrefabPool.Instance.getPrefab<Material>("Material/UI/UI_Gray"));
            if (isCascade)
            {
                Transform[] allChildren = transform.GetComponentsInChildren<Transform>();

                if (allChildren != null && allChildren.Length > 0)
                {
                    for (int i = 0; i < allChildren.Length; i++)
                    {
                        GameObject obj = allChildren[i].gameObject;
                        if (obj != null)
                        {
                            setUIMaterial(obj.transform, mt, incldeText);
                        }
                    }
                }
            }
            else
            {
                setUIMaterial(transform, mt, incldeText);
            }
        }

        /// <summary>
        /// 设置材质
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="mt"></param>
        private void setUIMaterial(Transform transform, Material mt)
        {
            setUIMaterial(transform, mt, true);
        }

        /// <summary>
        /// 设置材质
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="mt"></param>
        private void setUIMaterial(Transform transform, Material mt, bool includeText)
        {
            if (transform.gameObject != null)
            {
                Image img = transform.gameObject.GetComponent<Image>();
                RawImage rawImg = transform.gameObject.GetComponent<RawImage>();
                Text text = transform.gameObject.GetComponent<Text>();
                if (img != null)
                    img.material = mt;
                if (rawImg != null)
                    rawImg.material = mt;
                if (includeText)
                {
                    if (text != null)
                        text.material = mt;
                }
            }
        }

        /// <summary>
        /// 设置UI默认的材质 
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="isCascade"></param>
        public void setUIDefaultMaterial(Transform transform, bool isCascade)
        {
            if (isCascade)
            {
                Transform[] allChildren = transform.GetComponentsInChildren<Transform>();
                if (allChildren != null && allChildren.Length > 0)
                {
                    for (int i = 0; i < allChildren.Length; i++)
                    {
                        GameObject obj = allChildren[i].gameObject;
                        if (obj != null)
                        {
                            setUIMaterial(obj.transform, null);
                        }
                    }
                }
            }
            else
            {
                setUIMaterial(transform, null);
            }
        }

        ///// <summary>
        ///// 字体颜色渐变
        ///// </summary>
        ///// <param name="img"></param>
        ///// <param name="topColor"></param>
        ///// <param name="bottomColor"></param>
        //public void FontColorGradient(Text text, Color topColor, Color bottomColor)
        //{
        //    FontColorGradient gradient = text.gameObject.GetComponent<FontColorGradient>();
        //    if (gradient == null)
        //    {
        //        text.gameObject.AddComponent<FontColorGradient>();
        //        gradient = text.gameObject.GetComponent<FontColorGradient>();
        //    }
        //    gradient.topColor = topColor;
        //    gradient.bottomColor = bottomColor;
        //}

        ///// <summary>
        ///// 图片颜色渐变
        ///// </summary>
        ///// <param name="img"></param>
        ///// <param name="topColor"></param>
        ///// <param name="BottomColor"></param>
        //public void ImageColorGradient(Image img, Color topColor, Color bottomColor)
        //{
        //    ImageColorGradient gradient = img.gameObject.GetComponent<ImageColorGradient>();
        //    if (gradient == null)
        //    {
        //        img.gameObject.AddComponent<ImageColorGradient>();
        //        gradient = img.gameObject.GetComponent<ImageColorGradient>();
        //    }
        //    gradient.topColor = topColor;
        //    gradient.bottomColor = bottomColor;
        //}
    }
}
