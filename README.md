# AZAK Rocket Team Ground Station
## AZAK Roket Takımı Yer İstasyonu

This repository contains the ground station software developed for the AZAK Rocket Team. The application is designed to receive, visualize, monitor, and record telemetry data during rocket operations.

Bu repository, AZAK Roket Takımı için geliştirilen yer istasyonu yazılımını içermektedir. Uygulama; roket operasyonları sırasında telemetri verilerinin alınması, görselleştirilmesi, izlenmesi ve kaydedilmesi amacıyla geliştirilmiştir.

> This repository is private and contains project-specific software developed for team use.
>
> Bu repository özeldir ve takım kullanımına yönelik proje özelinde geliştirilmiş yazılımlar içermektedir.

---

## Overview | Genel Bakış

The ground station provides a graphical interface for monitoring telemetry data received from the rocket system.

Yer istasyonu, roket sisteminden alınan telemetri verilerinin grafiksel bir arayüz üzerinden izlenmesini sağlar.

The software is developed as a Windows desktop application using C# and Windows Forms.

Yazılım, C# ve Windows Forms kullanılarak Windows masaüstü uygulaması olarak geliştirilmiştir.

---

## Main Features | Temel Özellikler

- Real-time telemetry monitoring  
  Gerçek zamanlı telemetri izleme

- Graphical data visualization  
  Grafiksel veri görselleştirme

- Custom gauge components  
  Özel gösterge bileşenleri

- Rocket orientation visualization  
  Roket yönelim görselleştirmesi

- Local telemetry data recording  
  Yerel telemetri veri kaydı

- Modular Windows Forms interface  
  Modüler Windows Forms arayüzü

- Integration with a 3D rocket model  
  3B roket modeli entegrasyonu

---

## Technologies | Kullanılan Teknolojiler

- C#
- .NET
- Windows Forms
- Visual Studio
- 3D model integration
- Desktop telemetry visualization

---

## Project Structure | Proje Yapısı

```text
AZAK-Rocket-Team-Ground-Station/
│
├── WinFormGiris.sln
│
├── WinFormGiris/
│   ├── Form1.cs
│   ├── Form1.Designer.cs
│   ├── Program.cs
│   │
│   ├── DensityGaugeControl.cs
│   ├── HumidityGaugeControl.cs
│   ├── PressureGaugeControl.cs
│   ├── RadialGaugeControl.cs
│   ├── ThermometerControl.cs
│   │
│   ├── Models/
│   │   └── roket.STL
│   │
│   ├── Resources/
│   │
│   └── WinFormGiris.csproj
│
├── .gitignore
└── README.md
```

---

## User Interface | Kullanıcı Arayüzü

The application is structured around a Windows Forms interface designed to present telemetry data clearly during ground operations.

Uygulama, yer operasyonları sırasında telemetri verilerini açık ve anlaşılır biçimde göstermek amacıyla tasarlanmış bir Windows Forms arayüzüne sahiptir.

Custom interface components are used for displaying different telemetry parameters in a more readable form.

Farklı telemetri parametrelerinin daha okunabilir biçimde gösterilmesi amacıyla özel arayüz bileşenleri kullanılmaktadır.

---

## Custom Gauge Components | Özel Gösterge Bileşenleri

The project includes multiple custom controls developed for telemetry visualization.

Projede telemetri görselleştirmesi için geliştirilmiş çeşitli özel kontrol bileşenleri bulunmaktadır.

These include:

- Density Gauge
- Humidity Gauge
- Pressure Gauge
- Radial Gauge
- Thermometer Control

Bu bileşenler, farklı sensör ve sistem verilerinin arayüz üzerinde daha anlaşılır şekilde gösterilmesini sağlar.

---

## 3D Rocket Visualization | 3B Roket Görselleştirmesi

The application includes a 3D rocket model that can be used as part of the ground station visualization interface.

Uygulama, yer istasyonu görselleştirme arayüzünün bir parçası olarak kullanılabilen bir 3B roket modeli içermektedir.

The model is stored under:

```text
WinFormGiris/Models/roket.STL
```

---

## Data Recording | Veri Kaydı

The ground station supports local recording of telemetry data for later analysis.

Yer istasyonu, alınan telemetri verilerinin daha sonra incelenebilmesi amacıyla yerel olarak kaydedilmesini desteklemektedir.

Local telemetry records are intentionally excluded from this repository.

Yerel telemetri kayıtları bilinçli olarak bu repository dışında tutulmaktadır.

---

## Ignored Files | Hariç Tutulan Dosyalar

Some local and generated files are excluded from version control using `.gitignore`.

Bazı yerel ve otomatik oluşturulan dosyalar `.gitignore` kullanılarak Git takibinin dışında bırakılmıştır.

Examples include:

```text
.vs/
bin/
obj/
CacheOnly(yurt)/
veri_kayitlari/
```

This prevents local cache files, build outputs, and telemetry records from being uploaded to the repository.

Bu sayede yerel önbellek dosyalarının, derleme çıktılarının ve telemetri kayıtlarının repository içerisine yüklenmesi önlenmektedir.

---

## Requirements | Gereksinimler

Recommended development environment:

Önerilen geliştirme ortamı:

- Windows 10 / 11
- Visual Studio
- .NET-compatible Windows Forms development environment

---

## Running the Project | Projeyi Çalıştırma

Open the solution file in Visual Studio:

Visual Studio üzerinden solution dosyasını açın:

```text
WinFormGiris.sln
```

Then restore the required dependencies if necessary and build the solution.

Ardından gerekli bağımlılıkları yükleyin ve projeyi derleyin.

---

## Repository Access | Repository Erişimi

This repository is private.

Bu repository gizlidir.

Access is limited to authorized GitHub users who have been added as collaborators.

Erişim yalnızca collaborator olarak eklenen yetkili GitHub kullanıcılarıyla sınırlıdır.

Project files, source code, and implementation details should not be redistributed without permission.

Proje dosyaları, kaynak kodları ve uygulama detayları izin alınmadan yeniden paylaşılmamalıdır.

---

## Security and Confidentiality | Güvenlik ve Gizlilik

This repository intentionally does not document competition-specific communication parameters, packet structures, telemetry protocols, operational procedures, or other sensitive implementation details.

Bu repository içerisinde yarışmaya özel haberleşme parametreleri, paket yapıları, telemetri protokolleri, operasyon prosedürleri veya diğer hassas uygulama detayları bilinçli olarak dokümante edilmemektedir.

---

## Development Status | Geliştirme Durumu

The project is under active development and may continue to evolve as the ground station architecture and visualization requirements are improved.

Proje aktif geliştirme sürecindedir. Yer istasyonu mimarisi ve görselleştirme ihtiyaçları geliştikçe yazılım da güncellenmeye devam edecektir.

---

## Team | Takım

Developed for:

**AZAK Rocket Team**

AZAK Roket Takımı için geliştirilmiştir.

---

## Technologies

`C#` `.NET` `Windows Forms` `Visual Studio` `Telemetry` `3D Visualization` `Ground Station`
