using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace QA
{
    [Serializable]
    public class NinePatch
    {
        public Image Image { get; set; }

        public int PaddingTop { get; set; }

        public int PaddingLeft { get; set; }

        public int PaddingRight { get; set; }

        public int PaddingBottom { get; set; }

        private Rectangle patch_nw_src = Rectangle.Empty;
        private Rectangle patch_n_src = Rectangle.Empty;
        private Rectangle patch_ne_src = Rectangle.Empty;
        private Rectangle patch_w_src = Rectangle.Empty;
        private Rectangle patch_mid_src = Rectangle.Empty;
        private Rectangle patch_e_src = Rectangle.Empty;
        private Rectangle patch_sw_src = Rectangle.Empty;
        private Rectangle patch_s_src = Rectangle.Empty;
        private Rectangle patch_se_src = Rectangle.Empty;

        private Rectangle patch_nw_dest = Rectangle.Empty;
        private Rectangle patch_n_dest = Rectangle.Empty;
        private Rectangle patch_ne_dest = Rectangle.Empty;
        private Rectangle patch_w_dest = Rectangle.Empty;
        private Rectangle patch_mid_dest = Rectangle.Empty;
        private Rectangle patch_e_dest = Rectangle.Empty;
        private Rectangle patch_sw_dest = Rectangle.Empty;
        private Rectangle patch_s_dest = Rectangle.Empty;
        private Rectangle patch_se_dest = Rectangle.Empty;

        public NinePatch()
        {
        }

        public static NinePatch FromFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);

            return FromBytes(bytes);
        }

        public static NinePatch FromBytes(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return FromStream(stream);
            }
        }

        public static NinePatch FromStream(Stream stream)
        {
            var result = new NinePatch();

            BinaryReader reader = new BinaryReader(stream);

            result.PaddingTop = reader.ReadInt32();
            result.PaddingLeft = reader.ReadInt32();
            result.PaddingRight = reader.ReadInt32();
            result.PaddingBottom = reader.ReadInt32();

            var formatter = new BinaryFormatter();
            result.Image = formatter.Deserialize(stream) as Image;

            return result;
        }

        public void Save(string path)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Save(stream);
                File.WriteAllBytes(path, stream.ToArray());
            }
        }

        public void Save(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(PaddingTop);
            writer.Write(PaddingLeft);
            writer.Write(PaddingRight);
            writer.Write(PaddingBottom);

            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, this.Image);
        }

        private void ResetPatchSrcRectangles()
        {
            int src_mid_width = this.Image.Width - PaddingLeft - PaddingRight;
            int src_mid_height = this.Image.Height - PaddingTop - PaddingBottom;

            int src_x1 = PaddingLeft;
            int src_y1 = PaddingTop;
            int src_x2 = src_x1 + src_mid_width;
            int src_y2 = src_y1 + src_mid_height;

            src_mid_width--;
            src_mid_height--;

            patch_nw_src = new Rectangle(0, 0, PaddingLeft, PaddingTop);
            patch_n_src = new Rectangle(src_x1, 0, src_mid_width, PaddingTop);
            patch_ne_src = new Rectangle(src_x2, 0, PaddingRight, PaddingTop);
            patch_w_src = new Rectangle(0, src_y1, PaddingLeft, src_mid_height);
            patch_mid_src = new Rectangle(src_x1, src_y1, src_mid_width, src_mid_height);
            patch_e_src = new Rectangle(src_x2, src_y1, PaddingRight, src_mid_height);
            patch_sw_src = new Rectangle(0, src_y2, PaddingLeft, PaddingBottom);
            patch_s_src = new Rectangle(src_x1, src_y2, src_mid_width, PaddingBottom);
            patch_se_src = new Rectangle(src_x2, src_y2, PaddingRight, PaddingBottom);
        }

        private void ResetPatchDestRectangles(int x, int y, int width, int height)
        {
            int dest_mid_width = width - PaddingLeft - PaddingRight;
            int dest_mid_height = height - PaddingTop - PaddingBottom;

            int dest_x1 = PaddingLeft;
            int dest_y1 = PaddingTop;
            int dest_x2 = dest_x1 + dest_mid_width;
            int dest_y2 = dest_y1 + dest_mid_height;

            patch_nw_dest = new Rectangle(x + 0, y + 0, PaddingLeft, PaddingTop);
            patch_n_dest = new Rectangle(x + dest_x1, y + 0, dest_mid_width, PaddingTop);
            patch_ne_dest = new Rectangle(x + dest_x2, y + 0, PaddingRight, PaddingTop);
            patch_w_dest = new Rectangle(x + 0, y + dest_y1, PaddingLeft, dest_mid_height);
            patch_mid_dest = new Rectangle(x + dest_x1, y + dest_y1, dest_mid_width, dest_mid_height);
            patch_e_dest = new Rectangle(x + dest_x2, y + dest_y1, PaddingRight, dest_mid_height);
            patch_sw_dest = new Rectangle(x + 0, y + dest_y2, PaddingLeft, PaddingBottom);
            patch_s_dest = new Rectangle(x + dest_x1, y + dest_y2, dest_mid_width, PaddingBottom);
            patch_se_dest = new Rectangle(x + dest_x2, y + dest_y2, PaddingRight, PaddingBottom);
        }

        // Cache parameters
        private int cache_paddingleft = 0;

        private int cache_paddingright = 0;
        private int cache_paddingtop = 0;
        private int cache_paddingbottom = 0;
        private int src_width = 0;
        private int src_height = 0;
        private int dest_x = 0;
        private int dest_y = 0;
        private int dest_width = 0;
        private int dest_height = 0;

        public void Draw(Graphics graphics, int x, int y, int width, int height)
        {
            if (this.Image == null) return;

            bool reset_src_rects = false;
            bool reset_dest_rects = false;

            if (src_width != this.Image.Width || src_height != this.Image.Height)
            {
                src_width = this.Image.Width;
                src_height = this.Image.Height;
                reset_src_rects = true;
            }

            if (dest_x != x || dest_y != y || dest_width != width || dest_height != height)
            {
                dest_x = x;
                dest_y = y;
                dest_width = width;
                dest_height = height;
                reset_dest_rects = true;
            }

            if (cache_paddingleft != PaddingLeft || cache_paddingright != PaddingRight ||
                cache_paddingtop != PaddingTop || cache_paddingbottom != PaddingBottom)
            {
                cache_paddingleft = PaddingLeft;
                cache_paddingright = PaddingRight;
                cache_paddingtop = PaddingTop;
                cache_paddingbottom = PaddingBottom;
                reset_src_rects = true;
                reset_dest_rects = true;
            }

            if (reset_src_rects)
            {
                ResetPatchSrcRectangles();
            }

            if (reset_dest_rects)
            {
                ResetPatchDestRectangles(x, y, width, height);
            }

            var savedInterpolationMode = graphics.InterpolationMode;
            var savedPixelOffsetMode = graphics.PixelOffsetMode;

            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            //graphics.PixelOffsetMode = PixelOffsetMode.Half;

            //this order looks faster during resizing
            DrawImage(graphics, patch_s_dest, patch_s_src);
            DrawImage(graphics, patch_e_dest, patch_e_src);
            DrawImage(graphics, patch_w_dest, patch_w_src);
            DrawImage(graphics, patch_n_dest, patch_n_src);
            DrawImage(graphics, patch_se_dest, patch_se_src);
            DrawImage(graphics, patch_sw_dest, patch_sw_src);
            DrawImage(graphics, patch_ne_dest, patch_ne_src);
            DrawImage(graphics, patch_nw_dest, patch_nw_src);
            DrawImage(graphics, patch_mid_dest, patch_mid_src);

            graphics.InterpolationMode = savedInterpolationMode;
            graphics.PixelOffsetMode = savedPixelOffsetMode;
        }

        private void DrawImage(Graphics graphics, Rectangle dest, Rectangle src)
        {
            if (dest.Width == 0) return;
            if (dest.Height == 0) return;
            if (src.Width == 0) return;
            if (src.Height == 0) return;

            graphics.DrawImage(this.Image, dest, src, GraphicsUnit.Pixel);
        }

        public Bitmap CreateBitmap(int width, int height)
        {
            if (width == 0) return null;
            if (height == 0) return null;

            var result = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(result))
            {
                this.Draw(graphics, 0, 0, width, height);
            }
            return result;
        }
    }
}