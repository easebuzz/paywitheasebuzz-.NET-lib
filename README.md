
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

	Easebuzz t = new Easebuzz(salt, Key, env);
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

3. setup transaction api in your system
	
	3.1 create object of the class by pass key,salt,environment
	```
		string salt = "XXXXXX";
		string Key = "XXXXXX";
		string env = "test";		// test for testing env and prod for production use wisely
		Easebuzz t = new Easebuzz(salt, Key, env);
	````	
	3.2 call the transactional api call 
	```
		string strForm = t.transactionAPI(txnid, amount, email, phone);
	```
	3.3 Required parameters in transactionAPicall 
	```
			string txnid = Request.Form["txnid"].Trim();
			string amount = Request.Form["amount"].Trim();
			string email = Request.Form["email"].Trim();
			string phone = Request.Form["phone"].Trim();
	```
	3.4 ready to start  transactional information real time.
	
4. setup transactional records date wise.

	4.1 create object of the class by pass key,salt,environment
	```
		string salt = "XXXXXX";
		string Key = "XXXXXX";
		string env = "test";		// test for testing env and prod for production use wisely
		Easebuzz t = new Easebuzz(salt, Key, env);
	````	
	4.2 call the transactional api call 
	```
		string strForm = t.transactionDateAPI(merchant_email, transaction_date);
	```
	4.3 Required parameters in transactionDateAPI function
	```
	   			string merchant_email = Request.Form["email"].Trim();	//merchant email address 
	   			string transaction_date = Request.Form["transaction_date"].Trim(); // transaction date format (DD-MM-YYYY)
	```
	4.4 ready with transactional information date wise.

5. setup payout information date wise.

	5.1 create object of the class by pass key,salt,environment
	```
		string salt = "XXXXXX";
		string Key = "XXXXXX";
		string env = "test";		// test for testing env and prod for production use wisely
		Easebuzz t = new Easebuzz(salt, Key, env);
	````	
	5.2 call the payoutAPICall api call 
	```
		string strForm = t.payoutAPI(merchant_email, payout_date);
	```
	5.3 Required parameters in main function
	```
	    string merchant_email = Request.Form["email"].Trim();	//merchant email address 
        string payout_date = Request.Form["payout_date"].Trim();	//payout date format (DD-MM-YYYY)
    ```
	5.4 ready to start receiving payout information date wise.

6. setup Refund api so that you can start refunding your customers as you needed.

	6.1 create object of the class by pass key,salt,environment
	```
		string salt = "XXXXXX";
		string Key = "XXXXXX";
		string env = "test";		// test for testing env and prod for production use wisely
		Easebuzz t = new Easebuzz(salt, Key, env);		
	````	
	6.2 call the refund api call 
	```
		string strForm = t.RefundAPI(txnid, refund_amount, phone, amount, email);
	```
	6.3 Required parameters in main function
	```
	    	string txnid= Request.Form["txnid"].Trim();			// trasnaction id 
			string refund_amount=Request.Form["refund_amount"].Trim();	// refund amount should be float
			string phone=Request.Form["phone"].Trim();		// Customer's phone number
			string amount=Request.Form["amount"].Trim();	// actual amount before tdr and taxes
			string email=Request.Form["email"].Trim();		// customer's email address
    ```
	6.4 ready to start invoking refunds from your system.