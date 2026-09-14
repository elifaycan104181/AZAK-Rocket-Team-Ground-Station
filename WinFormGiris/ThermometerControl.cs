using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormGiris
{
    public class ThermometerControl : Control
    {
        private double sicaklik;

        public double Minimum { get; set; } = -20;
        public double Maximum { get; set; } = 60;

        public double Sicaklik
        {
            get => sicaklik;

            set
            {
                sicaklik = Math.Max(
                    Minimum,
                    Math.Min(Maximum, value));

                Invalidate();
            }
        }

        public ThermometerControl()
        {
            DoubleBuffered = true;

            BackColor = Color.FromArgb(
                10,
                25,
                45);

            Size = new Size(
                140,
                320);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            g.Clear(BackColor);

            using Font baslikFont = new Font(
    "Segoe UI",
    11,
    FontStyle.Bold);

            using Brush baslikFirca =
                new SolidBrush(Color.White);

            StringFormat sf = new StringFormat();

            sf.Alignment = StringAlignment.Center;

            g.DrawString(
                "GÖREV YÜKÜ\nSICAKLIK",
                baslikFont,
                baslikFirca,
                new RectangleF(
                    0,
                    5,
                    Width,
                    45),
                sf);

            int merkezX = Width / 2;

            int boruGenisligi = 34;
            int ampulBoyutu = 70;

            int ustBosluk = 70;
            int altYaziBosluk = 45;

            int boruX =
                merkezX - boruGenisligi / 2;

            int ampulY =
                Height - ampulBoyutu - altYaziBosluk;

            int boruYuksekligi =
                ampulY - ustBosluk + 10;

            Rectangle boruDis = new Rectangle(
                boruX,
                ustBosluk,
                boruGenisligi,
                boruYuksekligi);

            Rectangle ampul = new Rectangle(
                merkezX - ampulBoyutu / 2,
                ampulY,
                ampulBoyutu,
                ampulBoyutu);

            double oran =
                (Sicaklik - Minimum) /
                (Maximum - Minimum);

            oran = Math.Max(
                0,
                Math.Min(1, oran));

            int icBosluk = 6;

            int kullanilabilirYukseklik =
                boruYuksekligi - icBosluk * 2;

            int dolulukYuksekligi =
                (int)(kullanilabilirYukseklik * oran);

            Rectangle doluluk = new Rectangle(
                boruX + icBosluk,
                ustBosluk +
                boruYuksekligi -
                icBosluk -
                dolulukYuksekligi,
                boruGenisligi - icBosluk * 2,
                dolulukYuksekligi + 12);

            Color dolulukRengi;

            if (Sicaklik < 0)
            {
                dolulukRengi =
                    Color.DeepSkyBlue;
            }
            else if (Sicaklik <= 35)
            {
                dolulukRengi =
                    Color.YellowGreen;
            }
            else
            {
                dolulukRengi =
                    Color.Red;
            }

            using Pen disCizgi = new Pen(
                Color.White,
                4);

            using Brush dolulukFirca =
                new SolidBrush(dolulukRengi);

            g.FillRectangle(
                dolulukFirca,
                doluluk);

            g.FillEllipse(
                dolulukFirca,
                ampul);

            using GraphicsPath boruYolu =
                YuvarlakDikdortgen(
                    boruDis,
                    boruGenisligi / 2);

            g.DrawPath(
                disCizgi,
                boruYolu);

            g.DrawEllipse(
                disCizgi,
                ampul);

            OlcekCiz(
                g,
                boruDis);

            string degerMetni =
                Sicaklik.ToString("0.0") +
                " °C";

            using Font degerFontu = new Font(
                "Segoe UI",
                14,
                FontStyle.Bold);

            using Brush yaziFirca =
                new SolidBrush(Color.White);

            SizeF degerBoyutu =
                g.MeasureString(
                    degerMetni,
                    degerFontu);

            g.DrawString(
                degerMetni,
                degerFontu,
                yaziFirca,
                merkezX -
                degerBoyutu.Width / 2,
                Height - 35);
        }

        private void OlcekCiz(
            Graphics g,
            Rectangle boruDis)
        {
            using Pen olcekKalemi = new Pen(
                Color.LightGray,
                1);

            using Font olcekFontu = new Font(
                "Segoe UI",
                8);

            using Brush yaziFirca =
                new SolidBrush(
                    Color.LightGray);

            int bolumSayisi = 8;

            for (int i = 0; i <= bolumSayisi; i++)
            {
                double oran =
                    i / (double)bolumSayisi;

                int y =
                    boruDis.Bottom -
                    (int)(oran * boruDis.Height);

                int x1 =
                    boruDis.Right + 6;

                int x2 =
                    boruDis.Right + 16;

                g.DrawLine(
                    olcekKalemi,
                    x1,
                    y,
                    x2,
                    y);

                double deger =
                    Minimum +
                    oran * (Maximum - Minimum);

                string metin =
                    deger.ToString("0");

                g.DrawString(
                    metin,
                    olcekFontu,
                    yaziFirca,
                    x2 + 3,
                    y - 7);
            }
        }

        private static GraphicsPath YuvarlakDikdortgen(
            Rectangle alan,
            int yaricap)
        {
            int cap = yaricap * 2;

            GraphicsPath yol =
                new GraphicsPath();

            Rectangle yay = new Rectangle(
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