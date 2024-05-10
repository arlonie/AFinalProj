using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AFinalProj
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void add_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EmpNum.Text) && !string.IsNullOrEmpty(EmpName.Text) && !string.IsNullOrEmpty(EmploymentStat.Text) && !string.IsNullOrEmpty(HoursWork.Text) && !string.IsNullOrEmpty(CivilStat.Text))
            {
                string empnum = EmpNum.Text;
                string empname = EmpName.Text;

                //SA HOURS WORK
                double overtime_hours = 0;
                if (double.TryParse(HoursWork.Text, out double hourswork))
                {
                    if (hourswork > 120)
                    {
                        overtime_hours = hourswork - 120;
                    }
                }

                //SA EMPLOYEE STAT
                string empstat;
                double rate_per_day = 0;
                if (EmploymentStat.Text == "R" || EmploymentStat.Text == "r")
                {
                    empstat = "R";
                    rate_per_day = 800;
                }
                else if (EmploymentStat.Text == "P" || EmploymentStat.Text == "p")
                {
                    empstat = "P";
                    rate_per_day = 600;
                }
                else if (EmploymentStat.Text == "C" || EmploymentStat.Text == "c")
                {
                    empstat = "C";
                    rate_per_day = 500;
                }
                else if (EmploymentStat.Text == "T" || EmploymentStat.Text == "t")
                {
                    empstat = "T";
                    rate_per_day = 450;
                }
                else
                {

                    empstat = "otherwise";
                    rate_per_day = 400;
                }

                //CIVIL STAT OG PHILHEALTH
                double philhealth = 0;
                string civil_stat = "";
                if (CivilStat.Text == "S" || CivilStat.Text == "s")
                {
                    civil_stat = "S";
                    philhealth = 500;
                }
                else if (CivilStat.Text == "M" || CivilStat.Text == "m")
                {
                    civil_stat = "M";
                    philhealth = 300;
                }
                else if (CivilStat.Text == "W" || CivilStat.Text == "w")
                {
                    civil_stat = "W";
                    philhealth = 400;
                }
                else
                {
                    civil_stat = "Otherwise";
                    philhealth = 350;
                }

                //COMPUTATION PART 1 HERE
                double rate_per_hour = rate_per_day / 8;
                double basic = hourswork * rate_per_hour;
                double overtime = (rate_per_hour * 1.5) * overtime_hours;
                double gross = basic + overtime;

                // SA SSS
                double sss = 0;
                if (gross >= 10000)
                {
                    sss = gross * 0.1; //10%
                }
                else if (gross >= 5000)
                {
                    sss = gross * 0.08; //8%
                }
                else
                {
                    sss = gross * 0.05; //5%
                }

                // SA WTAX
                double wtax = 0;
                if (gross >= 8000)
                {
                    wtax = 3129 * 0.15; //3,192 x 15%
                }
                else if (gross >= 30000)
                {
                    wtax = 9167 * 0.15; //9,167 x 15% 
                }
                else
                {
                    double Base = 61.65;
                    double Excess = 904 * 0.2; //Excess= 904 x 20% 
                    wtax = Base + Excess;
                }

                //SA PAG IBIG 
                double pagibig = 0;
                if (gross <= 1500)
                {
                    pagibig = gross * 0.01; //1%
                }
                else
                {
                    pagibig = gross * 0.02; //2%
                }

                //COMPUTATION PART 2 HERE
                double deduction = wtax + sss + philhealth + pagibig;
                double net_income = gross - deduction;

                RECORDS records = new RECORDS()
                {
                    EMPNUM = empnum,
                    EMPNAME = empname,
                    HOURSWORK = hourswork,
                    EMPSTATUS = empstat,
                    CIVILSTAT = civil_stat,
                    RATEPERHOUR = rate_per_hour,
                    BASIC = basic,
                    OVERTIME = overtime,
                    GROSS = gross,
                    SSS = sss,
                    WTAX = wtax,
                    PHILHEALTH = philhealth,
                    PAGIBIG = pagibig,
                    DEDUCTION = deduction,
                    NETINCOME = net_income
                };

                await App.SQLiteDb.Save(empnum, records);
                EmpName.Text = string.Empty;
                EmpNum.Text = string.Empty;
                HoursWork.Text = string.Empty;
                EmploymentStat.Text = string.Empty;
                CivilStat.Text = string.Empty;

                await DisplayAlert("Success", "Added Successfully", "Ok");

                Application.Current.Properties["EmpNum"] = empnum;
                Application.Current.Properties["EmpName"] = empname;
                Application.Current.Properties["HourWork"] = hourswork;
                Application.Current.Properties["EmpStat"] = empstat;
                Application.Current.Properties["CivilStat"] = civil_stat;

                Application.Current.Properties["RateperHour"] = rate_per_hour;
                Application.Current.Properties["Basic"] = basic;
                Application.Current.Properties["Overtime"] = overtime;
                Application.Current.Properties["Gross"] = gross;
                Application.Current.Properties["SSS"] = sss;
                Application.Current.Properties["WTax"] = wtax;
                Application.Current.Properties["Philhealth"] = philhealth;
                Application.Current.Properties["Pagibig"] = pagibig;
                Application.Current.Properties["Deduction"] = deduction;
                Application.Current.Properties["NetIncome"] = net_income;

                Navigation.PushAsync(new Page1());
            }
        }

        private async void update_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EmpNum.Text))
            {
                string empnum = EmpNum.Text;
                string empname = "";
                var existingRecord = await App.SQLiteDb.Search(empnum);
                if (existingRecord != null)
                {
                    empname = existingRecord.EMPNAME;
                    //SA HOURS WORK
                    double overtime_hours = 0;
                    if (double.TryParse(HoursWork.Text, out double hourswork))
                    {
                        if (hourswork > 120)
                        {
                            overtime_hours = hourswork - 120;
                        }
                    }

                    //SA EMPLOYEE STAT
                    string empstat;
                    double rate_per_day = 0;
                    if (EmploymentStat.Text == "R" || EmploymentStat.Text == "r")
                    {
                        empstat = "R";
                        rate_per_day = 800;
                    }
                    else if (EmploymentStat.Text == "P" || EmploymentStat.Text == "p")
                    {
                        empstat = "P";
                        rate_per_day = 600;
                    }
                    else if (EmploymentStat.Text == "C" || EmploymentStat.Text == "c")
                    {
                        empstat = "C";
                        rate_per_day = 500;
                    }
                    else if (EmploymentStat.Text == "T" || EmploymentStat.Text == "t")
                    {
                        empstat = "T";
                        rate_per_day = 450;
                    }
                    else
                    {

                        empstat = "otherwise";
                        rate_per_day = 400;
                    }

                    //CIVIL STAT OG PHILHEALTH
                    double philhealth = 0;
                    string civil_stat = "";
                    if (CivilStat.Text == "S" || CivilStat.Text == "s")
                    {
                        civil_stat = "S";
                        philhealth = 500;
                    }
                    else if (CivilStat.Text == "M" || CivilStat.Text == "m")
                    {
                        civil_stat = "M";
                        philhealth = 300;
                    }
                    else if (CivilStat.Text == "W" || CivilStat.Text == "w")
                    {
                        civil_stat = "W";
                        philhealth = 400;
                    }
                    else
                    {
                        civil_stat = "Otherwise";
                        philhealth = 350;
                    }

                    //COMPUTATION PART 1 HERE
                    double rate_per_hour = rate_per_day / 8;
                    double basic = hourswork * rate_per_hour;
                    double overtime = (rate_per_hour * 1.5) * overtime_hours;
                    double gross = basic + overtime;

                    // SA SSS
                    double sss = 0;
                    if (gross >= 10000)
                    {
                        sss = gross * 0.1; //10%
                    }
                    else if (gross >= 5000)
                    {
                        sss = gross * 0.08; //8%
                    }
                    else
                    {
                        sss = gross * 0.05; //5%
                    }

                    // SA WTAX
                    double wtax = 0;
                    if (gross >= 8000)
                    {
                        wtax = 3129 * 0.15; //3,192 x 15%
                    }
                    else if (gross >= 30000)
                    {
                        wtax = 9167 * 0.15; //9,167 x 15% 
                    }
                    else
                    {
                        double Base = 61.65;
                        double Excess = 904 * 0.2; //Excess= 904 x 20% 
                        wtax = Base + Excess;
                    }

                    //SA PAG IBIG 
                    double pagibig = 0;
                    if (gross <= 1500)
                    {
                        pagibig = gross * 0.01; //1%
                    }
                    else
                    {
                        pagibig = gross * 0.02; //2%
                    }

                    //COMPUTATION PART 2 HERE
                    double deduction = wtax + sss + philhealth + pagibig;
                    double net_income = gross - deduction;

                    //MO UPDATE DIRI
                    existingRecord.HOURSWORK = hourswork;
                    existingRecord.EMPSTATUS = empstat;
                    existingRecord.CIVILSTAT = civil_stat;
                    existingRecord.RATEPERHOUR = rate_per_hour;
                    existingRecord.BASIC = basic;
                    existingRecord.OVERTIME = overtime;
                    existingRecord.GROSS = gross;
                    existingRecord.SSS = sss;
                    existingRecord.WTAX = wtax;
                    existingRecord.PHILHEALTH = philhealth;
                    existingRecord.PAGIBIG = pagibig;
                    existingRecord.DEDUCTION = deduction;
                    existingRecord.NETINCOME = net_income;

                    // Save changes
                    await App.SQLiteDb.Save(empnum, existingRecord); 
                    EmpName.Text = string.Empty;
                    EmpNum.Text = string.Empty;
                    HoursWork.Text = string.Empty;
                    EmploymentStat.Text = string.Empty;
                    CivilStat.Text = string.Empty;

                    await DisplayAlert("Success", "Updated Successfully", "Ok");

                    Application.Current.Properties["EmpNum"] = empnum;
                    Application.Current.Properties["EmpName"] = empname;
                    Application.Current.Properties["HourWork"] = hourswork;
                    Application.Current.Properties["EmpStat"] = empstat;
                    Application.Current.Properties["CivilStat"] = civil_stat;

                    Application.Current.Properties["RateperHour"] = rate_per_hour;
                    Application.Current.Properties["Basic"] = basic;
                    Application.Current.Properties["Overtime"] = overtime;
                    Application.Current.Properties["Gross"] = gross;
                    Application.Current.Properties["SSS"] = sss;
                    Application.Current.Properties["WTax"] = wtax;
                    Application.Current.Properties["Philhealth"] = philhealth;
                    Application.Current.Properties["Pagibig"] = pagibig;
                    Application.Current.Properties["Deduction"] = deduction;
                    Application.Current.Properties["NetIncome"] = net_income;

                    Navigation.PushAsync(new Page1());


                }

            }
        }

        private async void search_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EmpNum.Text))
            {
                string empnum = EmpNum.Text;
                var existingRecord = await App.SQLiteDb.Search(empnum);

                if (existingRecord != null) // Check if the record exists
                {
                    // Get the record properties
                    string empname = existingRecord.EMPNAME;
                    double hourswork = existingRecord.HOURSWORK;
                    string empstat = existingRecord.EMPSTATUS;
                    string civil_stat = existingRecord.CIVILSTAT;
                    double rate_per_hour = existingRecord.RATEPERHOUR;
                    double basic = existingRecord.BASIC;
                    double overtime = existingRecord.OVERTIME;
                    double gross = existingRecord.GROSS;
                    double sss = existingRecord.SSS;
                    double wtax = existingRecord.WTAX;
                    double philhealth = existingRecord.PHILHEALTH;
                    double pagibig = existingRecord.PAGIBIG;
                    double deduction = existingRecord.DEDUCTION;
                    double net_income = existingRecord.NETINCOME;

                    // Set properties to pass to the next page (Page1)
                    Application.Current.Properties["EmpNum"] = empnum;
                    Application.Current.Properties["EmpName"] = empname;
                    Application.Current.Properties["HourWork"] = hourswork;
                    Application.Current.Properties["EmpStat"] = empstat;
                    Application.Current.Properties["CivilStat"] = civil_stat;
                    Application.Current.Properties["RateperHour"] = rate_per_hour;
                    Application.Current.Properties["Basic"] = basic;
                    Application.Current.Properties["Overtime"] = overtime;
                    Application.Current.Properties["Gross"] = gross;
                    Application.Current.Properties["SSS"] = sss;
                    Application.Current.Properties["WTax"] = wtax;
                    Application.Current.Properties["Philhealth"] = philhealth;
                    Application.Current.Properties["Pagibig"] = pagibig;
                    Application.Current.Properties["Deduction"] = deduction;
                    Application.Current.Properties["NetIncome"] = net_income;

                    // Navigate to Page1
                    await Navigation.PushAsync(new Page1());
                }
                else
                {
                    // No record found, display an alert
                    await DisplayAlert("Not Found", "No record found with the specified employee number", "OK");
                }
            }
            else
            {
                // Employee number is empty, display an alert
                await DisplayAlert("Required", "Please enter Employee Number", "OK");
            }
        }

        private async void delete_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EmpNum.Text))
            {
                string empnum = EmpNum.Text;
                var existingRecord = await App.SQLiteDb.Search(empnum);
                if (existingRecord != null)
                {
                    await App.SQLiteDb.Delete(existingRecord);
                    EmpNum.Text = string.Empty;
                    await DisplayAlert("Success", "Record Deleted", "OK");
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter Employee Number", "OK");
            }
        }
    }
}
