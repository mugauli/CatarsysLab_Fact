using DTO.General;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CatarsysLab_Fact.Utilerias
{
    public class EncriptPasswordSha3
    {
        public MethodResponseDTO<string> RandomString(int size, bool lowerCase)
        {
            try
            {
                var response = new MethodResponseDTO<string>() { Code = 0 };

                var builder = new StringBuilder();
                var random = new Random();
                for (int i = 0; i < size; i++)
                {
                    char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(ch);
                }
                response.Result = lowerCase ? builder.ToString().ToLower() : builder.ToString();
                return response;
            }
            catch (Exception ex)
            {
                //Logger.Error("Code: -200 Mensaje: " + ex.Message + ", InnerException: " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex);
                return new MethodResponseDTO<string> { Code = -200, Message = ex.Message };
            }
        }
        public MethodResponseDTO<string> CreateSalt512()
        {
            try
            {
                var response = new MethodResponseDTO<string>() { Code = 0 };

                var respRanStr = RandomString(512, false);
                if (respRanStr.Code < 0)
                {
                    response.Code = respRanStr.Code;
                    response.Message = respRanStr.Message;
                    return response;
                }
                var message = respRanStr.Result;
                response.Result = BitConverter.ToString((new SHA512Managed()).ComputeHash(Encoding.ASCII.GetBytes(message))).Replace("-", "");
                return response;
            }
            catch (Exception ex)
            {
                //Logger.Error("Code: -200 Mensaje: " + ex.Message + ", InnerException: " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex);
                return new MethodResponseDTO<string> { Code = -200, Message = ex.Message };
            }
        }

        public MethodResponseDTO<string> GenerateHMAC(string clearMessage, string secretKeyString)
        {
            try
            {
                var response = new MethodResponseDTO<string>() { Code = 0 };
                var encoder = new ASCIIEncoding();
                var messageBytes = encoder.GetBytes(clearMessage);
                var secretKeyBytes = new byte[secretKeyString.Length / 2];
                for (int index = 0; index < secretKeyBytes.Length; index++)
                {
                    string byteValue = secretKeyString.Substring(index * 2, 2);
                    secretKeyBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                }
                var hmacsha512 = new HMACSHA512(secretKeyBytes);
                byte[] hashValue = hmacsha512.ComputeHash(messageBytes);
                string hmac = "";
                foreach (byte x in hashValue)
                {
                    hmac += String.Format("{0:x2}", x);
                }
                response.Result = hmac.ToUpper();
                return response;
            }
            catch (Exception ex)
            {
                //Logger.Error("Code: -200 Mensaje: " + ex.Message + ", InnerException: " + (ex.InnerException != null ? ex.InnerException.Message : ""), ex);
                return new MethodResponseDTO<string> { Code = -200, Message = ex.Message };
            }
        }
    }
}