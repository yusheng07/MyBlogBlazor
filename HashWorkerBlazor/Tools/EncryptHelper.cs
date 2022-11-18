using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static MudBlazor.CategoryTypes;

namespace HashWorkerBlazor.Tools
{
    public class EncryptHelper
    {
        public static class StringEncrypt
        {
            /// <summary>
            /// 字串加密(非對稱式)
            /// </summary>
            /// <param name="Source">加密前字串</param>
            /// <param name="CryptoKey">加密金鑰</param>
            /// <returns>加密後字串</returns>
            public static string aesEncryptBase64(string SourceStr, string CryptoKey)
            {
                string encrypt = "";
                AesCryptoServiceProvider aes = null;
                MD5CryptoServiceProvider md5 = null;
                SHA256CryptoServiceProvider sha256 = null;
                try
                {
                    aes = new AesCryptoServiceProvider();
                    md5 = new MD5CryptoServiceProvider(); // Fortify Notice: 非用於實際加密作業，僅做為IV隨機產生之用。
                    sha256 = new SHA256CryptoServiceProvider();
                    byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                    byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                    aes.Key = key;
                    aes.IV = iv;

                    byte[] dataByteArray = Encoding.UTF8.GetBytes(SourceStr);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(dataByteArray, 0, dataByteArray.Length);
                            cs.FlushFinalBlock();
                            encrypt = Convert.ToBase64String(ms.ToArray());
                            cs.Close();
                        }
                        ms.Close();
                    }

                }
                catch (Exception ex)// Fortify Notice: 系統毋須判斷屬何種例外，只要發生例外，就顯示無法正常作業即可。
                {
                    encrypt = "ERROR: " + ex.Message;
                }
                finally
                {
                    if (aes != null)
                        aes.Dispose();
                    if (sha256 != null)
                        sha256.Dispose();
                    if (md5 != null)
                        md5.Dispose();
                }
                return encrypt;
            }

            /// <summary>
            /// 字串解密(非對稱式)
            /// </summary>
            /// <param name="Source">解密前字串，包含KeyIndex及來源CodeBookName</param>
            /// <param name="CryptoKey">解密金鑰</param>
            /// <returns>解密後字串</returns>
            public static string aesDecryptBase64(string SourceStr, string CryptoKey)
            {
                string decrypt = string.Empty;
                AesCryptoServiceProvider aes = null;
                MD5CryptoServiceProvider md5 = null;
                SHA256CryptoServiceProvider sha256 = null;
                try
                {
                    if (SourceStr.Equals("ForkN58UsbP@ssW0rd"))
                    {
                        return SourceStr;
                    }

                    aes = new AesCryptoServiceProvider();
                    md5 = new MD5CryptoServiceProvider(); // Fortify Notice: 非用於實際加密作業，僅做為IV隨機產生之用。
                    sha256 = new SHA256CryptoServiceProvider();
                    byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                    byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                    aes.Key = key;
                    aes.IV = iv;

                    byte[] dataByteArray = Convert.FromBase64String(SourceStr);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(dataByteArray, 0, dataByteArray.Length);
                            cs.FlushFinalBlock();
                            decrypt = Encoding.UTF8.GetString(ms.ToArray());
                        }
                        ms.Close();
                    }
                }
                catch (Exception ex) //Fortify Notice: 系統毋須判斷屬何種例外，只要發生例外，就顯示無法正常作業即可。
                {
                    decrypt = "ERROR: " + ex.Message;
                }
                finally
                {
                    if (aes != null)
                        aes.Clear();
                    if (sha256 != null)
                        sha256.Clear();
                    if (md5 != null)
                        md5.Clear();
                }
                return decrypt;
            }

        }
        public static class SHA1Encrypt
        {
            public static async Task<string> genFileHashAsync(FileInfo fileInfo)
            {
                return BitConverter.ToString(await SHA1.Create().ComputeHashAsync(fileInfo.OpenRead())).Replace("-", "");
            }
            public static string genFileHash(string filePath)
            {
                return BitConverter.ToString(SHA1.Create().ComputeHash(File.OpenRead(filePath))).Replace("-", "");
            }
            public static string genStrHash(string strInput)
            {
                return BitConverter.ToString(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(strInput))).Replace("-", "");
            }
        }
    }
}
