
using System.Windows.Forms.DataVisualization.Charting;

namespace WinFormGiris
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label16 = new Label();
            label18 = new Label();
            PGpsIrtifa = new TextBox();
            GyroX = new TextBox();
            PIvmeZ = new TextBox();
            PIvmeY = new TextBox();
            PIvmeX = new TextBox();
            PBoylam = new TextBox();
            PEnlem = new TextBox();
            irtifa = new TextBox();
            RGpsIrtifa = new TextBox();
            REnlem = new TextBox();
            RBoylam = new TextBox();
            State = new TextBox();
            Aci = new TextBox();
            GyroZ = new TextBox();
            GyroY = new TextBox();
            cmbUKBPort = new ComboBox();
            cmbPayloadPort = new ComboBox();
            cmbUKBBaud = new ComboBox();
            cmbPayloadBaud = new ComboBox();
            lblSonPayloadPaket = new Label();
            panel1 = new Panel();
            linkLabel2 = new LinkLabel();
            panel2 = new Panel();
            lblHaritaLegend = new Label();
            button1 = new Button();
            panel3 = new Panel();
            BAGLANTIBILGILERI = new Panel();
            btnKayitKlasoruAc = new Button();
            btnKayit = new Button();
            lblKayitDosyasi = new Label();
            panel4 = new Panel();
            Ppktsayaci = new TextBox();
            label19 = new Label();
            PYogunluk = new TextBox();
            PNem = new TextBox();
            PSicaklik = new TextBox();
            PBasinc = new TextBox();
            pcrc = new Label();
            yogunluk = new Label();
            basinc = new Label();
            sicaklik = new Label();
            linkLabel1 = new LinkLabel();
            panel5 = new Panel();
            panel6 = new Panel();
            panelPayloadGrafik = new Panel();
            panel7 = new Panel();
            lblBaglanti = new Label();
            btnUKBBaglan = new Button();
            btnPayloadBaglan = new Button();
            lblPayloadBaglanti = new Label();
            panelSicaklikGosterge = new Panel();
            panelBasincGosterge = new Panel();
            panelNemGosterge = new Panel();
            panelYogunlukGosterge = new Panel();
            panelUcusDurumu = new Panel();
            lblUcusDurumuBaslik = new Label();
            lblMevcutDurum = new Label();
            lblKalkis = new Label();
            lblBurnOut = new Label();
            lblIrtifaEsigi = new Label();
            lblAciDurumu = new Label();
            lblAlcalma = new Label();
            lblSuruklenmeParasutu = new Label();
            lblBelirliIrtifa = new Label();
            lblAnaParasut = new Label();
            lblGorevSuresi = new Label();
            lblMesafeYerUKB = new Label();
            lblMesafeYerPayload = new Label();
            lblMesafeUKBPayload = new Label();
            lblUKBKalite = new Label();
            lblPayloadKalite = new Label();
            panelMesafeBilgileri = new Panel();
            lblYerEnlem = new Label();
            txtYerEnlem = new TextBox();
            lblYerBoylam = new Label();
            txtYerBoylam = new TextBox();
            btnYerKonumUygula = new Button();
            lblMesafeBaslik = new Label();
            UKBVERİLERİ = new Panel();
            UKBGRAFIKLERI = new LinkLabel();
            BAGLANTIPANELI = new Panel();
            label1 = new Label();
            GOKCE = new Label();
            gy_ibreleri = new Panel();
            btnHaritaCache = new Button();
            btnHaritaCacheKlasoruAc = new Button();
            CachePanel = new Panel();
            pictureBoxTakim = new PictureBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            BAGLANTIBILGILERI.SuspendLayout();
            panel4.SuspendLayout();
            panelUcusDurumu.SuspendLayout();
            panelMesafeBilgileri.SuspendLayout();
            UKBVERİLERİ.SuspendLayout();
            BAGLANTIPANELI.SuspendLayout();
            gy_ibreleri.SuspendLayout();
            CachePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTakim).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(17, 40);
            label3.Name = "label3";
            label3.Size = new Size(34, 15);
            label3.TabIndex = 3;
            label3.Text = "İrtifa:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(17, 65);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 4;
            label4.Text = "GPS İrtifa:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(17, 91);
            label5.Name = "label5";
            label5.Size = new Size(67, 15);
            label5.TabIndex = 5;
            label5.Text = "GPS Enlem:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.White;
            label6.Location = new Point(17, 117);
            label6.Name = "label6";
            label6.Size = new Size(74, 15);
            label6.TabIndex = 6;
            label6.Text = "GPS Boylam:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(17, 58);
            label7.Name = "label7";
            label7.Size = new Size(122, 15);
            label7.TabIndex = 7;
            label7.Text = "Görev Yükü GPS İrtifa:";
            label7.Click += label7_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.White;
            label8.Location = new Point(17, 83);
            label8.Name = "label8";
            label8.Size = new Size(107, 15);
            label8.TabIndex = 8;
            label8.Text = "Görev Yükü Enlem:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(17, 108);
            label9.Name = "label9";
            label9.Size = new Size(114, 15);
            label9.TabIndex = 9;
            label9.Text = "Görev Yükü Boylam:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = Color.White;
            label10.Location = new Point(17, 222);
            label10.Name = "label10";
            label10.Size = new Size(46, 15);
            label10.TabIndex = 10;
            label10.Text = "İvme X:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = Color.White;
            label11.Location = new Point(17, 248);
            label11.Name = "label11";
            label11.Size = new Size(46, 15);
            label11.TabIndex = 11;
            label11.Text = "İvme Y:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = Color.White;
            label12.Location = new Point(17, 276);
            label12.Name = "label12";
            label12.Size = new Size(46, 15);
            label12.TabIndex = 12;
            label12.Text = "İvme Z:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.ForeColor = Color.White;
            label13.Location = new Point(17, 143);
            label13.Name = "label13";
            label13.Size = new Size(45, 15);
            label13.TabIndex = 13;
            label13.Text = "Gyro X:";
            label13.Click += label13_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.ForeColor = Color.White;
            label14.Location = new Point(17, 197);
            label14.Name = "label14";
            label14.Size = new Size(45, 15);
            label14.TabIndex = 14;
            label14.Text = "Gyro Z:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.ForeColor = Color.White;
            label15.Location = new Point(17, 170);
            label15.Name = "label15";
            label15.Size = new Size(45, 15);
            label15.TabIndex = 15;
            label15.Text = "Gyro Y:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.ForeColor = Color.White;
            label16.Location = new Point(17, 305);
            label16.Name = "label16";
            label16.Size = new Size(27, 15);
            label16.TabIndex = 16;
            label16.Text = "Açı:";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.ForeColor = Color.White;
            label18.Location = new Point(17, 332);
            label18.Name = "label18";
            label18.Size = new Size(47, 15);
            label18.TabIndex = 18;
            label18.Text = "Durum:";
            // 
            // PGpsIrtifa
            // 
            PGpsIrtifa.Location = new Point(145, 55);
            PGpsIrtifa.Name = "PGpsIrtifa";
            PGpsIrtifa.Size = new Size(114, 23);
            PGpsIrtifa.TabIndex = 19;
            // 
            // GyroX
            // 
            GyroX.Location = new Point(145, 140);
            GyroX.Name = "GyroX";
            GyroX.Size = new Size(114, 23);
            GyroX.TabIndex = 20;
            GyroX.TextChanged += GyroX_TextChanged;
            // 
            // PIvmeZ
            // 
            PIvmeZ.Location = new Point(145, 273);
            PIvmeZ.Name = "PIvmeZ";
            PIvmeZ.Size = new Size(114, 23);
            PIvmeZ.TabIndex = 21;
            PIvmeZ.TextChanged += PIvmeZ_TextChanged;
            // 
            // PIvmeY
            // 
            PIvmeY.Location = new Point(145, 245);
            PIvmeY.Name = "PIvmeY";
            PIvmeY.Size = new Size(114, 23);
            PIvmeY.TabIndex = 22;
            PIvmeY.TextChanged += PIvmeY_TextChanged;
            // 
            // PIvmeX
            // 
            PIvmeX.Location = new Point(145, 219);
            PIvmeX.Name = "PIvmeX";
            PIvmeX.Size = new Size(114, 23);
            PIvmeX.TabIndex = 23;
            PIvmeX.TextChanged += PIvmeX_TextChanged;
            // 
            // PBoylam
            // 
            PBoylam.Location = new Point(145, 105);
            PBoylam.Name = "PBoylam";
            PBoylam.Size = new Size(114, 23);
            PBoylam.TabIndex = 24;
            // 
            // PEnlem
            // 
            PEnlem.Location = new Point(145, 80);
            PEnlem.Name = "PEnlem";
            PEnlem.Size = new Size(114, 23);
            PEnlem.TabIndex = 25;
            // 
            // irtifa
            // 
            irtifa.Location = new Point(145, 37);
            irtifa.Name = "irtifa";
            irtifa.Size = new Size(114, 23);
            irtifa.TabIndex = 28;
            irtifa.TextChanged += irtifa_TextChanged;
            // 
            // RGpsIrtifa
            // 
            RGpsIrtifa.Location = new Point(145, 62);
            RGpsIrtifa.Name = "RGpsIrtifa";
            RGpsIrtifa.Size = new Size(114, 23);
            RGpsIrtifa.TabIndex = 29;
            RGpsIrtifa.TextChanged += RGpsIrtifa_TextChanged;
            // 
            // REnlem
            // 
            REnlem.Location = new Point(145, 88);
            REnlem.Name = "REnlem";
            REnlem.Size = new Size(114, 23);
            REnlem.TabIndex = 30;
            REnlem.TextChanged += REnlem_TextChanged;
            // 
            // RBoylam
            // 
            RBoylam.Location = new Point(145, 114);
            RBoylam.Name = "RBoylam";
            RBoylam.Size = new Size(114, 23);
            RBoylam.TabIndex = 31;
            RBoylam.TextChanged += RBoylam_TextChanged;
            // 
            // State
            // 
            State.Location = new Point(145, 329);
            State.Name = "State";
            State.Size = new Size(114, 23);
            State.TabIndex = 33;
            State.TextChanged += State_TextChanged;
            // 
            // Aci
            // 
            Aci.Location = new Point(145, 302);
            Aci.Name = "Aci";
            Aci.Size = new Size(114, 23);
            Aci.TabIndex = 34;
            Aci.TextChanged += Aci_TextChanged;
            // 
            // GyroZ
            // 
            GyroZ.Location = new Point(145, 194);
            GyroZ.Name = "GyroZ";
            GyroZ.Size = new Size(114, 23);
            GyroZ.TabIndex = 35;
            GyroZ.TextChanged += GyroZ_TextChanged;
            // 
            // GyroY
            // 
            GyroY.Location = new Point(145, 167);
            GyroY.Name = "GyroY";
            GyroY.Size = new Size(114, 23);
            GyroY.TabIndex = 36;
            GyroY.TextChanged += GyroY_TextChanged;
            // 
            // cmbUKBPort
            // 
            cmbUKBPort.FormattingEnabled = true;
            cmbUKBPort.Items.AddRange(new object[] { "COM3", "COM4" });
            cmbUKBPort.Location = new Point(12, 13);
            cmbUKBPort.Name = "cmbUKBPort";
            cmbUKBPort.Size = new Size(124, 23);
            cmbUKBPort.TabIndex = 37;
            cmbUKBPort.Text = "UKB PORT SEÇİMİ";
            // 
            // cmbPayloadPort
            // 
            cmbPayloadPort.FormattingEnabled = true;
            cmbPayloadPort.Items.AddRange(new object[] { "COM8", "COM9" });
            cmbPayloadPort.Location = new Point(423, 15);
            cmbPayloadPort.Name = "cmbPayloadPort";
            cmbPayloadPort.Size = new Size(169, 23);
            cmbPayloadPort.TabIndex = 38;
            cmbPayloadPort.Text = "PAYLOAD PORT SEÇİMİ";
            // 
            // cmbUKBBaud
            // 
            cmbUKBBaud.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUKBBaud.FormattingEnabled = true;
            cmbUKBBaud.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            cmbUKBBaud.Location = new Point(142, 13);
            cmbUKBBaud.Name = "cmbUKBBaud";
            cmbUKBBaud.Size = new Size(90, 23);
            cmbUKBBaud.TabIndex = 58;
            // 
            // cmbPayloadBaud
            // 
            cmbPayloadBaud.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPayloadBaud.FormattingEnabled = true;
            cmbPayloadBaud.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            cmbPayloadBaud.Location = new Point(598, 15);
            cmbPayloadBaud.Name = "cmbPayloadBaud";
            cmbPayloadBaud.Size = new Size(90, 23);
            cmbPayloadBaud.TabIndex = 59;
            // 
            // lblSonPayloadPaket
            // 
            lblSonPayloadPaket.AutoSize = true;
            lblSonPayloadPaket.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSonPayloadPaket.ForeColor = Color.Orange;
            lblSonPayloadPaket.Location = new Point(1035, 32);
            lblSonPayloadPaket.Name = "lblSonPayloadPaket";
            lblSonPayloadPaket.Size = new Size(147, 15);
            lblSonPayloadPaket.TabIndex = 60;
            lblSonPayloadPaket.Text = "SON PAKET: BEKLENİYOR";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(5, 12, 22);
            panel1.Controls.Add(linkLabel2);
            panel1.Controls.Add(Aci);
            panel1.Controls.Add(GyroY);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(GyroZ);
            panel1.Controls.Add(PIvmeZ);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(PIvmeY);
            panel1.Controls.Add(PIvmeX);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(State);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(RBoylam);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(REnlem);
            panel1.Controls.Add(RGpsIrtifa);
            panel1.Controls.Add(irtifa);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(label16);
            panel1.Controls.Add(label18);
            panel1.Controls.Add(GyroX);
            panel1.Location = new Point(12, 70);
            panel1.Name = "panel1";
            panel1.Size = new Size(273, 360);
            panel1.TabIndex = 39;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.BackColor = Color.Transparent;
            linkLabel2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            linkLabel2.ForeColor = Color.DeepSkyBlue;
            linkLabel2.LinkColor = Color.DeepSkyBlue;
            linkLabel2.Location = new Point(17, 9);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(218, 15);
            linkLabel2.TabIndex = 37;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "ÖZGÜN UÇUŞ KONTROL BİLGİSAYARI";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(5, 12, 22);
            panel2.Controls.Add(lblHaritaLegend);
            panel2.Location = new Point(291, 70);
            panel2.Name = "panel2";
            panel2.Size = new Size(350, 360);
            panel2.TabIndex = 40;
            panel2.Paint += panel2_Paint;
            // 
            // lblHaritaLegend
            // 
            lblHaritaLegend.BackColor = Color.FromArgb(180, 20, 28, 38);
            lblHaritaLegend.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHaritaLegend.ForeColor = Color.White;
            lblHaritaLegend.Location = new Point(0, 1);
            lblHaritaLegend.Name = "lblHaritaLegend";
            lblHaritaLegend.Size = new Size(138, 71);
            lblHaritaLegend.TabIndex = 0;
            lblHaritaLegend.Text = "● YEŞİL  Yer İstasyonu\n● MAVİ   GÖKÇE / UKB\n● KIRMIZI Görev Yükü\n──── Bağlantı / Mesafe";
            // 
            // button1
            // 
            button1.Location = new Point(15, 38);
            button1.Name = "button1";
            button1.Size = new Size(270, 32);
            button1.TabIndex = 41;
            button1.Text = "MESAFELERİ HESAPLA";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(5, 12, 22);
            panel3.Location = new Point(1239, 70);
            panel3.Name = "panel3";
            panel3.Size = new Size(327, 360);
            panel3.TabIndex = 42;
            panel3.Paint += panel3_Paint;
            // 
            // BAGLANTIBILGILERI
            // 
            BAGLANTIBILGILERI.BackColor = Color.FromArgb(5, 12, 22);
            BAGLANTIBILGILERI.Controls.Add(btnKayitKlasoruAc);
            BAGLANTIBILGILERI.Controls.Add(btnKayit);
            BAGLANTIBILGILERI.Controls.Add(lblKayitDosyasi);
            BAGLANTIBILGILERI.Location = new Point(1239, 436);
            BAGLANTIBILGILERI.Name = "BAGLANTIBILGILERI";
            BAGLANTIBILGILERI.Size = new Size(327, 68);
            BAGLANTIBILGILERI.TabIndex = 76;
            BAGLANTIBILGILERI.Paint += flowLayoutPanel1_Paint;
            // 
            // btnKayitKlasoruAc
            // 
            btnKayitKlasoruAc.Location = new Point(171, 7);
            btnKayitKlasoruAc.Name = "btnKayitKlasoruAc";
            btnKayitKlasoruAc.Size = new Size(135, 23);
            btnKayitKlasoruAc.TabIndex = 51;
            btnKayitKlasoruAc.Text = "KLASÖRÜ AÇ";
            btnKayitKlasoruAc.UseVisualStyleBackColor = true;
            btnKayitKlasoruAc.Click += btnKayitKlasoruAc_Click;
            // 
            // btnKayit
            // 
            btnKayit.Location = new Point(24, 7);
            btnKayit.Margin = new Padding(8, 6, 3, 2);
            btnKayit.Name = "btnKayit";
            btnKayit.Size = new Size(126, 23);
            btnKayit.TabIndex = 49;
            btnKayit.Text = "KAYIT BAŞLAT";
            btnKayit.UseVisualStyleBackColor = true;
            btnKayit.Click += btnKayit_Click;
            // 
            // lblKayitDosyasi
            // 
            lblKayitDosyasi.AutoEllipsis = true;
            lblKayitDosyasi.BackColor = Color.FromArgb(10, 25, 45);
            lblKayitDosyasi.BorderStyle = BorderStyle.FixedSingle;
            lblKayitDosyasi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblKayitDosyasi.ForeColor = Color.DeepSkyBlue;
            lblKayitDosyasi.Location = new Point(10, 36);
            lblKayitDosyasi.Margin = new Padding(8, 2, 3, 3);
            lblKayitDosyasi.Name = "lblKayitDosyasi";
            lblKayitDosyasi.Size = new Size(312, 24);
            lblKayitDosyasi.TabIndex = 50;
            lblKayitDosyasi.Text = "DOSYA: -";
            lblKayitDosyasi.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(5, 12, 22);
            panel4.Controls.Add(Ppktsayaci);
            panel4.Controls.Add(label19);
            panel4.Controls.Add(PYogunluk);
            panel4.Controls.Add(PNem);
            panel4.Controls.Add(PSicaklik);
            panel4.Controls.Add(PBasinc);
            panel4.Controls.Add(pcrc);
            panel4.Controls.Add(yogunluk);
            panel4.Controls.Add(basinc);
            panel4.Controls.Add(sicaklik);
            panel4.Controls.Add(linkLabel1);
            panel4.Controls.Add(label8);
            panel4.Controls.Add(PGpsIrtifa);
            panel4.Controls.Add(PBoylam);
            panel4.Controls.Add(PEnlem);
            panel4.Controls.Add(label9);
            panel4.Controls.Add(label7);
            panel4.Location = new Point(12, 436);
            panel4.Name = "panel4";
            panel4.Size = new Size(273, 241);
            panel4.TabIndex = 44;
            // 
            // Ppktsayaci
            // 
            Ppktsayaci.Location = new Point(145, 29);
            Ppktsayaci.Name = "Ppktsayaci";
            Ppktsayaci.Size = new Size(114, 23);
            Ppktsayaci.TabIndex = 36;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.ForeColor = Color.White;
            label19.Location = new Point(17, 34);
            label19.Name = "label19";
            label19.Size = new Size(75, 15);
            label19.TabIndex = 35;
            label19.Text = "Paket Sayacı:";
            // 
            // PYogunluk
            // 
            PYogunluk.Location = new Point(145, 208);
            PYogunluk.Name = "PYogunluk";
            PYogunluk.Size = new Size(114, 23);
            PYogunluk.TabIndex = 34;
            // 
            // PNem
            // 
            PNem.Location = new Point(145, 182);
            PNem.Name = "PNem";
            PNem.Size = new Size(114, 23);
            PNem.TabIndex = 33;
            // 
            // PSicaklik
            // 
            PSicaklik.Location = new Point(145, 156);
            PSicaklik.Name = "PSicaklik";
            PSicaklik.Size = new Size(114, 23);
            PSicaklik.TabIndex = 32;
            // 
            // PBasinc
            // 
            PBasinc.Location = new Point(145, 130);
            PBasinc.Name = "PBasinc";
            PBasinc.Size = new Size(114, 23);
            PBasinc.TabIndex = 31;
            // 
            // pcrc
            // 
            pcrc.AutoSize = true;
            pcrc.ForeColor = Color.White;
            pcrc.Location = new Point(17, 211);
            pcrc.Name = "pcrc";
            pcrc.Size = new Size(60, 15);
            pcrc.TabIndex = 30;
            pcrc.Text = "Yoğunluk:";
            pcrc.Click += label22_Click;
            // 
            // yogunluk
            // 
            yogunluk.AutoSize = true;
            yogunluk.ForeColor = Color.White;
            yogunluk.Location = new Point(17, 185);
            yogunluk.Name = "yogunluk";
            yogunluk.Size = new Size(79, 15);
            yogunluk.TabIndex = 29;
            yogunluk.Text = "Nem Yüzdesi:";
            yogunluk.Click += label21_Click;
            // 
            // basinc
            // 
            basinc.AutoSize = true;
            basinc.ForeColor = Color.White;
            basinc.Location = new Point(17, 159);
            basinc.Name = "basinc";
            basinc.Size = new Size(49, 15);
            basinc.TabIndex = 28;
            basinc.Text = "Sıcaklık:";
            // 
            // sicaklik
            // 
            sicaklik.AutoSize = true;
            sicaklik.ForeColor = Color.White;
            sicaklik.Location = new Point(17, 135);
            sicaklik.Name = "sicaklik";
            sicaklik.Size = new Size(44, 15);
            sicaklik.TabIndex = 27;
            sicaklik.Text = "Basınç:";
            // 
            // linkLabel1
            // 
            linkLabel1.ActiveLinkColor = Color.WhiteSmoke;
            linkLabel1.AutoSize = true;
            linkLabel1.BackColor = Color.Transparent;
            linkLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            linkLabel1.LinkColor = Color.DarkOrange;
            linkLabel1.Location = new Point(20, 9);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(83, 15);
            linkLabel1.TabIndex = 26;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "GÖREV YÜKÜ";
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(45, 45, 48);
            panel5.Location = new Point(632, 21);
            panel5.Name = "panel5";
            panel5.Size = new Size(290, 199);
            panel5.TabIndex = 45;
            panel5.Paint += panel5_Paint;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(45, 45, 48);
            panel6.Location = new Point(316, 22);
            panel6.Name = "panel6";
            panel6.Size = new Size(310, 198);
            panel6.TabIndex = 47;
            // 
            // panelPayloadGrafik
            // 
            panelPayloadGrafik.BackColor = Color.FromArgb(5, 12, 22);
            panelPayloadGrafik.Location = new Point(12, 679);
            panelPayloadGrafik.Name = "panelPayloadGrafik";
            panelPayloadGrafik.Size = new Size(764, 183);
            panelPayloadGrafik.TabIndex = 90;
            // 
            // panel7
            // 
            panel7.BackColor = Color.FromArgb(45, 45, 48);
            panel7.Location = new Point(4, 23);
            panel7.Name = "panel7";
            panel7.Size = new Size(306, 197);
            panel7.TabIndex = 48;
            // 
            // lblBaglanti
            // 
            lblBaglanti.AutoSize = true;
            lblBaglanti.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblBaglanti.ForeColor = Color.Red;
            lblBaglanti.Location = new Point(12, 40);
            lblBaglanti.Name = "lblBaglanti";
            lblBaglanti.Size = new Size(117, 15);
            lblBaglanti.TabIndex = 50;
            lblBaglanti.Text = "● UKB BAĞLI DEĞİL";
            lblBaglanti.Click += lblBaglanti_Click;
            // 
            // btnUKBBaglan
            // 
            btnUKBBaglan.Location = new Point(238, 14);
            btnUKBBaglan.Name = "btnUKBBaglan";
            btnUKBBaglan.Size = new Size(87, 23);
            btnUKBBaglan.TabIndex = 51;
            btnUKBBaglan.Text = "UKB BAĞLAN";
            btnUKBBaglan.UseVisualStyleBackColor = true;
            btnUKBBaglan.Click += btnUKBBaglan_Click;
            // 
            // btnPayloadBaglan
            // 
            btnPayloadBaglan.Location = new Point(694, 14);
            btnPayloadBaglan.Name = "btnPayloadBaglan";
            btnPayloadBaglan.Size = new Size(102, 23);
            btnPayloadBaglan.TabIndex = 52;
            btnPayloadBaglan.Text = "PYLD BAĞLAN";
            btnPayloadBaglan.UseVisualStyleBackColor = true;
            btnPayloadBaglan.Click += btnPayloadBaglan_Click;
            // 
            // lblPayloadBaglanti
            // 
            lblPayloadBaglanti.AutoSize = true;
            lblPayloadBaglanti.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblPayloadBaglanti.ForeColor = Color.Red;
            lblPayloadBaglanti.ImageAlign = ContentAlignment.TopCenter;
            lblPayloadBaglanti.Location = new Point(423, 40);
            lblPayloadBaglanti.Name = "lblPayloadBaglanti";
            lblPayloadBaglanti.Size = new Size(144, 15);
            lblPayloadBaglanti.TabIndex = 53;
            lblPayloadBaglanti.Text = "● PAYLOAD BAĞLI DEĞİL";
            lblPayloadBaglanti.TextAlign = ContentAlignment.TopRight;
            // 
            // panelSicaklikGosterge
            // 
            panelSicaklikGosterge.BackColor = Color.FromArgb(5, 12, 22);
            panelSicaklikGosterge.BorderStyle = BorderStyle.FixedSingle;
            panelSicaklikGosterge.Location = new Point(395, 6);
            panelSicaklikGosterge.Name = "panelSicaklikGosterge";
            panelSicaklikGosterge.Size = new Size(188, 170);
            panelSicaklikGosterge.TabIndex = 54;
            // 
            // panelBasincGosterge
            // 
            panelBasincGosterge.BackColor = Color.FromArgb(10, 25, 45);
            panelBasincGosterge.BorderStyle = BorderStyle.FixedSingle;
            panelBasincGosterge.Location = new Point(7, 7);
            panelBasincGosterge.Name = "panelBasincGosterge";
            panelBasincGosterge.Size = new Size(189, 170);
            panelBasincGosterge.TabIndex = 55;
            // 
            // panelNemGosterge
            // 
            panelNemGosterge.BackColor = Color.FromArgb(10, 25, 45);
            panelNemGosterge.BorderStyle = BorderStyle.FixedSingle;
            panelNemGosterge.Location = new Point(589, 5);
            panelNemGosterge.Name = "panelNemGosterge";
            panelNemGosterge.Size = new Size(190, 169);
            panelNemGosterge.TabIndex = 56;
            // 
            // panelYogunlukGosterge
            // 
            panelYogunlukGosterge.BackColor = Color.FromArgb(10, 25, 45);
            panelYogunlukGosterge.BorderStyle = BorderStyle.FixedSingle;
            panelYogunlukGosterge.Location = new Point(201, 7);
            panelYogunlukGosterge.Name = "panelYogunlukGosterge";
            panelYogunlukGosterge.Size = new Size(188, 170);
            panelYogunlukGosterge.TabIndex = 57;
            // 
            // panelUcusDurumu
            // 
            panelUcusDurumu.BackColor = Color.FromArgb(10, 25, 45);
            panelUcusDurumu.Controls.Add(lblUcusDurumuBaslik);
            panelUcusDurumu.Controls.Add(lblMevcutDurum);
            panelUcusDurumu.Location = new Point(953, 70);
            panelUcusDurumu.Name = "panelUcusDurumu";
            panelUcusDurumu.Size = new Size(280, 360);
            panelUcusDurumu.TabIndex = 70;
            // 
            // lblUcusDurumuBaslik
            // 
            lblUcusDurumuBaslik.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUcusDurumuBaslik.ForeColor = Color.DeepSkyBlue;
            lblUcusDurumuBaslik.Location = new Point(20, 8);
            lblUcusDurumuBaslik.Name = "lblUcusDurumuBaslik";
            lblUcusDurumuBaslik.Size = new Size(150, 28);
            lblUcusDurumuBaslik.TabIndex = 0;
            lblUcusDurumuBaslik.Text = "UÇUŞ DURUMU";
            // 
            // lblMevcutDurum
            // 
            lblMevcutDurum.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMevcutDurum.ForeColor = Color.DeepSkyBlue;
            lblMevcutDurum.Location = new Point(20, 324);
            lblMevcutDurum.Name = "lblMevcutDurum";
            lblMevcutDurum.Size = new Size(240, 28);
            lblMevcutDurum.TabIndex = 1;
            lblMevcutDurum.Text = "MEVCUT DURUM: BEKLEME";
            lblMevcutDurum.Click += lblMevcutDurum_Click;
            // 
            // lblKalkis
            // 
            lblKalkis.Location = new Point(0, 0);
            lblKalkis.Name = "lblKalkis";
            lblKalkis.Size = new Size(100, 23);
            lblKalkis.TabIndex = 0;
            // 
            // lblBurnOut
            // 
            lblBurnOut.Location = new Point(0, 0);
            lblBurnOut.Name = "lblBurnOut";
            lblBurnOut.Size = new Size(100, 23);
            lblBurnOut.TabIndex = 0;
            // 
            // lblIrtifaEsigi
            // 
            lblIrtifaEsigi.Location = new Point(0, 0);
            lblIrtifaEsigi.Name = "lblIrtifaEsigi";
            lblIrtifaEsigi.Size = new Size(100, 23);
            lblIrtifaEsigi.TabIndex = 0;
            // 
            // lblAciDurumu
            // 
            lblAciDurumu.Location = new Point(0, 0);
            lblAciDurumu.Name = "lblAciDurumu";
            lblAciDurumu.Size = new Size(100, 23);
            lblAciDurumu.TabIndex = 0;
            // 
            // lblAlcalma
            // 
            lblAlcalma.Location = new Point(0, 0);
            lblAlcalma.Name = "lblAlcalma";
            lblAlcalma.Size = new Size(100, 23);
            lblAlcalma.TabIndex = 0;
            // 
            // lblSuruklenmeParasutu
            // 
            lblSuruklenmeParasutu.Location = new Point(0, 0);
            lblSuruklenmeParasutu.Name = "lblSuruklenmeParasutu";
            lblSuruklenmeParasutu.Size = new Size(100, 23);
            lblSuruklenmeParasutu.TabIndex = 0;
            // 
            // lblBelirliIrtifa
            // 
            lblBelirliIrtifa.Location = new Point(0, 0);
            lblBelirliIrtifa.Name = "lblBelirliIrtifa";
            lblBelirliIrtifa.Size = new Size(100, 23);
            lblBelirliIrtifa.TabIndex = 0;
            // 
            // lblAnaParasut
            // 
            lblAnaParasut.Location = new Point(0, 0);
            lblAnaParasut.Name = "lblAnaParasut";
            lblAnaParasut.Size = new Size(100, 23);
            lblAnaParasut.TabIndex = 0;
            // 
            // lblGorevSuresi
            // 
            lblGorevSuresi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblGorevSuresi.ForeColor = Color.DeepSkyBlue;
            lblGorevSuresi.Location = new Point(1035, 11);
            lblGorevSuresi.Name = "lblGorevSuresi";
            lblGorevSuresi.Size = new Size(190, 25);
            lblGorevSuresi.TabIndex = 71;
            lblGorevSuresi.Text = "GÖREV SÜRESİ: 00:00:00";
            // 
            // lblMesafeYerUKB
            // 
            lblMesafeYerUKB.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMesafeYerUKB.ForeColor = Color.White;
            lblMesafeYerUKB.Location = new Point(15, 80);
            lblMesafeYerUKB.Name = "lblMesafeYerUKB";
            lblMesafeYerUKB.Size = new Size(220, 22);
            lblMesafeYerUKB.TabIndex = 0;
            lblMesafeYerUKB.Text = "YER İSTASYONU ↔ UKB: -- m";
            // 
            // lblMesafeYerPayload
            // 
            lblMesafeYerPayload.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMesafeYerPayload.ForeColor = Color.White;
            lblMesafeYerPayload.Location = new Point(15, 110);
            lblMesafeYerPayload.Name = "lblMesafeYerPayload";
            lblMesafeYerPayload.Size = new Size(260, 22);
            lblMesafeYerPayload.TabIndex = 1;
            lblMesafeYerPayload.Text = "YER İSTASYONU ↔ GÖREV YÜKÜ: -- m";
            // 
            // lblMesafeUKBPayload
            // 
            lblMesafeUKBPayload.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMesafeUKBPayload.ForeColor = Color.White;
            lblMesafeUKBPayload.Location = new Point(15, 140);
            lblMesafeUKBPayload.Name = "lblMesafeUKBPayload";
            lblMesafeUKBPayload.Size = new Size(240, 22);
            lblMesafeUKBPayload.TabIndex = 2;
            lblMesafeUKBPayload.Text = "UKB ↔ GÖREV YÜKÜ: -- m";
            // 
            // lblUKBKalite
            // 
            lblUKBKalite.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUKBKalite.ForeColor = Color.Gray;
            lblUKBKalite.Location = new Point(819, 10);
            lblUKBKalite.Name = "lblUKBKalite";
            lblUKBKalite.Size = new Size(180, 23);
            lblUKBKalite.TabIndex = 72;
            lblUKBKalite.Text = "UKB KALİTE: %0";
            // 
            // lblPayloadKalite
            // 
            lblPayloadKalite.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPayloadKalite.ForeColor = Color.Gray;
            lblPayloadKalite.Location = new Point(819, 35);
            lblPayloadKalite.Name = "lblPayloadKalite";
            lblPayloadKalite.Size = new Size(210, 23);
            lblPayloadKalite.TabIndex = 73;
            lblPayloadKalite.Text = "PAYLOAD KALİTE: %0";
            lblPayloadKalite.Click += lblPayloadKalite_Click;
            // 
            // panelMesafeBilgileri
            // 
            panelMesafeBilgileri.BackColor = Color.FromArgb(5, 12, 22);
            panelMesafeBilgileri.Controls.Add(lblMesafeYerUKB);
            panelMesafeBilgileri.Controls.Add(lblMesafeYerPayload);
            panelMesafeBilgileri.Controls.Add(lblMesafeUKBPayload);
            panelMesafeBilgileri.Controls.Add(button1);
            panelMesafeBilgileri.Controls.Add(lblYerEnlem);
            panelMesafeBilgileri.Controls.Add(txtYerEnlem);
            panelMesafeBilgileri.Controls.Add(lblYerBoylam);
            panelMesafeBilgileri.Controls.Add(txtYerBoylam);
            panelMesafeBilgileri.Controls.Add(btnYerKonumUygula);
            panelMesafeBilgileri.Controls.Add(lblMesafeBaslik);
            panelMesafeBilgileri.Location = new Point(647, 71);
            panelMesafeBilgileri.Name = "panelMesafeBilgileri";
            panelMesafeBilgileri.Size = new Size(300, 285);
            panelMesafeBilgileri.TabIndex = 71;
            // 
            // lblYerEnlem
            // 
            lblYerEnlem.AutoSize = true;
            lblYerEnlem.ForeColor = Color.White;
            lblYerEnlem.Location = new Point(15, 175);
            lblYerEnlem.Name = "lblYerEnlem";
            lblYerEnlem.Size = new Size(115, 15);
            lblYerEnlem.TabIndex = 42;
            lblYerEnlem.Text = "Yer İstasyonu Enlem:";
            // 
            // txtYerEnlem
            // 
            txtYerEnlem.Location = new Point(150, 172);
            txtYerEnlem.Name = "txtYerEnlem";
            txtYerEnlem.Size = new Size(130, 23);
            txtYerEnlem.TabIndex = 43;
            // 
            // lblYerBoylam
            // 
            lblYerBoylam.AutoSize = true;
            lblYerBoylam.ForeColor = Color.White;
            lblYerBoylam.Location = new Point(15, 205);
            lblYerBoylam.Name = "lblYerBoylam";
            lblYerBoylam.Size = new Size(122, 15);
            lblYerBoylam.TabIndex = 44;
            lblYerBoylam.Text = "Yer İstasyonu Boylam:";
            // 
            // txtYerBoylam
            // 
            txtYerBoylam.Location = new Point(150, 202);
            txtYerBoylam.Name = "txtYerBoylam";
            txtYerBoylam.Size = new Size(130, 23);
            txtYerBoylam.TabIndex = 45;
            // 
            // btnYerKonumUygula
            // 
            btnYerKonumUygula.Location = new Point(15, 235);
            btnYerKonumUygula.Name = "btnYerKonumUygula";
            btnYerKonumUygula.Size = new Size(265, 30);
            btnYerKonumUygula.TabIndex = 46;
            btnYerKonumUygula.Text = "YER İSTASYONU KONUMUNU UYGULA";
            btnYerKonumUygula.UseVisualStyleBackColor = true;
            btnYerKonumUygula.Click += btnYerKonumUygula_Click;
            // 
            // lblMesafeBaslik
            // 
            lblMesafeBaslik.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMesafeBaslik.ForeColor = Color.DeepSkyBlue;
            lblMesafeBaslik.Location = new Point(15, 10);
            lblMesafeBaslik.Name = "lblMesafeBaslik";
            lblMesafeBaslik.Size = new Size(250, 25);
            lblMesafeBaslik.TabIndex = 47;
            lblMesafeBaslik.Text = "MESAFE BİLGİLERİ";
            // 
            // UKBVERİLERİ
            // 
            UKBVERİLERİ.BackColor = Color.FromArgb(5, 12, 22);
            UKBVERİLERİ.Controls.Add(UKBGRAFIKLERI);
            UKBVERİLERİ.Controls.Add(panel7);
            UKBVERİLERİ.Controls.Add(panel6);
            UKBVERİLERİ.Controls.Add(panel5);
            UKBVERİLERİ.Location = new Point(291, 436);
            UKBVERİLERİ.Name = "UKBVERİLERİ";
            UKBVERİLERİ.Size = new Size(942, 241);
            UKBVERİLERİ.TabIndex = 74;
            // 
            // UKBGRAFIKLERI
            // 
            UKBGRAFIKLERI.ActiveLinkColor = Color.LightSeaGreen;
            UKBGRAFIKLERI.AutoSize = true;
            UKBGRAFIKLERI.BackColor = Color.Transparent;
            UKBGRAFIKLERI.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            UKBGRAFIKLERI.LinkColor = Color.DeepSkyBlue;
            UKBGRAFIKLERI.Location = new Point(16, 3);
            UKBGRAFIKLERI.Name = "UKBGRAFIKLERI";
            UKBGRAFIKLERI.Size = new Size(108, 17);
            UKBGRAFIKLERI.TabIndex = 76;
            UKBGRAFIKLERI.TabStop = true;
            UKBGRAFIKLERI.Text = "UKB GRAFİKLERİ";
            // 
            // BAGLANTIPANELI
            // 
            BAGLANTIPANELI.BackColor = Color.FromArgb(5, 12, 22);
            BAGLANTIPANELI.Controls.Add(label1);
            BAGLANTIPANELI.Controls.Add(GOKCE);
            BAGLANTIPANELI.Controls.Add(cmbPayloadPort);
            BAGLANTIPANELI.Controls.Add(cmbPayloadBaud);
            BAGLANTIPANELI.Controls.Add(cmbUKBBaud);
            BAGLANTIPANELI.Controls.Add(cmbUKBPort);
            BAGLANTIPANELI.Controls.Add(lblBaglanti);
            BAGLANTIPANELI.Controls.Add(btnUKBBaglan);
            BAGLANTIPANELI.Controls.Add(btnPayloadBaglan);
            BAGLANTIPANELI.Controls.Add(lblSonPayloadPaket);
            BAGLANTIPANELI.Controls.Add(lblPayloadBaglanti);
            BAGLANTIPANELI.Controls.Add(lblPayloadKalite);
            BAGLANTIPANELI.Controls.Add(lblGorevSuresi);
            BAGLANTIPANELI.Controls.Add(lblUKBKalite);
            BAGLANTIPANELI.Location = new Point(12, 3);
            BAGLANTIPANELI.Name = "BAGLANTIPANELI";
            BAGLANTIPANELI.Size = new Size(1554, 61);
            BAGLANTIPANELI.TabIndex = 77;
            BAGLANTIPANELI.Paint += BAGLANTIPANELI_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Vladimir Script", 9F);
            label1.ForeColor = SystemColors.Window;
            label1.Location = new Point(1345, 33);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 75;
            label1.Text = "Göğe Değmek Üzere";
            label1.Click += label1_Click_1;
            // 
            // GOKCE
            // 
            GOKCE.AutoSize = true;
            GOKCE.Font = new Font("STZhongsong", 20.2499962F, FontStyle.Bold, GraphicsUnit.Point, 162);
            GOKCE.ForeColor = Color.FromArgb(192, 0, 0);
            GOKCE.Location = new Point(1332, 7);
            GOKCE.Name = "GOKCE";
            GOKCE.Size = new Size(121, 31);
            GOKCE.TabIndex = 74;
            GOKCE.Text = "GÖKÇE";
            // 
            // gy_ibreleri
            // 
            gy_ibreleri.BackColor = Color.FromArgb(10, 25, 45);
            gy_ibreleri.Controls.Add(panelSicaklikGosterge);
            gy_ibreleri.Controls.Add(panelBasincGosterge);
            gy_ibreleri.Controls.Add(panelYogunlukGosterge);
            gy_ibreleri.Controls.Add(panelNemGosterge);
            gy_ibreleri.Location = new Point(782, 679);
            gy_ibreleri.Name = "gy_ibreleri";
            gy_ibreleri.Size = new Size(784, 183);
            gy_ibreleri.TabIndex = 91;
            // 
            // btnHaritaCache
            // 
            btnHaritaCache.Location = new Point(68, 8);
            btnHaritaCache.Name = "btnHaritaCache";
            btnHaritaCache.Size = new Size(167, 20);
            btnHaritaCache.TabIndex = 92;
            btnHaritaCache.Text = "HARİTAYI CACHE'LE";
            btnHaritaCache.UseVisualStyleBackColor = true;
            btnHaritaCache.Click += btnHaritaCache_Click;
            // 
            // btnHaritaCacheKlasoruAc
            // 
            btnHaritaCacheKlasoruAc.Location = new Point(68, 39);
            btnHaritaCacheKlasoruAc.Name = "btnHaritaCacheKlasoruAc";
            btnHaritaCacheKlasoruAc.Size = new Size(166, 20);
            btnHaritaCacheKlasoruAc.TabIndex = 93;
            btnHaritaCacheKlasoruAc.Text = "CACHE KLASÖRÜNÜ AÇ";
            btnHaritaCacheKlasoruAc.UseVisualStyleBackColor = true;
            btnHaritaCacheKlasoruAc.Click += btnHaritaCacheKlasoruAc_Click;
            // 
            // CachePanel
            // 
            CachePanel.BackColor = Color.FromArgb(10, 25, 45);
            CachePanel.Controls.Add(btnHaritaCache);
            CachePanel.Controls.Add(btnHaritaCacheKlasoruAc);
            CachePanel.Location = new Point(647, 362);
            CachePanel.Name = "CachePanel";
            CachePanel.Size = new Size(300, 68);
            CachePanel.TabIndex = 94;
            // 
            // pictureBoxTakim
            // 
            pictureBoxTakim.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxTakim.Image = (Image)resources.GetObject("pictureBoxTakim.Image");
            pictureBoxTakim.Location = new Point(1239, 506);
            pictureBoxTakim.Name = "pictureBoxTakim";
            pictureBoxTakim.Size = new Size(327, 171);
            pictureBoxTakim.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxTakim.TabIndex = 95;
            pictureBoxTakim.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGray;
            ClientSize = new Size(1584, 861);
            Controls.Add(pictureBoxTakim);
            Controls.Add(CachePanel);
            Controls.Add(gy_ibreleri);
            Controls.Add(BAGLANTIBILGILERI);
            Controls.Add(BAGLANTIPANELI);
            Controls.Add(UKBVERİLERİ);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panelUcusDurumu);
            Controls.Add(panelMesafeBilgileri);
            Controls.Add(panelPayloadGrafik);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "AZAK ROKET DGM TAKIMI YER İSTASYONU";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            BAGLANTIBILGILERI.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panelUcusDurumu.ResumeLayout(false);
            panelMesafeBilgileri.ResumeLayout(false);
            panelMesafeBilgileri.PerformLayout();
            UKBVERİLERİ.ResumeLayout(false);
            UKBVERİLERİ.PerformLayout();
            BAGLANTIPANELI.ResumeLayout(false);
            BAGLANTIPANELI.PerformLayout();
            gy_ibreleri.ResumeLayout(false);
            CachePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxTakim).EndInit();
            ResumeLayout(false);
        }


        private void lblYukselis_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label18;
        private TextBox PGpsIrtifa;
        private TextBox GyroX;
        private TextBox PIvmeZ;
        private TextBox PIvmeY;
        private TextBox PIvmeX;
        private TextBox PBoylam;
        private TextBox PEnlem;
        private TextBox irtifa;
        private TextBox RGpsIrtifa;
        private TextBox REnlem;
        private TextBox RBoylam;
        private TextBox State;
        private TextBox Aci;
        private TextBox GyroZ;
        private TextBox GyroY;
        private ComboBox cmbUKBPort;
        private ComboBox cmbPayloadPort;
        private ComboBox cmbUKBBaud;
        private ComboBox cmbPayloadBaud;
        private Panel panel1;
        private Panel panel2;
        private Button button1;
        private Panel panel3;
        private Panel panel4;
        private LinkLabel linkLabel2;
        private Label basinc;
        private Label sicaklik;
        private LinkLabel linkLabel1;
        private TextBox PYogunluk;
        private TextBox PNem;
        private TextBox PSicaklik;
        private TextBox PBasinc;
        private Label pcrc;
        private Label yogunluk;
        private Panel panel5;
        private Panel panel6;
        private Panel panel7;
        private Button btnKayit;
        private Label lblBaglanti;
        private Button btnUKBBaglan;
        private Button btnPayloadBaglan;
        private Label lblPayloadBaglanti;
        private Label lblSonPayloadPaket;
        private Label label19;
        private TextBox Ppktsayaci;
        private Panel panelSicaklikGosterge;
        private Panel panelBasincGosterge;
        private Panel panelNemGosterge;
        private Panel panelYogunlukGosterge;
        private Panel panelUcusDurumu;
        private Label lblKalkis;
        private Label lblBurnOut;
        private Label lblIrtifaEsigi;
        private Label lblAciDurumu;
        private Label lblAlcalma;
        private Label lblSuruklenmeParasutu;
        private Label lblBelirliIrtifa;
        private Label lblAnaParasut;
        private Label lblUcusDurumuBaslik;
        private Label lblMevcutDurum;
        private Label lblGorevSuresi;
        private Label lblUKBKalite;
        private Label lblPayloadKalite;
        private Label lblMesafeYerUKB;
        private Label lblMesafeYerPayload;
        private Label lblMesafeUKBPayload;
        private Panel panelMesafeBilgileri;
        private Label lblMesafeBaslik;
        private Panel UKBVERİLERİ;
        private LinkLabel UKBGRAFIKLERI;
        private LinkLabel linkLabel3;
        private Panel BAGLANTIBILGILERI;
        private Panel BAGLANTIPANELI;
        private TextBox txtYerEnlem;
        private TextBox txtYerBoylam;
        private Button btnYerKonumUygula;
        private Label lblYerEnlem;
        private Label lblYerBoylam;
        private Label lblHaritaLegend;
        private Panel panelPayloadGrafik;
        private Label GOKCE;
        private Label label1;
        private Panel gy_ibreleri;
        private Label lblKayitDosyasi;
        private Button btnKayitKlasoruAc;
        private Button btnHaritaCache;
        private Button btnHaritaCacheKlasoruAc;
        private Panel CachePanel;
        private PictureBox pictureBoxTakim;
    }
}
