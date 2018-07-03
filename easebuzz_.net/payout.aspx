<%@ Page Language="C#" Inherits="easebuzz_.net.payout" %>
<!DOCTYPE html>
<html>
<head runat="server">
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="ie=edge">
        <link rel="stylesheet" href="assets/css/style.css">
	<title>payout</title>
</head>
    <body>
        <div class="grid-container">
            <header class="wrapper">
                <div class="logo">
                    <a href="../index.html">
                        <img src="../assets/images/eb-logo.svg" alt="Easebuzz">
                    </a>
                </div>

                <div class="hedding">
                    <h2><a class="highlight" href="../index.html">Back</a></h2>
                </div>
            </header>
        </div>

        <div class="form-container">
            <h2>PAYOUT API</h2>
            <hr>
            <form id="form1" runat="server">
                <div class="main-form">
                    <h3>Mandatory Parameters</h3>
                    <hr>
                    <div class="mandatory-data">
                        <div class="form-field">
                            <label for="merchant_email">Merchant Email ID<sup>*</sup></label>
                            <input id="merchant_email" class="merchant_email" name="merchant_email" value=""
                            placeholder="payout@easebuzz.in">
                        </div>
        
                        <div class="form-field">
                            <label for="payout_date">Payout Date<sup>*</sup></label>
                            <input id="payout_date" class="payout_date" name="payout_date" value="" placeholder="DD-MM-YYYY">
                        </div>
        
                        
                    </div>
                
                    <div class="btn-submit">
                        <asp:Button id="button3" runat="server" Text="Payout Record" OnClick="payoutAPICall" />
                    </div> 
                </div>
            </form>
        </div>


        <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
        <script src="http://code.jquery.com/ui/1.11.0/jquery-ui.js"></script>

    </body>
</html>
