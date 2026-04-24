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
* **Metotlar (Methods):**
  * `arac_durumu_guncelle()`: Aracın müsaitlik durumunu (kirada/boşta) değiştirir.
  * `kilometre_guncelle()`: Kiralama bitiminde aracın yeni kilometresini sisteme kaydeder.

### 2. `Kullanıcı` Sınıfı
Sisteme kayıtlı olan ve araç kiralama işlemi gerçekleştiren müşterilerin yönetildiği sınıftır.
* **Özellikler (Attributes):**
  * `kullanici_id`: Kullanıcının sistemdeki benzersiz numarası.
  * `ad`: Kullanıcının ad ve soyad bilgisi.
  * `ehliyet_no`: Araç kiralama şartı olan ehliyetin kayıt numarası.


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
"📝 Kapsamlı Rezervasyon Formu ve Nesne Oluşturma: Sistem mimarisindeki Kiralama sınıfının (class) somutlaştığı (instantiate) ana veri giriş arayüzü. Kullanıcının bir önceki ekranda seçtiği araç bilgisi forma otomatik yansıtılır (Data Binding). Formda yer alan 'Yaş' ve 'Ehliyet Yılı' gibi kritik alanlar, sistemin arka planındaki kiralama iş kurallarının (segment bazlı yaş/ehliyet kısıtlamaları) kontrol edilmesi ve kiralama_baslat() metodunun güvenli bir şekilde tetiklenmesi için tasarlanmıştır."
<img width="650" height="899" alt="WhatsApp Image 2026-04-24 at 02 14 09" src="https://github.com/user-attachments/assets/9fbefa39-4fdd-4a2c-bcf2-9faa9ffc67aa" />

"💰 Dinamik Fiyatlandırma ve Araç Paketleri: Veritabanında kayıtlı Araç nesnelerinin günlük, haftalık ve aylık kiralama opsiyonlarıyla listelendiği detaylı fiyatlandırma ekranı. Sistem, her bir aracın yakıt/vites gibi teknik özelliklerini (attributes) ve çoklu fiyat tarifelerini dinamik olarak çekerken, 'Hemen Kirala' butonu üzerinden seçilen araç verisini doğrudan Kiralama formuna (Data Binding) aktarır."
<img width="1600" height="795" alt="WhatsApp Image 2026-04-24 at 02 13 34" src="https://github.com/user-attachments/assets/ba61eac3-dae9-4c37-8137-8975102932af" />

"🔐 Kullanıcı Kimlik Doğrulama (Authentication): Platforma kayıtlı müşterilerin (veya yöneticilerin) güvenli bir şekilde hesaplarına erişim sağladığı giriş arayüzü. Arka planda Kullanıcı sınıfı ile entegre çalışan bu modül; oturum yönetimi (Session Management), şifreleme ve yetkilendirme süreçlerinin başlatıldığı kritik güvenlik katmanıdır."
<img width="1525" height="825" alt="WhatsApp Image 2026-04-24 at 02 16 29" src="https://github.com/user-attachments/assets/573da86b-bffb-4c82-96c7-477ac33affbf" />


<img width="1600" height="677" alt="WhatsApp Image 2026-04-24 at 02 15 30" src="https://github.com/user-attachments/assets/33612dbf-fe38-403e-b6f0-17bb67306dfc" />
<img width="1355" height="670" alt="WhatsApp Image 2026-04-24 at 02 16 21" src="https://github.com/user-attachments/assets/eafbb092-35ac-43ac-b114-c65a9a35f00c" />
<img width="1600" height="889" alt="WhatsApp Image 2026-04-24 at 02 17 38" src="https://github.com/user-attachments/assets/a371f77f-a87e-428a-83a3-98426d42e10e" />
<img width="1600" height="786" alt="WhatsApp Image 2026-04-24 at 02 16 04" src="https://github.com/user-attachments/assets/bf12eb49-812d-469a-923d-e7a030981d10" />
<img width="1600" height="494" alt="WhatsApp Image 2026-04-24 at 02 11 10" src="https://github.com/user-attachments/assets/17240528-396d-422b-bf7a-13c883ac778b" />
<img width="1600" height="791" alt="WhatsApp Image 2026-04-24 at 02 10 44" src="https://github.com/user-attachments/assets/2a9923f7-25ca-4c99-8127-44b5513df06f" />
<img width="1600" height="654" alt="WhatsApp Image 2026-04-24 at 02 10 58" src="https://github.com/user-attachments/assets/9b5df7f4-252e-43d5-8349-9bf5414576f8" />
<img width="1600" height="523" alt="WhatsApp Image 2026-04-24 at 02 11 26" src="https://github.com/user-attachments/assets/193dbad4-377a-4106-861c-73bc753baea2" />
<img width="1600" height="757" alt="WhatsApp Image 2026-04-24 at 02 11 52" src="https://github.com/user-attachments/assets/f91a09de-b289-4ec2-aff4-6a0b4c8a8439" />
<img width="1600" height="712" alt="WhatsApp Image 2026-04-24 at 02 12 31" src="https://github.com/user-attachments/assets/87b51fae-2708-46ed-80a7-7285953dd7a8" />
<img width="1600" height="523" alt="WhatsApp Image 2026-04-24 at 02 12 07" src="https://github.com/user-attachments/assets/2d57663e-4713-497e-a66d-84fa91452bb9" />
<img width="1600" height="806" alt="WhatsApp Image 2026-04-24 at 02 12 41" src="https://github.com/user-attachments/assets/2e8e4f66-b596-419b-87cf-8c13f2a89cab" />
<img width="1600" height="651" alt="WhatsApp Image 2026-04-24 at 02 12 19" src="https://github.com/user-attachments/assets/4efe072e-cbd5-4096-9d7c-574e3b9afa1f" />
<img width="1600" height="381" alt="WhatsApp Image 2026-04-24 at 02 13 13" src="https://github.com/user-attachments/assets/7989c14d-bb9c-4564-9f87-c7b3ab40d183" />
<img width="1600" height="292" alt="WhatsApp Image 2026-04-24 at 02 13 04" src="https://github.com/user-attachments/assets/66a1dde6-ffa2-437b-a437-aa14a31d32a7" />





---

