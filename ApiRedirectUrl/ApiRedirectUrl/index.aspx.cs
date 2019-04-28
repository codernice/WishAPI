using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApiRedirectUrl
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var client = new WebClient())
                {
                    try
                    {
                        var values = new NameValueCollection();
                        values["client_id"] = "xxxxxxxxxxxxxxx";
                        values["client_secret"] = "xxxxxxxxxxxxxxx";
                        values["code"] = "xxxxxxxxxxxxxxx";
                        values["grant_type"] = "authorization_code";
                        values["redirect_uri"] = "https://example.redirect_uri.com";
                        var response = client.UploadValues("https://merchant.wish.com/api/v2/oauth/access_token", values);
                        var responseString = Encoding.UTF8.GetString(response);
                        Response.Write(responseString);
                    }
                    catch (WebException we)
                    {
                        var errorMsg = "";
                        if (((WebException)we).Status == WebExceptionStatus.ProtocolError)
                        {
                            WebResponse errResp = ((WebException)we).Response; using (Stream respStream = errResp.GetResponseStream())
                            {
                                StreamReader reader = new StreamReader(respStream);
                                errorMsg = String.Format("{0}", reader.ReadToEnd());
                                Console.WriteLine(errorMsg);
                            }
                        }
                        Response.Write(errorMsg);
                    }
                }
            }
        }
    }
}