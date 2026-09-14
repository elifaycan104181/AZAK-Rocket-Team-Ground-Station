namespace WinFormGiris;

using ClosedXML.Excel;
// --- Gerekli NuGet paketleri --- //
//Haritalandırma kısmı için:
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
//3D modelleme ve IMU verileri için:
using HelixToolkit.Geometry;
using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
// STL dosyası yüklemek için:
using System.IO;
// Seri port iletişimi için:
using System.IO.Ports;
using System.Numerics;
// Diğer WinForms bileşenleri için:
using System.Windows.Forms;
// Grafik çizimi için:
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;
using System.Windows.Media.Media3D;
using System.Diagnostics;
using System.Threading.Tasks;


public partial class Form1 : Form
{
    // Seri port nesnesi
    // SerialPort1: UKB'den veri almak için
    SerialPort serialPort1 = new SerialPort();
    // SerialPort2: Görev yükünden veri almak için
    SerialPort serialPort2 = new SerialPort();
    //Harita Kontrolü
    GMapControl harita;
    GMapOverlay markerOverlay;
    GMapOverlay routeOverlay;

    GMapMarker yerIstasyonuMarker;
    GMapMarker ukbMarker;
    GMapMarker gorevYukuMarker;

    GMapRoute yerUKBRoute;
    GMapRoute yerPayloadRoute;
    GMapRoute ukbPayloadRoute;

    List<PointLatLng> ukbRotaNoktalari = new List<PointLatLng>();

    // Koordinat Değişkenleri
    PointLatLng yerIstasyonuKonum;
    PointLatLng ukbKonum;
    PointLatLng gorevYukuKonum;

    // UKB için manuel girilecek koordinatlar
    private double manuelUkbEnlem = 40.845783;
    private double manuelUkbBoylam = 31.116659;


    //fake veri üretmek için sayaç ve değişkenler
    int fakeIndex = 0;
    int fakeMax = 50;

    // 3D ROKET MODELİ İÇİN
    //private readonly ModelVisual3D roketModelVisual;
    // STL dosyasından yüklenen roket modeli için
    private ModelVisual3D roketModelVisual;

    AxisAngleRotation3D rotX;
    AxisAngleRotation3D rotY;
    AxisAngleRotation3D rotZ;

    // 3D model açılarını yumuşatmak için
    double modelRoll = 0;
    double modelPitch = 0;
    double modelYaw = 0;

    bool modelAciBaslatildi = false;

    // Küçük değer = daha yumuşak, daha yavaş
    // Büyük değer = daha hızlı tepki
    const double modelAciAlpha = 0.20;
    // IMU verilerini simüle etmek için sayaç ve değişkenler
    Chart chartIvme;
    Chart chartGyro;
    Chart chartAlt;
    Chart chartPayloadCevresel;
    int payloadGrafikZaman = 0;

    RadialGaugeControl sicaklikGosterge;
    RadialGaugeControl basincGosterge;
    RadialGaugeControl nemGosterge;
    RadialGaugeControl yogunlukGosterge;

    // Son veri alma zamanını takip etmek için
    DateTime sonVeriZamani = DateTime.MinValue;
    Timer baglantiTimer = new Timer();
    DateTime sonPayloadVeriZamani = DateTime.MinValue;

    Timer payloadPaketTimer = new Timer();
    Timer gorevSuresiTimer = new Timer();
    Timer excelKayitTimer = new Timer();

    DateTime gorevBaslangicZamani = DateTime.MinValue;

    bool gorevBasladi = false;

    // Bağlantı kalitesi hesaplama
    int ukbPaketSayaciKalite = 0;
    int payloadPaketSayaciKalite = 0;

    double ukbBaglantiKalitesi = 0;
    double payloadBaglantiKalitesi = 0;

    Timer baglantiKaliteTimer = new Timer();

    // Veri üretme simülasyonu için
    int zaman = 0;
    // Rastgele ivme verileri üretmek için başlangıç değerleri ve yönler
    double ax = 9.81;
    double ay = 0;
    double az = 0;
    // İvme değerlerinin artış yönlerini kontrol etmek için
    bool axYukari = true;
    bool ayYukari = true;
    bool azYukari = true;
    // LED göstergesi için
    Timer timer = new Timer();
    bool ledState = false;
    Timer ledTimer = new Timer();
    // Telemetri verilerini kaydetmek için
    string kayitDosyaYolu;
    bool kayitAktif = false;
    XLWorkbook excelDosyasi;

    IXLWorksheet ukbSayfasi;
    IXLWorksheet payloadSayfasi;

    int ukbExcelSatir = 2;
    int payloadExcelSatir = 2;

    readonly object excelKilidi =
        new object();

    // Form kapanırken seri portları kapatmak için
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (serialPort1 != null && serialPort1.IsOpen)
            serialPort1.Close();

        if (serialPort2 != null && serialPort2.IsOpen)
            serialPort2.Close();
        if (kayitAktif &&
    excelDosyasi != null)
        {
            try
            {
                lock (excelKilidi)
                {
                    excelDosyasi.SaveAs(
                        kayitDosyaYolu);

                    excelDosyasi.Dispose();

                    excelDosyasi = null;
                }
            }
            catch
            {
            }
        }
        base.OnFormClosing(e);
    }
    public Form1()
    {
        // Formun temel bileşenlerini başlat
        InitializeComponent();

        HaritaCacheYolunuAyarla();

        UcusDurumPaneliniHazirla();

        // Termometre kontrolünü oluştur ve panel içine ekle
        sicaklikGosterge =
    new RadialGaugeControl();

        sicaklikGosterge.Dock =
            DockStyle.None;

        sicaklikGosterge.Size =
            new Size(
                panelSicaklikGosterge.ClientSize.Width - 8,
                panelSicaklikGosterge.ClientSize.Height - 8);

        sicaklikGosterge.Location =
            new Point(4, 4);

        sicaklikGosterge.Minimum = -20;
        sicaklikGosterge.Maximum = 60;

        sicaklikGosterge.Baslik =
            "SICAKLIK";

        sicaklikGosterge.Birim =
            "°C";

        sicaklikGosterge.OndalikBasamak =
            1;

        sicaklikGosterge.Deger =
            20;

        panelSicaklikGosterge.Controls.Add(
            sicaklikGosterge);

        // Manometre kontrolünü oluştur ve panel içine ekle
        basincGosterge =
     new RadialGaugeControl();

        basincGosterge.Dock =
            DockStyle.None;

        basincGosterge.Size =
            new Size(
                panelBasincGosterge.ClientSize.Width - 8,
                panelBasincGosterge.ClientSize.Height - 8);

        basincGosterge.Location =
            new Point(4, 4);

        basincGosterge.Minimum =
            300;

        basincGosterge.Maximum =
            1100;

        basincGosterge.Baslik =
            "BASINÇ";

        basincGosterge.Birim =
            "hPa";

        basincGosterge.OndalikBasamak =
            0;

        basincGosterge.Deger =
            1013;

        panelBasincGosterge.Controls.Add(
            basincGosterge);

        nemGosterge =
    new RadialGaugeControl();

        nemGosterge.Dock =
            DockStyle.None;

        nemGosterge.Size =
            new Size(
                panelNemGosterge.ClientSize.Width - 8,
                panelNemGosterge.ClientSize.Height - 8);

        nemGosterge.Location =
            new Point(4, 4);

        nemGosterge.Minimum =
            0;

        nemGosterge.Maximum =
            100;

        nemGosterge.Baslik =
            "NEM";

        nemGosterge.Birim =
            "%";

        nemGosterge.OndalikBasamak =
            1;

        nemGosterge.Deger =
            50;

        panelNemGosterge.Controls.Add(
            nemGosterge);


        yogunlukGosterge =
    new RadialGaugeControl();

        yogunlukGosterge.Dock =
            DockStyle.None;

        yogunlukGosterge.Size =
            new Size(
                panelYogunlukGosterge.ClientSize.Width - 8,
                panelYogunlukGosterge.ClientSize.Height - 8);

        yogunlukGosterge.Location =
            new Point(4, 4);

        yogunlukGosterge.Minimum =
            0.8;

        yogunlukGosterge.Maximum =
            1.4;

        yogunlukGosterge.Baslik =
            "YOĞUNLUK";

        yogunlukGosterge.Birim =
            "kg/m³";

        yogunlukGosterge.OndalikBasamak =
            2;

        yogunlukGosterge.Deger =
            1.225;

        panelYogunlukGosterge.Controls.Add(
            yogunlukGosterge);

        // Seri portları listele
        cmbUKBPort.Items.Clear();
        cmbUKBPort.Items.AddRange(SerialPort.GetPortNames());

        cmbPayloadPort.Items.Clear();
        cmbPayloadPort.Items.AddRange(SerialPort.GetPortNames());
        cmbUKBPort.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbPayloadPort.DropDownStyle = ComboBoxStyle.DropDownList;
        // Seri portları varsayılan olarak seç
        cmbUKBBaud.SelectedItem = "115200";
        cmbPayloadBaud.SelectedItem = "115200";
        // Bağlantı kontrol timer'ını başlat
        baglantiTimer.Interval = 1000;
        baglantiTimer.Tick += BaglantiTimer_Tick;
        baglantiTimer.Start();


        // Payload paket timer
        payloadPaketTimer.Interval = 200;

        payloadPaketTimer.Tick +=
            PayloadPaketTimer_Tick;

        payloadPaketTimer.Start();
        // Görev süresi timer
        gorevSuresiTimer.Interval = 200;

        gorevSuresiTimer.Tick +=
            GorevSuresiTimer_Tick;

        gorevSuresiTimer.Start();

        baglantiKaliteTimer.Interval = 5000;

        baglantiKaliteTimer.Tick +=
            BaglantiKaliteTimer_Tick;

        baglantiKaliteTimer.Start();

        lblBaglanti.Text = "● UKB BAĞLI DEĞİL";
        lblBaglanti.ForeColor = Color.Red;

        excelKayitTimer.Interval = 2000;

        excelKayitTimer.Tick +=
            ExcelKayitTimer_Tick;

        excelKayitTimer.Start();

        // Grafik oluşturma fonksiyonunu çağır
        GrafikKur();
        GrafikKurGyro();
        GrafikKurAlt();
        GrafikKurPayloadCevresel();
        


        // HARİTA
        harita = new GMapControl();

        harita.Dock = DockStyle.Fill;

        // Şimdilik geliştirme sırasında Google harita
        harita.MapProvider = GMapProviders.GoogleMap;
        // Şimdilik internetten çalıştır
        //GMaps.Instance.Mode = AccessMode.ServerOnly;
        // Şimdilik internetten çalıştır ve önbelleğe al,
        GMaps.Instance.Mode = AccessMode.ServerAndCache;
        // Zoom ayarları
        harita.MinZoom = 2;
        harita.MaxZoom = 20;
        harita.Zoom = 15;

        // Fare ile haritayı hareket ettirme
        harita.CanDragMap = true;
        harita.DragButton = MouseButtons.Left;

        // Marker'a tıklamayı açık tut
        harita.MarkersEnabled = true;
        harita.RoutesEnabled = true;

        // Başlangıçta Türkiye üzerinde test konumu
        // Gerçek konum geldiğinde markerlar zaten güncellenecek.
        harita.Position = new PointLatLng(
            39.0,
            35.0);

        // Haritayı SADECE BİR KEZ panele ekle
        panel2.Controls.Add(harita);

        // Legend haritanın üstünde kalsın
        lblHaritaLegend.BringToFront();

        // Marker katmanı
        markerOverlay = new GMapOverlay("markerlar");
        harita.Overlays.Add(markerOverlay);

        // Markerlar gerçek koordinat geldikten sonra oluşturulacak
        yerIstasyonuMarker = null;
        ukbMarker = null;
        gorevYukuMarker = null;
        // Mesafe / bağlantı çizgileri katmanı
        routeOverlay = new GMapOverlay("baglanti_cizgileri");

        harita.Overlays.Add(routeOverlay);

        // IMU VERİLERİ İLE ROKET MODELİ
        ElementHost host = new ElementHost();
        host.Dock = DockStyle.Fill;

        HelixViewport3D viewport = new HelixViewport3D();
        viewport.Background = System.Windows.Media.Brushes.DimGray;

        viewport.Camera = new PerspectiveCamera
        {
            Position = new System.Windows.Media.Media3D.Point3D(0, -20, 10),
            LookDirection = new Vector3D(0, 20, -10),
            UpDirection = new Vector3D(0, 0, 1),
            FieldOfView = 45
        };

        viewport.Children.Add(new DefaultLights());

        // STL ROKET MODELİ YÜKLEME
        string stlYolu = Path.Combine(Application.StartupPath, "Models", "roket.STL");

        ModelImporter importer = new ModelImporter();
        Model3D stlModel = importer.Load(stlYolu);
        Rect3D bounds = stlModel.Bounds;

        double centerX = bounds.X + bounds.SizeX / 2;
        double centerY = bounds.Y + bounds.SizeY / 2;
        double centerZ = bounds.Z + bounds.SizeZ / 2;

        // Malzeme / renk verme
        Material roketMalzeme = MaterialHelper.CreateMaterial(System.Windows.Media.Brushes.SkyBlue);

        if (stlModel is Model3DGroup grup)
        {
            int index = 0;

            foreach (var child in grup.Children)
            {
                if (child is GeometryModel3D geo)
                {
                    if (index % 3 == 0)
                    {
                        geo.Material = MaterialHelper.CreateMaterial(System.Windows.Media.Brushes.SkyBlue); // gövde
                    }
                    else if (index % 3 == 1)
                    {
                        geo.Material = MaterialHelper.CreateMaterial(System.Windows.Media.Brushes.DarkGray); // kanatlar
                    }
                    else
                    {
                        geo.Material = MaterialHelper.CreateMaterial(System.Windows.Media.Brushes.White); // burun
                    }

                    geo.BackMaterial = geo.Material;
                    index++;
                }
            }
        }

        // DÖNÜŞLER
        rotX = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 180); //  aşağı baksın
        rotY = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);
        rotZ = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);

        Transform3DGroup donusumGrubu = new Transform3DGroup();

        // MERKEZE ÇEK
        donusumGrubu.Children.Add(
            new TranslateTransform3D(-centerX, -centerY, -centerZ)
        );

        // ÖLÇEK
        donusumGrubu.Children.Add(
            new ScaleTransform3D(0.01, 0.01, 0.01)
        );

        // DÖNÜŞ
        donusumGrubu.Children.Add(new RotateTransform3D(rotX));
        donusumGrubu.Children.Add(new RotateTransform3D(rotY));
        donusumGrubu.Children.Add(new RotateTransform3D(rotZ));

        roketModelVisual = new ModelVisual3D();
        roketModelVisual.Content = stlModel;
        roketModelVisual.Transform = donusumGrubu;

        viewport.Children.Add(roketModelVisual);

        // BİRAZ BEKLEYİP ZOOM YAP
        this.Load += (s, e) =>
        {
            viewport.ZoomExtents();
        };
        // Modeli ekrana sığdır
        //viewport.ZoomExtents();
        // WPF 3D Model için
        /*roket.Transform = donusumGrubu;

        viewport.Children.Add(roket);*/

        host.Child = viewport;
        panel3.Controls.Add(host);
        ledTimer.Interval = 300;
        ledTimer.Tick += (s, e) =>
        {
            ledState = !ledState;

            var renk = ledState
                ? System.Windows.Media.Brushes.DarkBlue
                : System.Windows.Media.Brushes.Yellow;

            foreach (var child in ((Model3DGroup)roketModelVisual.Content).Children)
            {
                if (child is GeometryModel3D geo)
                {
                    geo.Material = MaterialHelper.CreateMaterial(renk);
                    geo.BackMaterial = geo.Material;
                }
            }
        };

        ledTimer.Start();

    }
    // Payload verilerini kaydetmek için fonksiyon
    private void PayloadVeriKaydet(
    ushort paketNo,
    double gpsIrtifa,
    double enlem,
    double boylam,
    double basinc,
    double sicaklik,
    double nem,
    double yogunluk)
    {
        if (!kayitAktif ||
            excelDosyasi == null ||
            payloadSayfasi == null)
        {
            return;
        }

        lock (excelKilidi)
        {
            DateTime simdi =
                DateTime.Now;

            payloadSayfasi.Cell(
                payloadExcelSatir,
                1).Value =
                simdi.Date;

            payloadSayfasi.Cell(
                payloadExcelSatir,
                2).Value =
                simdi;

            payloadSayfasi.Cell(
                payloadExcelSatir,
                3).Value =
                paketNo;

            payloadSayfasi.Cell(
                payloadExcelSatir,
                4).Value =
                gpsIrtifa;

            payloadSayfasi.Cell(
                payloadExcelSatir,
                5).Value =
                enlem;

            payloadSayfasi.Cell(
                payloadExcelSatir,
                6).Value =
                boylam;

            payloadSayfasi.Cell(
                payloadExcelSatir,
                7).Value =
                basinc;

            payloadSayfasi.Cell(
                payloadExcelSatir,
                8).Value =
                sicaklik;

            payloadSayfasi.Cell(
                payloadExcelSatir,
                9).Value =
                nem;

            payloadSayfasi.Cell(
                payloadExcelSatir,
                10).Value =
                yogunluk;

            payloadSayfasi.Cell(
                payloadExcelSatir,
                1).Style.DateFormat.Format =
                "dd.MM.yyyy";

            payloadSayfasi.Cell(
                payloadExcelSatir,
                2).Style.DateFormat.Format =
                "HH:mm:ss.fff";

            payloadExcelSatir++;
        }
    }
    // Telemetri verilerini kaydetmek için fonksiyon
    private void VeriKaydet(
    double ax,
    double ay,
    double az,
    double gyroX,
    double gyroY,
    double gyroZ,
    double roll,
    double pitch,
    double yaw,
    double enlem,
    double boylam,
    double gpsIrtifa,
    double msirtifa,
    int durum)
    {
        if (!kayitAktif ||
            excelDosyasi == null ||
            ukbSayfasi == null)
        {
            return;
        }

        lock (excelKilidi)
        {
            DateTime simdi =
                DateTime.Now;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                1).Value =
                simdi.Date;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                2).Value =
                simdi;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                3).Value = ax;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                4).Value = ay;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                5).Value = az;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                6).Value = gyroX;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                7).Value = gyroY;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                8).Value = gyroZ;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                9).Value = roll;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                10).Value = pitch;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                11).Value = yaw;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                12).Value = enlem;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                13).Value = boylam;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                14).Value = gpsIrtifa;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                15).Value = msirtifa;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                16).Value = durum;

            ukbSayfasi.Cell(
                ukbExcelSatir,
                1).Style.DateFormat.Format =
                "dd.MM.yyyy";

            ukbSayfasi.Cell(
                ukbExcelSatir,
                2).Style.DateFormat.Format =
                "HH:mm:ss.fff";

            ukbExcelSatir++;
        }
    }
    // Excel dosyasını otomatik kaydetmek için timer tick fonksiyonu
    private void ExcelKayitTimer_Tick(
    object sender,
    EventArgs e)
    {
        if (!kayitAktif)
            return;

        if (excelDosyasi == null)
            return;

        if (string.IsNullOrWhiteSpace(
            kayitDosyaYolu))
        {
            return;
        }

        try
        {
            lock (excelKilidi)
            {
                excelDosyasi.SaveAs(
                    kayitDosyaYolu);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                "Excel otomatik kayıt hatası: " +
                ex.Message);
        }
    }
    // İvme grafiği kurmak için fonksiyon
    private void GrafikKur()
    {
        chartIvme = new Chart();
        chartIvme.Dock = DockStyle.Fill;
        chartIvme.BackColor = Color.FromArgb(5, 12, 22);

        ChartArea alan = new ChartArea();
        alan.CursorX.IsUserEnabled = true;
        alan.CursorX.IsUserSelectionEnabled = true;

        alan.CursorY.IsUserEnabled = true;
        alan.CursorY.IsUserSelectionEnabled = true;
        alan.AxisX.ScaleView.Zoomable = true;
        alan.AxisY.ScaleView.Zoomable = true;
        alan.AxisX.ScrollBar.Enabled = true;
        alan.AxisY.ScrollBar.Enabled = true;
        alan.AxisX.Title = "Time";
        alan.AxisY.Title = "Acceleration (m/s²)";
        alan.BackColor = Color.FromArgb(10, 25, 45);
        alan.AxisX.LabelStyle.ForeColor = Color.White;
        alan.AxisY.LabelStyle.ForeColor = Color.White;

        alan.AxisX.TitleForeColor = Color.White;
        alan.AxisY.TitleForeColor = Color.White;

        alan.AxisX.MajorGrid.LineColor = Color.FromArgb(35, 55, 75);
        alan.AxisY.MajorGrid.LineColor = Color.FromArgb(35, 55, 75);

        chartIvme.ChartAreas.Add(alan);

        // Grafik başlığı ve legend
        Title baslik = new Title();
        baslik.Text = "Acceleration Chart";
        baslik.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        baslik.ForeColor = Color.White;
        chartIvme.Titles.Add(baslik);

        Legend legend = new Legend();
        legend.Docking = Docking.Top;
        legend.BackColor = Color.FromArgb(10, 25, 45);
        legend.ForeColor = Color.White;

        chartIvme.Legends.Add(legend);

        Series sx = new Series("Ax");
        sx.ChartType = SeriesChartType.Line;
        sx.Color = Color.Red;

        Series sy = new Series("Ay");
        sy.ChartType = SeriesChartType.Line;
        sy.Color = Color.Cyan;

        Series sz = new Series("Az");
        sz.ChartType = SeriesChartType.Line;
        sz.Color = Color.Lime;

        chartIvme.Series.Add(sx);
        chartIvme.Series.Add(sy);
        chartIvme.Series.Add(sz);

        panel5.Controls.Add(chartIvme);
        chartIvme.MouseWheel += chart_MouseWheel;
        chartIvme.Focus();
        chartIvme.MouseEnter += (s, e) => chartIvme.Focus();
    }
    private void UcusDurumPaneliniHazirla()
    {
        Label[] ucusEtiketleri =
        {
        lblKalkis,
        lblBurnOut,
        lblIrtifaEsigi,
        lblAciDurumu,
        lblAlcalma,
        lblSuruklenmeParasutu,
        lblBelirliIrtifa,
        lblAnaParasut
    };

        string[] ucusMetinleri =
        {
        "KALKIŞ",
        "BURN OUT",
        "İRTİFA EŞİĞİ",
        "AÇI",
        "ALÇALMA",
        "SÜRÜKLENME PARAŞÜTÜ",
        "BELİRLİ İRTİFA",
        "ANA PARAŞÜT"
    };

        for (int i = 0; i < ucusEtiketleri.Length; i++)
        {
            ucusEtiketleri[i].AutoSize = false;
            ucusEtiketleri[i].Size = new Size(300, 28);

            ucusEtiketleri[i].Location =
                new Point(20, 45 + i * 32);

            ucusEtiketleri[i].Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold);

            ucusEtiketleri[i].ForeColor =
                Color.Gray;

            ucusEtiketleri[i].Text =
                "○ " + ucusMetinleri[i];

            panelUcusDurumu.Controls.Add(
                ucusEtiketleri[i]);
        }
    }
    // Fare ile zoom yapma fonksiyonu
    private void chart_MouseWheel(object sender, MouseEventArgs e)
    {
        try
        {
            var chart = sender as Chart;
            if (chart == null) return;

            var area = chart.ChartAreas[0];

            if (e.Delta < 0) // zoom out
            {
                area.AxisX.ScaleView.ZoomReset();
                area.AxisY.ScaleView.ZoomReset();
            }
            else if (e.Delta > 0) // zoom in
            {
                double xMin = area.AxisX.ScaleView.ViewMinimum;
                double xMax = area.AxisX.ScaleView.ViewMaximum;

                double yMin = area.AxisY.ScaleView.ViewMinimum;
                double yMax = area.AxisY.ScaleView.ViewMaximum;

                double posXStart = area.AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                double posXFinish = area.AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;

                double posYStart = area.AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                double posYFinish = area.AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                area.AxisX.ScaleView.Zoom(posXStart, posXFinish);
                area.AxisY.ScaleView.Zoom(posYStart, posYFinish);
            }
        }
        catch { }
    }
    // Görev süresini güncellemek için timer tick fonksiyonu görev yükü
    private void GorevSuresiTimer_Tick(
    object sender,
    EventArgs e)
    {
        if (!gorevBasladi)
        {
            lblGorevSuresi.Text =
                "GÖREV SÜRESİ: 00:00:00";

            return;
        }

        TimeSpan gecenSure =
            DateTime.Now -
            gorevBaslangicZamani;

        lblGorevSuresi.Text =
            "GÖREV SÜRESİ: " +
            gecenSure.ToString(@"hh\:mm\:ss");
    }
    // Bağlantı kalitesini güncellemek için timer tick fonksiyonu
    private void BaglantiKaliteTimer_Tick(
    object sender,
    EventArgs e)
    {
        const int beklenenPaket = 25;

        ukbBaglantiKalitesi =
            Math.Min(
                100,
                (ukbPaketSayaciKalite /
                (double)beklenenPaket) * 100);

        payloadBaglantiKalitesi =
            Math.Min(
                100,
                (payloadPaketSayaciKalite /
                (double)beklenenPaket) * 100);

        lblUKBKalite.Text =
            "UKB KALİTE: %" +
            ukbBaglantiKalitesi.ToString("0");

        lblPayloadKalite.Text =
            "PAYLOAD KALİTE: %" +
            payloadBaglantiKalitesi.ToString("0");

        BaglantiKaliteRenkGuncelle(
            lblUKBKalite,
            ukbBaglantiKalitesi);

        BaglantiKaliteRenkGuncelle(
            lblPayloadKalite,
            payloadBaglantiKalitesi);

        // Yeni 5 saniyelik pencere
        ukbPaketSayaciKalite = 0;
        payloadPaketSayaciKalite = 0;
    }
    // Bağlantı kalitesine göre label rengini güncellemek için fonksiyon
    private void BaglantiKaliteRenkGuncelle(
    Label label,
    double kalite)
    {
        if (kalite >= 90)
        {
            label.ForeColor = Color.Lime;
        }
        else if (kalite >= 70)
        {
            label.ForeColor = Color.YellowGreen;
        }
        else if (kalite >= 50)
        {
            label.ForeColor = Color.Orange;
        }
        else
        {
            label.ForeColor = Color.Red;
        }
    }
    // UKB bağlantı durumunu kontrol etmek için timer tick fonksiyonu
    private void BaglantiTimer_Tick(object sender, EventArgs e)
    {
        if (!serialPort1.IsOpen)
        {
            lblBaglanti.Text = "● UKB BAĞLI DEĞİL";
            lblBaglanti.ForeColor = Color.Red;
            return;
        }

        double gecenSure = (DateTime.Now - sonVeriZamani).TotalSeconds;

        if (sonVeriZamani == DateTime.MinValue)
        {
            lblBaglanti.Text = "● UKB BEKLENİYOR";
            lblBaglanti.ForeColor = Color.Orange;
        }
        else if (gecenSure > 3)
        {
            lblBaglanti.Text = "● UKB VERİ YOK";
            lblBaglanti.ForeColor = Color.OrangeRed;
        }
        else
        {
            lblBaglanti.Text = "● UKB AKTİF";
            lblBaglanti.ForeColor = Color.Lime;
        }
    }

    private void PayloadPaketTimer_Tick(object sender, EventArgs e)
    {
        if (!serialPort2.IsOpen)
        {
            lblSonPayloadPaket.Text = "SON PAKET: BAĞLANTI YOK";
            lblSonPayloadPaket.ForeColor = Color.Red;
            return;
        }

        if (sonPayloadVeriZamani == DateTime.MinValue)
        {
            lblSonPayloadPaket.Text = "SON PAKET: BEKLENİYOR";
            lblSonPayloadPaket.ForeColor = Color.Orange;
            return;
        }

        double gecenSure =
            (DateTime.Now - sonPayloadVeriZamani).TotalSeconds;

        if (gecenSure < 0.5)
        {
            lblSonPayloadPaket.Text =
                "SON PAKET: " +
                gecenSure.ToString("0.00") +
                " sn önce";

            lblSonPayloadPaket.ForeColor = Color.Lime;
        }
        else if (gecenSure < 1.0)
        {
            lblSonPayloadPaket.Text =
                "SON PAKET: " +
                gecenSure.ToString("0.00") +
                " sn önce";

            lblSonPayloadPaket.ForeColor = Color.Orange;
        }
        else
        {
            lblSonPayloadPaket.Text =
                "SON PAKET: " +
                gecenSure.ToString("0.00") +
                " sn önce";

            lblSonPayloadPaket.ForeColor = Color.Red;
        }
    }
    // IMU verilerini filtrelemek için basit bir model
    private double ModelAciFiltrele(
    double yeniDeger,
    double eskiDeger,
    double alpha)
    {
        return
            eskiDeger +
            alpha * (yeniDeger - eskiDeger);
    }

    // Roketin 3D modelini IMU verilerine göre döndürmek için fonksiyon
    private void RoketiDondur(
    double roll,
    double pitch,
    double yaw)
    {
        if (rotX == null ||
            rotY == null ||
            rotZ == null)
        {
            return;
        }

        rotX.Angle = 0;

        rotY.Angle = 270-pitch;

        rotZ.Angle = 0;
    }

    // İki koordinat arasındaki mesafeyi hesaplamak için Haversine formülü
    private static double MesafeHesapla(double lat1, double lon1, double lat2, double lon2)
    {
        double R = 6371000; // metre

        double dLat = (lat2 - lat1) * Math.PI / 180.0;
        double dLon = (lon2 - lon1) * Math.PI / 180.0;

        double a =
            Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(lat1 * Math.PI / 180.0) * Math.Cos(lat2 * Math.PI / 180.0) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return R * c;
    }
    // Koordinatların geçerli olup olmadığını kontrol etmek için fonksiyon
    private bool KoordinatGecerli(PointLatLng konum)
    {
        return
            !double.IsNaN(konum.Lat) &&
            !double.IsNaN(konum.Lng) &&
            konum.Lat >= -90 &&
            konum.Lat <= 90 &&
            konum.Lng >= -180 &&
            konum.Lng <= 180 &&
            !(konum.Lat == 0 && konum.Lng == 0);
    }
    // Harita üzerindeki bağlantı çizgilerini güncellemek için fonksiyon
    private void HaritaBaglantilariniGuncelle()
    {
        if (routeOverlay == null)
            return;

        routeOverlay.Routes.Clear();

        // 1) Yer İstasyonu -> UKB
        if (KoordinatGecerli(yerIstasyonuKonum) &&
            KoordinatGecerli(ukbKonum))
        {
            List<PointLatLng> noktalar =
                new List<PointLatLng>
                {
                yerIstasyonuKonum,
                ukbKonum
                };

            yerUKBRoute =
                new GMapRoute(
                    noktalar,
                    "YerIstasyonu_UKB");

            yerUKBRoute.Stroke =
                new Pen(Color.DeepSkyBlue, 3);

            routeOverlay.Routes.Add(
                yerUKBRoute);
        }

        // 2) Yer İstasyonu -> Görev Yükü
        if (KoordinatGecerli(yerIstasyonuKonum) &&
            KoordinatGecerli(gorevYukuKonum))
        {
            List<PointLatLng> noktalar =
                new List<PointLatLng>
                {
                yerIstasyonuKonum,
                gorevYukuKonum
                };

            yerPayloadRoute =
                new GMapRoute(
                    noktalar,
                    "YerIstasyonu_Payload");

            yerPayloadRoute.Stroke =
                new Pen(Color.LimeGreen, 3);

            routeOverlay.Routes.Add(
                yerPayloadRoute);
        }

        // 3) UKB -> Görev Yükü
        if (KoordinatGecerli(ukbKonum) &&
            KoordinatGecerli(gorevYukuKonum))
        {
            List<PointLatLng> noktalar =
                new List<PointLatLng>
                {
                ukbKonum,
                gorevYukuKonum
                };

            ukbPayloadRoute =
                new GMapRoute(
                    noktalar,
                    "UKB_Payload");

            ukbPayloadRoute.Stroke =
                new Pen(Color.Orange, 3);

            routeOverlay.Routes.Add(
                ukbPayloadRoute);
        }
        MesafeBilgileriniGuncelle();
        harita.Refresh();
    }
    // Yer istasyonu koordinatlarını uygulamak için buton tıklama olayı
    private void btnYerKonumUygula_Click(
    object sender,
    EventArgs e)
    {
        bool enlemOk = double.TryParse(
            txtYerEnlem.Text.Trim().Replace(',', '.'),
            NumberStyles.Float,
            CultureInfo.InvariantCulture,
            out double enlem);

        bool boylamOk = double.TryParse(
            txtYerBoylam.Text.Trim().Replace(',', '.'),
            NumberStyles.Float,
            CultureInfo.InvariantCulture,
            out double boylam);

        if (!enlemOk || !boylamOk)
        {
            MessageBox.Show(
                "Geçerli bir enlem ve boylam giriniz.\n" +
                "Örnek: 40.901289 / 31.173363",
                "Konum Hatası",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return;
        }

        if (enlem < -90 || enlem > 90 ||
            boylam < -180 || boylam > 180)
        {
            MessageBox.Show(
                "Enlem -90 ile 90,\n" +
                "boylam -180 ile 180 arasında olmalıdır.",
                "Konum Hatası",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return;
        }

        yerIstasyonuKonum =
            new PointLatLng(enlem, boylam);

        if (yerIstasyonuMarker == null)
        {
            yerIstasyonuMarker =
                new GMarkerGoogle(
                    yerIstasyonuKonum,
                    GMarkerGoogleType.green_dot);

            yerIstasyonuMarker.ToolTipMode =
                MarkerTooltipMode.Always;

            markerOverlay.Markers.Add(
                yerIstasyonuMarker);
        }
        else
        {
            yerIstasyonuMarker.Position =
                yerIstasyonuKonum;
        }

        yerIstasyonuMarker.ToolTipMode =
     MarkerTooltipMode.Always;

        yerIstasyonuMarker.ToolTipText =
            "YER İSTASYONU\n" +
            "Enlem: " +
            enlem.ToString(
                "0.000000",
                CultureInfo.InvariantCulture) +
            "\nBoylam: " +
            boylam.ToString(
                "0.000000",
                CultureInfo.InvariantCulture);
        HaritaBaglantilariniGuncelle();
        harita.Position =
            yerIstasyonuKonum;

        harita.Refresh();

        MessageBox.Show(
            "Yer istasyonu konumu güncellendi.",
            "Konum",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }
    // Mesafe bilgilerini güncellemek için fonksiyon
    private void MesafeBilgileriniGuncelle()
    {
        if (KoordinatGecerli(yerIstasyonuKonum) &&
            KoordinatGecerli(ukbKonum))
        {
            double mesafe =
                MesafeHesapla(
                    yerIstasyonuKonum.Lat,
                    yerIstasyonuKonum.Lng,
                    ukbKonum.Lat,
                    ukbKonum.Lng);

            lblMesafeYerUKB.Text =
                "YER İSTASYONU ↔ UKB: " +
                mesafe.ToString("0.0") +
                " m";
        }
        else
        {
            lblMesafeYerUKB.Text =
                "YER İSTASYONU ↔ UKB: -- m";
        }


        if (KoordinatGecerli(yerIstasyonuKonum) &&
            KoordinatGecerli(gorevYukuKonum))
        {
            double mesafe =
                MesafeHesapla(
                    yerIstasyonuKonum.Lat,
                    yerIstasyonuKonum.Lng,
                    gorevYukuKonum.Lat,
                    gorevYukuKonum.Lng);

            lblMesafeYerPayload.Text =
                "YER İSTASYONU ↔ GÖREV YÜKÜ: " +
                mesafe.ToString("0.0") +
                " m";
        }
        else
        {
            lblMesafeYerPayload.Text =
                "YER İSTASYONU ↔ GÖREV YÜKÜ: -- m";
        }


        if (KoordinatGecerli(ukbKonum) &&
            KoordinatGecerli(gorevYukuKonum))
        {
            double mesafe =
                MesafeHesapla(
                    ukbKonum.Lat,
                    ukbKonum.Lng,
                    gorevYukuKonum.Lat,
                    gorevYukuKonum.Lng);

            lblMesafeUKBPayload.Text =
                "UKB ↔ GÖREV YÜKÜ: " +
                mesafe.ToString("0.0") +
                " m";
        }
        else
        {
            lblMesafeUKBPayload.Text =
                "UKB ↔ GÖREV YÜKÜ: -- m";
        }
    }
    // Excel kaydını başlatmak için fonksiyon
    private void ExcelKaydiniBaslat()
    {
        string kayitKlasoru =
    Path.Combine(
        Application.StartupPath,
        "veri_kayitlari");

        if (!Directory.Exists(kayitKlasoru))
        {
            Directory.CreateDirectory(
                kayitKlasoru);
        }

        string dosyaAdi =
            "GOKCE_TELEMETRI_" +
            DateTime.Now.ToString(
                "yyyyMMdd_HHmmss") +
            ".xlsx";

        kayitDosyaYolu =
            Path.Combine(
                kayitKlasoru,
                dosyaAdi);

        excelDosyasi =
            new XLWorkbook();

        ukbSayfasi =
            excelDosyasi.Worksheets.Add(
                "UKB");

        payloadSayfasi =
            excelDosyasi.Worksheets.Add(
                "GOREV_YUKU");

        UKBBasliklariniOlustur();

        PayloadBasliklariniOlustur();

        ukbExcelSatir = 2;
        payloadExcelSatir = 2;

        excelDosyasi.SaveAs(
            kayitDosyaYolu);
    }
    // UKB sayfası başlıklarını oluşturmak için fonksiyon EXCEL
    private void UKBBasliklariniOlustur()
    {
        string[] basliklar =
{
    "Tarih",
    "Saat",
    "İvme X (m/s²)",
    "İvme Y (m/s²)",
    "İvme Z (m/s²)",
    "Gyro X (°/s)",
    "Gyro Y (°/s)",
    "Gyro Z (°/s)",
    "Roll (°)",
    "Pitch (°)",
    "Yaw (°)",
    "GPS Enlem (°)",
    "GPS Boylam (°)",
    "GPS İrtifa (m)",
    "Barometrik İrtifa (m)",
    "Durum"
};

        for (int i = 0;
             i < basliklar.Length;
             i++)
        {
            ukbSayfasi
                .Cell(1, i + 1)
                .Value =
                basliklar[i];
        }

        ukbSayfasi
            .Row(1)
            .Style.Font.Bold = true;
    }
    // Payload sayfası başlıklarını oluşturmak için fonksiyon EXCEL
    private void PayloadBasliklariniOlustur()
    {
        string[] basliklar =
{
    "Tarih",
    "Saat",
    "Paket Sayacı",
    "GPS İrtifa (m)",
    "GPS Enlem (°)",
    "GPS Boylam (°)",
    "Basınç (hPa)",
    "Sıcaklık (°C)",
    "Nem (%)",
    "Yoğunluk (kg/m³)"
};

        for (int i = 0;
             i < basliklar.Length;
             i++)
        {
            payloadSayfasi
                .Cell(1, i + 1)
                .Value =
                basliklar[i];
        }

        payloadSayfasi
            .Row(1)
            .Style.Font.Bold = true;
    }
    // Dereceyi radyana çeviren yardımcı fonksiyon
    private static double DereceToRadyan(double derece)
    {
        return derece * Math.PI / 180.0;
    }
    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void label3_Click(object sender, EventArgs e)
    {

    }

    private void label7_Click(object sender, EventArgs e)
    {

    }

    private void Form1_Load(object sender, EventArgs e)
    {
        // UcusDurumlariniGuncelle(3); //debug için
    }

    private void label13_Click(object sender, EventArgs e)
    {

    }

    private void ID_TextChanged(object sender, EventArgs e)
    {

    }

    private void panel2_Paint(object sender, PaintEventArgs e)
    {

    }
    // Mesafe hesaplama butonu
    private void button1_Click(
    object sender,
    EventArgs e)
    {
        HaritaBaglantilariniGuncelle();
    }

    private void panel3_Paint(object sender, PaintEventArgs e)
    {

    }

    private void button2_Click(object sender, EventArgs e)
    {
        RoketiDondur(30, 15, 45);
    }

    private void label21_Click(object sender, EventArgs e)
    {

    }

    private void label22_Click(object sender, EventArgs e)
    {

    }

    private void panel5_Paint(object sender, PaintEventArgs e)
    {

    }
    // Haritayı güncelleme fonksiyonu
    private void HaritaGuncelle(double enlem, double boylam)
    {
        if (enlem == 0 || boylam == 0)
            return;

        PointLatLng yeniKonum = new PointLatLng(enlem, boylam);
        ukbKonum = yeniKonum;

        if (ukbMarker == null)
        {
            ukbMarker = new GMarkerGoogle(yeniKonum, GMarkerGoogleType.blue_dot);
            ukbMarker.ToolTipText = "UKB";
            markerOverlay.Markers.Add(ukbMarker);
        }
        else
        {
            ukbMarker.Position = yeniKonum;
        }

        ukbMarker.ToolTipMode =
    MarkerTooltipMode.Always;

        ukbMarker.ToolTipText =
            "GÖKÇE / UKB\n" +
            "Enlem: " +
            enlem.ToString(
                "0.000000",
                CultureInfo.InvariantCulture) +
            "\nBoylam: " +
            boylam.ToString(
                "0.000000",
                CultureInfo.InvariantCulture) +
            "\nİrtifa: " +
            irtifa.Text +
            " m";

        HaritaBaglantilariniGuncelle();
        harita.Refresh();
    }
    private void SerialPort2_DataReceived(
     object sender,
     SerialDataReceivedEventArgs e)
    {
        try
        {
            string veri = serialPort2.ReadLine().Trim();

            System.Diagnostics.Debug.WriteLine(
                "PAYLOAD HAM VERİ: " + veri);

            // Arduino'nun gönderdiği format:
            // paketSayaci,gpsIrtifa,enlem,boylam,
            // basinc,sicaklik,nem,yogunluk

            string[] p = veri.Split(',');

            if (p.Length != 8)
            {
                System.Diagnostics.Debug.WriteLine(
                    "Payload alan sayısı hatalı. Gelen alan: " +
                    p.Length);

                return;
            }

            bool paketNoOk = ushort.TryParse(
                p[0].Trim(),
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out ushort paketNo);

            bool gpsIrtifaOk = double.TryParse(
                p[1].Trim(),
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out double gpsIrtifa);

            bool enlemOk = double.TryParse(
                p[2].Trim(),
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out double enlem);

            bool boylamOk = double.TryParse(
                p[3].Trim(),
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out double boylam);

            bool basincOk = double.TryParse(
                p[4].Trim(),
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out double basinc);

            bool sicaklikOk = double.TryParse(
                p[5].Trim(),
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out double sicaklik);

            bool nemOk = double.TryParse(
                p[6].Trim(),
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out double nem);

            bool yogunlukOk = double.TryParse(
                p[7].Trim(),
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out double yogunluk);

            if (!paketNoOk ||
                !gpsIrtifaOk ||
                !enlemOk ||
                !boylamOk ||
                !basincOk ||
                !sicaklikOk ||
                !nemOk ||
                !yogunlukOk)
            {
                System.Diagnostics.Debug.WriteLine(
                    "Payload sayısal dönüşüm hatası: " + veri);

                return;
            }
            // SADECE GEÇERLİ PAKETLERİ EXCEL'E KAYDET
            PayloadVeriKaydet(
                paketNo,
                gpsIrtifa,
                enlem,
                boylam,
                basinc,
                sicaklik,
                nem,
                yogunluk);
            payloadPaketSayaciKalite++;
            if (IsDisposed || !IsHandleCreated)
                return;

            // Geçerli bir görev yükü paketi başarıyla alındı
            sonPayloadVeriZamani = DateTime.Now;

            BeginInvoke((MethodInvoker)delegate
            {
                Ppktsayaci.Text = paketNo.ToString(
                    CultureInfo.InvariantCulture);

                PGpsIrtifa.Text = gpsIrtifa.ToString(
                    "0.00",
                    CultureInfo.InvariantCulture);

                PEnlem.Text = enlem.ToString(
                    "0.000000",
                    CultureInfo.InvariantCulture);

                PBoylam.Text = boylam.ToString(
                    "0.000000",
                    CultureInfo.InvariantCulture);

                PBasinc.Text = basinc.ToString(
                    "0.00",
                    CultureInfo.InvariantCulture);

                basincGosterge.Deger = basinc;

                PSicaklik.Text = sicaklik.ToString(
                    "0.00",
                    CultureInfo.InvariantCulture);

                sicaklikGosterge.Deger = sicaklik;

                PNem.Text = nem.ToString(
                    "0.00",
                    CultureInfo.InvariantCulture);

                nemGosterge.Deger = nem;

                PYogunluk.Text = yogunluk.ToString(
                    "0.0000",
                    CultureInfo.InvariantCulture);

                yogunlukGosterge.Deger = yogunluk;
                chartPayloadCevresel
    .Series["Sıcaklık"]
    .Points
    .AddXY(
        payloadGrafikZaman,
        sicaklik);

                chartPayloadCevresel
                    .Series["Nem"]
                    .Points
                    .AddXY(
                        payloadGrafikZaman,
                        nem);

                chartPayloadCevresel
                    .Series["Basınç"]
                    .Points
                    .AddXY(
                        payloadGrafikZaman,
                        basinc);
                if (chartPayloadCevresel
    .Series["Sıcaklık"]
    .Points.Count > 100)
                {
                    chartPayloadCevresel
                        .Series["Sıcaklık"]
                        .Points.RemoveAt(0);

                    chartPayloadCevresel
                        .Series["Nem"]
                        .Points.RemoveAt(0);

                    chartPayloadCevresel
                        .Series["Basınç"]
                        .Points.RemoveAt(0);
                }
                ChartArea alan =
    chartPayloadCevresel.ChartAreas[
        "PayloadCevreselAlan"];

                if (payloadGrafikZaman > 100)
                {
                    alan.AxisX.Minimum =
                        payloadGrafikZaman - 100;

                    alan.AxisX.Maximum =
                        payloadGrafikZaman;
                }
                else
                {
                    alan.AxisX.Minimum = 0;
                    alan.AxisX.Maximum = 100;
                }
                payloadGrafikZaman++;

                HaritaGorevYukuGuncelle(
                    enlem,
                    boylam);

                lblPayloadBaglanti.Text =
                    "● PAYLOAD AKTİF";

                lblPayloadBaglanti.ForeColor =
                    Color.Lime;
            });
        }
        catch (TimeoutException)
        {
            System.Diagnostics.Debug.WriteLine(
                "Payload seri port zaman aşımı.");
        }
        catch (InvalidOperationException ex)
        {
            System.Diagnostics.Debug.WriteLine(
                "Payload port kapalı veya kullanılamıyor: " +
                ex.Message);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                "Payload veri okuma hatası: " +
                ex.Message);
        }
    }
    // Seri porttan veri geldiğinde işlenecek fonksiyon
    private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            string veri = serialPort1.ReadLine();

            sonVeriZamani = DateTime.Now;
            string[] p = veri.Split(',');

            if (p.Length == 14)
            {
                ukbPaketSayaciKalite++;
                double ax_g = double.Parse(p[0].Trim(), CultureInfo.InvariantCulture);
                double ay_g = double.Parse(p[1].Trim(), CultureInfo.InvariantCulture);
                double az_g = double.Parse(p[2].Trim(), CultureInfo.InvariantCulture);

                double gyroX = double.Parse(p[3].Trim(), CultureInfo.InvariantCulture);
                double gyroY = double.Parse(p[4].Trim(), CultureInfo.InvariantCulture);
                double gyroZ = double.Parse(p[5].Trim(), CultureInfo.InvariantCulture);

                double roll = double.Parse(p[6].Trim(), CultureInfo.InvariantCulture);
                double pitch = double.Parse(p[7].Trim(), CultureInfo.InvariantCulture);
                double yaw = double.Parse(p[8].Trim(), CultureInfo.InvariantCulture);

                double enlem = double.Parse(p[9].Trim(), CultureInfo.InvariantCulture);
                double boylam = double.Parse(p[10].Trim(), CultureInfo.InvariantCulture);

                double gpsIrtifa = double.Parse(p[11].Trim(), CultureInfo.InvariantCulture);
                double msirtifa = double.Parse(p[12].Trim(), CultureInfo.InvariantCulture);

                int durum = int.Parse(p[13].Trim(), CultureInfo.InvariantCulture);

                // g -> m/s² dönüşümü
                double ax = ax_g;
                double ay = ay_g;
                double az = az_g;
                // Verileri kaydet
                VeriKaydet(
    ax, ay, az,
    gyroX, gyroY, gyroZ,
    roll, pitch, yaw,
    enlem, boylam,
    gpsIrtifa, msirtifa,
    durum
);
                // UI güncellemeleri ana thread'de yapılmalı
                this.Invoke((MethodInvoker)delegate
                {
                    irtifa.Text = msirtifa.ToString("0.00", CultureInfo.InvariantCulture);
                    RGpsIrtifa.Text = gpsIrtifa.ToString("0.00", CultureInfo.InvariantCulture);

                    REnlem.Text = enlem.ToString("0.000000", CultureInfo.InvariantCulture);
                    RBoylam.Text = boylam.ToString("0.000000", CultureInfo.InvariantCulture);

                    HaritaGuncelle(enlem, boylam);

                    GyroX.Text = gyroX.ToString("0.00", CultureInfo.InvariantCulture);
                    GyroY.Text = gyroY.ToString("0.00", CultureInfo.InvariantCulture);
                    GyroZ.Text = gyroZ.ToString("0.00", CultureInfo.InvariantCulture);

                    PIvmeX.Text = ax.ToString("0.00", CultureInfo.InvariantCulture);
                    PIvmeY.Text = ay.ToString("0.00", CultureInfo.InvariantCulture);
                    PIvmeZ.Text = az.ToString("0.00", CultureInfo.InvariantCulture);
                    Aci.Text = pitch.ToString("0.00", CultureInfo.InvariantCulture);
                    State.Text = durum.ToString(CultureInfo.InvariantCulture);
                    UcusDurumlariniGuncelle(durum);

                    chartIvme.Series["Ax"].Points.AddXY(zaman, ax);
                    chartIvme.Series["Ay"].Points.AddXY(zaman, ay);
                    chartIvme.Series["Az"].Points.AddXY(zaman, az);

                    chartGyro.Series["Gx"].Points.AddXY(zaman, gyroX);
                    chartGyro.Series["Gy"].Points.AddXY(zaman, gyroY);
                    chartGyro.Series["Gz"].Points.AddXY(zaman, gyroZ);

                    chartAlt.Series["Altitude"].Points.AddXY(zaman, msirtifa);

                    if (chartIvme.Series["Ax"].Points.Count > 50)
                    {
                        chartIvme.Series["Ax"].Points.RemoveAt(0);
                        chartIvme.Series["Ay"].Points.RemoveAt(0);
                        chartIvme.Series["Az"].Points.RemoveAt(0);

                        chartGyro.Series["Gx"].Points.RemoveAt(0);
                        chartGyro.Series["Gy"].Points.RemoveAt(0);
                        chartGyro.Series["Gz"].Points.RemoveAt(0);

                        chartAlt.Series["Altitude"].Points.RemoveAt(0);
                    }

                    chartIvme.ChartAreas[0].RecalculateAxesScale();
                    chartGyro.ChartAreas[0].RecalculateAxesScale();
                    chartAlt.ChartAreas[0].RecalculateAxesScale();

                    zaman++;
                    // Roketi döndür
                    if (!modelAciBaslatildi)
                    {
                        modelRoll = roll;
                        modelPitch = pitch;
                        modelYaw = yaw;

                        modelAciBaslatildi = true;
                    }
                    else
                    {
                        modelRoll =
                            ModelAciFiltrele(
                                roll,
                                modelRoll,
                                modelAciAlpha);

                        modelPitch =
                            ModelAciFiltrele(
                                pitch,
                                modelPitch,
                                modelAciAlpha);

                        modelYaw =
                            ModelAciFiltrele(
                                yaw,
                                modelYaw,
                                modelAciAlpha);
                    }

                    RoketiDondur(
                        modelRoll,
                        modelPitch,
                        modelYaw);
                });
            }
        }
        catch
        {
            // Hatalı veri gelirse program çökmesin
        }
    }
    // Uçuş durumlarını güncellemek için fonksiyon
    private void UcusDurumlariniGuncelle(int durumBits)
    {
        bool[] durumlar =
        {
        (durumBits & (1 << 0)) != 0, // Kalkış
        (durumBits & (1 << 1)) != 0, // Burn Out
        (durumBits & (1 << 2)) != 0, // İrtifa Eşiği
        (durumBits & (1 << 3)) != 0, // Açı
        (durumBits & (1 << 4)) != 0, // Alçalma
        (durumBits & (1 << 5)) != 0, // Sürüklenme Paraşütü
        (durumBits & (1 << 6)) != 0, // Belirli İrtifa
        (durumBits & (1 << 7)) != 0  // Ana Paraşüt
    };
        bool kalkisGerceklesti =
    durumlar[0];

        if (kalkisGerceklesti &&
            !gorevBasladi)
        {
            gorevBasladi = true;

            gorevBaslangicZamani =
                DateTime.Now;
        }

        Label[] etiketler =
        {
        lblKalkis,
        lblBurnOut,
        lblIrtifaEsigi,
        lblAciDurumu,
        lblAlcalma,
        lblSuruklenmeParasutu,
        lblBelirliIrtifa,
        lblAnaParasut
    };

        string[] metinler =
        {
        "KALKIŞ",
        "BURN OUT",
        "İRTİFA EŞİĞİ",
        "AÇI",
        "ALÇALMA",
        "SÜRÜKLENME PARAŞÜTÜ",
        "BELİRLİ İRTİFA",
        "ANA PARAŞÜT"
    };

        int aktifIndex = -1;

        for (int i = 0; i < durumlar.Length; i++)
        {
            if (durumlar[i])
                aktifIndex = i;
        }
        string mevcutDurum = "BEKLEME";

        if (aktifIndex >= 0)
        {
            mevcutDurum = metinler[aktifIndex];
        }

        lblMevcutDurum.Text =
            "MEVCUT DURUM: " + mevcutDurum;

        if (aktifIndex >= 0)
        {
            lblMevcutDurum.ForeColor =
                Color.DeepSkyBlue;
        }
        else
        {
            lblMevcutDurum.ForeColor =
                Color.Gray;
        }
        for (int i = 0; i < etiketler.Length; i++)
        {
            if (durumlar[i])
            {
                if (i == aktifIndex)
                {
                    etiketler[i].Text = "▶ " + metinler[i] + "   AKTİF";
                    etiketler[i].ForeColor = Color.DeepSkyBlue;
                }
                else
                {
                    etiketler[i].Text = "● " + metinler[i];
                    etiketler[i].ForeColor = Color.Lime;
                }
            }
            else
            {
                etiketler[i].Text = "○ " + metinler[i];
                etiketler[i].ForeColor = Color.Gray;
            }
        }
    }
    // Uçuş durumlarını güncellemek için yardımcı fonksiyon
    private void DurumEtiketiGuncelle(
        Label label,
        string metin,
        bool gerceklesti)
    {
        if (gerceklesti)
        {
            label.Text = "● " + metin;
            label.ForeColor = Color.Lime;
        }
        else
        {
            label.Text = "○ " + metin;
            label.ForeColor = Color.Gray;
        }
    }
    // Görev yükü konumunu haritada güncellemek için fonksiyon
    private void HaritaGorevYukuGuncelle(
    double enlem,
    double boylam)
    {
        if (double.IsNaN(enlem) ||
            double.IsNaN(boylam) ||
            enlem < -90 ||
            enlem > 90 ||
            boylam < -180 ||
            boylam > 180 ||
            (enlem == 0 && boylam == 0))
        {
            return;
        }

        PointLatLng yeniKonum =
            new PointLatLng(enlem, boylam);

        gorevYukuKonum = yeniKonum;

        if (gorevYukuMarker == null)
        {
            gorevYukuMarker = new GMarkerGoogle(
                yeniKonum,
                GMarkerGoogleType.red_dot);

            gorevYukuMarker.ToolTipMode =
                MarkerTooltipMode.Always;

            gorevYukuMarker.ToolTipText =
                "Görev Yükü";

            markerOverlay.Markers.Add(
                gorevYukuMarker);
        }
        else
        {
            gorevYukuMarker.Position =
                yeniKonum;
        }

        gorevYukuMarker.ToolTipMode =
    MarkerTooltipMode.Always;

        gorevYukuMarker.ToolTipText =
            "GÖREV YÜKÜ\n" +
            "Enlem: " +
            enlem.ToString(
                "0.000000",
                CultureInfo.InvariantCulture) +
            "\nBoylam: " +
            boylam.ToString(
                "0.000000",
                CultureInfo.InvariantCulture) +
            "\nGPS İrtifa: " +
            PGpsIrtifa.Text +
            " m";

        HaritaBaglantilariniGuncelle();
        harita.Refresh();
    }

    private void PIvmeX_TextChanged(object sender, EventArgs e)
    {

    }

    private void PIvmeY_TextChanged(object sender, EventArgs e)
    {

    }

    private void PIvmeZ_TextChanged(object sender, EventArgs e)
    {

    }

    private void irtifa_TextChanged(object sender, EventArgs e)
    {

    }

    private void GyroX_TextChanged(object sender, EventArgs e)
    {

    }

    private void GyroY_TextChanged(object sender, EventArgs e)
    {

    }

    private void GyroZ_TextChanged(object sender, EventArgs e)
    {

    }

    private void RGpsIrtifa_TextChanged(object sender, EventArgs e)
    {

    }

    private void REnlem_TextChanged(object sender, EventArgs e)
    {

    }

    private void RBoylam_TextChanged(object sender, EventArgs e)
    {

    }

    private void Aci_TextChanged(object sender, EventArgs e)
    {

    }

    private void State_TextChanged(object sender, EventArgs e)
    {

    }

    private void crc_TextChanged(object sender, EventArgs e)
    {

    }

    private void sayac_TextChanged(object sender, EventArgs e)
    {

    }

    private void label23_Click(object sender, EventArgs e)
    {

    }
    // Jiroskop grafiği kurmak için fonksiyon
    private void GrafikKurGyro()
    {
        chartGyro = new Chart();
        chartGyro.Dock = DockStyle.Fill;
        chartGyro.BackColor = Color.FromArgb(5, 12, 22);

        ChartArea alan = new ChartArea();
        alan.AxisY.IsStartedFromZero = false;
        alan.AxisY.Minimum = Double.NaN;
        alan.CursorX.IsUserEnabled = true;
        alan.CursorX.IsUserSelectionEnabled = true;

        alan.CursorY.IsUserEnabled = true;
        alan.CursorY.IsUserSelectionEnabled = true;
        alan.AxisX.ScaleView.Zoomable = true;
        alan.AxisY.ScaleView.Zoomable = true;
        alan.AxisX.ScrollBar.Enabled = true;
        alan.AxisY.ScrollBar.Enabled = true;
        alan.AxisX.Title = "Time";
        alan.AxisY.Title = "Gyroscope";
        alan.BackColor = Color.FromArgb(10, 25, 45);
        alan.AxisX.LabelStyle.ForeColor = Color.White;
        alan.AxisY.LabelStyle.ForeColor = Color.White;

        alan.AxisX.TitleForeColor = Color.White;
        alan.AxisY.TitleForeColor = Color.White;

        alan.AxisX.MajorGrid.LineColor = Color.FromArgb(35, 55, 75);
        alan.AxisY.MajorGrid.LineColor = Color.FromArgb(35, 55, 75);

        chartGyro.ChartAreas.Add(alan);

        Title baslik = new Title();
        baslik.Text = "Gyroscope Chart";
        baslik.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        baslik.ForeColor = Color.White;
        chartGyro.Titles.Add(baslik);

        Legend legend = new Legend();
        legend.Docking = Docking.Top;
        legend.BackColor = Color.FromArgb(10, 25, 45);
        legend.ForeColor = Color.White;

        chartGyro.Legends.Add(legend);

        Series sx = new Series("Gx");
        sx.ChartType = SeriesChartType.Line;
        sx.Color = Color.Red;

        Series sy = new Series("Gy");
        sy.ChartType = SeriesChartType.Line;
        sy.Color = Color.Cyan;

        Series sz = new Series("Gz");
        sz.ChartType = SeriesChartType.Line;
        sz.Color = Color.Lime;

        chartGyro.Series.Add(sx);
        chartGyro.Series.Add(sy);
        chartGyro.Series.Add(sz);

        panel6.Controls.Add(chartGyro);
        chartGyro.MouseWheel += chart_MouseWheel;
        chartGyro.Focus();
        chartGyro.MouseEnter += (s, e) => chartGyro.Focus();
    }
    // Görev yükü çevresel verileri grafiğini kurmak için fonksiyon
    private void GrafikKurPayloadCevresel()
    {
        chartPayloadCevresel = new Chart();

        chartPayloadCevresel.Dock =
            DockStyle.Fill;

        chartPayloadCevresel.BackColor =
            Color.FromArgb(5, 12, 22);


        ChartArea alan =
            new ChartArea("PayloadCevreselAlan");

        alan.BackColor =
            Color.FromArgb(10, 25, 45);

        alan.AxisX.Title = "Zaman";
        alan.AxisX.TitleForeColor = Color.White;
        alan.AxisX.LabelStyle.ForeColor = Color.White;
        alan.AxisX.MajorGrid.LineColor =
            Color.FromArgb(35, 55, 75);

        alan.AxisY.Title =
            "Sıcaklık / Nem";

        alan.AxisY.TitleForeColor =
            Color.White;

        alan.AxisY.LabelStyle.ForeColor =
            Color.White;

        alan.AxisY.MajorGrid.LineColor =
            Color.FromArgb(35, 55, 75);

        // Sağ eksen
        alan.AxisY2.Enabled =
            AxisEnabled.True;

        alan.AxisY2.Title =
            "Basınç";

        alan.AxisY2.TitleForeColor =
            Color.White;

        alan.AxisY2.LabelStyle.ForeColor =
            Color.White;


        chartPayloadCevresel.ChartAreas.Add(
            alan);


        Title baslik = new Title();

        baslik.Text =
            "Görev Yükü Çevresel Verileri";

        baslik.Font =
            new Font(
                "Segoe UI",
                11,
                FontStyle.Bold);

        baslik.ForeColor =
            Color.White;

        chartPayloadCevresel.Titles.Add(
            baslik);


        Legend legend = new Legend();

        legend.Docking = Docking.Top;

        legend.BackColor =
            Color.FromArgb(10, 25, 45);

        legend.ForeColor =
            Color.White;

        chartPayloadCevresel.Legends.Add(
            legend);


        Series sicaklikSeries =
            new Series("Sıcaklık");

        sicaklikSeries.ChartType =
            SeriesChartType.Line;

        sicaklikSeries.BorderWidth = 2;


        Series nemSeries =
            new Series("Nem");

        nemSeries.ChartType =
            SeriesChartType.Line;

        nemSeries.BorderWidth = 2;


        Series basincSeries =
            new Series("Basınç");

        basincSeries.ChartType =
            SeriesChartType.Line;

        basincSeries.BorderWidth = 2;

        basincSeries.YAxisType =
            AxisType.Secondary;


        chartPayloadCevresel.Series.Add(
            sicaklikSeries);

        chartPayloadCevresel.Series.Add(
            nemSeries);

        chartPayloadCevresel.Series.Add(
            basincSeries);


        panelPayloadGrafik.Controls.Add(
            chartPayloadCevresel);
    }
    // İrtifa grafiği kurmak için fonksiyon
    private void GrafikKurAlt()
    {
        chartAlt = new Chart();
        chartAlt.Dock = DockStyle.Fill;
        chartAlt.BackColor = Color.FromArgb(5, 12, 22);

        ChartArea alan = new ChartArea();
        alan.CursorX.IsUserEnabled = true;
        alan.CursorX.IsUserSelectionEnabled = true;

        alan.CursorY.IsUserEnabled = true;
        alan.CursorY.IsUserSelectionEnabled = true;
        alan.AxisX.ScaleView.Zoomable = true;
        alan.AxisY.ScaleView.Zoomable = true;
        alan.AxisX.ScrollBar.Enabled = true;
        alan.AxisY.ScrollBar.Enabled = true;
        alan.AxisX.Title = "Time";
        alan.AxisY.Title = "Altitude";
        alan.BackColor = Color.FromArgb(10, 25, 45);
        alan.AxisX.LabelStyle.ForeColor = Color.White;
        alan.AxisY.LabelStyle.ForeColor = Color.White;

        alan.AxisX.TitleForeColor = Color.White;
        alan.AxisY.TitleForeColor = Color.White;

        alan.AxisX.MajorGrid.LineColor = Color.FromArgb(35, 55, 75);
        alan.AxisY.MajorGrid.LineColor = Color.FromArgb(35, 55, 75);

        chartAlt.ChartAreas.Add(alan);

        Title baslik = new Title();
        baslik.Text = "Altitude Chart";
        baslik.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        baslik.ForeColor = Color.White;
        chartAlt.Titles.Add(baslik);

        Legend legend = new Legend();
        legend.Docking = Docking.Top;
        legend.BackColor = Color.FromArgb(10, 25, 45);
        legend.ForeColor = Color.White;

        chartAlt.Legends.Add(legend);

        Series sx = new Series("Altitude");
        sx.ChartType = SeriesChartType.Line;
        sx.Color = Color.Red;

        chartAlt.Series.Add(sx);


        panel7.Controls.Add(chartAlt);
        chartAlt.MouseWheel += chart_MouseWheel;
        chartAlt.Focus();
        chartAlt.MouseEnter += (s, e) => chartAlt.Focus();
    }
    // Kayıt başlat/durdur butonu
    private void btnKayit_Click(
    object sender,
    EventArgs e)
    {
        if (!kayitAktif)
        {
            try
            {
                ExcelKaydiniBaslat();

                kayitAktif = true;

                btnKayit.Text =
                    "KAYIT DURDUR";

                btnKayit.BackColor =
                    Color.Red;
                lblKayitDosyasi.Text =
    "DOSYA: " +
    Path.GetFileName(
        kayitDosyaYolu);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Excel kayıt dosyası oluşturulamadı:\n" +
                    ex.Message);

                kayitAktif = false;
            }
        }
        else
        {
            try
            {
                kayitAktif = false;

                lock (excelKilidi)
                {
                    if (excelDosyasi != null)
                    {
                        excelDosyasi.SaveAs(
                            kayitDosyaYolu);

                        excelDosyasi.Dispose();

                        excelDosyasi = null;
                    }
                }

                btnKayit.Text =
                    "KAYIT BAŞLAT";

                btnKayit.BackColor =
                    Color.Green;
                lblKayitDosyasi.Text =
    "SON DOSYA: " +
    Path.GetFileName(
        kayitDosyaYolu);

                MessageBox.Show(
                    "Kayıt tamamlandı.\n\n" +
                    Path.GetFileName(
                        kayitDosyaYolu));
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Kayıt kapatılırken hata oluştu:\n" +
                    ex.Message);
            }
        }
    }

    private void lblBaglanti_Click(object sender, EventArgs e)
    {

    }

    private void btnUKBBaglan_Click(object sender, EventArgs e)
    {
        try
        {
            if (!serialPort1.IsOpen)
            {
                if (cmbUKBPort.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen UKB portu seç.");
                    return;
                }

                serialPort1.PortName = cmbUKBPort.SelectedItem.ToString();
                if (cmbUKBBaud.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen UKB baudrate seç.");
                    return;
                }

                serialPort1.BaudRate =
                    int.Parse(cmbUKBBaud.SelectedItem.ToString());
                serialPort1.DataReceived -= SerialPort1_DataReceived;
                serialPort1.DataReceived += SerialPort1_DataReceived;

                serialPort1.Open();

                btnUKBBaglan.Text = "UKB KES";
                lblBaglanti.Text = "● UKB BAĞLI";
                lblBaglanti.ForeColor = Color.Lime;
            }
            else
            {
                serialPort1.Close();

                btnUKBBaglan.Text = "UKB BAĞLAN";
                lblBaglanti.Text = "● UKB BAĞLI DEĞİL";
                lblBaglanti.ForeColor = Color.Red;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Port bağlantı hatası: " + ex.Message);
            lblBaglanti.Text = "● UKB BAĞLANTI HATASI";
            lblBaglanti.ForeColor = Color.Red;
        }
    }

    private void btnPayloadBaglan_Click(
     object sender,
     EventArgs e)
    {
        try
        {
            if (!serialPort2.IsOpen)
            {
                if (cmbPayloadPort.SelectedItem == null)
                {
                    MessageBox.Show(
                        "Lütfen Payload portunu seç.");

                    return;
                }

                serialPort2.PortName =
                    cmbPayloadPort.SelectedItem.ToString();

                if (cmbPayloadBaud.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen Görev Yükü baudrate seç.");
                    return;
                }

                serialPort2.BaudRate =
                    int.Parse(cmbPayloadBaud.SelectedItem.ToString());
                serialPort2.DataBits = 8;
                serialPort2.Parity = Parity.None;
                serialPort2.StopBits = StopBits.One;
                serialPort2.Handshake = Handshake.None;

                serialPort2.NewLine = "\n";
                serialPort2.ReadTimeout = 1000;

                serialPort2.DataReceived -=
                    SerialPort2_DataReceived;

                serialPort2.DataReceived +=
                    SerialPort2_DataReceived;

                serialPort2.Open();

                // Port açılırken tamponda kalmış eski verileri temizle
                serialPort2.DiscardInBuffer();

                btnPayloadBaglan.Text =
                    "PAYLOAD KES";

                lblPayloadBaglanti.Text =
                    "● PAYLOAD BAĞLI";

                lblPayloadBaglanti.ForeColor =
                    Color.Lime;
            }
            else
            {
                serialPort2.DataReceived -=
                    SerialPort2_DataReceived;

                serialPort2.Close();

                btnPayloadBaglan.Text =
                    "PAYLOAD BAĞLAN";

                lblPayloadBaglanti.Text =
                    "● PAYLOAD BAĞLI DEĞİL";

                lblPayloadBaglanti.ForeColor =
                    Color.Red;
            }
        }
        catch (UnauthorizedAccessException)
        {
            MessageBox.Show(
                "Payload COM portu başka bir uygulama tarafından kullanılıyor.\n\n" +
                "Arduino IDE Seri Monitörü ve Seri Çiziciyi kapatıp tekrar dene.");

            lblPayloadBaglanti.Text =
                "● PORT KULLANIMDA";

            lblPayloadBaglanti.ForeColor =
                Color.Red;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Payload bağlantı hatası: " +
                ex.Message);

            lblPayloadBaglanti.Text =
                "● PAYLOAD HATASI";

            lblPayloadBaglanti.ForeColor =
                Color.Red;
        }
    }
    // Kayıt klasörünü açmak için buton
    private void btnKayitKlasoruAc_Click(
    object sender,
    EventArgs e)
    {
        try
        {
            string kayitKlasoru =
                Path.Combine(
                    Application.StartupPath,
                    "veri_kayitlari");

            if (!Directory.Exists(kayitKlasoru))
            {
                Directory.CreateDirectory(
                    kayitKlasoru);
            }

            Process.Start(
                new ProcessStartInfo
                {
                    FileName = kayitKlasoru,
                    UseShellExecute = true
                });
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Kayıt klasörü açılamadı:\n" +
                ex.Message);
        }
    }
    // Harita cache hazırlama butonu
    private async void btnHaritaCache_Click(
    object sender,
    EventArgs e)
    {
        try
        {
            DialogResult cevap =
                MessageBox.Show(
                    "Yarışma alanının offline haritası indirilecek.\n\n" +
                    "Zoom 11-16: geniş yarışma alanı\n" +
"Zoom 17: yarışma merkezi detay alanı\n\n" +
"İnternet bağlantısının açık olduğundan emin ol.\n\n" +
"Devam edilsin mi?" +
                    "İnternet bağlantısının açık olduğundan emin ol.\n\n" +
                    "Devam edilsin mi?",
                    "Offline Harita Hazırlama",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (cevap != DialogResult.Yes)
                return;

            btnHaritaCache.Enabled = false;

            // GEÇİCİ ALAN
            // Kesin Hisar Atış Alanı koordinatlarını bulunca
            // bunu değiştireceğiz.
            double kuzey = 38.6125;
            double guney = 38.1634;

            double bati = 33.4550;
            double dogu = 34.0287;

            RectLatLng yarismaAlani =
                RectLatLng.FromLTRB(
                    bati,
                    kuzey,
                    dogu,
                    guney);

            harita.Position =
                new PointLatLng(
                    (kuzey + guney) / 2.0,
                    (bati + dogu) / 2.0);

            harita.SetZoomToFitRect(yarismaAlani);

            await HaritaCacheIndir(
    yarismaAlani,
    11,
    16);
            // 2) Sadece merkez çevresi detay
            double merkezEnlem = 38.387948;
            double merkezBoylam = 33.741858;

            double yarimEnlemFarki = 2.5 / 111.32;
            double yarimBoylamFarki =
                2.5 /
                (111.32 * Math.Cos(
                    merkezEnlem * Math.PI / 180.0));

            double kucukKuzey =
                merkezEnlem + yarimEnlemFarki;

            double kucukGuney =
                merkezEnlem - yarimEnlemFarki;

            double kucukBati =
                merkezBoylam - yarimBoylamFarki;

            double kucukDogu =
                merkezBoylam + yarimBoylamFarki;

            RectLatLng detayAlani =
                RectLatLng.FromLTRB(
                    kucukBati,
                    kucukKuzey,
                    kucukDogu,
                    kucukGuney);

            await HaritaCacheIndir(
                detayAlani,
                17,
                17);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Harita indirme sırasında hata oluştu:\n\n" +
                ex.Message,
                "Harita Hatası",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            btnHaritaCache.Enabled = true;
        }
    }
    private async Task HaritaCacheIndir(
    RectLatLng alan,
    int minZoom,
    int maxZoom)
    {
        GMaps.Instance.Mode = AccessMode.CacheOnly;

        var provider = harita.MapProvider;

        int toplamBasarili = 0;
        int toplamHatali = 0;

        for (int zoom = minZoom; zoom <= maxZoom; zoom++)
        {
            var tileListesi =
                provider.Projection.GetAreaTileList(
                    alan,
                    zoom,
                    0);

            int mevcut = 0;

            foreach (var tile in tileListesi)
            {
                mevcut++;

                try
                {
                    foreach (var overlayProvider in provider.Overlays)
                    {
                        Exception hata;

                        var resim =
                            GMaps.Instance.GetImageFrom(
                                overlayProvider,
                                tile,
                                zoom,
                                out hata);

                        if (resim != null)
                        {
                            resim.Dispose();
                            toplamBasarili++;
                        }
                        else
                        {
                            toplamHatali++;
                        }
                    }
                }
                catch
                {
                    toplamHatali++;
                }

                this.Text =
                    $"Harita indiriliyor | Zoom {zoom} | " +
                    $"{mevcut}/{tileListesi.Count}";

                // UI donmasın ve sunucuya abanmayalım
                await Task.Delay(30);
            }
        }

        this.Text = "AZAK ROKET DGM TAKIMI";

        MessageBox.Show(
            "Offline harita hazırlama tamamlandı.\n\n" +
            $"Başarılı tile: {toplamBasarili}\n" +
            $"Hatalı tile: {toplamHatali}\n\n" +
            "Şimdi interneti kapatıp CacheOnly modunda test edebiliriz.",
            "Harita Cache",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void HaritaCacheYolunuAyarla()
    {
        try
        {
            string cacheYolu =
                Path.Combine(
                    Application.StartupPath,
                    "MapsCache");

            if (!Directory.Exists(cacheYolu))
            {
                Directory.CreateDirectory(
                    cacheYolu);
            }

            var cache =
                GMaps.Instance.PrimaryCache;

            if (cache == null)
                return;

            var property =
                cache.GetType().GetProperty(
                    "CacheLocation");

            if (property == null ||
                !property.CanWrite)
                return;

            property.SetValue(
                cache,
                cacheYolu);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Harita cache yolu ayarlanamadı:\n\n" +
                ex.Message,
                "Harita Cache Hatası",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
    private void btnHaritaCacheKlasoruAc_Click(
    object sender,
    EventArgs e)
    {
        try
        {
            string cacheYolu =
                Path.Combine(
                    Application.StartupPath,
                    "MapsCache");

            if (!Directory.Exists(cacheYolu))
            {
                Directory.CreateDirectory(
                    cacheYolu);
            }

            Process.Start(
                new ProcessStartInfo
                {
                    FileName = cacheYolu,
                    UseShellExecute = true
                });
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Cache klasörü açılamadı:\n\n" +
                ex.Message,
                "Harita Cache",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void BAGLANTIPANELI_Paint(object sender, PaintEventArgs e)
    {

    }

    private void lblPayloadKalite_Click(object sender, EventArgs e)
    {

    }

    private void lblMevcutDurum_Click(object sender, EventArgs e)
    {

    }

    private void label1_Click_1(object sender, EventArgs e)
    {

    }
}
