# 🚗 Proje 1: Araç Paylaşım Sistemi (Car Sharing System)

Bu proje, kullanıcıların sistemdeki uygun araçları saatlik olarak kiralayabildiği dinamik bir **Araç Paylaşım Sistemi** simülasyonudur. Sistem analizi ve Nesne Yönelimli Programlama (OOP) prensipleri merkeze alınarak; araç yönetimi, kullanıcı doğrulama ve kiralama süreçlerinin güvenli ve modüler bir yapıda çalışması hedeflenmiştir.

## 🏗️ Sistem Mimarisi ve Sınıf Yapısı

Proje, gerçek dünya senaryolarını kod ortamına aktarmak amacıyla birbiriyle ilişkili üç temel sınıf üzerine inşa edilmiştir:

### 1. `Araç` Sınıfı
Filoda bulunan araçların güncel durumlarını, teknik bilgilerini ve uygunluk statülerini yöneten ana sınıftır.
* **Özellikler (Attributes):**
  * `arac_id`: Aracı sistemde benzersiz kılan kimlik/plaka numarası.
  * `marka` & `model`: Aracın marka ve model bilgileri.
  * `kilometre`: Aracın güncel kilometre bilgisi.
  * `musait_mi`: Aracın o an kiralanabilir durumda olup olmadığını tutan boolean (mantıksal) değer.
* **Metotlar (Methods):**
  * `arac_durumu_guncelle()`: Aracın müsaitlik durumunu (kirada/boşta) değiştirir.
  * `kilometre_guncelle()`: Kiralama bitiminde aracın yeni kilometresini sisteme kaydeder.

### 2. `Kullanıcı` Sınıfı
Sisteme kayıtlı olan ve araç kiralama işlemi gerçekleştiren müşterilerin yönetildiği sınıftır.
* **Özellikler (Attributes):**
  * `kullanici_id`: Kullanıcının sistemdeki benzersiz numarası.
  * `ad`: Kullanıcının ad ve soyad bilgisi.
  * `ehliyet_no`: Araç kiralama şartı olan ehliyetin kayıt numarası.
* **Metotlar (Methods):**
  * `kiralama_gecmisi()`: Kullanıcının daha önce yaptığı tüm kiralama işlemlerini ve detaylarını listeler.

### 3. `Kiralama` Sınıfı
Araç ve Kullanıcı sınıflarını birbirine bağlayan, kiralama işleminin tüm operasyonel sürecini (başlangıç, bitiş ve süre) yöneten işlem sınıfıdır.
* **Özellikler (Attributes):**
  * `kiralama_id`: İşleme ait benzersiz fatura/kayıt numarası.
  * `arac`: Kiralanan `Araç` nesnesinin referansı.
  * `kullanici`: İşlemi gerçekleştiren `Kullanıcı` nesnesinin referansı.
  * `baslangic_saati` & `bitis_saati`: Kiralamanın başladığı ve sona erdiği zaman damgaları (Timestamp/DateTime).
* **Metotlar (Methods):**
  * `kiralama_baslat()`: Gerekli kontrolleri (araç müsait mi?) yaparak kiralama işlemini başlatır ve aracın durumunu günceller.
  * `kiralama_bitir()`: İşlemi sonlandırır, süreyi hesaplar ve aracın yeni kilometresini kaydederek tekrar müsait hale getirir.
  * `kiralama_bilgisi()`: Aktif veya tamamlanmış bir kiralama işleminin tüm detaylarını ekrana yazdırır.

---

## 📸 Proje Ekran Görüntüleri




---

