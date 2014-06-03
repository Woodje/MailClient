using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace MailClient
{
    class EncryptionDecryption
    {
        // Declare a variable for a memorystream.
        private MemoryStream memoryStream;

        // Declare a variable for the use of RijndaelManaged class.
        private RijndaelManaged cryptoRM;

        // Declare and initialize variable for the use of the TripleDESCryptoServiceProvider.
        private TripleDESCryptoServiceProvider keyGenerator = new TripleDESCryptoServiceProvider();

        // Make a static IV for the encoding. This should be generated randomely.
        byte[] IV = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

        public EncryptionDecryption()
        {
            // Instantiate the memorystream.
            memoryStream = new MemoryStream();

            // Instantiate the RijndaelManaged class.
            cryptoRM = new RijndaelManaged();
        }

        public byte [] Key()
        {
            // Generate a new key.
            keyGenerator.GenerateKey();

            // Return the generated key.
            return keyGenerator.Key;
        }

        public string EncryptString(string stringToBeEncrypted, byte [] key)
        {
            // Convert the string to bytes.
            byte[] stringInBytes = Encoding.UTF8.GetBytes(stringToBeEncrypted);

            // Declare and instantiate the CryptoStream to write to the memorystream with key, and the initialization vector.
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoRM.CreateEncryptor(key, IV), CryptoStreamMode.Write);

            // Use the cryptostream to encrypt the bytes of the string.
            cryptoStream.Write(stringInBytes, 0, stringInBytes.Length);

            // Close the cryptostream again.
            cryptoStream.Close();
            
            // Return the converted memorystreamsbytes to string in encoded base64 mode.
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public string DecryptString(string stringToBeDecrypted, byte [] key)
        {
            // Convert the base64 string to bytes.
            byte[] stringInBytes = Convert.FromBase64String(stringToBeDecrypted);

            // Declare and instantiate the CryptoStream to write to the memorystream with key, and the initialization vector.
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoRM.CreateDecryptor(key, IV), CryptoStreamMode.Write);

            // Use the cryptostream to encrypt the bytes of the string.
            cryptoStream.Write(stringInBytes, 0, stringInBytes.Length);

            // Close the cryptostream again.
            cryptoStream.Close();

            // Return the converted memorystreamsbytes to a string;
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}
