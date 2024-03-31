using UnityEngine;
using System.Collections;
using SevenZip;
using System;
using System.IO;

public class AudioCompress
{

    public static byte[] LzmaCompress(byte[] inpbuf) 
    {
        CoderPropID[] propIDs =
       {
        CoderPropID.DictionarySize,
        CoderPropID.PosStateBits,
        CoderPropID.LitContextBits,
        CoderPropID.LitPosBits,
        CoderPropID.Algorithm,
        CoderPropID.NumFastBytes,
        CoderPropID.MatchFinder,
        CoderPropID.EndMarker
       };
        object[] properties =
       {
        (Int32)(23),
        (Int32)(2),
        (Int32)(3),
        (Int32)(2),
        (Int32)(1),
        (Int32)(128),
        (string)("bt4"),
        (bool)(true)
       };

        SevenZip.Compression.LZMA.Encoder enc = new SevenZip.Compression.LZMA.Encoder();
        enc.SetCoderProperties(propIDs, properties);

        MemoryStream msInp = new MemoryStream(inpbuf);
        MemoryStream msOut = new MemoryStream();
        enc.WriteCoderProperties(msOut);
        enc.Code(msInp, msOut, -1, -1, null);
        return msOut.ToArray();
    }

    public static byte[] LzmaDecompress(byte[] inpbuf)
    {
        CoderPropID[] propIDs =
   {
    CoderPropID.DictionarySize,
    CoderPropID.PosStateBits,
    CoderPropID.LitContextBits,
    CoderPropID.LitPosBits,
    CoderPropID.Algorithm,
    CoderPropID.NumFastBytes,
    CoderPropID.MatchFinder,
    CoderPropID.EndMarker
   };
        object[] properties =
   {
    (Int32)(23),
    (Int32)(2),
    (Int32)(3),
    (Int32)(2),
    (Int32)(1),
    (Int32)(128),
    (string)("bt4"),
    (Int32)(0)
   };
        SevenZip.Compression.LZMA.Decoder dec = new SevenZip.Compression.LZMA.Decoder();
        byte[] prop = new byte[5];
        Array.Copy(inpbuf, prop, 5);
        dec.SetDecoderProperties(prop);
        MemoryStream msInp = new MemoryStream(inpbuf);
        msInp.Seek(5, SeekOrigin.Current);
        MemoryStream msOut = new MemoryStream();
        dec.Code(msInp, msOut, -1, -1, null);
        return msOut.ToArray();
    }
}
