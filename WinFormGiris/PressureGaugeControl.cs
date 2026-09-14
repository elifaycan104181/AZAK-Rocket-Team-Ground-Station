using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormGiris
{
    public class PressureGaugeControl : Control
    {
        private double basinc;

        public double Minimum { get; set; } = 300;
        public double Maximum { get; set; } = 1100;

        public double Basinc
        {
            get => basinc;

            set
            {
                basinc = Math.Max(
                    Minimum,
                    Math.Min(Maximum, value));

                Invalidate();
            }
        }

        public PressureGaugeControl()
        {
            DoubleBuffered = true;

            BackColor = Color.FromArgb(
                10,
                25,
                45);

            Size = new Size(
                300,
                230);
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
            ManometreCiz(g);
        }

        private void BaslikCiz(Graphics g)
        {
            using Font baslikFontu = new Font(
                "Segoe UI",
                11,
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
                "GÖREV YÜKÜ\nBASINÇ",
                baslikFontu,
                baslikFirca,
                new RectangleF(
                    0,
                    5,
                    Width,
                    50),
                format);
        }

        private void ManometreCiz(Graphics g)
        {
            int merkezX = Width / 2;
            int merkezY = Height - 45;

            int yaricap = Math.Min(
                Width / 2 - 30,
                Height - 95);

            Rectangle yayAlani = new Rectangle(
                merkezX - yaricap,
                merkezY - yaricap,
                yaricap * 2,
                yaricap * 2);

            using Pen arkaPlanKalemi = new Pen(
                Color.FromArgb(
                    50,
                    70,
                    90),
                16);

            arkaPlanKalemi.StartCap =
                LineCap.Round;

            arkaPlanKalemi.EndCap =
                LineCap.Round;

            g.DrawArc(
                arkaPlanKalemi,
                yayAlani,
                180,
                180);

            double oran =
                (Basinc - Minimum) /
                (Maximum - Minimum);

            oran = Math.Max(
                0,
                Math.Min(1, oran));

            float dolulukAcisi =
                (float)(oran * 180);

            Color yayRengi;

            if (Basinc < 500)
            {
                yayRengi =
                    Color.DeepSkyBlue;
            }
            else if (Basinc <= 1050)
            {
                yayRengi =
                    Color.LimeGreen;
            }
            else
            {
                yayRengi =
                    Color.Red;
            }

            using Pen dolulukKalemi = new Pen(
                yayRengi,
                16);

            dolulukKalemi.StartCap =
                LineCap.Round;

            dolulukKalemi.EndCap =
                LineCap.Round;

            g.DrawArc(
                dolulukKalemi,
                yayAlani,
                180,
                dolulukAcisi);

            OlcekCiz(
                g,
                merkezX,
                merkezY,
                yaricap);

            IbreCiz(
                g,
                merkezX,
                merkezY,
                yaricap,
                oran);

            DegerCiz(
                g,
                merkezX,
                merkezY);
        }

        private void OlcekCiz(
            Graphics g,
            int merkezX,
            int merkezY,
            int yaricap)
        {
            using Pen cizgiKalemi =
                new Pen(Color.White, 2);

            using Font olcekFontu =
                new Font("Segoe UI", 8);

            using Brush yaziFirca =
                new SolidBrush(
                    Color.LightGray);

            int bolumSayisi = 8;

            for (int i = 0;
                 i <= bolumSayisi;
                 i++)
            {
                double oran =
                    i / (double)bolumSayisi;

                double aci =
                    Math.PI -
                    oran * Math.PI;

                int disX =
                    merkezX +
                    (int)(Math.Cos(aci) *
                    (yaricap - 5));

                int disY =
                    merkezY -
                    (int)(Math.Sin(aci) *
                    (yaricap - 5));

                int icX =
                    merkezX +
                    (int)(Math.Cos(aci) *
                    (yaricap - 20));

                int icY =
                    merkezY -
                    (int)(Math.Sin(aci) *
                    (yaricap - 20));

                g.DrawLine(
                    cizgiKalemi,
                    icX,
                    icY,
                    disX,
                    disY);

                double deger =
                    Minimum +
                    oran *
                    (Maximum - Minimum);

                string metin =
                    deger.ToString("0");

                SizeF boyut =
                    g.MeasureString(
                        metin,
                        olcekFontu);

                int yaziX =
                    merkezX +
                    (int)(Math.Cos(aci) *
                    (yaricap - 38)) -
                    (int)(boyut.Width / 2);

                int yaziY =
                    merkezY -
                    (int)(Math.Sin(aci) *
                    (yaricap - 38)) -
                    (int)(boyut.Height / 2);

                g.DrawString(
                    metin,
                    olcekFontu,
                    yaziFirca,
                    yaziX,
                    yaziY);
            }
        }

        private void IbreCiz(
            Graphics g,
            int merkezX,
            int merkezY,
            int yaricap,
            double oran)
        {
            double aci =
                Math.PI -
                oran * Math.PI;

            int ibreX =
                merkezX +
                (int)(Math.Cos(aci) *
                (yaricap - 35));

            int ibreY =
                merkezY -
                (int)(Math.Sin(aci) *
                (yaricap - 35));

            using Pen ibreKalemi =
                new Pen(
                    Color.OrangeRed,
                    4);

            ibreKalemi.EndCap =
                LineCap.ArrowAnchor;

            g.DrawLine(
                ibreKalemi,
                merkezX,
                merkezY,
                ibreX,
                ibreY);

            using Brush merkezFirca =
                new SolidBrush(Color.White);

            g.FillEllipse(
                merkezFirca,
                merkezX - 7,
                merkezY - 7,
                14,
                14);
        }

        private void DegerCiz(
            Graphics g,
            int merkezX,
            int merkezY)
        {
            using Font degerFontu =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold);

            using Brush degerFirca =
                new SolidBrush(Color.White);

            string metin =
                Basinc.ToString("0.00") +
                " hPa";

            SizeF boyut =
                g.MeasureString(
                    metin,
                    degerFontu);

            g.DrawString(
                metin,
                degerFontu,
                degerFirca,
                merkezX -
                boyut.Width / 2,
                merkezY + 10);
        }
    }
}