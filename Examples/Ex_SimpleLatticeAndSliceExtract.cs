//
// SPDX-License-Identifier: CC0-1.0
//
// This example code file is released to the public under Creative Commons CC0.
// See https://creativecommons.org/publicdomain/zero/1.0/legalcode
//
// To the extent possible under law, LEAP 71 has waived all copyright and
// related or neighboring rights to this PicoGK example code file.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS
// OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using PicoGK;
using System.Numerics;
using System;
using System.IO;

namespace PicoGKExamples
{
    ///////////////////////////////////////////////////////////////////////////
    // Below is a static class that implements a single static function
    // that can be called from Library::Go()

    class SimpleLatticeAndSliceExtract
    {
        public static void Task()
        {
            try
            {
                // Create a new empty lattice
                Lattice lat = new Lattice();

                // Add a beam from (0,0,0) to (50,0,0) mm;
                // starting radius is 5mm end radius is 10mm
                // defaults to a round cap
                lat.AddBeam(Vector3.Zero, 5f, new Vector3(50f, 0f, 0f), 10f);

                // Add another pointy beam a little higher
                lat.AddBeam(new Vector3(50f, 0f, 16f), 1f, new Vector3(0f, 0f, 16f), 10f);

                // Add a sphere at the origin with 2mm diameter
                lat.AddSphere(new Vector3(55f, 0f, 16f), 2f);

                // Add a lattice cylinder without round cap
                lat.AddBeam(new Vector3(60f, 0f, 16f), 2f, new Vector3(80f, 0f, 16f), 2f, false);

                // Lattice now contains three beams and a sphere

                // Create a voxel field from the lattice
                Voxels vox = new Voxels(lat);

                // Add the voxel field to the viewer
                Library.oViewer().Add(vox);

                // Get voxel dimensions info (in voxel coordinates)
                vox.GetVoxelDimensions( out int nXSize,
                                        out int nYSize,
                                        out int nZSize);

                // Create a new Grayscale image to receive the
                // voxel slice with the dimensions of the voxels
                ImageGrayScale img = new ImageGrayScale(nXSize, nYSize);

                // Read the voxel slice from the middle of the voxel field
                // into our grayscale image
                vox.GetVoxelSlice(nZSize / 2, ref img);

                // Save it as an TGA file to the log folder
                TgaIo.SaveTga(Path.Combine(Library.strLogFolder, "Slice.tga"), img);
            }

            catch (Exception e)
            {
                Library.Log($"Failed to run example: \n{e.Message}"); ;
            }
        }
    }
}

