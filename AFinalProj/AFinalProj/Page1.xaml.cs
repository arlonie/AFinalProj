using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AFinalProj
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();

            EmpNum.Text = $"{Application.Current.Properties["EmpNum"]}";
            EmpName.Text = $"{Application.Current.Properties["EmpName"]}";
            HoursWork.Text = $"{Application.Current.Properties["HourWork"]}";
            EmployeeStat.Text = $"{Application.Current.Properties["EmpStat"]}";
            CivilStat.Text = $"{Application.Current.Properties["CivilStat"]}";

            RatePerHour.Text = $"{Application.Current.Properties["RateperHour"]}";
            Basic.Text = $"{Application.Current.Properties["Basic"]}";
            Overtime.Text = $"{Application.Current.Properties["Overtime"]}";
            Gross.Text = $"{Application.Current.Properties["Gross"]}";
            SSS.Text = $"{Application.Current.Properties["SSS"]}";
            WTax.Text = $"{Application.Current.Properties["WTax"]}";
            Philhealth.Text = $"{Application.Current.Properties["Philhealth"]}";
            Pagibig.Text = $"{Application.Current.Properties["Pagibig"]}";
            Deduction.Text = $"{Application.Current.Properties["Deduction"]}";
            NetIncome.Text = $"{Application.Current.Properties["NetIncome"]}";
        }
    }
}