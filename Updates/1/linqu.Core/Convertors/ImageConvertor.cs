
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;



namespace linqu.Core.Convertors
{
    public class ImageConvertor
    {
        #region Resize Image
        public static void Image_Resize(string input_Image_Path, string output_Image_Path, int new_Width)
        {

            const long quality = 50L;

            Bitmap source_Bitmap = new Bitmap(input_Image_Path);



            double dblWidth_origial = source_Bitmap.Width;

            double dblHeigth_origial = source_Bitmap.Height;

            double relation_heigth_width = dblHeigth_origial / dblWidth_origial;

            int new_Height = (int)(new_Width * relation_heigth_width);



            // Create Empty Drawarea 

            var new_DrawArea = new Bitmap(new_Width, new_Height);

            // Create Empty Drawarea 



            using (var graphic_of_DrawArea = Graphics.FromImage(new_DrawArea))

            {

                // Setup 

                graphic_of_DrawArea.CompositingQuality = CompositingQuality.HighSpeed;

                graphic_of_DrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphic_of_DrawArea.CompositingMode = CompositingMode.SourceCopy;

                // Setup 



                // Draw Into Placeholder 

                // * Imports the Image Into the Drawarea

                graphic_of_DrawArea.DrawImage(source_Bitmap, 0, 0, new_Width, new_Height);

                // Draw Into Placeholder 



                // Output as .Jpg 

                using (var output = System.IO.File.Open(output_Image_Path, FileMode.Create))

                {

                    // Setup Jpg 

                    var qualityParamId = System.Drawing.Imaging.Encoder.Quality;

                    var encoderParameters = new EncoderParameters(1);

                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);

                    // Setup Jpg 



                    // Save Bitmap as Jpg 

                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    new_DrawArea.Save(output, codec, encoderParameters);

                    // Resized_Bitmap.Dispose ();

                    output.Close();

                    // Save Bitmap as Jpg 

                }

                // Output as .Jpg  

                graphic_of_DrawArea.Dispose();

            }

            source_Bitmap.Dispose();

            //---------------< Image_Resize() >---------------

        }

        #endregion
    }


}
