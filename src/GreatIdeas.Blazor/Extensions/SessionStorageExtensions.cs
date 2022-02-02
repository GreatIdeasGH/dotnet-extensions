using Blazored.SessionStorage;

namespace GreatIdeas.Blazor.Extensions
{
    public static class SessionStorageExtensions
    {
        public static async ValueTask SetAccessToken(this ISessionStorageService sessionStorage, string accessToken)
        {
            await sessionStorage.SetItemAsync("accessToken", accessToken);
        }
        
        public static async ValueTask<string> GetAccessToken(this ISessionStorageService sessionStorage)
        {
           var token = await sessionStorage.GetItemAsync<string>("accessToken");
           return token;
        }
        
        public static async ValueTask SetRefreshToken(this ISessionStorageService sessionStorage, string refreshToken)
        {
            await sessionStorage.SetItemAsync("refreshToken", refreshToken);
        }
        
        public static async ValueTask<string> GetRefreshToken(this ISessionStorageService sessionStorage)
        {
            var token = await sessionStorage.GetItemAsync<string>("refreshToken");
            return token;
        }
        
       
        public static async ValueTask SetPhoneToken(this ISessionStorageService sessionStorage, string phoneToken)
        {
            await sessionStorage.SetItemAsync("phoneToken", phoneToken);
        }
        
        public static async ValueTask<string> GetPhoneToken(this ISessionStorageService sessionStorage)
        {
            var token = await sessionStorage.GetItemAsync<string>("phoneToken");
            return token;
        }
        
        public static async ValueTask SetVerifyToken(this ISessionStorageService sessionStorage, string token)
        {
            await sessionStorage.SetItemAsync("verifyToken", token);
        }
        
        public static async ValueTask<string> GetVerifyToken(this ISessionStorageService sessionStorage)
        {
            var token = await sessionStorage.GetItemAsync<string>("verifyToken");
            return token;
        }
        
        public static async ValueTask SetUsername(this ISessionStorageService sessionStorage, string token)
        {
            await sessionStorage.SetItemAsync("username", token);
        }
        
        public static async ValueTask<string> GetUsername(this ISessionStorageService sessionStorage)
        {
            var token = await sessionStorage.GetItemAsync<string>("username");
            return token;
        }
        
        public static async ValueTask SetUserId(this ISessionStorageService sessionStorage, string token)
        {
            await sessionStorage.SetItemAsync("userid", token);
        }
        
        public static async ValueTask<string> GetUserId(this ISessionStorageService sessionStorage)
        {
            var token = await sessionStorage.GetItemAsync<string>("userid");
            return token;
        }
    }
}