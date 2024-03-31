using UnityEngine;

[ExecuteInEditMode]
public class RadialBlurFilter : MonoBehaviour
{

    #region Variables
    public Shader curShader;
    public float _sampleDist = 1.0f;
    public float _sampleStrength = 1.0f;
    private Material curMaterial;
    #endregion

    #region Properties
    public Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(curShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }
    #endregion

    // Use this for initialization  
    void Start()
    {
        enabled = false;
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (curShader != null)
        {
            material.SetFloat("_fSampleDist", _sampleDist);
            material.SetFloat("_fSampleStrength", _sampleStrength);

            Graphics.Blit(sourceTexture, destTexture, material);
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    }

    void Update()
    {

    }

    void OnDisable()
    {
        if (curMaterial != null)
        {
            DestroyImmediate(curMaterial);
        }
    }

    public void setStrength(float strength)
    {
        _sampleStrength = strength;
    }

    public void setEnable(bool isEnable)
    {
        this.enabled = isEnable;
    }
}