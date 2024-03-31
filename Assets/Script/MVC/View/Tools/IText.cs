using UnityEngine;
using System.Collections;

namespace Chengzi
{
    public delegate void HrefClickEvent(string name, Vector2 pos,bool isDown);

    public interface IText
    {
        string text { get; set; } 

        void Refresh();

        T GetComponent<T>();

        void addListener(HrefClickEvent l);

        void removeListener(HrefClickEvent l);
    }
}