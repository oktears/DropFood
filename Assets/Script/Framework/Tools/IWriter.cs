using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chengzi
{
    public interface IWriter
    {
        void write(byte v);

        void write(short v); 

        void write(int v);

        void write(uint v);

        void write(long v);

        void write(ulong v);

        void write(float v);

        void write(string v);
        
        void write(Vector3 v);
        
        void write(Vector2 v);

        void close();
    }
}
