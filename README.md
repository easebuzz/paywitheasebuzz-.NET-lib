
.NET integration kit for pay with easebuzz pay.easebuzz.in

# Software Requirement
*setup .NET kits on test/devlopement/production enviroment install below software*

1. Visual Studio

# easebuzz Documentation for kit integration
https://docs.easebuzz.in/


### Run the kit on your machine
1. clone the repository on your's system.
2. unzip it
3. import the solution to visual studio
4. configure key salt in web.config
5. run the projct


# Process for integrate .net kit in <Your Project>

1. Copy paste the easebuzz class which is present in Default.aspx.cs to your namespace.
2.  Setup initiate payment 

	2.1 Declare key,salt,environment and iframe (enable/disable) flag in Web.config inside appSettings tag

	```
	<appSettings>
		<add key="key" value="XXXXX" />
		<add key="salt" value="XXXXX" />
		<add key="env" value="XXXXX" /> // test for testing env and prod for production use wisely
		<add key="enable_iframe" value="false" />  // if you want to use iframe then it should be true otherwise it should false
	</appSettings>
	````
	2.2 To initiate payment create an object of Easebuzz class with key, salt and env then call initiate Payment API function.

	```
	string salt = System.Configuration.ConfigurationSettings.AppSettings["salt"];
	string Key = System.Configuration.ConfigurationSettings.AppSettings["key"];
	string env = System.Configuration.ConfigurationSettings.AppSettings["env"];
	string is_enable_iframe = System.Configuration.ConfigurationSettings.AppSettings["enable_iframe"];

	Easebuzz t = new Easebuzz(salt, Key, env, false);
	string result = t.initiatePaymentAPI(dict);

	if (is_enable_iframe == "true") {	// code for iframe
		if (((JToken)result).Type == JTokenType.Object) {
			Response.Write(result);
		}
		else
		{
			this.accessKey = result;
			ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "processPayment()", true);
		}	
	}
	else
	{
		bool isUri = Uri.IsWellFormedUriString(result, UriKind.RelativeOrAbsolute);
		if (isUri)
		{
			Response.Write(string.Format("<script type='text/javascript'>window.open('{0}', '_self');</script>", result));
		}
		else
		{
			Response.Write(result);
		}
	}
    ```
    2.3 Pass all required parameters to initiate payment api using Dictionary<string, string> (Sample show as the below)
    ```
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
	```
	2.4 ready to start receiving payment online.

3.  Setup initiate payment with seamless 

	3.1 Declare key,salt,environment and iframe (enable/disable) flag in Web.config inside appSettings tag

	```
	<appSettings>
		<add key="key" value="XXXXX" />
		<add key="salt" value="XXXXX" />
		<add key="env" value="XXXXX" /> // test for testing env and prod for production use wisely
		<add key="enable_iframe" value="false" />  // if you want to use iframe then it should be true otherwise it should false
	</appSettings>
	````
	3.2 To initiate payment create an object of Easebuzz class with key, salt, env and true value for seamless then call initiate Payment API function.

	```
	string salt = System.Configuration.ConfigurationSettings.AppSettings["salt"];
	string Key = System.Configuration.ConfigurationSettings.AppSettings["key"];
	string env = System.Configuration.ConfigurationSettings.AppSettings["env"];
	string is_enable_iframe = System.Configuration.ConfigurationSettings.AppSettings["enable_iframe"];

	Easebuzz t = new Easebuzz(salt, Key, env, true);
	string result = t.initiatePaymentAPI(dict); 


	if (!String.IsNullOrEmpty(result)) {
		if (((JToken)result).Type == JTokenType.Object)
		{
			Response.Write(result);
		}
		else
		{
			accessKey = result;
			// Do your seamless code and call seamless api
		}
		
	}
	else
	{
		Response.Write(result);
	}

    ```

    3.3 Pass all required parameters to initiate payment api using Dictionary<string, string> (Sample show as the below)

    ```
		Dictionary<string, string> dict = new Dictionary<string, string>();
		dict.Add("txnid", Txnid);
		dict.Add("key", Key);
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
		dict.Add("request_flow", "SEAMLESS"); // This is mandatory for seamless
	```

3.4 Once you receive access key then you can use the below code for card encryption and pass all values to seamless api.

   ```
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
		}
		catch (Exception exp)
		{
			Console.WriteLine(exp.Message);
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
		
```

4. setup transaction api in your system
	
	4.1 create object of the class by pass key,salt,environment
	```
		string salt = "XXXXXX";
		string Key = "XXXXXX";
		string env = "test";		// test for testing env and prod for production use wisely
		Easebuzz t = new Easebuzz(salt, Key, env, false);
	````	
	4.2 call the transactional api call 
	```
		string strForm = t.transactionAPI(txnid, amount, email, phone);
	```
	4.3 Required parameters in transactionAPicall 
	```
		string txnid = Request.Form["txnid"].Trim();
		string amount = Request.Form["amount"].Trim();
		string email = Request.Form["email"].Trim();
		string phone = Request.Form["phone"].Trim();
	```
	
	4.4 ready to start  transactional information real time.
	
5. setup transactional records date wise.

	5.1 create object of the class by pass key,salt,environment
	```
		string salt = "XXXXXX";
		string Key = "XXXXXX";
		string env = "test";		// test for testing env and prod for production use wisely
		Easebuzz t = new Easebuzz(salt, Key, env, false);
	````	
	5.2 call the transactional api call 
	```
		string strForm = t.transactionDateAPI(merchant_email, transaction_date);
	```
	5.3 Required parameters in transactionDateAPI function
	```
	   			string merchant_email = Request.Form["email"].Trim();	//merchant email address 
	   			string transaction_date = Request.Form["transaction_date"].Trim(); // transaction date format (DD-MM-YYYY)
	```
	5.4 ready with transactional information date wise.

6. setup payout information date wise.

	6.1 create object of the class by pass key,salt,environment
	```
		string salt = "XXXXXX";
		string Key = "XXXXXX";
		string env = "test";		// test for testing env and prod for production use wisely
		Easebuzz t = new Easebuzz(salt, Key, env, false);
	````	
	6.2 call the payoutAPICall api call 
	```
		string strForm = t.payoutAPI(merchant_email, payout_date);
	```
	6.3 Required parameters in main function
	```
	    string merchant_email = Request.Form["email"].Trim();	//merchant email address 
        string payout_date = Request.Form["payout_date"].Trim();	//payout date format (DD-MM-YYYY)
    ```
	6.4 ready to start receiving payout information date wise.

7. setup Refund api so that you can start refunding your customers as you needed.

	7.1 create object of the class by pass key,salt,environment
	```
		string salt = "XXXXXX";
		string Key = "XXXXXX";
		string env = "test";		// test for testing env and prod for production use wisely
		Easebuzz t = new Easebuzz(salt, Key, env, false);		
	````	
	7.2 call the refund api call 
	```
		string strForm = t.RefundAPI(txnid, refund_amount, phone, amount, email);
	```
	7.3 Required parameters in main function
	```
	    	string txnid= Request.Form["txnid"].Trim();			// trasnaction id 
			string refund_amount=Request.Form["refund_amount"].Trim();	// refund amount should be float
			string phone=Request.Form["phone"].Trim();		// Customer's phone number
			string amount=Request.Form["amount"].Trim();	// actual amount before tdr and taxes
			string email=Request.Form["email"].Trim();		// customer's email address
    ```
	7.4 ready to start invoking refunds from your system.