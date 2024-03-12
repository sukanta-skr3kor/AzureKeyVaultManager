//*********************************************************************************************
//* File             :   KeyVaultSecretManager.cs
//* Author           :   Rout, Sukanta
//* Date             :   12/3/2024
//* Description      :   Initial version
//* Version          :   1.0
//*-------------------------------------------------------------------------------------------
//* dd-MMM-yyyy	: Version 1.x, Changed By : xxx
//*
//*                 - 1)
//*                 - 2)
//*                 - 3)
//*                 - 4)
//*
//*********************************************************************************************
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sukanta.Azure.KeyVaultManager
{
    public class KeyVaultSecretManager : IKeyVaultSecretManager
    {
        /// <summary>
        /// secret client to request for keyvaul services
        /// </summary>
        private readonly SecretClient keyVaultClient;

        /// <summary>
        /// credential for auth
        /// </summary>
        public TokenCredential tokenCredential { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyVaultUrl"></param>
        /// <param name="clientId"></param>
        /// <param name="tenantId"></param>
        /// <param name="clientSecret"></param>
        public KeyVaultSecretManager(string keyVaultUrl, string clientId = null, string tenantId = null, string clientSecret = null)
        {
            if (string.IsNullOrEmpty(keyVaultUrl))
            {
                throw new ArgumentNullException(nameof(keyVaultUrl));
            }

            if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(tenantId) && !string.IsNullOrEmpty(clientSecret))
            {
                tokenCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            }
            else
            {
                tokenCredential = new DefaultAzureCredential();
            }

            keyVaultClient = new SecretClient(new Uri(keyVaultUrl), tokenCredential);
        }

        /// <summary>
        /// Read the value of the key from azure keyvault
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="version"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<string> GetSecretAsync(string secretName, string version = null, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(secretName))
            {
                throw new ArgumentNullException(nameof(secretName));
            }

            KeyVaultSecret secret = await keyVaultClient.GetSecretAsync(secretName, version, ct).ConfigureAwait(false);

            return secret != null ? secret.Value : string.Empty;
        }

        /// <summary>
        /// Write the value of the key from azure keyvault
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="value"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task SetSecretAsync(string secretName, string value, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(secretName))
            {
                throw new ArgumentNullException(nameof(secretName));
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            await keyVaultClient.SetSecretAsync(secretName, value, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete the value and key from azure keyvault
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task DeleteSecret(string secretName, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(secretName))
            {
                throw new ArgumentNullException(nameof(secretName));
            }

            await keyVaultClient.StartDeleteSecretAsync(secretName, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Update the value of an existing key in azure keyvault
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="value"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task UpdateSecret(string secretName, string value, CancellationToken ct = default)
        {
            await SetSecretAsync(secretName, value, ct).ConfigureAwait(false);
        }

    }
}
