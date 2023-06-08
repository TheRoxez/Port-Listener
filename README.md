# PLMSQL Servisi

PLMSQL, TCP üzerinden gelen verileri MySQL veritabanına yazan bir Windows servisidir.

## Kullanım

Servis başlatıldığında, belirtilen TCP portundan gelen verileri dinlemeye başlar ve her bir veriyi MySQL veritabanına ekler. Veriler ASCII formatında beklenir ve veritabanına eklemek için MySQL bağlantısı kullanılır.

## Veritabanı Bağlantısı

Veritabanı bağlantısı için aşağıdaki parametreleri kontrol edin ve düzenleyin:

- Sunucu: localhost
- Kullanıcı adı: root
- Veritabanı: test
- Şifre: 6660

MySQL bağlantı parametrelerini düzenlemek için `HandleTcpClientConnected` metodundaki `connectionString` değişkenini güncelleyin.

## Loglama

Servis, çalışması sırasında bilgileri bir log dosyasına kaydeder. Log dosyası, programın çalıştığı dizindeki "Logs" klasöründe yer alır ve "servis.txt" adıyla kaydedilir.

## Gereksinimler

- .NET Framework 4.0 veya daha yeni bir sürümü gerektirir.
- İşletim sistemi: Windows

## Servis İşlemleri

Servisi yönetmek için aşağıdaki komutları kullanabilirsiniz:

- Servisi başlatmak için: `sc start PLMSQL`
- Servisi durdurmak için: `sc stop PLMSQL`
- Servisi silmek için: `sc delete PLMSQL`

Notlar:
- Servis dosyasının uygun izinlere sahip olduğundan emin olun.
- Servis başlatıldığında, gelen veriler MySQL veritabanına yazılır ve işlem kaydedilir. Hata ayıklama yapmak ve gerekli izlemeleri yapmak için log dosyasını kontrol edin.
- Gerekli ayarlamaları yaparak veya ihtiyaçlarınıza göre değiştirerek kodu uyarlayabilirsiniz.

## Lisans

Bu servis MIT Lisansı ile lisanslanmıştır. Daha fazla bilgi için "LICENSE" dosyasını inceleyin.
