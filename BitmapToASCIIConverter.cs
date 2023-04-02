using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIArt
{
    internal class BitmapToASCIIConverter
    {
        private readonly char[] _asciiTable = { '.', ',', ':', '+', '*', '?', '%', '$', '#', '@' };
        private readonly char[] _asciiTableNegative = { '@', '#', '$', '%', '?', '*', '+', ':', ',', '.' };
        private readonly Bitmap _bitmap;
        public BitmapToASCIIConverter(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }

        public char[][] Convert()
        {
            return Convert(_asciiTable);
        }

        public char[][] ConvertAsNegative()
        {
            return Convert(_asciiTableNegative);
        }

        /// <summary>
        /// Convert black and white image of pixels int to image of ascii symbols
        /// </summary>
        private char[][] Convert(char[] asciiTable)
        {
            var result = new char[_bitmap.Height][];

            for (int y = 0; y < _bitmap.Height; y++)
            {
                result[y] = new char[_bitmap.Width];
                for (int x = 0; x < _bitmap.Width; x++)
                {
                    int mapIndex = (int)Map(_bitmap.GetPixel(x, y).R, 0, 255, 0, asciiTable.Length - 1);
                    result[y][x] = asciiTable[mapIndex];
                }
            }

            return result;
        }

        /// <summary>
        /// Map Range of 256 elements to Range of 10 elements
        /// </summary>
        /// <param name="valueTop"></param>
        /// <param name="start1"></param>
        /// <param name="stop1"></param>
        /// <param name="start2"></param>
        /// <param name="stop2"></param>
        /// <returns></returns>
        private float Map(float valueTop, float start1, float stop1, float start2, float stop2)
        {
            return ((valueTop - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }
    }
}
