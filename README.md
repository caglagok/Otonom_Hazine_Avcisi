# Otonom Hazine Avcisi
Otonom Hazine Avcısı projesinde, otonom hareket eden bir karakterin, içerisinde çeşitli hazineler ve engeller bulunan bir harita üzerindeki hazineleri topladığı bir oyun tasarladık. Oyunun teması yarısı kış yarısı yaz olacak şekilde ayarlandı. Nesneler hiyerarşik bir düzende yerleştirilmektedir. Oyunda amaç, karakterin tüm hazineleri en kısa sürede toplamasını sağlamaktır. Bunun için en kısa yol algoritmaları kullandık.

C# dilinde yazdığımız bu projeyi Visual Studio Windows Form’da geliştirdik. Projenin isterlerine göre karakterimiz ve diğer tüm nesneler harita her yeni oluşturulduğunda random yerlere atanmaktadır. Oyun başlatıldığında karakterimiz en kısa yolu kullanarak sandıkları toplamaktadır. Her sandık toplandığında listBox1 de konumları yazdırılmaktadır. Bütün sandıklar toplandığında oyun sonlanmaktadır. 
Projenin ana isterleri şunlardır: 
   - Oyun haritasını istenen ölçüde başlatma, 
   - Yeni harita oluştur ve Başlat butonlarının olması, 
   - Her yeniden başlatıldığında yarısı yaz yarısı kış temalı harita oluşturma, 
   - Bütün nesnelerin rastgele konumlarda yerleştirilmesi, 
   - Nesnelerin rastgele yerleşimi sırasında çakışmaması, 
   - Kuş ve arının hareket edeceği yerlerin kırmızı renkli olması 
   - Karakter hareket ettiğinde geçtiği yolların yeşil renkli olması 
   - Karakterin sandıkları en kısa yol algoritmasıyla bulması, 
   - Sandıklar toplandığında konumlarının yazdırılması.
     
![girisekranı](https://github.com/caglagok/OtonomHazineAvcisi/assets/114026286/c79eacd1-710c-4d95-89e5-1b3cb09f4ffa)

Projemizi çalıştırdığımızda oyun penceresine geçebilmek için bir giriş ekranı açılıyor. Bu ekranda yer alan Maceraya Atıl butonuyla asıl oyun ekranına geçiyoruz.

Açılan yeni ekranda haritanın boyutunu kullanıcının girmesi gerekiyor. Harita boyutu girildikten sonra Yeni Harita Oluştur butonuna tıklanıp oyun için bir harita oluşturuluyor. 

![3](https://github.com/caglagok/OtonomHazineAvcisi/assets/114026286/62eb946c-93b0-41e6-b4ca-6d664421de64)

Kullanıcının girdiği boyut ölçüsünde oyun haritası oluşturulup nesnelerin tamamı çakışmayacak şekilde random atanma yapılıyor. Ardından Başlat butonuna basıldığında oyun başlamakta olup karakter en kısa yol algoritmasıyla sandıkları bulmaya başlıyor. 
Karakter otonom hareketini tamamladığında oyun bitiyor.
