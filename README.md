
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

	2.1 create object of the class by pass key,salt,environment
	```
		string salt = "XXXXXX";
		string Key = "XXXXXX";
		string env = "test";		// test for testing env and prod for production use wisely
		Easebuzz t = new Easebuzz(salt, Key, env);
	````
	2.2 call the initiate paymentv function and invoke the form submit
	```
		string strForm = t.initiatePaymentAPI(amount, firstname, email, phone, productinfo, surl, furl,Txnid);
    	Page.Controls.Add(new LiteralControl(strForm));
    ```
    2.3 Parameters required in initiate payment
    ```
    		string amount = Request.Form["amount"].Trim();
			string firstname = Request.Form["firstname"].Trim();
			string email = Request.Form["email"].Trim();
			string phone = Request.Form["phone"].Trim();
			string productinfo = Request.Form["productinfo"].Trim();
			string surl = Request.Form["surl"].Trim();
			string furl = Request.Form["furl"].Trim();
			string Txnid = Request.Form["Txnid"].Trim();
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