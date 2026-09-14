using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormGiris
{
    public class DensityGaugeControl : Control
    {
        private double yogunluk;

        public double Minimum { get; set; } = 0.8;
        public double Maximum { get; set; } = 1.4;

        public double Yogunluk
        {
            get => yogunluk;

            set
            {
                yogunluk = Math.Max(
                    Minimum,
                    Math.Min(Maximum, value));

                Invalidate();
            }
        }

        public DensityGaugeControl()
        {
            DoubleBuffered = true;

            BackColor = Color.FromArgb(
                10,
                25,
                45);

            Size = new Size(
                220,
                320);
        }

        protected override void OnPaint(
            PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            g.Clear(BackColor);

            BaslikCiz(g);
            YogunlukGostergeCiz(g);
        }

        private void BaslikCiz(Graphics g)
        {
            using Font baslikFontu = new Font(
                "Segoe UI",
                14,
                FontStyle.Bold);

            using Brush baslikFirca =
                new SolidBrush(Color.White);

            using StringFormat format =
                new StringFormat
                {
                    Alignment =
                        StringAlignment.Center
                };

            g.DrawString(
                "GÖREV YÜKÜ\nYOĞUNLUK",
                baslikFontu,
                baslikFirca,
                new RectangleF(
                    0,
                    5,
                    Width,
                    50),
                format);
        }

        private void YogunlukGostergeCiz(
            Graphics g)
        {
            int barGenisligi = 55;
            int barYuksekligi = Height - 130;

            int barX =
                Width / 2 -
                barGenisligi / 2;

            int barY = 70;

            Rectangle disAlan = new Rectangle(
                barX,
                barY,
                barGenisligi,
                barYuksekligi);

            using Pen disCizgi = new Pen(
                Color.White,
                3);

            using GraphicsPath disYol =
                YuvarlakDikdortgen(
                    disAlan,
                    15);

            g.DrawPath(
                disCizgi,
                disYol);

            double oran =
                (Yogunluk - Minimum) /
                (Maximum - Minimum);

            oran = Math.Max(
                0,
                Math.Min(1, oran));

            int icBosluk = 6;

            int kullanilabilirYukseklik =
                barYuksekligi -
                icBosluk * 2;

            int dolulukYuksekligi =
                (int)(
                    kullanilabilirYukseklik *
                    oran);

            Rectangle dolulukAlani =
                new Rectangle(
                    barX + icBosluk,
                    barY +
                    barYuksekligi -
                    icBosluk -
                    dolulukYuksekligi,
                    barGenisligi -
                    icBosluk * 2,
                    dolulukYuksekligi);

            Color dolulukRengi;

            if (Yogunluk < 1.0)
            {
                dolulukRengi =
                    Color.DeepSkyBlue;
            }
            else if (Yogunluk <= 1.3)
            {
                dolulukRengi =
                    Color.LimeGreen;
            }
            else
            {
                dolulukRengi =
                    Color.OrangeRed;
            }

            using Brush dolulukFirca =
                new SolidBrush(
                    dolulukRengi);

            using GraphicsPath dolulukYolu =
                YuvarlakDikdortgen(
                    dolulukAlani,
                    10);

            g.FillPath(
                dolulukFirca,
                dolulukYolu);

            OlcekCiz(
                g,
                disAlan);

            DegerCiz(
                g,
                dolulukRengi);
        }

        private void OlcekCiz(
            Graphics g,
            Rectangle disAlan)
        {
            using Pen olcekKalemi =
                new Pen(
                    Color.LightGray,
                    1);

            using Font olcekFontu =
                new Font(
                    "Segoe UI",
                    8);

            using Brush yaziFirca =
                new SolidBrush(
                    Color.LightGray);

            int bolumSayisi = 6;

            for (int i = 0;
                 i <= bolumSayisi;
                 i++)
            {
                double oran =
                    i /
                    (double)bolumSayisi;

                int y =
                    disAlan.Bottom -
                    (int)(
                        oran *
                        disAlan.Height);

                int x1 =
                    disAlan.Right + 6;

                int x2 =
                    disAlan.Right + 16;

                g.DrawLine(
                    olcekKalemi,
                    x1,
                    y,
                    x2,
                    y);

                double deger =
                    Minimum +
                    oran *
                    (Maximum - Minimum);

                string metin =
                    deger.ToString("0.00");

                g.DrawString(
                    metin,
                    olcekFontu,
                    yaziFirca,
                    x2 + 3,
                    y - 7);
            }
        }

        private void DegerCiz(
            Graphics g,
            Color vurguRengi)
        {
            using Font degerFontu =
                new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold);

            using Brush degerFirca =
                new SolidBrush(
                    Color.White);

            string metin =
                Yogunluk.ToString("0.0000") +
                " kg/m³";

            SizeF boyut =
                g.MeasureString(
                    metin,
                    degerFontu);

            g.DrawString(
                metin,
                degerFontu,
                degerFirca,
                Width / 2f -
                boyut.Width / 2f,
                Height - 45);

            string durum;

            if (Yogunluk < 1.0)
            {
                durum = "DÜŞÜK";
            }
            else if (Yogunluk <= 1.3)
            {
                durum = "NORMAL";
            }
            else
            {
                durum = "YÜKSEK";
            }

            using Font durumFontu =
                new Font(
                    "Segoe UI",
                    9,
                    FontStyle.Bold);

            using Brush durumFirca =
                new SolidBrush(
                    vurguRengi);

            SizeF durumBoyutu =
                g.MeasureString(
                    durum,
                    durumFontu);

            g.DrawString(
                durum,
                durumFontu,
                durumFirca,
                Width / 2f -
                durumBoyutu.Width / 2f,
                Height - 22);
        }

        private static GraphicsPath YuvarlakDikdortgen(
            Rectangle alan,
            int yaricap)
        {
            int cap = yaricap * 2;

            GraphicsPath yol =
                new GraphicsPath();

            Rectangle yay =
                new Rectangle(
                    alan.X,
                    alan.Y,
                    cap,
                    cap);

            yol.AddArc(
                yay,
                180,
                90);

            yay.X =
                alan.Right - cap;

            yol.AddArc(
                yay,
                270,
                90);

            yay.Y =
                alan.Bottom - cap;

            yol.AddArc(
                yay,
                0,
                90);

            yay.X =
                alan.Left;

            yol.AddArc(
                yay,
                90,
                90);

            yol.CloseFigure();

            return yol;
        }
    }
}