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

using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace PicoGK
{

#if !NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_0_OR_GREATER
    public static class MathF
    {
        public static float Ceiling(float x) { return (float)Math.Ceiling(x); }
        public static float Cos(float x) { return (float)Math.Cos(x); }
        public static float Max(float x, float y) { return (float)Math.Max(x, y); }
        public static float PI => (float)Math.PI;
        public static float Pow(float x, float y) { return (float)Math.Pow(x, y); }
        public static float Sin(float x) { return (float)Math.Sin(x); }
        public static float Sqrt(float x) { return (float)Math.Sqrt(x); }
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

}