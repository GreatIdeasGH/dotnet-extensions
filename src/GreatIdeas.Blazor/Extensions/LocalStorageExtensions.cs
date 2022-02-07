using Blazored.LocalStorage;

namespace GreatIdeas.Blazor.Extensions
{
    public static class LocalStorageExtensions
    {
        public static async ValueTask SetAccessToken(this ILocalStorageService storageStorage, string accessToken)
        {
            await storageStorage.SetItemAsync("accessToken", accessToken);
        }

        public static async ValueTask<string> GetAccessToken(this ILocalStorageService storageStorage)
        {
            var token = await storageStorage.GetItemAsync<string>("accessToken");
            return token;
        }

        public static async ValueTask SetRefreshToken(this ILocalStorageService storageStorage, string refreshToken)
        {
            await storageStorage.SetItemAsync("refreshToken", refreshToken);
        }

        public static async ValueTask<string> GetRefreshToken(this ILocalStorageService storageStorage)
        {
            var token = await storageStorage.GetItemAsync<string>("refreshToken");
            return token;
        }


        public static async ValueTask SetPhoneToken(this ILocalStorageService storageStorage, string phoneToken)
        {
            await storageStorage.SetItemAsync("phoneToken", phoneToken);
        }

        public static async ValueTask<string> GetPhoneToken(this ILocalStorageService storageStorage)
        {
            var token = await storageStorage.GetItemAsync<string>("phoneToken");
            return token;
        }

        public static async ValueTask SetVerifyToken(this ILocalStorageService storageStorage, string token)
        {
            await storageStorage.SetItemAsync("verifyToken", token);
        }

        public static async ValueTask<string> GetVerifyToken(this ILocalStorageService storageStorage)
        {
            var token = await storageStorage.GetItemAsync<string>("verifyToken");
            return token;
        }

        public static async ValueTask SetUsername(this ILocalStorageService storageStorage, string token)
        {
            await storageStorage.SetItemAsync("username", token);
        }

        public static async ValueTask<string> GetUsername(this ILocalStorageService storageStorage)
        {
            var token = await storageStorage.GetItemAsync<string>("username");
            return token;
        }

        public static async ValueTask SetUserId(this ILocalStorageService storageStorage, string token)
        {
            await storageStorage.SetItemAsync("userid", token);
        }

        public static async ValueTask<string> GetUserId(this ILocalStorageService storageStorage)
        {
            var token = await storageStorage.GetItemAsync<string>("userid");
            return token;
        }
    }
}