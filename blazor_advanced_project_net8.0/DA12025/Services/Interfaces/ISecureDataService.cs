namespace Services.Interfaces;

public interface ISecureDataService
{
    string Encrypt(string data);
    string Decrypt(string data);
    string Hash(string data);
    bool CompareHashes(string storedHash, string dataInput);
}