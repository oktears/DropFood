
using System.IO;
using System.Text;

namespace Chengzi
{
    public class XDWriter : IWriter
    {

        public static XDWriter Create(string path)
        {
            return Create(path, 100, "XDWriter");
        }

        public static XDWriter Create(string path,int ver,string des)
        {
            XDWriter w = new XDWriter();
            
            if(des == null)
            {
                des = "XDWriter";
            }

            if(w.init(PathHelper.OpenWrite(path),ver,des) )
            {
                return w;
            }
            w.close();
            w = null;
            return null;

        }



        private Stream m_stream = null;

        private XDWriter()
        {

        }
        
        protected bool init(Stream s,int ver,string des)
        {
            bool ret = false;

            do
            {
                if (s == null) break;
                m_stream = s;
                if (!s.CanWrite) break;
                // version
                write(ver);
                // describe
                write(des);
                
                ret = true;
            } while (false);

            return ret;
        }


        public unsafe void write(byte v)
        {
            writeBuffer((byte*)&v, sizeof(byte));
        }


        public unsafe void write(short v)
        {
            writeBuffer((byte*)&v, sizeof(short));
        }

        public unsafe void write(int v)
        {
            writeBuffer((byte*)&v, sizeof(int));
        }

        public unsafe void write(uint v)
        {
            writeBuffer((byte*)&v, sizeof(uint));
        }

        public unsafe void write(long v)
        {
            writeBuffer((byte*)&v, sizeof(long));
        }

        public unsafe void write(ulong v)
        {
            writeBuffer((byte*)&v, sizeof(ulong));
        }

        public unsafe void write(float v)
        {
            writeBuffer((byte*)&v, sizeof(float));
        }

        public void write(string v)
        {
            if (m_stream != null)
            {
                byte[] b = Encoding.UTF8.GetBytes(v);
                write((short)b.Length);
                m_stream.Write(b, 0, b.Length);
            }

        }

        public void write(UnityEngine.Vector3 v)
        {
            write(v.x);
            write(v.y);
            write(v.z);
        }


        public void write(UnityEngine.Vector2 v)
        {
            write(v.x);
            write(v.y);
        }


        public void close()
        {
            if(m_stream != null)
            {
                m_stream.Flush();
                m_stream.Close();
                m_stream = null;
            }
        }

        protected unsafe bool writeBuffer(byte* data,int size)
        {
            if (m_stream == null) return false;
            for (int i = 0; i < size; i++)
            {
                m_stream.WriteByte(data[size - i - 1]);
            }
            return true;
        }
    }
}
