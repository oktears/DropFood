using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chengzi
{
    public class UIEffectSort : MonoBehaviour
    {
        public int sortingOrder = 100;
        private Renderer[] m_EffectRend; void Awake()
        {
            //获取脚本下所有Renderer    
            m_EffectRend = GetComponentsInChildren<Renderer>();
            //遍历Renderer        
            for (int i = 0; i < m_EffectRend.Length; i++)
            {
                m_EffectRend[i].sortingOrder = sortingOrder;
                //设置层级           
            }
        }
    }


}
