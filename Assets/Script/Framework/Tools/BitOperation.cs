using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{
    public static class BitOperation
    {
        //////////////////////////////////////////////////////////////////////////////////////
        // int
        public static uint int32_or(uint a,uint b)
        {
            return a | b;
        }

        public static uint int32_not(uint a)
        {
            return ~a;
        }

        public static uint int32_and(uint a,uint b)
        {
            return a & b;
        }

        public static uint int32_xor(uint a,uint b)
        {
            return a ^ b;
        }

        public static uint int32_lshift(uint a,int bit)
        {
            return a << bit;
        }

        public static uint int32_rshift(uint a,int bit)
        {
            return a >> bit;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////
        // byte
        public static int byte_or(byte a,byte b)
        {
            return a | b;
        }

        public static int byte_not(byte a)
        {
            return ~a;
        }

        public static int byte_and(byte a,byte b)
        {
            return a & b;
        }

        public static int byte_xor(byte a,byte b)
        {
            return a ^ b;
        }

        public static int byte_lshift(byte a,int bit)
        {
            return a << bit;
        }

        public static int byte_rshift(byte a,int bit)
        {
            return a >> bit;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////
        // ulong
        public static ulong ulong_or(ulong a,ulong b)
        {
            return a | b;
        }

        public static ulong ulong_not(ulong a)
        {
            return ~a;
        }

        public static ulong ulong_and(ulong a,ulong b)
        {
            return a & b;
        }

        public static ulong ulong_xor(ulong a,ulong b)
        {
            return a ^ b;
        }

        public static ulong ulong_lshift(ulong a,int bit)
        {
            return a << bit;
        }

        public static ulong ulong_rshift(ulong a,int bit)
        {
            return a >> bit;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////
        // short
        public static int ushort_or(ushort a,ushort b)
        {
            return a | b;
        }

        public static int ushort_not(ushort a)
        {
            return ~a;
        }

        public static int ushort_and(ushort a,ushort b)
        {
            return a & b;
        }

        public static int ushort_xor(ushort a,ushort b)
        {
            return a ^ b;
        }

        public static int ushort_lshift(ushort a,int bit)
        {
            return a << bit;
        }

        public static int ushort_rshift(ushort a,int bit)
        {
            return a >> bit;
        }

    }
}
