namespace Chat.Core.Messages
{
    
    public static class DbMessage
    {
        public static string DeleteError => "{0} tablosu silmeye çalıştığınız kayıta bağımlı. Bu kayıt silinemez.";
        public static string IdentityInsertError => "Girmeye çalıştığınız kayıt tabloda mevcut.";
        public static string DataInserted => "Kayıt Eklendi";
        public static string DataNotFound => "Kayıt bulunamadı";
        public static string DataUpdated => "Güncellendi";
        public static string DataRemoved => "Silindi";
        public static string AlredyIsComplated => "Zaten tamamlanmış.";
    }
}
