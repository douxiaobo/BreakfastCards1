﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;

using LitJson;
using Newtonsoft.Json.Linq;
using System.Resources;
using Newtonsoft.Json;
using BreakfastCards1.Core;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Cryptography;

namespace BreakfastCards1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string ThisYear = DateTime.Now.ToString("yyyy");
        string ThisMonth = DateTime.Now.ToString("MMMM", CultureInfo.GetCultureInfo("en-US"));

        bool LostCard_bool;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindYear();
                BindMonth();
                BindGroupName();
                //BindActualBreakfast_AddCards();
                //BindActualBreakfast_Add_CheckboxList();
                GridView1.DataBind();
            }
            /*
            string url = @"http://timor.tech/api/holiday/info/2022-09-01";
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream webstream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(webstream);
            string json = streamReader.ReadToEnd();
            Label_Json.Text = json;
            */
        }

        protected void BindYear()
        {
            DropDownList_ActualBreakfast_AddYear.Items.Clear();
            DropDownList_ActualBreakfast_InquiryYear.Items.Clear();
            DropDownList_Add_Year.Items.Clear();
            DropDownList_Delete_Year.Items.Clear();
            DropDownList_Inquiry_Year.Items.Clear();
            DropDownList_Json_Year.Items.Clear();
            DropDownList_Revise_Year.Items.Clear();

            DropDownList_Inquiry_Year.Items.Add("Never Choose");

            //年份Year
            int ThisYearInt = Convert.ToInt16(ThisYear);
            if (ThisMonth == "December")
                ThisYearInt++;
            for (int i = ThisYearInt; i >= 1997; i--)
            {
                DropDownList_Add_Year.Items.Add(i.ToString());
                DropDownList_Delete_Year.Items.Add(i.ToString());
                DropDownList_Revise_Year.Items.Add(i.ToString());
                DropDownList_Inquiry_Year.Items.Add(i.ToString());
                DropDownList_Json_Year.Items.Add(i.ToString());
                DropDownList_ActualBreakfast_AddYear.Items.Add(i.ToString());
                DropDownList_ActualBreakfast_InquiryYear.Items.Add(i.ToString());
            }
        }

        protected void BindMonth()
        {
            DropDownList_ActualBreakfast_AddMonth.Items.Clear();
            DropDownList_ActualBreakfast_InquiryMonth.Items.Clear();
            DropDownList_Add_Month.Items.Clear();
            DropDownList_Delete_Month.Items.Clear();
            DropDownList_Inquiry_Month.Items.Clear();
            DropDownList_Json_Month.Items.Clear();
            DropDownList_Revise_Month.Items.Clear();

            DropDownList_Inquiry_Month.Items.Add("Never Choose");

            //Month月份
            Dictionary<int, string> Month_DigitToEng = new Dictionary<int, string>();
            Month_DigitToEng.Add(1, "January");
            Month_DigitToEng.Add(2, "February");
            Month_DigitToEng.Add(3, "March");
            Month_DigitToEng.Add(4, "April");
            Month_DigitToEng.Add(5, "May");
            Month_DigitToEng.Add(6, "June");
            Month_DigitToEng.Add(7, "July");
            Month_DigitToEng.Add(8, "August");
            Month_DigitToEng.Add(9, "September");
            Month_DigitToEng.Add(10, "October");
            Month_DigitToEng.Add(11, "November");
            Month_DigitToEng.Add(12, "December");

            string ThisMonth = DateTime.Now.ToString("MM");
            for(int i=Convert.ToInt16(ThisMonth)-1;i<=12;i++)
            {
                DropDownList_ActualBreakfast_AddMonth.Items.Add(Month_DigitToEng[i]);
                DropDownList_ActualBreakfast_InquiryMonth.Items.Add(Month_DigitToEng[i]);
                DropDownList_Add_Month.Items.Add(Month_DigitToEng[i]);
                DropDownList_Delete_Month.Items.Add(Month_DigitToEng[i]);
                DropDownList_Inquiry_Month.Items.Add(Month_DigitToEng[i]);
                DropDownList_Json_Month.Items.Add(Month_DigitToEng[i]);
                DropDownList_Revise_Month.Items.Add(Month_DigitToEng[i]);
            }
            for(int i=1;i<Convert.ToInt16(ThisMonth)-1;i++)
            {
                DropDownList_ActualBreakfast_AddMonth.Items.Add(Month_DigitToEng[i]);
                DropDownList_ActualBreakfast_InquiryMonth.Items.Add(Month_DigitToEng[i]);
                DropDownList_Add_Month.Items.Add(Month_DigitToEng[i]);
                DropDownList_Delete_Month.Items.Add(Month_DigitToEng[i]);
                DropDownList_Inquiry_Month.Items.Add(Month_DigitToEng[i]);
                DropDownList_Json_Month.Items.Add(Month_DigitToEng[i]);
                DropDownList_Revise_Month.Items.Add(Month_DigitToEng[i]);
            }

            /*
            Dictionary<int, string>.ValueCollection MonthCol = Month_DigitToEng.Values;
            foreach (string value in MonthCol)
            {
                DropDownList_Add_Month.Items.Add(value.ToString());
                DropDownList_Delete_Month.Items.Add(value.ToString());
                DropDownList_Revise_Month.Items.Add(value.ToString());
                DropDownList_Inquiry_Month.Items.Add(value.ToString());
                DropDownList_Json_Month.Items.Add(value.ToString());
                DropDownList_ActualBreakfast_AddMonth.Items.Add(value.ToString());
                DropDownList_ActualBreakfast_InquiryMonth.Items.Add(value.ToString());
            }
            */
        }

        protected void BindGroupName()
        {
            DropDownList_ActualBreakfast_AddGroupName.Items.Clear();
            DropDownList_ActualBreakfast_InquiryGroupName.Items.Clear();
            DropDownList_Add_GroupName.Items.Clear();
            DropDownList_Delete_GroupName.Items.Clear();
            DropDownList_Inquiry_GroupName.Items.Clear();
            DropDownList_Revise_GroupName.Items.Clear();

            DropDownList_Inquiry_GroupName.Items.Add("Never Choose");

            //GroupName
            Dictionary<int, string> GroupName_Dic = new Dictionary<int, string>();
            GroupName_Dic.Add(1, "Intune");
            GroupName_Dic.Add(2, "Office");
            GroupName_Dic.Add(3, "SCCM");
            GroupName_Dic.Add(4, "Teams");
            GroupName_Dic.Add(5, "POD1");
            GroupName_Dic.Add(6, "POD2");
            GroupName_Dic.Add(7, "S500_1");
            GroupName_Dic.Add(8, "S500_2");
            GroupName_Dic.Add(9, "SharePoint");
            Dictionary<int, string>.ValueCollection GroupNameCol = GroupName_Dic.Values;
            foreach (string value in GroupNameCol)
            {
                DropDownList_Add_GroupName.Items.Add(value.ToString());
                DropDownList_Delete_GroupName.Items.Add(value.ToString());
                DropDownList_Revise_GroupName.Items.Add(value.ToString());
                DropDownList_Inquiry_GroupName.Items.Add(value.ToString());
                DropDownList_ActualBreakfast_AddGroupName.Items.Add(value.ToString());
                DropDownList_ActualBreakfast_InquiryGroupName.Items.Add(value.ToString());
            }
        }
        
        protected void BindActualBreakfast_AddCards()
        {
            DropDownList_ActualBreakfast_AddCards.Items.Clear();

            string ID = DropDownList_ActualBreakfast_AddYear.SelectedValue + Month_EngToDigit(DropDownList_ActualBreakfast_AddMonth.SelectedValue) + GroupName_Num(DropDownList_ActualBreakfast_AddGroupName.SelectedValue);

            // HardCode for testing... 
            //ID = "20220801";

            BreakfastCardsEntities db = new BreakfastCardsEntities();
            var client = db.Table_FourName.FirstOrDefault(c => c.ID == ID);

            Dictionary<int, string> ActualBreakfast_Add_DigitToEng = new Dictionary<int, string>();
            ActualBreakfast_Add_DigitToEng.Add(1, "First");
            ActualBreakfast_Add_DigitToEng.Add(2, "Second");
            ActualBreakfast_Add_DigitToEng.Add(3, "Third");
            ActualBreakfast_Add_DigitToEng.Add(4, "Fourth");
            ActualBreakfast_Add_DigitToEng.Add(5, "Fifth");
            ActualBreakfast_Add_DigitToEng.Add(6, "Sixth");
            ActualBreakfast_Add_DigitToEng.Add(7, "Seventh");
            ActualBreakfast_Add_DigitToEng.Add(8, "Eighth");
            ActualBreakfast_Add_DigitToEng.Add(9, "Ninth");
            ActualBreakfast_Add_DigitToEng.Add(10, "Tenth");
            ActualBreakfast_Add_DigitToEng.Add(11, "Eleventh");
            ActualBreakfast_Add_DigitToEng.Add(12, "Twelfth");
            ActualBreakfast_Add_DigitToEng.Add(13, "Thirteenth");
            ActualBreakfast_Add_DigitToEng.Add(14, "Fourteenth");
            ActualBreakfast_Add_DigitToEng.Add(15, "Fifteenth");

            try
            {
                int a = Convert.ToInt16(client.Quantity.Value);
                for (int i = 1; i <= a; i++)
                {
                    DropDownList_ActualBreakfast_AddCards.Items.Add(ActualBreakfast_Add_DigitToEng[i]);
                }
            }
            catch (System.NullReferenceException)
            {
                Label_Inquiry.Text = "Sorry_ActualBreakfast_AddCards!";
            }

        }

        protected void BindActualBreakfast_Add_CheckboxList()
        {
            CheckBoxList_ActualBreakfast_Add.Items.Clear();
            int year = Convert.ToInt16(DropDownList_ActualBreakfast_AddYear.Text);

            int month = Convert.ToInt16(Month_EngToDigit(DropDownList_ActualBreakfast_AddMonth.Text));

            int days = DateTime.DaysInMonth(year, month);

            DateTime dt = Convert.ToDateTime(DropDownList_ActualBreakfast_AddYear.Text + "-" + DropDownList_ActualBreakfast_AddMonth.Text + "-" + 01.ToString());
            
            for (int i = 1; i <= days; i++) // 问题出在这里，这里重复了多次去触发下文的request
            {
                //我遇到一个棘手的问题，在CheckBoxList控件里，遇到周五之后，都要换行，不知道怎么解决。间隔距离，不知道怎么解决。
                if (dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday) 
                {
                    /*
                    if ((i == 1 && dt.DayOfWeek == DayOfWeek.Friday) || (i == 4 && dt.DayOfWeek == DayOfWeek.Monday))
                        CheckBoxList_ActualBreakfast_Add.RepeatColumns = 1;
                    else if ((i == 1 && dt.DayOfWeek == DayOfWeek.Thursday) || (i == 2 && dt.DayOfWeek == DayOfWeek.Friday) || (i == 5 && dt.DayOfWeek == DayOfWeek.Monday))
                        CheckBoxList_ActualBreakfast_Add.RepeatColumns = 2;
                    else if (i == 1 && dt.DayOfWeek == DayOfWeek.Wednesday)
                        CheckBoxList_ActualBreakfast_Add.RepeatColumns = 3;
                    else if (i == 1 && dt.DayOfWeek == DayOfWeek.Tuesday)
                        CheckBoxList_ActualBreakfast_Add.RepeatColumns = 4;
                    else if ((i == 1 && dt.DayOfWeek == DayOfWeek.Monday) || (i == 4 && dt.DayOfWeek == DayOfWeek.Thursday))
                        CheckBoxList_ActualBreakfast_Add.RepeatColumns = 5;
                    else if(i>4)
                        CheckBoxList_ActualBreakfast_Add.RepeatColumns = 5;
                    */
                    
                    string url = @"http://timor.tech/api/holiday/info/";
                    url += year.ToString() + "-" + month.ToString() + "-" + i.ToString().PadLeft(2, '0');
                    string json=urltojson(url);
                    var jsonDes = JsonConvert.DeserializeObject<WorkDay>(json);
                    if (Convert.ToInt16(jsonDes.Type.Type) == 0)
                        CheckBoxList_ActualBreakfast_Add.Items.Add(DropDownList_ActualBreakfast_AddYear.Text + "-" + Month_EngToDigit(DropDownList_ActualBreakfast_AddMonth.Text) + "-" + i.ToString("00"));
                    
                }
                dt = dt.AddDays(1);
            }
            
        }

        protected string urltojson(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url); // 这里重复多次去请求页面，导致了429的问题，我们要避免这样的情况发生，不能短时间内触发多个request去页面
                WebResponse response = request.GetResponse();   //System.Net.WebException:“请求被中止: 操作超时。”//System.Net.WebException:“远程服务器返回错误: (429) Too Many Requests。”
                Stream webstream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(webstream);
                string json = streamReader.ReadToEnd();
                return json;
            }
            catch(System.Net.WebException)
            {
                WebClient wc = new WebClient();
                wc.Credentials=CredentialCache.DefaultCredentials;
                wc.Encoding=Encoding.UTF8;
                string returnText=wc.DownloadString(url);   //System.Net.WebException:“远程服务器返回错误: (429) Too Many Requests。”
                return returnText;

                /*
                Uri uri = new Uri(url);
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(uri);
                HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();//System.Net.WebException:“远程服务器返回错误: (429) Too Many Requests。”
                Stream respStream = httpResp.GetResponseStream();
                StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);
                string strBuff = respStreamReader.ReadToEnd();
                return strBuff;
                */
            }
        }

        protected void Button_Add_Comfirm_Click(object sender, EventArgs e)
        {
            Table_FourName a = new Table_FourName();
            a.Date = DropDownList_Add_Year.Text + "-" + DropDownList_Add_Month.Text;
            a.GroupName = DropDownList_Add_GroupName.Text;
            a.Quantity = Convert.ToInt16(TextBox_Add_Quantity.Text);

            //在Table_FourName里，ID的规则，顺序分别：4位是年份，2位是月份，2位是团队代号。
            a.ID = DropDownList_Add_Year.Text + Month_EngToDigit(DropDownList_Add_Month.Text) + GroupName_Num(DropDownList_Add_GroupName.Text);

            Dictionary<string, string> GroupName_Manager = new Dictionary<string, string>();
            GroupName_Manager.Add("Intune", "John Huang");
            GroupName_Manager.Add("Office", "Amanda Luo");
            GroupName_Manager.Add("SCCM", "Eric Zhang");
            GroupName_Manager.Add("Teams", "Peter Gao");
            GroupName_Manager.Add("POD1", "Haiwei Xu");
            GroupName_Manager.Add("POD2", "Liu Bai");
            GroupName_Manager.Add("S500_1", "Dongli Li");
            GroupName_Manager.Add("S500_2", "Dorothy Deng");
            GroupName_Manager.Add("SharePoint", "Cindy Ge");
            a.Manager = GroupName_Manager[DropDownList_Add_GroupName.Text];

            try
            {
                BreakfastCardsEntities db = new BreakfastCardsEntities();
                db.Table_FourName.Add(a);
                db.SaveChanges();
                Response.Redirect(Request.Url.ToString());
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                Label_Json.Text = "Sorry";
            }
        }

        protected void Button_Delete_Click(object sender, EventArgs e)
        {
            BreakfastCardsEntities db = new BreakfastCardsEntities();
            Table_FourName a = new Table_FourName();

            a.ID = DropDownList_Delete_Year.Text + Month_EngToDigit(DropDownList_Delete_Month.Text) + GroupName_Num(DropDownList_Delete_GroupName.Text);

            try
            {
                db.Table_FourName.Attach(a);
                db.Table_FourName.Remove(a);
                db.SaveChanges();
                Response.Redirect(Request.Url.ToString());

            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
            {
                Label_Json.Text = "Sorry_catch";
            }
        }

        protected string GroupName_Num(string groupname)
        {
            Dictionary<string, string> GroupName_Num = new Dictionary<string, string>();
            GroupName_Num.Add("Intune", "01");
            GroupName_Num.Add("Office", "02");
            GroupName_Num.Add("SCCM", "03");
            GroupName_Num.Add("Teams", "04");
            GroupName_Num.Add("POD1", "05");
            GroupName_Num.Add("POD2", "06");
            GroupName_Num.Add("S500_1", "07");
            GroupName_Num.Add("S500_2", "08");
            GroupName_Num.Add("SharePoint", "09");
            return GroupName_Num[groupname];
        }

        protected string Month_EngToDigit(string month)
        {
            Dictionary<string, string> Month_EngToDigit = new Dictionary<string, string>();
            Month_EngToDigit.Add("January", "01");
            Month_EngToDigit.Add("February", "02");
            Month_EngToDigit.Add("March", "03");
            Month_EngToDigit.Add("April", "04");
            Month_EngToDigit.Add("May", "05");
            Month_EngToDigit.Add("June", "06");
            Month_EngToDigit.Add("July", "07");
            Month_EngToDigit.Add("August", "08");
            Month_EngToDigit.Add("September", "09");
            Month_EngToDigit.Add("October", "10");
            Month_EngToDigit.Add("November", "11");
            Month_EngToDigit.Add("December", "12");
            return Month_EngToDigit[month];         //System.Collections.Generic.KeyNotFoundException:“给定关键字不在字典中。”
        }

        protected string EngOrderToDigit(string a)
        {
            Dictionary<string, string> EngOrderToDigit = new Dictionary<string, string>();
            EngOrderToDigit.Add("First", "01");
            EngOrderToDigit.Add("Second", "02");
            EngOrderToDigit.Add("Third", "03");
            EngOrderToDigit.Add("Fourth", "04");
            EngOrderToDigit.Add("Fifth", "05");
            EngOrderToDigit.Add("Sixth", "06");
            EngOrderToDigit.Add("Seventh", "07");
            EngOrderToDigit.Add("Eighth", "08");
            EngOrderToDigit.Add("Ninth", "09");
            EngOrderToDigit.Add("Tenth", "10");
            EngOrderToDigit.Add("Eleventh", "11");
            EngOrderToDigit.Add("Twelfth", "12");
            EngOrderToDigit.Add("Thirteenth", "13");
            EngOrderToDigit.Add("Fourteenth", "14");
            EngOrderToDigit.Add("Fifteenth", "15");

            return EngOrderToDigit[a];
        }

        protected void Button_Revise_Click(object sender, EventArgs e)
        {
            BreakfastCardsEntities db = new BreakfastCardsEntities();

            Table_FourName a = new Table_FourName() { ID = DropDownList_Revise_Year.Text + Month_EngToDigit(DropDownList_Revise_Month.Text) + GroupName_Num(DropDownList_Revise_GroupName.Text) };

            try
            {
                db.Table_FourName.Attach(a);
                a.Quantity = Convert.ToInt16(TextBox_Revise_Quantity.Text);
                db.SaveChanges();
                Response.Redirect(Request.Url.ToString());
            }
            catch
            {
                Label_Inquiry.Text = "Sorry_Revise!";
            }
        }

        protected void Button_Inquiry_Click(object sender, EventArgs e)
        {
            BreakfastCardsEntities db = new BreakfastCardsEntities();
            Label_Inquiry.Text = "";
            if (DropDownList_Inquiry_Year.Text != "Never Choose" && DropDownList_Inquiry_Month.Text != "Never Choose" && DropDownList_Inquiry_GroupName.Text != "Never Choose")//OK
            {
                string Date = DropDownList_Inquiry_Year.Text + "-" + DropDownList_Inquiry_Month.Text;
                string GroupName = DropDownList_Inquiry_GroupName.Text;
                var clients = from c in db.Table_FourName
                              where c.Date == Date && c.GroupName == GroupName
                              select c;
                foreach (var client in clients)
                {
                    Label_Inquiry.Text += "Data:" + client.Date + "----" + "GroupName:" + client.GroupName + "----" + "Manager:" + client.Manager + "<br/>";
                }
                GridView_Inquiry.DataSource = clients.ToList();
                GridView_Inquiry.DataBind();
            }
            else if (DropDownList_Inquiry_Year.Text != "Never Choose" && DropDownList_Inquiry_Month.Text != "Never Choose")//OK
            {
                string Date = DropDownList_Inquiry_Year.Text + "-" + DropDownList_Inquiry_Month.Text;
                var clients = from c in db.Table_FourName
                              where c.Date == Date
                              select c;
                foreach (var client in clients)
                {
                    Label_Inquiry.Text += "Data:" + client.Date + "----" + "GroupName:" + client.GroupName + "----" + "Manager:" + client.Manager + "<br/>";
                }
                GridView_Inquiry.DataSource = clients.ToList();
                GridView_Inquiry.DataBind();
            }
            else if (DropDownList_Inquiry_Year.Text != "Never Choose" && DropDownList_Inquiry_GroupName.Text != "Never Choose")  //OK
            {
                string Year = DropDownList_Inquiry_Year.Text;
                string GroupName = DropDownList_Inquiry_GroupName.Text;
                var clients = from c in db.Table_FourName
                              where c.Date.StartsWith(Year) && c.GroupName == GroupName
                              select c;
                foreach (var client in clients)
                {
                    Label_Inquiry.Text += "Data:" + client.Date + "----" + "GroupName:" + client.GroupName + "----" + "Manager:" + client.Manager + "<br/>";
                }
                GridView_Inquiry.DataSource = clients.ToList();
                GridView_Inquiry.DataBind();
            }
            else if (DropDownList_Inquiry_Month.Text != "Never Choose" && DropDownList_Inquiry_GroupName.Text != "Never Choose")
            {
                string Month = DropDownList_Inquiry_Month.Text;
                string GroupName = DropDownList_Inquiry_GroupName.Text;
                var clients = from c in db.Table_FourName
                              where c.Date.EndsWith(Month) && c.GroupName == GroupName
                              select c;
                foreach (var client in clients)
                {
                    Label_Inquiry.Text += "Data:" + client.Date + "----" + "GroupName:" + client.GroupName + "----" + "Manager:" + client.Manager + "<br/>";
                }
                GridView_Inquiry.DataSource = clients.ToList();
                GridView_Inquiry.DataBind();
            }
            else
            {
                Label_Inquiry.Text = "Sorry!";//不行
            }
        }

        protected int workdays(string year, string month)
        {
            int workdays = 0;
            int days=DateTime.DaysInMonth(Convert.ToInt16(year), Convert.ToInt16(Month_EngToDigit(month)));
            DateTime dt = Convert.ToDateTime(year + "-" + month + "-" + 01.ToString());
            for(int i=1;i<=days;i++)
            {
                if (dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday)
                {
                    string url = @"http://timor.tech/api/holiday/info/";
                    url += year.ToString() + "-" + month.ToString() + "-" + i.ToString().PadLeft(2, '0');
                    string json = urltojson(url);
                    var jsonDes = JsonConvert.DeserializeObject<WorkDay>(json);
                    if (Convert.ToInt16(jsonDes.Type.Type) == 0)
                        workdays++;

                    /*                    
                    WebClient client = new WebClient();
                    client.Encoding = Encoding.UTF8;
                    var jsondata=client.DownloadString(url);
                    var model = JsonConvert.DeserializeObject<Types>(jsondata);//Newtonsoft.Json.JsonReaderException:“Unexpected character encountered while parsing value: {. Path 'type', line 1, position 18.”
                    if (model.type == "0")
                        workdays++;
                    */
                    /*
                    WebRequest request = WebRequest.Create(url);
                    WebResponse response = request.GetResponse();
                    Stream webstream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(webstream);
                    string json = streamReader.ReadToEnd();
                    JObject jobj = JObject.Parse(json);
                    JArray types = JArray.Parse(jobj["type"].ToString());
                    if (types["type"].ToString() == "1")
                        workdays++;
                    */

                    /*
                    string url = "https://www.mxnzp.com/api/holiday/single/";
                    url += year.ToString() + "-" + month.ToString() + "-" + i.ToString().PadLeft(2,'0');
                    url += "ignoreHoliday=false&app_id=igsgjipmqtktwrmp&app_secret=a2plWmRqNSs2MUNseVlSYnJtTEdzZz09";
                    WebRequest request = WebRequest.Create(url);
                    WebResponse response = request.GetResponse();
                    Stream webstream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(webstream);
                    string json = streamReader.ReadToEnd();
                    Newtonsoft.Json.Linq.JObject jobject = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    string typeDsc = jobject["date"]["typeDes"].ToString();     //System.NullReferenceException:“未将对象引用设置到对象的实例。”
                    if (typeDsc == "工作日")
                        workdays++;
                    */
                    /*
                    WebRequest request = WebRequest.Create(url);
                    WebResponse response = request.GetResponse();
                    Stream webstream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(webstream);
                    string json = streamReader.ReadToEnd();
                    Newtonsoft.Json.Linq.JObject jobject = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    */
                    /*
                    string holiday = jobject["holiday"].ToString();
                    if(holiday==null
                    /*
                        workdays++;
                    */
                    /*
                    decimal type_type = Convert.ToDecimal(jobject["type"]["type"]);
                    if (type_type == 0)
                        workdays++;
                    */
                    /*
                    string type_type = jobject["type"]["type"].ToString();
                    if (type_type == "0")
                        workdays++;
                    */

                    /*
                    JavaScriptSerializer workdays_json = new JavaScriptSerializer();
                    JsonContent content = workdays_json.Deserialize<JsonContent>(url);
                    if (content.holiday.holiday != true)
                        workdays++;
                    */

                    /*
                    WebRequest request = WebRequest.Create(url);
                    WebResponse response = request.GetResponse();       //System.Net.WebException:“无法连接到远程服务器”
                    Stream webstream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(webstream);
                    string json = streamReader.ReadToEnd();
                    JavaScriptSerializer workdays_Json = new JavaScriptSerializer();
                    JsonContent Workdays_Content = workdays_Json.Deserialize<JsonContent>(json);
                    */

                    /*
                    System.NullReferenceException:“未将对象引用设置到对象的实例。”

                    BreakfastCards1.WebForm1.JsonContent.holiday.get 返回 null。
                    */

                    //if (Convert.ToInt16(Workdays_Content.type.type) == 0)
                    //    workdays++;

                    //if (Workdays_Content.type.type=='0')
                    //    workdays++;


                    /*
                    WebRequest request = WebRequest.Create(url);
                    WebResponse response = request.GetResponse();
                    Stream webstream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(webstream);
                    string json = streamReader.ReadToEnd();
                    JavaScriptSerializer json1 = new JavaScriptSerializer();
                    Dictionary<string, object> DicText = (Dictionary<string, object>)json1.DeserializeObject(json);

                    if (DicText["type"] == 'Y')        //这个代码有问题，大括号/花括号的事，不知道怎么实现holiday!=true。
                        workdays++;
                    */




                    //Types type = json1.Deserialize<Types>(json1);
                    //if (type["type"] == 1)
                    ;//    workdays++;
                    //Dictionary<string, object> DicText = (Dictionary<string, object>)json1.DeserializeObject(json);
                    //if (DicText["holiday"] == null )        //这个代码有问题，大括号/花括号的事，不知道怎么实现holiday!=true。
                    //    workdays++;
                    

                    /*  周一至周五非节假日的Json格式例子：
                    {"code":0,
                    "type":{"type":0,"name":"周四","week":4},
                    "holiday":null}
                    */

                    /*
                    https://timor.tech/api/holiday/info/2022-09-12的Json格式例子
                    { "code":0,"type":{ "type":2,"name":"中秋节","week":1},"holiday":{ "holiday":true,"name":"中秋节","wage":2,"date":"2022-09-12","rest":1} }
                    */
                }
                dt = dt.AddDays(1);
            }            
            return workdays;
        }

        protected void Button_Json_Click(object sender, EventArgs e)
        {
            int days = DateTime.DaysInMonth(Convert.ToInt16(DropDownList_Json_Year.Text), Convert.ToInt16(Month_EngToDigit(DropDownList_Json_Month.Text)));
            Label_Json.Text = "The Days of Month:" + days + "<br/>";
            Label_Json.Text += "The WorkDays of Month:" + workdays(DropDownList_Json_Year.Text,DropDownList_Json_Month.Text) + "<br/>";
        }

        protected void DropDownList_ActualBreakfast_AddYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindActualBreakfast_AddCards();
            BindActualBreakfast_Add_CheckboxList();
        }

        protected void DropDownList_ActualBreakfast_AddMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindActualBreakfast_AddCards();
            BindActualBreakfast_Add_CheckboxList();
        }

        protected void DropDownList_ActualBreakfast_AddGroupName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindActualBreakfast_AddCards();
            BindActualBreakfast_Add_CheckboxList();
        }
        
        protected void DropDownList_ActualBreakfast_AddCards_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindActualBreakfast_Add_CheckboxList();            
        }

        protected void Button_Actual_Breakfast_CheckBoxList_Add(object sender, EventArgs e)
        {
            BreakfastCardsEntities db = new BreakfastCardsEntities();

            String year = DropDownList_ActualBreakfast_AddYear.SelectedValue;
            String month = Month_EngToDigit(DropDownList_ActualBreakfast_AddMonth.SelectedValue);
            String groupname=GroupName_Num(DropDownList_ActualBreakfast_AddGroupName.SelectedValue);

            String cards = EngOrderToDigit(DropDownList_ActualBreakfast_AddCards.SelectedValue);

            int actualquantity = 0;

            Table_ActualQuantity a = new Table_ActualQuantity();            

            //在Table_ActualQuantity里，ID的规则，顺序分别：4位年份，2位月份，2位团队代码，2位卡号顺序。
            a.ID= year + month + groupname + cards;

            a.Year = year;
            a.Month = DropDownList_ActualBreakfast_AddMonth.SelectedValue;
            a.GroupName = DropDownList_ActualBreakfast_AddGroupName.SelectedValue;
            a.Cards = DropDownList_ActualBreakfast_AddCards.SelectedValue;

            Table_BreakfastBoolean b = new Table_BreakfastBoolean();

            foreach (ListItem item in CheckBoxList_ActualBreakfast_Add.Items)       //代码有问题。
            {       
                //在Table_BreakfastBoolean里，ID的规则，顺序分别：4位是年份，2位是月份，2位是团队代号，2位是卡号顺序，2位是日期
                b.ID = year + month + groupname + cards+ item.Text.Substring(8);
                b.Year = year;
                b.Month = DropDownList_ActualBreakfast_AddMonth.SelectedValue;
                b.GroupName=DropDownList_ActualBreakfast_AddGroupName.SelectedValue;
                b.Cards = DropDownList_ActualBreakfast_AddCards.SelectedValue;
                b.Data = item.Text.Substring(8);
                if(item.Selected)
                {
                    actualquantity++;
                    b.Breakfast_Boolean = "True";

                }
                else
                {
                    b.Breakfast_Boolean = "False";
                }
                try
                {
                    db.Table_BreakfastBoolean.Add(b);
                    //db.SaveChanges();                               //失败，不知道怎么解决。
                    //Response.Redirect(Request.Url.ToString());
                }
                catch(System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    Label_Json.Text = "Sorry_ActualBreakfast_Add";
                }
                
            }
            a.ActualQuantity = actualquantity;
            db.Table_ActualQuantity.Add(a);
            db.SaveChanges();
            Response.Redirect(Request.Url.ToString());
        }

        protected void FullAttendanceAndLostCard_ActualQuantity()
        {
            BreakfastCardsEntities db = new BreakfastCardsEntities();
            String year = DropDownList_ActualBreakfast_AddYear.SelectedValue;
            String month = Month_EngToDigit(DropDownList_ActualBreakfast_AddMonth.SelectedValue); 
            String groupname = GroupName_Num(DropDownList_ActualBreakfast_AddGroupName.SelectedValue);
            String cards = EngOrderToDigit(DropDownList_ActualBreakfast_AddCards.SelectedValue);           

            Table_ActualQuantity a = new Table_ActualQuantity();

            //在Table_ActualQuantity里，ID的规则，顺序分别：4位年份，2位月份，2位团队代码，2位卡号顺序。
            a.ID = year + month + groupname + cards;

            a.Year = year;
            a.Month = DropDownList_ActualBreakfast_AddMonth.SelectedValue;
            a.GroupName = DropDownList_ActualBreakfast_AddGroupName.SelectedValue;
            a.Cards = DropDownList_ActualBreakfast_AddCards.SelectedValue;

            if (LostCard_bool == true)
            {
                a.LostCard_Boolean = "True";
            }
            else
            {
                a.LostCard_Boolean = "False";
            }
            int workday = workdays(year, month);
            a.ActualQuantity = workday;
            db.Table_ActualQuantity.Add(a);
            db.SaveChanges();
            Response.Redirect(Request.Url.ToString());
        }

        protected void FullAttendanceAndLostCard_BreakfastBoolean() 
        {
            BreakfastCardsEntities db = new BreakfastCardsEntities();

            String year = DropDownList_ActualBreakfast_AddYear.SelectedValue;
            String month = Month_EngToDigit(DropDownList_ActualBreakfast_AddMonth.SelectedValue);
            String groupname = GroupName_Num(DropDownList_ActualBreakfast_AddGroupName.SelectedValue);
            String cards = EngOrderToDigit(DropDownList_ActualBreakfast_AddCards.SelectedValue);

            int actualquantity = 0;

            foreach (ListItem item in CheckBoxList_ActualBreakfast_Add.Items)       //代码有问题。
            {
                Table_BreakfastBoolean b = new Table_BreakfastBoolean();
                //在Table_BreakfastBoolean里，ID的规则，顺序分别：4位是年份，2位是月份，2位是团队代号，2位是卡号顺序，2位是日期
                b.ID = year + month + groupname + cards + item.Text.Substring(8);
                b.Year = year;
                b.Month = DropDownList_ActualBreakfast_AddMonth.SelectedValue;
                b.GroupName = DropDownList_ActualBreakfast_AddGroupName.SelectedValue;
                b.Cards = DropDownList_ActualBreakfast_AddCards.SelectedValue;
                b.Data = item.Text.Substring(8);
                actualquantity++;
                if (LostCard_bool == true)
                {
                    b.Breakfast_Boolean = "Null";
                }
                else
                {
                    b.Breakfast_Boolean = "True";
                }
                db.Table_BreakfastBoolean.Add(b);
                db.SaveChanges();
                Response.Redirect(Request.Url.ToString());
            }
        }

        protected void Button_FullAttendance_Click(object sender, EventArgs e)
        {
            LostCard_bool = false;
            FullAttendanceAndLostCard_ActualQuantity() ;
            FullAttendanceAndLostCard_BreakfastBoolean();
        }

        protected void Button_LostCard_Click(object sender, EventArgs e)
        {
            LostCard_bool = true;
            FullAttendanceAndLostCard_ActualQuantity();
            FullAttendanceAndLostCard_BreakfastBoolean();
        }
    }
}