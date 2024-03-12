//*********************************************************************************************
//* File             :   IKeyVaultSecretManager.cs
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
using System.Threading;
using System.Threading.Tasks;

namespace Sukanta.Azure.KeyVaultManager
{
    public interface IKeyVaultSecretManager
    {
        /// <summary>
        /// TokenCredential for authontication
        /// </summary>
        TokenCredential tokenCredential { get; set; }

        /// <summary>
        /// Read the value of the key
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="version"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<string> GetSecretAsync(string secretName, string version = null, CancellationToken ct = default);

        /// <summary>
        ///  Write the value to the key
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="value"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task SetSecretAsync(string secretName, string value, CancellationToken ct = default);

        /// <summary>
        ///  Delete key and value
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task DeleteSecret(string secretName, CancellationToken ct = default);

        /// <summary>
        /// Update the value to an existing key
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="value"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task UpdateSecret(string secretName, string value, CancellationToken ct = default);
    }
}
