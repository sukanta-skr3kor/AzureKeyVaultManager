//*********************************************************************************************
//* File             :   Program.cs
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

using Sukanta.Azure.KeyVaultManager;
using System;

namespace Sukanta.Azure.KeyVault.Demo
{
    public sealed class Program
    {
        private Program()
        { }

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            string keyVaultUrl = "https://xxx.vault.azure.net";

            KeyVaultSecretManager keyVaultSecretManager = new KeyVaultSecretManager(keyVaultUrl);

            string secretKey = "secretblobconnstring";

            var secretValue = await keyVaultSecretManager.GetSecretAsync(secretKey).ConfigureAwait(false);

            Console.WriteLine($"Secretkey '{secretKey}' : value '{secretValue}'");

            Console.ReadKey();
        }

    }
}
