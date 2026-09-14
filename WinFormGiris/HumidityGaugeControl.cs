using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormGiris
{
    public class HumidityGaugeControl : Control
    {
        private double nem;

        public double Nem
        {
            get => nem;

            set
            {
                nem = Math.Max(
                    0,
                    Math.Min(100, value));

                Invalidate();
            }
        }

        public HumidityGaugeControl()
        {
            DoubleBuffered = true;

            BackColor = Color.FromArgb(
                10,
                25,
                45);

            Size = new Size(
                230,
                260);
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
            NemGostergeCiz(g);
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
                "GÖREV YÜKÜ\nNEM",
                baslikFontu,
                baslikFirca,
                new RectangleF(
                    0,
                    5,
                    Width,
                    50),
                format);
        }

        private void NemGostergeCiz(Graphics g)
        {
            int kalinlik = 18;

            int cemberBoyutu = Math.Min(
                Width - 50,
                Height - 100);

            int x =
                (Width - cemberBoyutu) / 2;

            int y = 65;

            Rectangle cemberAlani = new Rectangle(
                x,
                y,
                cemberBoyutu,
                cemberBoyutu);

            using Pen arkaPlanKalemi = new Pen(
                Color.FromArgb(
                    45,
                    65,
                    85),
                kalinlik);

            arkaPlanKalemi.StartCap =
                LineCap.Round;

            arkaPlanKalemi.EndCap =
                LineCap.Round;

            g.DrawArc(
                arkaPlanKalemi,
                cemberAlani,
                -90,
                360);

            float dolulukAcisi =
                (float)(Nem / 100.0 * 360.0);

            Color nemRengi;

            if (Nem < 20)
            {
                nemRengi = Color.Orange;
            }
            else if (Nem <= 80)
            {
                nemRengi = Color.DeepSkyBlue;
            }
            else
            {
                nemRengi = Color.Red;
            }

            using Pen dolulukKalemi = new Pen(
                nemRengi,
                kalinlik);

            dolulukKalemi.StartCap =
                LineCap.Round;

            dolulukKalemi.EndCap =
                LineCap.Round;

            g.DrawArc(
                dolulukKalemi,
                cemberAlani,
                -90,
                dolulukAcisi);

            MerkezDegeriCiz(
                g,
                cemberAlani);

            DurumYazisiCiz(
                g,
                cemberAlani);
        }

        private void MerkezDegeriCiz(
            Graphics g,
            Rectangle cemberAlani)
        {
            using Font degerFontu = new Font(
                "Segoe UI",
                22,
                FontStyle.Bold);

            using Brush yaziFirca =
                new SolidBrush(Color.White);

            string metin =
                "%" + Nem.ToString("0.0");

            SizeF boyut =
                g.MeasureString(
                    metin,
                    degerFontu);

            float x =
                cemberAlani.X +
                cemberAlani.Width / 2f -
                boyut.Width / 2f;

            float y =
                cemberAlani.Y +
                cemberAlani.Height / 2f -
                boyut.Height / 2f -
                5;

            g.DrawString(
                metin,
                degerFontu,
                yaziFirca,
                x,
                y);
        }

        private void DurumYazisiCiz(
            Graphics g,
            Rectangle cemberAlani)
        {
            string durum;

            Color durumRengi;

            if (Nem < 20)
            {
                durum = "DÜŞÜK";
                durumRengi = Color.Orange;
            }
            else if (Nem <= 80)
            {
                durum = "NORMAL";
                durumRengi = Color.LimeGreen;
            }
            else
            {
                durum = "YÜKSEK";
                durumRengi = Color.Red;
            }

            using Font durumFontu = new Font(
                "Segoe UI",
                10,
                FontStyle.Bold);

            using Brush durumFirca =
                new SolidBrush(durumRengi);

            SizeF boyut =
                g.MeasureString(
                    durum,
                    durumFontu);

            float x =
                cemberAlani.X +
                cemberAlani.Width / 2f -
                boyut.Width / 2f;

            float y =
                cemberAlani.Y +
                cemberAlani.Height / 2f +
                25;

            g.DrawString(
                durum,
                durumFontu,
                durumFirca,
                x,
                y);
        }
    }
}