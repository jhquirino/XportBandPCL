using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSHealthAPI.Contracts;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace XportBand.Model
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private static string MSHealthTokenTypeKey = $"{nameof(MSHealthToken)}_{nameof(MSHealthToken.TokenType)}";
        private static string MSHealthAccessTokenKey = $"{nameof(MSHealthToken)}_{nameof(MSHealthToken.AccessToken)}";
        private static string MSHealthCreationTimeKey = $"{nameof(MSHealthToken)}_{nameof(MSHealthToken.CreationTime)}";
        private static string MSHealthExpiresInKey = $"{nameof(MSHealthToken)}_{nameof(MSHealthToken.ExpiresIn)}";
        private static string MSHealthExpirationTimeKey = $"{nameof(MSHealthToken)}_{nameof(MSHealthToken.ExpirationTime)}";
        private static string MSHealthScopeKey = $"{nameof(MSHealthToken)}_{nameof(MSHealthToken.Scope)}";
        private static string MSHealthRefreshTokenKey = $"{nameof(MSHealthToken)}_{nameof(MSHealthToken.RefreshToken)}";

        #endregion

        private static MSHealthToken _msHealthToken;

        public static MSHealthToken MSHealthToken
        {
            get
            {
                if (_msHealthToken == null &&
                    AppSettings.Contains(MSHealthAccessTokenKey) &&
                    AppSettings.Contains(MSHealthCreationTimeKey) &&
                    AppSettings.Contains(MSHealthExpiresInKey))
                {
                    _msHealthToken = new MSHealthToken
                    {
                        TokenType = AppSettings.GetValueOrDefault(MSHealthTokenTypeKey, MSHealthToken.TOKEN_TYPE),
                        AccessToken = AppSettings.GetValueOrDefault(MSHealthAccessTokenKey, string.Empty),
                        CreationTime = AppSettings.GetValueOrDefault(MSHealthCreationTimeKey, DateTime.MinValue),
                        ExpiresIn = AppSettings.GetValueOrDefault(MSHealthExpiresInKey, 0L),
                        Scope = AppSettings.GetValueOrDefault(MSHealthScopeKey, string.Empty),
                        RefreshToken = AppSettings.GetValueOrDefault(MSHealthRefreshTokenKey, string.Empty),
                    };
                }
                return _msHealthToken; /*/ = new MSHealthToken
                {
                    TokenType = AppSettings.GetValueOrDefault(MSHealthTokenTypeKey, MSHealthToken.TOKEN_TYPE),
                    AccessToken = AppSettings.GetValueOrDefault(MSHealthAccessTokenKey, string.Empty),
                    CreationTime = AppSettings.GetValueOrDefault(MSHealthCreationTimeKey, DateTime.MinValue),
                    ExpiresIn = AppSettings.GetValueOrDefault(MSHealthExpiresInKey, 0L),
                    Scope = AppSettings.GetValueOrDefault(MSHealthScopeKey, string.Empty),
                    RefreshToken = AppSettings.GetValueOrDefault(MSHealthRefreshTokenKey, string.Empty),
                };//*/
            }
            set
            {
                if (_msHealthToken == value) return;
                if (value != null)
                {
                    AppSettings.AddOrUpdateValue(MSHealthTokenTypeKey, value.TokenType);
                    AppSettings.AddOrUpdateValue(MSHealthAccessTokenKey, value.AccessToken);
                    AppSettings.AddOrUpdateValue(MSHealthCreationTimeKey, value.CreationTime);
                    AppSettings.AddOrUpdateValue(MSHealthExpiresInKey, value.ExpiresIn);
                    AppSettings.AddOrUpdateValue(MSHealthExpirationTimeKey, value.ExpirationTime);
                    AppSettings.AddOrUpdateValue(MSHealthScopeKey, value.Scope);
                    AppSettings.AddOrUpdateValue(MSHealthRefreshTokenKey, value.RefreshToken);
                }
                else
                {
                    AppSettings.Remove(MSHealthTokenTypeKey);
                    AppSettings.Remove(MSHealthAccessTokenKey);
                    AppSettings.Remove(MSHealthCreationTimeKey);
                    AppSettings.Remove(MSHealthExpiresInKey);
                    AppSettings.Remove(MSHealthExpirationTimeKey);
                    AppSettings.Remove(MSHealthScopeKey);
                    AppSettings.Remove(MSHealthRefreshTokenKey);
                }
            }
        }

    }

}
