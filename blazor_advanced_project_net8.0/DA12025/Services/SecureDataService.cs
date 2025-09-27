using Services.Interfaces;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Services.Settings;

namespace Services;

public class SecureDataService : ISecureDataService
{
    private readonly SystemSettings _settings;
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 200000;

    public SecureDataService(IOptions<SystemSettings> options)
    {
        _settings = options.Value;
    }

    /*encryption and decryption using AES-256*/

    public string Encrypt(string data)
    {
        byte[] key = DecodeBase64Token(_settings.Token);

        using var aesAlgorithm = Aes.Create();
        aesAlgorithm.Key = key;
        //generate a random initialization vector each time
        aesAlgorithm.GenerateIV();

        using var msEncrypt = new MemoryStream();
        //write the initialization vector first
        msEncrypt.Write(aesAlgorithm.IV, 0, aesAlgorithm.IV.Length);
        using var encryptor = aesAlgorithm.CreateEncryptor();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using var swEncrypt = new StreamWriter(csEncrypt);
        swEncrypt.Write(data);
        swEncrypt.Flush();
        csEncrypt.FlushFinalBlock();
        byte[] encryptedData = msEncrypt.ToArray();

        return Convert.ToBase64String(encryptedData);
    }
    
    public string Decrypt(string encryptedData)
    {
        byte[] key = DecodeBase64Token(_settings.Token);

        //decode the Base64 string into a byte array
        byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

        using var aesAlgorithm = Aes.Create();
        aesAlgorithm.Key = key;
        //extract the initialization vector (first 16 bytes)
        byte[] iv = new byte[16];
        Array.Copy(encryptedBytes, 0, iv, 0, iv.Length);
        aesAlgorithm.IV = iv;

        //extract ciphertext (bytes after the iv)
        byte[] cipherBytes = new byte[encryptedBytes.Length - iv.Length];
        Array.Copy(encryptedBytes, iv.Length, cipherBytes, 0, cipherBytes.Length);

        using var msDecrypt = new MemoryStream(cipherBytes);
        using var decryptor = aesAlgorithm.CreateDecryptor();
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);

        return srDecrypt.ReadToEnd();
    }

    //hashing using PBKDF2 
    public string Hash(string data)
    {
        using var randomNumber = RandomNumberGenerator.Create();
        byte[] salt = new byte[SaltSize];
        randomNumber.GetBytes(salt);

        using var pbkdf2 = new Rfc2898DeriveBytes(data, salt, Iterations, HashAlgorithmName.SHA512);
        byte[] key = pbkdf2.GetBytes(KeySize);

        //combine salt + key
        byte[] hashBytes = new byte[SaltSize + KeySize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(key, 0, hashBytes, SaltSize, KeySize);

        return Convert.ToBase64String(hashBytes);
    }

    public bool CompareHashes(string storedHash, string dataInput)
    {
        byte[] hashBytes = Convert.FromBase64String(storedHash);

        //extract the salt from the first SaltSize byte
        byte[] salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        //derive the key from the input data using the extracted salt
        using var pbkdf2 = new Rfc2898DeriveBytes(dataInput, salt, Iterations, HashAlgorithmName.SHA512);
        byte[] keyToCheck = pbkdf2.GetBytes(KeySize);

        //extract the stored key from the hashBytes (after the salt)
        byte[] storedKey = new byte[KeySize];
        Array.Copy(hashBytes, SaltSize, storedKey, 0, KeySize);

        //use a time-safe comparison
        return CryptographicOperations.FixedTimeEquals(storedKey, keyToCheck);
    }

    private byte[] DecodeBase64Token(string token)
    {
        byte[] key = Convert.FromBase64String(token);
        return key;
    }
}