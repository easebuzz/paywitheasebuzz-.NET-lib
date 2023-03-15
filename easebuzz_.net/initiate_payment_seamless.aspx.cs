using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace easebuzz_.net
{
	public partial class initiate_payment_seamless : System.Web.UI.Page
    {
		public string salt = System.Configuration.ConfigurationSettings.AppSettings["salt"];
        public string Key = System.Configuration.ConfigurationSettings.AppSettings["key"];

		public string env = System.Configuration.ConfigurationSettings.AppSettings["env"];
		public string is_enable_seamless = System.Configuration.ConfigurationSettings.AppSettings["enable_seamless"];

		public string accessKey, encrypted_card_number, encrypted_card_holder_name, encrypted_card_cvv, encrypted_card_expiry_date,
			card_token, cryptogram, token_expiry_date, token_requester_id, encodedKey, encodedIv;

		//initiate payment called
		public void button1Clicked(object sender, EventArgs args)
        {   //form the mandatory fields 
			string amount = Request.Form["amount"].Trim();
			string firstname = Request.Form["firstname"].Trim();
			string email = Request.Form["email"].Trim();
			string phone = Request.Form["phone"].Trim();
			string productinfo = Request.Form["productinfo"].Trim();
			string surl = Request.Form["surl"].Trim();
			string furl = Request.Form["furl"].Trim();
			string Txnid = Request.Form["Txnid"].Trim();
			string UDF1 = Request.Form["udf1"].Trim();
			string UDF2 = Request.Form["udf2"].Trim();
			string UDF3 = Request.Form["udf3"].Trim();
			string UDF4 = Request.Form["udf4"].Trim();
			string UDF5 = Request.Form["udf5"].Trim();

			string UDF6 = Request.Form["udf6"].Trim();
			string UDF7 = Request.Form["udf7"].Trim();
			string UDF8 = Request.Form["udf8"].Trim();
			string UDF9 = Request.Form["udf9"].Trim();
			string UDF10 = Request.Form["udf10"].Trim();

			string Show_payment_mode = Request.Form["show_payment_mode"].Trim();
						
			string split_payments = Request.Form["split_payments"].Trim();
			string sub_merchant_id = Request.Form["sub_merchant_id"].Trim();
			
			Dictionary<string, string> dict = new Dictionary<string, string>();
			dict.Add("txnid", Txnid);
			dict.Add("key", Key);
			amount = amount;
			dict.Add("amount", amount);
			dict.Add("firstname", firstname.Trim());
			dict.Add("email", email.Trim());
			dict.Add("phone", phone.Trim());
			dict.Add("productinfo", productinfo.Trim());
			dict.Add("surl", surl.Trim());
			dict.Add("furl", furl.Trim());
			dict.Add("udf1", UDF1.Trim());
			dict.Add("udf2", UDF2.Trim());
			dict.Add("udf3", UDF3.Trim());
			dict.Add("udf4", UDF4.Trim());
			dict.Add("udf5", UDF5.Trim());
			dict.Add("udf6", UDF6.Trim());
			dict.Add("udf7", UDF7.Trim());
			dict.Add("udf8", UDF8.Trim());
			dict.Add("udf9", UDF9.Trim());
			dict.Add("udf10", UDF10.Trim());
			
			dict.Add("show_payment_mode", Show_payment_mode.Trim());
			dict.Add("request_flow", "SEAMLESS");

		 	if (Request.Form["split_payments"].Trim().Length > 0)
			{
				dict.Add("split_payments", split_payments);
			}
			if (Request.Form["sub_merchant_id"].Trim().Length > 0)
			{
				dict.Add("sub_merchant_id", Request.Form["sub_merchant_id"].Trim());
			}

			Easebuzz t = new Easebuzz(salt, Key, env, "true");
			string result = t.initiatePaymentAPI(dict);

			if (!String.IsNullOrEmpty(result)) {
				if (((JToken)result).Type == JTokenType.Object)
				{
					Response.Write(result);
				}
				else
                {
					string encodedKey = ComputeSha256Hash(Key).Substring(0, 32);
					string encodedIv = ComputeSha256Hash(salt).Substring(0, 16);

					try
					{
						// Create Aes that generates a new key and initialization vector (IV).    
						// Same key must be used in encryption and decryption    
						using (AesManaged aes = new AesManaged())
						{
							
							aes.Key = Encoding.UTF8.GetBytes(encodedKey);
							aes.IV = Encoding.UTF8.GetBytes(encodedIv);
							if (!String.IsNullOrEmpty(Request.Form["card_number"].Trim()))
							{
								encrypted_card_number = Encrypt(Request.Form["card_number"], aes.Key, aes.IV);
							}

							if (!String.IsNullOrEmpty(Request.Form["card_holder_name"].Trim()))
							{
								encrypted_card_holder_name = Encrypt(Request.Form["card_holder_name"], aes.Key, aes.IV);
							}

							if (!String.IsNullOrEmpty(Request.Form["card_cvv"].Trim()))
							{
								encrypted_card_cvv = Encrypt(Request.Form["card_cvv"], aes.Key, aes.IV);
							}
							if (!String.IsNullOrEmpty(Request.Form["card_expiry_date"].Trim()))
							{
								encrypted_card_expiry_date = Encrypt(Request.Form["card_expiry_date"], aes.Key, aes.IV);
							}
						}


						accessKey = result;

						StringBuilder sb = new StringBuilder();
						sb.AppendLine("<html>");
						sb.AppendLine("<head>");

						sb.AppendLine("<meta charset='UTF-8'>" +
							"<meta name='viewport' content='width=device-width, initial-scale=1.0'>" +
							"<meta http-equiv='X-UA-Compatible' content='ie=edge'>" +
							"<style>" +
							"input, button {" +
							"width: 35%; " +
							"padding: 5px;" +
							"margin: 5px;" +
							"}" +
							"</style>");
						sb.AppendLine("</head>");
						sb.AppendLine("<body>");
						sb.AppendLine("<form id='seamless_auto_submit_form' method='POST' action='https://pay.easebuzz.in/initiate_seamless_payment/'>" +
							"<input type='hidden' name='access_key' value='" + accessKey + "'></input><br>" +
							"<input type='hidden' name='payment_mode' value='" + Request.Form["payment_mode"].Trim() + "'></input><br>" +
							"<input type='hidden' name='bank_code' value='" + Request.Form["bank_code"].Trim() + "'></input><br>" +
							"<input type='hidden' name='card_number' value='" + encrypted_card_number + "'></input><br>" +
							"<input type='hidden' name='card_holder_name' value='" + encrypted_card_holder_name + "'></input><br>" +
							"<input type='hidden' name='card_cvv' value='" + encrypted_card_cvv + "'></input><br>" +
							"<input type='hidden' name='card_expiry_date' value='" + encrypted_card_expiry_date + "'></input><br>" +
							"<input type='hidden' name='card_token' value='" + card_token + "'></input><br>" +
							"<input type='hidden' name='cryptogram' value='" + cryptogram + "'></input><br>" +
							"<input type='hidden' name='token_expiry_date' value='" + token_expiry_date + "'></input><br>" +
							"<input type='hidden' name='token_requester_id' value='" + token_requester_id + "'></input><br>" +
							"<input type='hidden' name='upi_va' value='" + Request.Form["upi_va"].Trim() + "'></input><br>" +
							"</form>" +
							"<script type='text/javascript'>" +
							"document.getElementById('seamless_auto_submit_form').submit();" +
							"</script>");
						sb.AppendLine("</body>");
						sb.AppendLine("</html>");
						string seamlessForm = sb.ToString();
						Response.Write(seamlessForm);
					}
					catch (Exception exp)
					{
						Console.WriteLine(exp.Message);
					}
					
				}
				
			}
			else
            {
				Response.Write(result);
			}
			
		}



		static string ComputeSha256Hash(string rawData)
		{
			// Create a SHA256   
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// ComputeHash - returns byte array  
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

				// Convert byte array to a string   
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}


		static string Encrypt(string plainText, byte[] Key, byte[] IV)
		{
			byte[] encrypted;
			// Create a new AesManaged.    
			using (AesManaged aes = new AesManaged())
			{
				// Create encryptor    
				ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
				// Create MemoryStream    
				using (MemoryStream ms = new MemoryStream())
				{
					// Create crypto stream using the CryptoStream class. This class is the key to encryption    
					// and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
					// to encrypt    
					using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
					{
						// Create StreamWriter and write data to a stream    
						using (StreamWriter sw = new StreamWriter(cs))
							sw.Write(plainText);
						encrypted = ms.ToArray();
					}
				}
			}
			// Return encrypted data    
			return Convert.ToBase64String(encrypted);
		}
		

	}
}
