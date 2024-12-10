using DAL.EF.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAL.Security
{
    class KeyPairSetter
    {
        private IKeyPairGenerator _keyPairGenerator;

        public KeyPairSetter(IKeyPairGenerator keyPairGenerator)
        {
            _keyPairGenerator = keyPairGenerator;
        }
        public void SetPublicPrivateKeys()
        {
            string currentDirectory = AppContext.BaseDirectory;
            string keysAuth = Path.Combine(AppContext.BaseDirectory, @"..\files\Key");
            string keyChat = Path.Combine(AppContext.BaseDirectory, @"..\..\ServerChat\files\Key");
            const string privateKeyPath = @"\PrivateKey.json";
            const string publicKeyPath = @"\PublicKey.json";
            Console.WriteLine("Ключи шифрования созданы");

            try
            {
                File.Delete(keysAuth);
                File.Delete(keyChat);
            }
            catch { }

            var (publicKey, privateKey) = _keyPairGenerator.GenerateKeyPair();

            var privateKeyJson = JsonSerializer.Serialize(new
            {
                Private = privateKey
            }, new JsonSerializerOptions { WriteIndented = true });

            var publicKeyJson = JsonSerializer.Serialize(new
            {
                Public = publicKey
            }, new JsonSerializerOptions { WriteIndented = true });

            Directory.CreateDirectory(keysAuth);
            Directory.CreateDirectory(keyChat);
            try
            {
                File.WriteAllText(keysAuth + privateKeyPath, privateKeyJson);
                File.WriteAllText(keyChat + publicKeyPath, publicKeyJson);
            }
            catch { }
        }

    }

    public interface IKeyPairGenerator
    {
        (string publicKey, string privateKey) GenerateKeyPair();
    }

    class KeyPairGenerator : IKeyPairGenerator
    {
        public (string publicKey, string privateKey) GenerateKeyPair()
        {
            using (var key = RSA.Create())
            {
                var privateKey = Convert.ToBase64String(key.ExportRSAPrivateKey());
                var publicKey = Convert.ToBase64String(key.ExportRSAPublicKey());

                return (publicKey, privateKey);
            }
        }
    }
}
