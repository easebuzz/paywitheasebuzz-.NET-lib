using System;
using System.Data;
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
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Collections.Specialized;


public partial class _Default : System.Web.UI.Page
{
    public string action1 = string.Empty;
    public string hash1 = string.Empty;
    public string txnid1 = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            key.Value = ConfigurationManager.AppSettings["MERCHANT_KEY"];
          
            if (!IsPostBack)
            {
                frmError.Visible = false; // error form
            }
            else
            {
                //frmError.Visible = true;
            }
            if (string.IsNullOrEmpty(Request.Form["hash"]))
            {
                submit.Visible = true;
            }
            else
            {
                submit.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Response.Write("<span style='color:red'>" + ex.Message + "</span>");

        }
       
    }
    public string Generatehash512(string text)
    {
        byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
	
        try
        { 
			string[] hashVarsSeq;
			string hash_string = string.Empty;


			if (string.IsNullOrEmpty(Request.Form["txnid"])) // generating txnid
			{
				Random rnd = new Random();
				string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
				txnid1 = strHash.ToString().Substring(0, 20);
				
			}
			else
			{
				txnid1 = Request.Form["txnid"];
			}	
			if (string.IsNullOrEmpty(Request.Form["hash"])) // generating hash value
			{	
				if (
					string.IsNullOrEmpty(ConfigurationManager.AppSettings["MERCHANT_KEY"]) ||
					string.IsNullOrEmpty(txnid1) ||
					string.IsNullOrEmpty(Request.Form["amount"]) ||
					string.IsNullOrEmpty(Request.Form["firstname"]) ||
					string.IsNullOrEmpty(Request.Form["email"]) ||
					string.IsNullOrEmpty(Request.Form["phone"]) ||
					string.IsNullOrEmpty(Request.Form["productinfo"]) ||
					string.IsNullOrEmpty(Request.Form["surl"]) ||
					string.IsNullOrEmpty(Request.Form["furl"]) 
					)
				{
					//error
					frmError.Visible = true;
					return;
				}
				else
				{
					frmError.Visible = false;
					hashVarsSeq = ConfigurationManager.AppSettings["hashSequence"].Split('|'); // spliting hash sequence from config
					hash_string = "";
					foreach (string hash_var in hashVarsSeq)
					{
						if (hash_var == "key")
						{
							hash_string = hash_string + ConfigurationManager.AppSettings["MERCHANT_KEY"];
							hash_string = hash_string + '|';
						}
						else if (hash_var == "txnid")
						{
							hash_string = hash_string + txnid1;
							hash_string = hash_string + '|';
						}
						else if (hash_var == "amount")
						{
							hash_string = hash_string + Convert.ToDecimal(Request.Form[hash_var]).ToString("g29");
							hash_string = hash_string + '|';
						}
						else
						{

							hash_string = hash_string + (Request.Form[hash_var] != null ? Request.Form[hash_var] : "");// isset if else
							hash_string = hash_string + '|';
						}
					}

					hash_string += ConfigurationManager.AppSettings["SALT"];// appending SALT

					hash1 = Generatehash512(hash_string).ToLower();         //generating hash
					action1 = ConfigurationManager.AppSettings["BASE_URL"] + "payment/initiateLink";// setting URL

				}
			}

			else if (!string.IsNullOrEmpty(Request.Form["hash"]))
			{
				hash1 = Request.Form["hash"];
				action1 = ConfigurationManager.AppSettings["BASE_URL"] + "payment/initiateLink";

			}
			if (!string.IsNullOrEmpty(hash1))
			{
				hash.Value = hash1; 
				txnid.Value = txnid1;

				System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
				data.Add("hash", hash.Value);
				data.Add("txnid", txnid.Value);
				data.Add("key", key.Value);
				string AmountForm = Convert.ToDecimal(amount.Text.Trim()).ToString("g29");// eliminating trailing zeros
				amount.Text = AmountForm;
				data.Add("amount", AmountForm);
				data.Add("firstname", firstname.Text.Trim());
				data.Add("email", email.Text.Trim());
				data.Add("phone", phone.Text.Trim());
				data.Add("productinfo", productinfo.Text.Trim());
				data.Add("surl", surl.Text.Trim());
				data.Add("furl", furl.Text.Trim());
				data.Add("udf1", udf1.Text.Trim());
				data.Add("udf2", udf2.Text.Trim());
				data.Add("udf3", udf3.Text.Trim());
				data.Add("udf4", udf4.Text.Trim());
				data.Add("udf5", udf5.Text.Trim());
				
				string url = action1;

				using (var client = new WebClient())
				{
					var values = new NameValueCollection();
					PreparePOSTForm(values, data);
					var response = client.UploadValues(url, values);

					var responseString = Encoding.Default.GetString(response);
					Response.Write("<span style='color:green'>" + responseString + "</span>");
					JavaScriptSerializer serializer = new JavaScriptSerializer();
					var dict = serializer.Deserialize<Dictionary<string,string>>(responseString);
					if(dict["status"]=="1"){
						Response.Redirect(ConfigurationManager.AppSettings["BASE_URL"]+"Pay/"+dict["data"]);
					}else{ // error
						Response.Write("<span style='color:red'>" + dict["data"] + "</span>");
					}
				}
				
			}
			else
			{
				//no hash
			}
		}
		catch (Exception ex)
		{
			Response.Write("<span style='color:red'>" + ex.Message + "</span>");
		}
    }
    private void PreparePOSTForm(NameValueCollection values,  System.Collections.Hashtable  data)
	{
		foreach (System.Collections.DictionaryEntry key in data)
		{	
			values[key.Key.ToString()] = key.Value.ToString();
		}
	}
}
