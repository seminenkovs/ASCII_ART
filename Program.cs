using ASCIIArt;
using System.IO;

class Program
{
    private const double WIDTH_OFFSET = 1.7;
    private const int MAX_WIDTH = ;

    [STAThread]
    static void Main()
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Image Files|*.bmp;*.png;*.jpg;*.JPEG"
        };

        Console.WriteLine("Press enter to start... \n");

        while(true)
        {
            Console.ReadLine();

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                continue;
            }

            Console.Clear();

            var bitmap = new Bitmap(openFileDialog.FileName);
            bitmap = ResizeBitmap(bitmap);
            bitmap.ToGrayscale();

            var convert = new BitmapToASCIIConverter(bitmap);
            var rows = convert.Convert();

            foreach (var row in rows)
            {
                Console.WriteLine(row);
            }

            var rowsNegative = convert.ConvertAsNegative();
            File.WriteAllLines("image.txt", rowsNegative.Select(r => new string(r)));

            Console.SetCursorPosition(0, 0);
        }
    }

    /// <summary>
    /// Resize image to fit in the console
    /// </summary>
    /// <param name="bitmap"></param>
    /// <returns>Bitmap</returns>
    private static Bitmap ResizeBitmap(Bitmap bitmap)
    {
        var newHeight = bitmap.Height / WIDTH_OFFSET * MAX_WIDTH / bitmap.Width;
        if (bitmap.Width > MAX_WIDTH || bitmap.Height > newHeight)
            bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, (int)newHeight));
        return bitmap;
    }
}







