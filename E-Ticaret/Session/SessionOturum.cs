using Newtonsoft.Json;

namespace E_Ticaret.Session
{
    public static class SessionOturum
    {
        // Oturumda JSON verisi olarak bir nesne saklamak için genişletme metodu
        public static void SetJson(this ISession session, string key, object value)
        {
            // Nesneyi JSON formatına dönüştürüp oturumda saklar
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Oturumdan JSON verisi olarak saklanan bir nesneyi almak için genişletme metodu
        public static T GetJson<T>(this ISession session, string key)
        {
            // Oturumdan JSON verisini alır
            var sessionData = session.GetString(key);

            // JSON verisi null ise varsayılan değeri döner, değilse JSON verisini nesneye dönüştürüp döner
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}

/*
 * Açıklamalar:
•	SetJson: Bu genişletme metodu, bir nesneyi JSON formatına dönüştürerek oturumda saklar.
•	session.SetString(key, JsonConvert.SerializeObject(value)): Verilen nesneyi JSON formatına dönüştürür ve oturumda belirtilen anahtar ile saklar.
•	GetJson: Bu genişletme metodu, oturumdan JSON formatında saklanan bir nesneyi alır ve orijinal nesneye dönüştürür.
•	var sessionData = session.GetString(key): Oturumdan belirtilen anahtar ile JSON verisini alır.
•	return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData): JSON verisi null ise varsayılan değeri döner, değilse JSON verisini orijinal nesneye dönüştürüp döner.
Bu metotlar, oturumda nesneleri JSON formatında saklamak ve almak için kullanılır. Bu sayede, sepet gibi karmaşık nesneleri oturumda kolayca saklayabilir ve alabilirsiniz.
*/