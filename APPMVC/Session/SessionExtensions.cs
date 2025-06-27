using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace APPMVC.Session
{
    public static class SessionExtensions
    {
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Thiết lập quy tắc đặt tên
            WriteIndented = true // Căn chỉnh JSON cho dễ đọc (tuỳ chọn)
        };

        public static void SetObject<T>(this ISession session, string key, T value)
        {
            if (value == null)
            {
                session.Remove(key); // Xóa key nếu value là null
                return;
            }

            var json = JsonSerializer.Serialize(value, jsonOptions);
            session.SetString(key, json);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var json = session.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                return default; // Trả về giá trị mặc định nếu không có dữ liệu
            }

            try
            {
                return JsonSerializer.Deserialize<T>(json, jsonOptions);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Deserialize Error: {ex.Message}");
                return default; // Trả về giá trị mặc định nếu có lỗi
            }
        }
    }
}