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

"💡 SEO Odaklı Blog ve Rehber Modülü: Müşterilere araç kiralama süreçleri, elektrikli araç teknolojileri gibi konularda rehberlik eden içerik sayfası. Ziyaretçilerin sitede geçirdiği süreyi artıran, platformun arama motoru optimizasyonunu (SEO) güçlendiren ve markaya kurumsal bir kimlik katan modern bir okuma deneyimi (UI) sunar."
<img width="1600" height="677" alt="WhatsApp Image 2026-04-24 at 02 15 30" src="https://github.com/user-attachments/assets/33612dbf-fe38-403e-b6f0-17bb67306dfc" />

"➕ Yeni Kullanıcı Kaydı ve Veritabanı İşlemleri (Create): Sistem mimarisindeki Kullanıcı sınıfından yeni bir nesne (object) türetilmesini sağlayan kayıt (Register) arayüzü. Ziyaretçilerin platforma üye olarak araç kiralama yetkisi kazanabilmesi için; ad, e-posta ve şifre gibi temel bilgilerin alınarak veritabanına güvenli bir şekilde kaydedildiği (Create) başlangıç adımıdır."
<img width="1355" height="670" alt="WhatsApp Image 2026-04-24 at 02 16 21" src="https://github.com/user-attachments/assets/eafbb092-35ac-43ac-b114-c65a9a35f00c" />

"⚙️ RESTful Web API ve Swagger Dokümantasyonu: Platformun ön yüzü (Frontend) ile veritabanı arasındaki iletişimi sağlayan güçlü Backend mimarisi. Sistemdeki varlıkların yönetimi için standart CRUD operasyonları HTTP metotlarıyla (GET, POST, PUT, DELETE) yapılandırılmış ve Swagger UI kullanılarak endüstri standartlarında dokümante edilmiştir. Bu yapı, projenin modüler ve dış entegrasyonlara (örneğin ileride eklenebilecek bir mobil uygulamaya) açık olduğunu gösterir."
<img width="1600" height="889" alt="WhatsApp Image 2026-04-24 at 02 17 38" src="https://github.com/user-attachments/assets/a371f77f-a87e-428a-83a3-98426d42e10e" />

"🗺️ Harita API Entegrasyonu ve İletişim Modülü: Kullanıcıların destek taleplerini sisteme iletebildiği dinamik iletişim arayüzü. Form üzerinden alınan veriler (Ad, E-posta, Mesaj) arka planda (Backend) işlenerek veritabanına aktarılır. Alt kısımdaki Leaflet tabanlı interaktif harita entegrasyonu (Third-Party API Integration), projenin dış servislerle başarılı ve asenkron bir şekilde haberleşebildiğini gösterir."
<img width="1600" height="786" alt="WhatsApp Image 2026-04-24 at 02 16 04" src="https://github.com/user-attachments/assets/bf12eb49-812d-469a-923d-e7a030981d10" />

"🎯 Satış Odaklı Etkileşim (CTA) Ekranı: Kullanıcıların dikkatini çekmek ve rezervasyon dönüşüm oranlarını (conversion rate) artırmak amacıyla tasarlanmış kampanya vitrini. Estetik tipografisi, akıcı kaydırma (Carousel) animasyonları ve belirgin eyleme çağrı butonuyla sistemin sadece işlevsel değil, aynı zamanda ticari bir ürün (SaaS) mantığıyla tasarlandığını kanıtlar."
<img width="1600" height="494" alt="WhatsApp Image 2026-04-24 at 02 11 10" src="https://github.com/user-attachments/assets/17240528-396d-422b-bf7a-13c883ac778b" />

"🚀 Dinamik Karşılama Ekranı (Landing Page) ve Merkezi Navigasyon: Platformun vitrini niteliğindeki ana giriş arayüzü. Üst kısımdaki akıllı navigasyon çubuğu (Navbar), kullanıcıları ASP.NET Core mimarisi üzerinde tanımlanmış olan ilgili Controller ve sayfalara (Araçlar, Fiyatlar, İletişim vb.) yönlendirir (Routing). Sağ üst köşede yer alan butonlar ise kimlik doğrulama (Authentication) sürecinin başlangıç noktalarıdır."
<img width="1600" height="791" alt="WhatsApp Image 2026-04-24 at 02 10 44" src="https://github.com/user-attachments/assets/2a9923f7-25ca-4c99-8127-44b5513df06f" />

"🧭 Hızlı Rezervasyon ve Kullanıcı Yönlendirmesi: Ziyaretçiyi karmaşaya boğmadan doğrudan amaca (araç kiralamaya) yönlendiren akıllı arama formu. Sol taraftaki koyu renkli form modülü dikkatleri üzerine çekerken, sağ taraftaki görsel ve metinsel rehberlik (Konum Seç > Araçları Gör > Kirala) kullanıcı deneyimini pürüzsüz hale getirerek işlemlerin hızla tamamlanmasını sağlar."
<img width="1600" height="654" alt="WhatsApp Image 2026-04-24 at 02 10 58" src="https://github.com/user-attachments/assets/9b5df7f4-252e-43d5-8349-9bf5414576f8" />

"🚗 Dinamik Araç Vitrini ve Veri Bağlama (Data Binding): Veritabanında kayıtlı Araç nesnelerinin marka, model ve günlük kiralama bedelleriyle birlikte listelendiği dinamik arayüz modülü. Sistem, öne çıkan araçları arka plandan çekerek slider (kaydırılabilir) formatında kullanıcıya sunar. Kartlar üzerinde yer alan 'Hemen Kirala' butonu, seçilen aracın benzersiz kimliğini (ID) parametre olarak alıp kullanıcıyı doğrudan kiralama akışına yönlendirir."
<img width="1600" height="523" alt="WhatsApp Image 2026-04-24 at 02 11 26" src="https://github.com/user-attachments/assets/193dbad4-377a-4106-861c-73bc753baea2" />

"📖 Marka Hikayesi ve Asimetrik Tasarım (Split Layout): Ziyaretçilere CarBook markasının 15 yıllık sektörel tecrübesini ve genişleyen hizmet ağını anlatan kurumsal bilgilendirme ekranı. Koyu ve açık temaların bir arada kullanıldığı modern bölünmüş ekran tasarımı okunabilirliği maksimize ederken, seçilen görsel ögelerle markanın profesyonel duruşu desteklenmiştir."
<img width="1600" height="757" alt="WhatsApp Image 2026-04-24 at 02 11 52" src="https://github.com/user-attachments/assets/f91a09de-b289-4ec2-aff4-6a0b4c8a8439" />

"💬 Dinamik Geri Bildirim Modülü (Data Binding): Veritabanında yer alan müşteri değerlendirmelerinin (Testimonial entity) arayüze dinamik olarak yansıtıldığı (Read işlemi) modül. Bu yapı, statik metinler yerine arka plandan çekilen kullanıcı adı, meslek, profil görseli ve yorum verilerinin bir kaydırıcı (Slider/Carousel) bileşeniyle sorunsuz bir şekilde entegre çalıştığını gösterir."
<img width="1600" height="712" alt="WhatsApp Image 2026-04-24 at 02 12 31" src="https://github.com/user-attachments/assets/87b51fae-2708-46ed-80a7-7285953dd7a8" />

"⚙️ Modüler Hizmet Bileşeni (ViewComponent/Partial View): ASP.NET Core mimarisinin modüler yapısına uygun olarak tasarlanmış bilgilendirme ekranı. Sistemin ana işlevi olan araç kiralama operasyonuna ek olarak sunulan VIP Kiralama, Havalimanı Transferi gibi yan hizmetlerin (Services) temiz bir arayüzle kullanıcıya aktarıldığı, projenin genişletilebilir (scalable) vizyonunu kanıtlayan arayüz bileşenidir."
<img width="1600" height="523" alt="WhatsApp Image 2026-04-24 at 02 12 07" src="https://github.com/user-attachments/assets/2d57663e-4713-497e-a66d-84fa91452bb9" />

"📈 SEO Destekli Ana Sayfa Blog Modülü: Ziyaretçilerin sitede geçirdiği süreyi (session duration) artırmak ve arama motoru optimizasyonunu (SEO) güçlendirmek için tasarlanmış güncel içerik vitrini. Elektrikli araç rehberi veya kiralama ipuçları gibi katma değerli içerikler, estetik kart tasarımlarıyla sunularak marka otoritesi pekiştirilmiştir."
<img width="1600" height="806" alt="WhatsApp Image 2026-04-24 at 02 12 41" src="https://github.com/user-attachments/assets/2e8e4f66-b596-419b-87cf-8c13f2a89cab" />

"🎯 Dinamik Yönlendirme (Routing) ve Eyleme Çağrı Modülü: Sistemin farklı sayfalarında kullanıcıyı tekrar ana kiralama akışına çekmek için tasarlanmış bağımsız (reusable) UI bileşeni. Arka planda 'Hemen Rezervasyon Yap' butonu, ASP.NET Core mimarisindeki ilgili Controller'a doğrudan yönlendirme yaparak dönüşüm hunisini (sales funnel) aktif tutar ve sepet terketme oranlarını düşürür."
<img width="1600" height="651" alt="WhatsApp Image 2026-04-24 at 02 12 19" src="https://github.com/user-attachments/assets/4efe072e-cbd5-4096-9d7c-574e3b9afa1f" />

"🧩 Merkezi Alt Bilgi (Footer) ve Partial View Entegrasyonu: Sistemin tüm arayüzlerinde ortak olarak render edilen, modüler (reusable) alt navigasyon bileşeni. ASP.NET Core mimarisindeki ViewComponent veya Partial View mantığıyla kurgulanmış bu yapı; kullanıcıları MVC routing (yönlendirme) altyapısı üzerinden iletişim, müşteri desteği ve yasal metin (Şartlar & Koşullar) sayfalarına sorunsuz bir şekilde bağlar."
<img width="1600" height="381" alt="WhatsApp Image 2026-04-24 at 02 13 13" src="https://github.com/user-attachments/assets/7989c14d-bb9c-4564-9f87-c7b3ab40d183" />

"📊 Dinamik İstatistik ve Veri Toplama (Data Aggregation) Modülü: Veritabanında yer alan tablolar üzerinde anlık (real-time) sayım sorgularının (LINQ Count() veya SQL COUNT()) çalıştırılarak ön yüze yansıtıldığı arayüz bileşeni. Sistemdeki toplam araç, aktif lokasyon, marka çeşitliliği ve elektrikli araç sayısı statik olarak girilmez; sistemin güncel durumuna göre arka planda hesaplanarak (Data Binding) ziyaretçiye sunulur."
<img width="1600" height="292" alt="WhatsApp Image 2026-04-24 at 02 13 04" src="https://github.com/user-attachments/assets/66a1dde6-ffa2-437b-a437-aa14a31d32a7" />





---

