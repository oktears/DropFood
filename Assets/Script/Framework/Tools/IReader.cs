using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chengzi
{
    public interface IReader
    {
        int readInt();

        uint readUint();

        byte readByte();

        long readLong();

        ulong readUlong();

        short readShort();

        float readFloat();

        Vector3 readVec3();

        Vector2 readVec2();

        string readString();

        void close();
    }
}
