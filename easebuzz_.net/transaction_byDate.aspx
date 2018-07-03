<%@ Page Language="C#" Inherits="easebuzz_.net.transaction_byDate" %>
<!DOCTYPE html>
<html>
<head runat="server">
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="ie=edge">
        <link rel="stylesheet" href="assets/css/style.css">
	<title>transaction_byDate</title>
</head>
    <body>
        <div class="grid-container">
            <header class="wrapper">
                <div class="logo">
                    <a href="../index.html">
                        <img src="assets/images/eb-logo.svg" alt="Easebuzz">
                    </a>
                </div>

                <div class="hedding">
                    <h2><a class="highlight" href="../index.html">Back</a></h2>
                </div>
            </header>
        </div>
        
        <div class="form-container">
            <h2>TRANSACTION DATE API</h2>
            <hr>
            <form id="form1" runat="server">
                <div class="main-form">
                    <h3>Mandatory Parameters</h3>
                    <hr>
                    <div class="mandatory-data">
                        <div class="form-field">
                            <label for="merchant_email">Merchant Email ID<sup>*</sup></label>
                            <input id="email" class="merchant_email" name="email" value=""
                            placeholder="date.transaction@easebuzz.in">
                        </div>
        
                        <div class="form-field">
                            <label for="transaction_date">Transaction Date<sup>*</sup></label>
                            <input id="transaction_date" class="transaction_date" name="transaction_date" value="" placeholder="DD-MM-YYYY">
                        </div>         
                        

                    </div>
                    
                    <div class="btn-submit">
                        <asp:Button id="button1" runat="server" Text="Date wise transactions" OnClick="transactionDateAPICall" />
                    </div> 
                </div>           
            </form>
        </div>


        <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
        <script src="http://code.jquery.com/ui/1.11.0/jquery-ui.js"></script>

    </body>

</html>
