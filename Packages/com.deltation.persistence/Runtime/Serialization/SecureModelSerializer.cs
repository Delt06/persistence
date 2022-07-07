using System;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Persistence.Serialization
{
	public sealed class SecureModelSerializer : FileModelSerializer
	{
		[SerializeField] private Encryption _encryption = Encryption.Base64;
		
		protected override void SerializeViaFormatter(object model)
		{
			_formatter.Serialize(_writeCryptoStream, model);
		}

		protected override object DeserializeViaFormatter()
		{
			return _formatter.Deserialize(_readCryptoStream);
		}

		protected override void SetUpProcedure(Type modelType)
		{
			base.SetUpProcedure(modelType);

			var (encryptor, decryptor) = CreateTransforms();
			_writeCryptoStream = new CryptoStream(Stream, encryptor, CryptoStreamMode.Write);
			_readCryptoStream = new CryptoStream(Stream, decryptor, CryptoStreamMode.Read);
		}

		private (ICryptoTransform encrptor, ICryptoTransform decrytor) CreateTransforms()
		{
			switch (_encryption)
			{
				case Encryption.Base64:
					return (new ToBase64Transform(), new FromBase64Transform());
				case Encryption.RijndaelManaged:
					
					var rijndaelManaged = new RijndaelManaged();
					rijndaelManaged.Key = GetKey(rijndaelManaged.KeySize / 8);

					return (rijndaelManaged.CreateEncryptor(), rijndaelManaged.CreateDecryptor());
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static byte[] GetKey(int byteCount)
		{
			var id = SystemInfo.deviceUniqueIdentifier;
			return Encoding.Unicode.GetBytes(id).Take(byteCount).ToArray();
		}

		private CryptoStream _writeCryptoStream;
		private CryptoStream _readCryptoStream;
		private RijndaelManaged _rijndaelManaged;
		private readonly BinaryFormatter _formatter = new BinaryFormatter();

		private enum Encryption
		{
			Base64, RijndaelManaged
		}
	}
}