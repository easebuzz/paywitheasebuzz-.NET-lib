<%@ Page Language="C#" Inherits="easebuzz_.net.refund" %>
<!DOCTYPE html>
<html>
<head runat="server">
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="ie=edge">
        <link rel="stylesheet" href="assets/css/style.css">
	<title>refund</title>
</head>



    <body>
        <div class="grid-container">
            <header class="wrapper">
                <div class="logo">
                    <a href="#">
                        <img src="assets/images/eb-logo.svg" alt="Easebuzz">
                    </a>
                </div>

                <div class="hedding">
                    <h2><a class="highlight" href="../index.html">Back</a></h2>
                </div>
            </header>

            <div class="form-container">
                <h2>REFUND API</h2>
                <hr>
                <form id="form1" runat="server">
                    <div class="main-form">
                        <h3>Mandatory Parameters</h3>
                        <hr>
                        <div class="mandatory-data">

                            <div class="form-field">
                                <label for="txnid">Easebuzz Transaction ID<sup>*</sup></label>
                                <input id="txnid" class="txnid" name="txnid" value="" placeholder="SBD12345">
                            </div>
            
                            <div class="form-field">
                                <label for="refund_amount">Refund Amount<sup>(should be float)*</sup></label>
                                <input id="refund_amount" class="refund_amount" name="refund_amount" value="" placeholder="125.25">
                            </div>
            
                            <div class="form-field">
                                <label for="phone">Customer Phone Number<sup>*</sup></label>
                                <input id="phone" class="phone" name="phone" value="" placeholder="0123456789">
                            </div>

                            <div class="form-field">
                                <label for="email">Customer Email ID<sup>*</sup></label>
                                <input id="email" class="email" name="email" value="" placeholder="refund@easebuzz.in">
                            </div>
            
                            <div class="form-field">
                                <label for="amount">Customer's Paid Amount<sup>(should be float)*</sup></label>
                                <input id="amount" class="amount" name="amount" value="" placeholder="125.25">
                            </div>
                        </div>


                        <div class="btn-submit">
                            <asp:Button id="button1" runat="server" Text="refund" OnClick="RefundApiCall" />
                        </div>

                    </div>
                </form>
            </div>

        </div>
    </body>

</html>
