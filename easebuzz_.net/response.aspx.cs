﻿using System;
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

namespace easebuzz_.net
{

	public partial class response : System.Web.UI.Page
	{
		public void Page_Load(object sender, EventArgs e)
		{
			Response.Write(Request.QueryString["params"]);
		}
			
	}
}
    

