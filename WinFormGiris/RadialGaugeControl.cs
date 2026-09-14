using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormGiris
{
    public class RadialGaugeControl : Control
    {
        private double deger;

        public double Minimum { get; set; } = 0;
        public double Maximum { get; set; } = 100;

        public string Baslik { get; set; } = "";
        public string Birim { get; set; } = "";

        public int OndalikBasamak { get; set; } = 1;

        public double Deger
        {
            get => deger;

            set
            {
                deger = Math.Max(
                    Minimum,
                    Math.Min(Maximum, value));

                Invalidate();
            }
        }

        public RadialGaugeControl()
        {
            DoubleBuffered = true;

            BackColor =
                Color.FromArgb(10, 25, 45);

            Size =
                new Size(180, 160);
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
            GaugeCiz(g);
        }

        private void BaslikCiz(Graphics g)
        {
            using Font font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            using Brush firca =
                new SolidBrush(Color.White);

            using StringFormat sf =
                new StringFormat
                {
                    Alignment =
                        StringAlignment.Center
                };

            g.DrawString(
                Baslik,
                font,
                firca,
                new RectangleF(
                    0,
                    5,
                    Width,
                    40),
                sf);
        }

        private void GaugeCiz(Graphics g)
        {
            int merkezX =
                Width / 2;

            int merkezY =
                Height - 35;

            int yaricap =
                Math.Min(
                    Width / 2 - 18,
                    Height - 75);

            if (yaricap < 25)
                return;

            Rectangle yay =
                new Rectangle(
                    merkezX - yaricap,
                    merkezY - yaricap,
                    yaricap * 2,
                    yaricap * 2);

            using Pen arkaPlan =
                new Pen(
                    Color.FromArgb(
                        45,
                        65,
                        85),
                    10);

            arkaPlan.StartCap =
                LineCap.Round;

            arkaPlan.EndCap =
                LineCap.Round;

            g.DrawArc(
                arkaPlan,
                yay,
                180,
                180);

            double oran =
                (Deger - Minimum) /
                (Maximum - Minimum);

            oran =
                Math.Max(
                    0,
                    Math.Min(1, oran));

            float dolulukAcisi =
                (float)(oran * 180);

            Color vurguRengi =
                DegerRengiBul();

            using Pen doluluk =
                new Pen(
                    vurguRengi,
                    10);

            doluluk.StartCap =
                LineCap.Round;

            doluluk.EndCap =
                LineCap.Round;

            g.DrawArc(
                doluluk,
                yay,
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
            using Pen cizgi =
                new Pen(
                    Color.White,
                    1.5f);

            using Font font =
                new Font(
                    "Segoe UI",
                    7F);

            using Brush firca =
                new SolidBrush(
                    Color.LightGray);

            int bolumSayisi = 4;

            for (int i = 0;
                 i <= bolumSayisi;
                 i++)
            {
                double oran =
                    i /
                    (double)bolumSayisi;

                double aci =
                    Math.PI -
                    oran * Math.PI;

                int disX =
                    merkezX +
                    (int)(
                        Math.Cos(aci) *
                        (yaricap - 3));

                int disY =
                    merkezY -
                    (int)(
                        Math.Sin(aci) *
                        (yaricap - 3));

                int icX =
                    merkezX +
                    (int)(
                        Math.Cos(aci) *
                        (yaricap - 13));

                int icY =
                    merkezY -
                    (int)(
                        Math.Sin(aci) *
                        (yaricap - 13));

                g.DrawLine(
                    cizgi,
                    icX,
                    icY,
                    disX,
                    disY);

                double deger =
                    Minimum +
                    oran *
                    (Maximum - Minimum);

                string metin =
                    OndalikBasamak == 0
                    ? deger.ToString("0")
                    : deger.ToString(
                        "0." +
                        new string(
                            '0',
                            OndalikBasamak));

                SizeF boyut =
                    g.MeasureString(
                        metin,
                        font);

                float yaziYariCap =
                    yaricap - 26;

                float x =
                    merkezX +
                    (float)(
                        Math.Cos(aci) *
                        yaziYariCap) -
                    boyut.Width / 2;

                float y =
                    merkezY -
                    (float)(
                        Math.Sin(aci) *
                        yaziYariCap) -
                    boyut.Height / 2;

                g.DrawString(
                    metin,
                    font,
                    firca,
                    x,
                    y);
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

            int ibreUzunlugu =
                yaricap - 30;

            int ibreX =
                merkezX +
                (int)(
                    Math.Cos(aci) *
                    ibreUzunlugu);

            int ibreY =
                merkezY -
                (int)(
                    Math.Sin(aci) *
                    ibreUzunlugu);

            using Pen ibre =
                new Pen(
                    Color.OrangeRed,
                    3);

            g.DrawLine(
                ibre,
                merkezX,
                merkezY,
                ibreX,
                ibreY);

            using Brush merkez =
                new SolidBrush(
                    Color.White);

            g.FillEllipse(
                merkez,
                merkezX - 5,
                merkezY - 5,
                10,
                10);
        }

        private void DegerCiz(
            Graphics g,
            int merkezX,
            int merkezY)
        {
            using Font font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            using Brush firca =
                new SolidBrush(
                    Color.White);

            string format =
                OndalikBasamak == 0
                ? "0"
                : "0." +
                  new string(
                      '0',
                      OndalikBasamak);

            string metin =
                Deger.ToString(format) +
                " " +
                Birim;

            SizeF boyut =
                g.MeasureString(
                    metin,
                    font);

            g.DrawString(
                metin,
                font,
                firca,
                merkezX -
                boyut.Width / 2,
                merkezY + 7);
        }

        private Color DegerRengiBul()
        {
            double oran =
                (Deger - Minimum) /
                (Maximum - Minimum);

            if (oran < 0.2)
                return Color.DeepSkyBlue;

            if (oran < 0.85)
                return Color.LimeGreen;

            return Color.OrangeRed;
        }
    }
}