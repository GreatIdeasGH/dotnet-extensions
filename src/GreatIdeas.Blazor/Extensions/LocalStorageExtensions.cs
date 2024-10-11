using Blazored.LocalStorage;

namespace GreatIdeas.Blazor.Extensions
{
    public static class LocalStorageExtensions
    {
        /// <summary>
        /// Set the access token in local storage
        /// </summary>
        /// <param name="storageStorage"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static async ValueTask SetAccessToken(this ILocalStorageService storageStorage, string accessToken)
        {
            await storageStorage.SetItemAsync("accessToken", accessToken);
        }

        /// <summary>
        /// Get the access token from local storage
        /// </summary>
        /// <param name="storageStorage"></param>
        /// <returns></returns>
        public static async ValueTask<string> GetAccessToken(this ILocalStorageService storageStorage)
        {
            var token = await storageStorage.GetItemAsync<string>("accessToken");
            return token;
        }

        /// <summary>
        /// Set the refresh token in local storage
        /// </summary>
        /// <param name="storageStorage"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public static async ValueTask SetRefreshToken(this ILocalStorageService storageStorage, string refreshToken)
        {
            await storageStorage.SetItemAsync("refreshToken", refreshToken);
        }

        /// <summary>
        /// Get the refresh token from local storage
        /// </summary>
        /// <param name="storageStorage"></param>
        /// <returns></returns>
        public static async ValueTask<string> GetRefreshToken(this ILocalStorageService storageStorage)
        {
            var token = await storageStorage.GetItemAsync<string>("refreshToken");
            return token;
        }

        /// <summary>
        /// Set the phone token in local storage
        /// </summary>
        /// <param name="storageStorage"></param>
        /// <param name="phoneToken"></param>
        /// <returns></returns>
        public static async ValueTask SetPhoneToken(this ILocalStorageService storageStorage, string phoneToken)
        {
            await storageStorage.SetItemAsync("phoneToken", phoneToken);
        }

        /// <summary>
        /// Get the phone token from local storage
        /// </summary>
        /// <param name="storageStorage"></param>
        /// <returns></returns>
        public static async ValueTask<string> GetPhoneToken(this ILocalStorageService storageStorage)
        {
            var token = await storageStorage.GetItemAsync<string>("phoneToken");
            return token;
        }

        /// <summary>
        /// Set a verify token from local storage
        /// </summary>
        /// <param name="storageStorage"></param>
        /// <param name="token"></param>
        /// <returns></returns>
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