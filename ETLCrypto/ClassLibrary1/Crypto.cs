﻿/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    public class Crypto
    {
                      // "ASDFGTRE$#@!6+|@", 
                      // "QWERTYUIOP!@#+4+"                 
        public static string CHAVE = "ASDFGTRE$#@!6+|@";
        public static string VETORINICIALIZACAO = "QWERTYUIOP!@#+4+";
        //////////////////////////////////////////////////////ARQUIVO DE CRIPTOGRAFIA////////////////////////////////////////////////////
        private static string ArrayBytesToHexString(byte[] conteudo) =>
            string.Concat(Array.ConvertAll<byte, string>(conteudo, b => b.ToString("X2")));

        private static Rijndael CriarInstanciaRijndael(string chave, string vetorInicializacao)
        {
            if ((chave == null) || (((chave.Length != 0x10) && (chave.Length != 0x18)) && (chave.Length != 0x20)))
            {
                return null;
            }
            if ((vetorInicializacao == null) || (vetorInicializacao.Length != 0x10))
            {
                return null;
            }
            Rijndael rijndael = Rijndael.Create();
            rijndael.Key = Encoding.ASCII.GetBytes(chave);
            rijndael.IV = Encoding.ASCII.GetBytes(vetorInicializacao);
            return rijndael;
        }

        public static string DecodeFrom64(string encodedData)
        {
            byte[] bytes = Convert.FromBase64String(encodedData);
            return Encoding.ASCII.GetString(bytes);
        }

        public static string Decriptar(string textoEncriptado)
        {
            string str2;
            if (string.IsNullOrWhiteSpace(textoEncriptado))
            {
                return "O conte\x00fado a ser decriptado n\x00e3o pode ser uma string vazia.";
            }
            if ((textoEncriptado.Length % 2) != 0)
            {
                return "O conte\x00fado a ser decriptado \x00e9 inv\x00e1lido.";
            }
            //                         CriarInstanciaRijndael("ASDFGTRE$#@!6+|@", "QWERTYUIOP!@#+4+"))
            using (Rijndael rijndael = CriarInstanciaRijndael(CHAVE, VETORINICIALIZACAO))
            {
                ICryptoTransform transform = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);
                string str = null;
                try
                {
                    using (MemoryStream stream = new MemoryStream(HexStringToArrayBytes(textoEncriptado)))
                    {
                        using (CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read))
                        {
                            using (StreamReader reader = new StreamReader(stream2))
                            {
                                try
                                {
                                    str = reader.ReadToEnd();
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                    }
                    str2 = str;
                }
                catch (Exception)
                {
                    str2 = "";
                }
            }
            return str2;
        }

        public static string EncodeTo64(string toEncode) =>
            Convert.ToBase64String(Encoding.ASCII.GetBytes(toEncode));

        public static string Encriptar(string textoNormal)
        {
            string str;
            if (string.IsNullOrWhiteSpace(textoNormal))
            {
                return "O conte\x00fado a ser encriptado n\x00e3o pode ser uma string vazia.";
            }
            //                         CriarInstanciaRijndael("ASDFGTRE$#@!6+|@", "QWERTYUIOP!@#+4+")
            using (Rijndael rijndael = CriarInstanciaRijndael(CHAVE, VETORINICIALIZACAO))
            {
                ICryptoTransform transform = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);
                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(stream2))
                        {
                            writer.Write(textoNormal);
                        }
                    }
                    str = ArrayBytesToHexString(stream.ToArray());
                }
            }
            return str;
        }

        private static byte[] HexStringToArrayBytes(string conteudo)
        {
            try
            {
                int num = conteudo.Length / 2;
                byte[] buffer = new byte[num];
                for (int i = 0; i < num; i++)
                {
                    buffer[i] = Convert.ToByte(conteudo.Substring(i * 2, 2), 0x10);
                }
                return buffer;
            }
            catch (Exception)
            {
                return null;
            }
        }
        //////////////////////////////////////////////////////FIM ARQUIVO DE CRIPTOGRAFIA///////////////////////////////////////////////
    }
}