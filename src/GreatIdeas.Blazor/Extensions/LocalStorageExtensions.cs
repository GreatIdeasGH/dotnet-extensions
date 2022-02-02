using Blazored.SessionStorage;

namespace GreatIdeas.Blazor.Extensions
{
    public static class LocalStorageExtensions
    {
        public static async ValueTask SetAccessToken(this ISessionStorageService storageStorage, string accessToken)
        {
            await storageStorage.SetItemAsync("accessToken", accessToken);
        }
        
        public static async ValueTask<string> GetAccessToken(this ISessionStorageService storageStorage)
        {
           var token = await storageStorage.GetItemAsync<string>("accessToken");
           return token;
        }
        
        public static async ValueTask SetRefreshToken(this ISessionStorageService storageStorage, string refreshToken)
        {
            await storageStorage.SetItemAsync("refreshToken", refreshToken);
        }
        
        public static async ValueTask<string> GetRefreshToken(this ISessionStorageService storageStorage)
        {
            var token = await storageStorage.GetItemAsync<string>("refreshToken");
            return token;
        }
        
       
        public static async ValueTask SetPhoneToken(this ISessionStorageService storageStorage, string phoneToken)
        {
            await storageStorage.SetItemAsync("phoneToken", phoneToken);
        }
        
        public static async ValueTask<string> GetPhoneToken(this ISessionStorageService storageStorage)
        {
            var token = await storageStorage.GetItemAsync<string>("phoneToken");
            return token;
        }
        
        public static async ValueTask SetVerifyToken(this ISessionStorageService storageStorage, string token)
        {
            await storageStorage.SetItemAsync("verifyToken", token);
        }
        
        public static async ValueTask<string> GetVerifyToken(this ISessionStorageService storageStorage)
        {
            var token = await storageStorage.GetItemAsync<string>("verifyToken");
            return token;
        }
        
        public static async ValueTask SetUsername(this ISessionStorageService storageStorage, string token)
        {
            await storageStorage.SetItemAsync("username", token);
        }
        
        public static async ValueTask<string> GetUsername(this ISessionStorageService storageStorage)
        {
            var token = await storageStorage.GetItemAsync<string>("username");
            return token;
        }
        
        public static async ValueTask SetUserId(this ISessionStorageService storageStorage, string token)
        {
            await storageStorage.SetItemAsync("userid", token);
        }
        
        public static async ValueTask<string> GetUserId(this ISessionStorageService storageStorage)
        {
            var token = await storageStorage.GetItemAsync<string>("userid");
            return token;
        }
    }
}