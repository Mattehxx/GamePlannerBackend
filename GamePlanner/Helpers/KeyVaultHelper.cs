using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using GamePlanner.DTO.ConfigurationDTO;
using System.Collections.Generic;
using System.Text.Json;

namespace GamePlanner.Helpers
{
    public static class KeyVaultHelper
    {
        private static readonly string _keyVaultUrl = $"https://kvgameplanner.vault.azure.net/";
        private static SecretClient? _secretClient = null;

        public static T? GetSecret<T>(string secretKey)
        {
            _secretClient ??= new SecretClient(new Uri(_keyVaultUrl), new DefaultAzureCredential());
            var response = _secretClient.GetSecret(secretKey);
            var secret = response.Value;
            return JsonSerializer.Deserialize<T>(secret.Value);
        }

        public static string GetSecrectConnectionString(string connectionString)
        {
            var connectionStrings = GetSecret<Dictionary<string, string>>("ConnectionStrings");
            if (connectionStrings is null) return string.Empty;

            return connectionStrings[connectionString];
        }

        public static string GetSecretString(string secretKey)
        {
            _secretClient ??= new SecretClient(new Uri(_keyVaultUrl), new DefaultAzureCredential());
            var response = _secretClient.GetSecret(secretKey);
            var secret = response.Value;
            return secret.Value;
        }

        public static int GetSecretInt(string secretKey)
        {
            _secretClient ??= new SecretClient(new Uri(_keyVaultUrl), new DefaultAzureCredential());
            var response = _secretClient.GetSecret(secretKey);
            var secret = response.Value;
            return int.Parse(secret.Value);
        }
    }
}
