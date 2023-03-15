using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace easebuzz_.net
{


	//easebuzz api system starts
	public class Easebuzz
	{

		public string easebuzz_action_url = string.Empty;
		public string gen_hash;
		public string txnid = String.Empty;
		public string easebuzz_merchant_key = string.Empty;
		public string salt = string.Empty;
		public string Key = string.Empty;
		public string env = string.Empty;
		public string is_enable_iframe;
		public string is_enable_seamless;
		string empty_value = "";


		public Easebuzz(string SALT, string KEY, string ENV, string is_seamless_request)
		{
			salt = SALT;
			Key = KEY;
			env = ENV;
			is_enable_seamless = is_seamless_request ?? "false";
		}
		// this function is required to initiate payment

		internal string initiatePaymentAPI(Dictionary<string, string> dict)
		{
			string result = "";

			if (emptyValidation(dict)) {
				var obj = new
				{
					status = "0",
					data = "Mandatory parameter " + empty_value+ " can not empty"
				};
				return obj.ToString();

			}
			else
            {
				// generate hash
				string hashVarsSeq = dict["key"] + "|" + dict["txnid"] + "|" + dict["amount"] + "|" + dict["productinfo"] + "|" + dict["firstname"] + "|"
					+ dict["email"] + "|" + dict["udf1"] + "|" + dict["udf2"] + "|" + dict["udf3"] + "|" + dict["udf4"] + "|" + dict["udf5"] + "|" + dict["udf6"] + "|" + dict["udf7"] + "|"
					+ dict["udf8"] + "|" + dict["udf9"] + "|" + dict["udf10"] + "|" + salt; // spliting hash sequence from config

				gen_hash = Easebuzz_Generatehash512(hashVarsSeq).ToLower();        //generating hash

				dict.Add("hash", gen_hash);

				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

				var client = new RestClient(getURL());
				RestRequest request = new RestRequest("/payment/initiateLink");

				foreach (var data in dict)
				{
					request.AddParameter(data.Key, data.Value);
				}
				var response = client.Post(request);

				var responseDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ToString());

				is_enable_iframe = System.Configuration.ConfigurationSettings.AppSettings["enable_iframe"] ?? "false";

				if (responseDict != null && responseDict["status"] == "1")
				{
					if (!string.IsNullOrEmpty(responseDict["data"]))
					{
						if (is_enable_seamless == "true")
                        {
							result = responseDict["data"];
						}
						else if (is_enable_iframe == "true")
						{
							result = responseDict["data"];
						}
						else
						{
							result = getURL() + "/pay/" + responseDict["data"];
						}
					}
				}
				else
				{
					result = response.Content.ToString();

				}


				return result;
			}
			
		}

		public bool emptyValidation(Dictionary<string, string> dictionary)
        {
			bool isValid = false;

			if (dictionary != null)
            {
				if (string.IsNullOrEmpty(dictionary["key"])) {
					empty_value = "Merchant Key";
					isValid = true;
				}
				else if (string.IsNullOrEmpty(dictionary["txnid"])) {
					empty_value = "Transaction Id";
					isValid = true;
				}
				else if(string.IsNullOrEmpty(dictionary["amount"])) {
					empty_value = "Amount";
					isValid = true;
				}
				else if(string.IsNullOrEmpty(dictionary["productinfo"])) {
					empty_value = "Product Infomation";
					isValid = true;
				}
				else if(string.IsNullOrEmpty(dictionary["firstname"])) {
					empty_value = "First Name";
					isValid = true;
				}
				else if(string.IsNullOrEmpty(dictionary["email"])) {
					empty_value = "Email";
					isValid = true;
				}
				else if(string.IsNullOrEmpty(dictionary["phone"])) {
					empty_value = "Phone";
					isValid = true;
				}
				else if(!string.IsNullOrEmpty(dictionary["phone"])) {
					if (dictionary["phone"].Length != 10)
                    {
						empty_value = "Phone number must be 10 digit";
						isValid = true;

					}
				}
				else if(string.IsNullOrEmpty(dictionary["surl"])) {
					empty_value = "Success URL";
					isValid = true;
				}
				else if(string.IsNullOrEmpty(dictionary["furl"])) {
					empty_value = "Failure URL";
					isValid = true;
				}
				else if(string.IsNullOrEmpty(salt)) {
					empty_value = "Merchant Salt Key";
					isValid = true;
				}

			}
			else
            {
				isValid = false;
			}
			return isValid;
		}



		// hashcode generation
		public string Easebuzz_Generatehash512(string text)
		{

			byte[] message = Encoding.UTF8.GetBytes(text);

			UnicodeEncoding UE = new UnicodeEncoding();
			byte[] hashValue;
			SHA512Managed hashString = new SHA512Managed();
			string hex = "";
			hashValue = hashString.ComputeHash(message);
			
			foreach (byte x in hashValue) {
				hex += String.Format("{0:x2}", x);
			}
			return hex;
		}


		//get url using env varibale
		public string getURL()
		{
			string paymentUrl = "";
			if (env == "test")
			{
				paymentUrl = "https://testpay.easebuzz.in";
			}
			else if (env == "prod")
			{
				paymentUrl = "https://pay.easebuzz.in";
			}
			else
            {
				paymentUrl = "https://testpay.easebuzz.in";
			}
			return paymentUrl;
		}

		//initiate refund api 
		public string RefundAPI(string txnid, string refund_amount, string phone, string amount, string email)
		{
			System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
			data.Add("txnid", txnid.Trim());
			data.Add("refund_amount", refund_amount.Trim());
			data.Add("key", Key);
			//string AmountForm = Convert.ToDecimal(amount.Trim()).ToString("g29");// eliminating trailing zeros
			//amount = AmountForm;
			data.Add("amount", amount);
			data.Add("email", email.Trim());
			data.Add("phone", phone.Trim());
			// generate hash
			string[] hashVarsSeq = "key|txnid|amount|refund_amount|email|phone".Split('|'); // spliting hash sequence from config
			string hash_string = "";
			foreach (string hash_var in hashVarsSeq)
			{
				hash_string = hash_string + (data.ContainsKey(hash_var) ? data[hash_var].ToString() : "");
				hash_string = hash_string + '|';
			}
			hash_string += salt;// appending SALT
			Console.WriteLine(hash_string);
			gen_hash = Easebuzz_Generatehash512(hash_string).ToLower();        //generating hash
			data.Add("hash", gen_hash);

			var postData = "txnid=" + txnid;
			postData += "&refund_amount=" + refund_amount;
			postData += "&phone=" + phone;
			postData += "&key=" + Key;
			postData += "&amount=" + amount;
			postData += "&email=" + email;
			postData += "&hash=" + gen_hash;
			
			string url = "https://dashboard.easebuzz.in/transaction/v1/refund";

			var request = (HttpWebRequest)WebRequest.Create(url);

			var Ndata = Encoding.ASCII.GetBytes(postData);

			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = Ndata.Length;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(Ndata, 0, Ndata.Length);
			}

			var response = (HttpWebResponse)request.GetResponse();

			var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
			

			return responseString;
		}

		//initiates transaction api 
		public string transactionAPI(string txnid, string amount, string email, string phone)
		{
			System.Collections.Hashtable data = new System.Collections.Hashtable();
			data.Add("key", Key);
			data.Add("txnid", txnid);
			data.Add("amount", amount);
			data.Add("email", email);
			data.Add("phone", phone);

			// generate hash
			string[] hashVarsSeq = "key|txnid|amount|email|phone".Split('|'); // spliting hash sequence from config
			string hash_string = "";
			foreach (string hash_var in hashVarsSeq)
			{
				hash_string = hash_string + (data.ContainsKey(hash_var) ? data[hash_var].ToString() : "");
				hash_string = hash_string + '|';
			}
			hash_string += salt;// appending SALT
			Console.WriteLine(hash_string);
			gen_hash = Easebuzz_Generatehash512(hash_string).ToLower();        //generating hash
			data.Add("hash", gen_hash);

			string url = "https://dashboard.easebuzz.in/transaction/v1/retrieve";
			var request = (HttpWebRequest)WebRequest.Create(url);

			var postData = "txnid=" + txnid;
			postData += "&amount=" + amount;
			postData += "&email=" + email;
			postData += "&phone=" + phone;
			postData += "&key=" + Key;
			postData += "&hash=" + gen_hash;

			var Ndata = Encoding.ASCII.GetBytes(postData);

			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = Ndata.Length;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(Ndata, 0, Ndata.Length);
			}

			var response = (HttpWebResponse)request.GetResponse();

			var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
			//Response.Write(responseString);
			//string testResponse = "take it or leave it </br>";
			return responseString;
		}

		//iniitiate transactionDateAPI api 
		public string transactionDateAPI(string merchant_email, string transaction_date)
		{
			System.Collections.Hashtable data = new System.Collections.Hashtable();
			data.Add("key", Key);
			data.Add("merchant_email", merchant_email);
			data.Add("transaction_date", transaction_date);
			// generate hash
			string[] hashVarsSeq = "key|merchant_email|transaction_date".Split('|'); // spliting hash sequence from config
			string hash_string = "";
			foreach (string hash_var in hashVarsSeq)
			{
				hash_string = hash_string + (data.ContainsKey(hash_var) ? data[hash_var].ToString() : "");
				hash_string = hash_string + '|';
			}
			hash_string += salt;// appending SALT
			gen_hash = Easebuzz_Generatehash512(hash_string).ToLower();        //generating hash
			data.Add("hash", gen_hash);

			string url = "https://dashboard.easebuzz.in/transaction/v1/retrieve/date";
			var request = (HttpWebRequest)WebRequest.Create(url);

			var postData = "merchant_key=" + Key;
			postData += "&merchant_email=" + merchant_email;
			postData += "&transaction_date=" + transaction_date;
			postData += "&hash=" + gen_hash;

			var Ndata = Encoding.ASCII.GetBytes(postData);

			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = Ndata.Length;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(Ndata, 0, Ndata.Length);
			}

			var response = (HttpWebResponse)request.GetResponse();

			var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
			return responseString;
		}

		//initiate payoutAPI api 
		public string payoutAPI(string merchant_email, string payout_date)
		{
			System.Collections.Hashtable data = new System.Collections.Hashtable();
			data.Add("key", Key);
			data.Add("merchant_email", merchant_email);
			data.Add("payout_date", payout_date);
			// generate hash
			string[] hashVarsSeq = "key|merchant_email|payout_date".Split('|'); // spliting hash sequence from config
			string hash_string = "";
			foreach (string hash_var in hashVarsSeq)
			{
				hash_string = hash_string + (data.ContainsKey(hash_var) ? data[hash_var].ToString() : "");
				hash_string = hash_string + '|';
			}
			hash_string += salt;// appending SALT
			gen_hash = Easebuzz_Generatehash512(hash_string).ToLower();        //generating hash
			data.Add("hash", gen_hash);

			string url = "https://dashboard.easebuzz.in/payout/v1/retrieve";
			var request = (HttpWebRequest)WebRequest.Create(url);

			var postData = "merchant_key=" + Key;
			postData += "&merchant_email=" + merchant_email;
			postData += "&payout_date=" + payout_date;
			postData += "&hash=" + gen_hash;
			var Ndata = Encoding.ASCII.GetBytes(postData);

			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = Ndata.Length;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(Ndata, 0, Ndata.Length);
			}

			var response = (HttpWebResponse)request.GetResponse();

			var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
			return responseString;
		}


	}


	//web functions start
	public partial class Default : System.Web.UI.Page
	{
		
	}


}
