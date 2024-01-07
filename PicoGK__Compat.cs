//
// SPDX-License-Identifier: Apache-2.0
//
// PicoGK ("peacock") is a compact software kernel for computational geometry,
// specifically for use in Computational Engineering Models (CEM).
//
// For more information, please visit https://picogk.org
// 
// PicoGK is developed and maintained by LEAP 71 - © 2023 by LEAP 71
// https://leap71.com
//
// Computational Engineering will profoundly change our physical world in the
// years ahead. Thank you for being part of the journey.
//
// We have developed this library to be used widely, for both commercial and
// non-commercial projects alike. Therefore, we have released it under a 
// permissive open-source license.
//
// The foundation of PicoGK is a thin layer on top of the powerful open-source
// OpenVDB project, which in turn uses many other Free and Open Source Software
// libraries. We are grateful to be able to stand on the shoulders of giants.
//
// LEAP 71 licenses this file to you under the Apache License, Version 2.0
// (the "License"); you may not use this file except in compliance with the
// License. You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, THE SOFTWARE IS
// PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED.
//
// See the License for the specific language governing permissions and
// limitations under the License.   
//

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace PicoGK
{

#if !NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_0_OR_GREATER
    public static class MathF
    {
        public static float Abs(float x) { return (float)Math.Abs(x); }
        public static float Acos(float x) { return (float)Math.Acos(x); }
        public static float Atan2(float x, float y) { return (float)Math.Atan2(x, y); }
        public static float Ceiling(float x) { return (float)Math.Ceiling(x); }
        public static float Cos(float x) { return (float)Math.Cos(x); }
        public static float Max(float x, float y) { return (float)Math.Max(x, y); }
        public static float Min(float x, float y) { return (float)Math.Min(x, y); }
        public const float PI = (float)Math.PI;
        public static float Pow(float x, float y) { return (float)Math.Pow(x, y); }
        public static float Sin(float x) { return (float)Math.Sin(x); }
        public static float Sqrt(float x) { return (float)Math.Sqrt(x); }
        public static float Tan(float x) { return (float)Math.Tan(x); }
    }

#endif

#if !NET6_0_OR_GREATER
    public static class SystemRandomExtensions
    {
        public static float NextSingle(this Random rnd)
        {
            return (float)rnd.Next();
        }
    }
#endif

    public static class MathCompat
    {
#if !NET6_0_OR_GREATER
        public static float Clamp(float x, float min, float max)
        {
            return Math.Max(min, Math.Min(max, x));
        }
#else
        public static float Clamp(float x, float min, float max)
        {
            return Math.Clamp(x, min, max);
        }
#endif
    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0_OR_GREATER
    public ref struct MarshalCompat<T> where T : struct
    {
        Span<byte> _byteSpan;

        public MarshalCompat(ref T obj)
        {
            _byteSpan = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan<T>(ref obj, 1));
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(_byteSpan);
        }

        public void Read(BinaryReader reader)
        {
            reader.Read(_byteSpan);
        }

        public void Dispose()
        {
        }
    }
#else
    public class MarshalCompat<T> : IDisposable
    {
        GCHandle _gcHandle;

        public MarshalCompat(ref T obj)
        {
            _gcHandle = GCHandle.Alloc(obj, GCHandleType.Pinned);
        }

        public void Write(BinaryWriter writer)
        {
            IntPtr ptr = _gcHandle.AddrOfPinnedObject();
            byte[] bytes = new byte[Marshal.SizeOf<T>()];
            Marshal.Copy(ptr, bytes, 0, bytes.Length);
            writer.Write(bytes);
        }

        public void Read(BinaryReader reader)
        {
            IntPtr ptr = _gcHandle.AddrOfPinnedObject();
            byte[] bytes = new byte[Marshal.SizeOf<T>()];
            reader.Read(bytes, 0, bytes.Length);
            Marshal.Copy(bytes, 0, ptr, bytes.Length);
        }

        public void Dispose()
        {
            _gcHandle.Free();
        }
    }
#endif

}