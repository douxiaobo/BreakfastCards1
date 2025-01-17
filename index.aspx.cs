﻿using BreakfastCards1.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace BreakfastCards1
{
    public partial class index : System.Web.UI.Page
    {
        string ThisYear = DateTime.Now.ToString("yyyy");
        string ThisMonth = DateTime.Now.ToString("MMMM", CultureInfo.GetCultureInfo("en-US"));
        string LastMonth = DateTime.Now.AddMonths(-1).ToString("MMMM", CultureInfo.GetCultureInfo("en-US"));
        string NextMonth= DateTime.Now.AddMonths(+1).ToString("MMMM", CultureInfo.GetCultureInfo("en-US"));
        List<string> HolidaysList = new List<string>();
        List<string> WorkdaysList = new List<string>();
        bool Lostcard;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindYear();
                BindMonth();
                BindGroupName();
                BindCard();
                BindCheckboxList();
                BindGridView();
            }
        }
        protected void Button_Add_Comfirm_Click(object sender, EventArgs e)
        {
            Table_FourName a = new Table_FourName();
            a.Date = DropDownList_Add_Year_Registration.Text + "-" + DropDownList_Add_Month_Registration.Text;
            a.GroupName = DropDownList_Add_GroupName_Registration.Text;
            a.Quantity = Convert.ToInt16(TextBox_Add_Quantity_Registration.Text);

            //在Table_FourName里，ID的规则，顺序分别：4位是年份，2位是月份，2位是团队代号。
            a.ID = DropDownList_Add_Year_Registration.Text + Month_EngToDigit(DropDownList_Add_Month_Registration.Text) + GroupName_Num(DropDownList_Add_GroupName_Registration.Text);

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
            a.Manager = GroupName_Manager[DropDownList_Add_GroupName_Registration.Text];
            BreakfastCardsEntities db = new BreakfastCardsEntities();
            db.Table_FourName.Add(a);
            db.SaveChanges();
            Response.Redirect(Request.Url.ToString());

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
        protected void BindYear()
        {
            DropDownList_Add_Year_Registration.Items.Clear();
            DropDownList_AddYear_Collection.Items.Clear();

            //年份Year
            int ThisYearInt = Convert.ToInt16(ThisYear);
            if (ThisMonth == "December")
                ThisYearInt++;
            for (int i = ThisYearInt; i >= 1997; i--)
            {
                DropDownList_Add_Year_Registration.Items.Add(i.ToString());
                DropDownList_AddYear_Collection.Items.Add(i.ToString());
            }
        }
        protected void BindMonth()
        {
            DropDownList_Add_Month_Registration.Items.Clear();
            DropDownList_AddMonth_Collection.Items.Clear();

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
            for (int i = Convert.ToInt16(ThisMonth) - 1; i <= 12; i++)
            {
                DropDownList_Add_Month_Registration.Items.Add(Month_DigitToEng[i]);
                DropDownList_AddMonth_Collection.Items.Add(Month_DigitToEng[i]);
            }
            for (int i = 1; i < Convert.ToInt16(ThisMonth) - 1; i++)
            {
                DropDownList_Add_Month_Registration.Items.Add(Month_DigitToEng[i]);
                DropDownList_AddMonth_Collection.Items.Add(Month_DigitToEng[i]);
            }
        }
        protected void BindGroupName()
        {
            DropDownList_Add_GroupName_Registration.Items.Clear();
            DropDownList_AddGroupName_Collection.Items.Clear();

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
                DropDownList_Add_GroupName_Registration.Items.Add(value.ToString());
                DropDownList_AddGroupName_Collection.Items.Add(value.ToString());
            }
        }
        protected void BindGridView()
        {
            BreakfastCardsEntities db = new BreakfastCardsEntities();
            string date = ThisYear + "-" + ThisMonth;
            string datelatemonth = ThisMonth + "-" + NextMonth;
            string year = DropDownList_AddYear_Collection.Text;
            string month = DropDownList_AddMonth_Collection.Text;
            var clients = from c in db.Table_FourName
                          where c.Date == date || c.Date==datelatemonth
                          select c;
            var clients_ActualQuantity = from e in db.Table_ActualQuantity
                                         where e.Year == year && e.Month == month
                                         select e;
            GridView_Registration.DataSource = clients.ToList();
            GridView_Collection.DataSource = clients_ActualQuantity.ToList();
            GridView_Registration.DataBind();
            GridView_Collection.DataBind();
        }
        protected void BindCard()
        {
            DropDownList_AddCards_Collection.Items.Clear();

            string ID = DropDownList_AddYear_Collection.Text + Month_EngToDigit(DropDownList_AddMonth_Collection.Text) + GroupName_Num(DropDownList_AddGroupName_Collection.Text);

            // HardCode for testing... 
            //ID = "20220801";

            BreakfastCardsEntities db = new BreakfastCardsEntities();
            var client = db.Table_FourName.FirstOrDefault(c => c.ID == ID); //System.Data.Entity.Core.EntityException:“The underlying provider failed on Open.”

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

            int a = Convert.ToInt16(client.Quantity.Value);
            for (int i = 1; i <= a; i++)
            {
                DropDownList_AddCards_Collection.Items.Add(ActualBreakfast_Add_DigitToEng[i]);
            }
        }
        protected void BindCheckboxList()
        {
            CheckBoxList_Add_Collection.Items.Clear();

            int workdays_Breakfast_Add = workdays(DropDownList_AddYear_Collection.Text.ToString(), DropDownList_AddMonth_Collection.Text.ToString());

            CheckBoxList_Add_Collection.RepeatColumns = 5;

            foreach (string workday in WorkdaysList)
            {
                CheckBoxList_Add_Collection.Items.Add(workday);
            }
        }
        protected int workdays(string year, string month)
        {
            int workdays = 0;
            int days = DateTime.DaysInMonth(Convert.ToInt16(year), Convert.ToInt16(Month_EngToDigit(month)));
            DateTime dt = Convert.ToDateTime(year + "-" + month + "-" + 01.ToString());
            string jsonPath = @"C:\Users\a-xiaobodou\OneDrive - Microsoft\Projects\ASP.NET\BreakfastCards1\" + year + ".json";
            HolidaysList.Clear();
            WorkdaysList.Clear();
            try
            {
                System.IO.StreamReader sr = File.OpenText(jsonPath);    //System.IO.FileNotFoundException:“未能找到文件“C:\Program Files\IIS Express\2022json”。”
                string json = sr.ReadToEnd();
                var root = JsonConvert.DeserializeObject<Holidays>(json);
                var keys = root.Holiday.Keys;
                String Month_Digit = Month_EngToDigit(month).ToString().PadLeft(2, '0');
                foreach (var key in keys)
                {
                    if (key.StartsWith(Month_Digit))
                        HolidaysList.Add(key);
                }
                bool workdaybool=true;
                /*
                for (int i = 1; i <= days; i++)
                {
                    string date_check = Month_Digit + "-" + i.ToString().PadLeft(2, '0');
                    if (dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday)
                    {
                        workdaybool = true;
                        foreach (string date in HolidaysList)
                        {
                            if (date_check == date)
                            {
                                workdaybool = false;
                                break;
                            }
                        }
                        if (workdaybool == true)
                        {
                            workdays++;
                            WorkdaysList.Add(date_check);
                        }
                    }
                    dt = dt.AddDays(1);
                }
                */
                for(int i=1;i<=days;i++)
                {                    
                    string date_check = Month_Digit + "-" + i.ToString().PadLeft(2, '0');
                    if (dt.DayOfWeek!=DayOfWeek.Saturday&&dt.DayOfWeek!=DayOfWeek.Sunday)
                    {                        
                        foreach(string date in HolidaysList)
                        {
                            workdaybool = true;
                            if (date_check==date)
                            {
                                workdaybool = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        workdaybool=false;
                        foreach(string date in HolidaysList)
                        {
                            if(date_check==date)
                            {
                                workdaybool = true;
                                break;
                            }
                        }
                    }
                    if(workdaybool==true)
                    {
                        workdays++;
                        WorkdaysList.Add(date_check);
                    }
                    dt = dt.AddDays(1);
                }
                return workdays;
            }
            catch (System.IO.FileNotFoundException)
            {
                return 0;
            }
        }
        protected void Button_FullAttendance_Collection_Click(object sender, EventArgs e)
        {
            Lostcard = false;
            FullAttendanceAndLostCard();
        }
        protected void Button_LostCard_Collection_Click(object sender, EventArgs e)
        {
            Lostcard = true;
            FullAttendanceAndLostCard();
        }
        protected void FullAttendanceAndLostCard()
        {
            BreakfastCardsEntities db = new BreakfastCardsEntities();

            String year = DropDownList_AddYear_Collection.SelectedValue;
            String month = Month_EngToDigit(DropDownList_AddMonth_Collection.SelectedValue);
            String groupname = GroupName_Num(DropDownList_AddGroupName_Collection.SelectedValue);
            String cards = EngOrderToDigit(DropDownList_AddCards_Collection.SelectedValue);

            Table_ActualQuantity a = new Table_ActualQuantity();

            //在Table_ActualQuantity里，ID的规则，顺序分别：4位年份，2位月份，2位团队代码，2位卡号顺序,1位是否丢失卡的符号。

            a.Year = year;
            a.Month = DropDownList_AddMonth_Collection.SelectedValue;
            a.GroupName = DropDownList_AddGroupName_Collection.SelectedValue;
            a.Cards = DropDownList_AddCards_Collection.SelectedValue;

            if (Lostcard == true)
            {
                a.LostCard_Boolean = "True";
                a.ID = year + month + groupname + cards + "T";
            }
            else
            {
                a.LostCard_Boolean = "False";
                a.ID = year + month + groupname + cards + "F";
            }
            int workday = workdays(DropDownList_AddYear_Collection.SelectedValue, DropDownList_AddMonth_Collection.SelectedValue);
            a.ActualQuantity = workday;
            a.Workdays = workday;

            foreach (ListItem item in CheckBoxList_Add_Collection.Items)       //代码有问题。
            {
                Table_BreakfastBoolean b = new Table_BreakfastBoolean();

                b.Year = year;
                b.Month = DropDownList_AddMonth_Collection.SelectedValue;
                b.GroupName = DropDownList_AddGroupName_Collection.SelectedValue;
                b.Cards = DropDownList_AddCards_Collection.SelectedValue;
                b.Data = item.Text;
                if (Lostcard == true)
                {
                    b.Breakfast_Boolean = "Lost";
                    //在Table_BreakfastBoolean里，ID的规则，顺序分别：4位是年份，2位是月份，2位是团队代号，2位是卡号顺序，2位是日期
                    b.ID = year + month + groupname + cards + item.Text.Substring(3) + "L";
                }
                else
                {
                    b.Breakfast_Boolean = "True";
                    b.ID = year + month + groupname + cards + item.Text.Substring(3) + "T";
                }
                db.Table_BreakfastBoolean.Add(b);
                int t = db.SaveChanges();
                //System.Data.Entity.Infrastructure.DbUpdateException:“An error occurred while updating the entries. See the inner exception for details.”
                //UpdateException: An error occurred while updating the entries. See the inner exception for details.
                //SqlException: Violation of PRIMARY KEY constraint 'PK_Table_Breakfast'. Cannot insert duplicate key in object 'dbo.Table_BreakfastBoolean'. The duplicate key value is (202208030101). The statement has been terminated.
            }
            db.Table_ActualQuantity.Add(a);
            db.SaveChanges();
            Response.Redirect(Request.Url.ToString());
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
        protected void Button_Add_Collection_Click(object sender, EventArgs e)
        {
            BreakfastCardsEntities db = new BreakfastCardsEntities();

            String year = DropDownList_AddYear_Collection.SelectedValue;
            String month = Month_EngToDigit(DropDownList_AddMonth_Collection.SelectedValue);
            String groupname = GroupName_Num(DropDownList_AddGroupName_Collection.SelectedValue);
            String cards = EngOrderToDigit(DropDownList_AddCards_Collection.SelectedItem.Value.ToString());            //这个问题

            int actualquantity = 0;

            Table_ActualQuantity a = new Table_ActualQuantity();

            //在Table_ActualQuantity里，ID的规则，顺序分别：4位年份，2位月份，2位团队代码，2位卡号顺序，1位是否丢失卡的符号。
            a.ID = year + month + groupname + cards + "F";
            a.Year = year;
            a.Month = DropDownList_AddMonth_Collection.SelectedValue;
            a.GroupName = DropDownList_AddGroupName_Collection.SelectedValue;
            a.Cards = DropDownList_AddCards_Collection.SelectedValue;
            a.Workdays = workdays(DropDownList_AddYear_Collection.SelectedValue, DropDownList_AddMonth_Collection.SelectedValue);
            a.LostCard_Boolean = "False";

            foreach (ListItem item in CheckBoxList_Add_Collection.Items)       //代码有问题。
            {
                Table_BreakfastBoolean b = new Table_BreakfastBoolean();

                b.Year = year;
                b.Month = DropDownList_AddMonth_Collection.SelectedValue;
                b.GroupName = DropDownList_AddGroupName_Collection.SelectedValue;
                b.Cards = DropDownList_AddCards_Collection.SelectedValue;
                b.Data = item.Text;
                if (item.Selected)
                {
                    actualquantity++;
                    b.Breakfast_Boolean = "True";
                    //在Table_BreakfastBoolean里，ID的规则，顺序分别：4位是年份，2位是月份，2位是团队代号，2位是卡号顺序，2位是日期
                    b.ID = year + month + groupname + cards + item.Text.Substring(3) + "T";
                }
                else
                {
                    b.Breakfast_Boolean = "False";
                    b.ID = year + month + groupname + cards + item.Text.Substring(3) + "F";
                }
                db.Table_BreakfastBoolean.Add(b);
                db.SaveChanges();
            }
            a.ActualQuantity = actualquantity;
            db.Table_ActualQuantity.Add(a);
            db.SaveChanges();
            Response.Redirect(Request.Url.ToString());
        }
        protected void DropDownList_AddGroupName_Collection_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCard();
        }
    }
}