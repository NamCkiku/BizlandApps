using Syncfusion.SfPicker.XForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Forms;

namespace Bizland.CustomControl
{
    public class CustomDatePicker : SfPicker
    {
        #region Public Properties

        // Months api is used to modify the Day collection as per change in Month

        internal Dictionary<string, string> Months { get; set; }

        /// <summary>
        /// Date is the acutal DataSource for SfPicker control which will holds the collection of Day ,Month and Year
        /// </summary>
        /// <value>The date.</value>
        public ObservableCollection<object> Date { get; set; }

        //Day is the collection of day numbers
        internal ObservableCollection<object> Day { get; set; }

        //Month is the collection of Month Names
        internal ObservableCollection<object> Month { get; set; }

        //Year is the collection of Years from 1990 to 2042
        internal ObservableCollection<object> Year { get; set; }

        /// <summary>
        /// Headers api is holds the column name for every column in date picker
        /// </summary>
        /// <value>The Headers.</value>
        public ObservableCollection<string> Headers { get; set; }

        #endregion

        public CustomDatePicker()
        {
            Months = new Dictionary<string, string>();

            Date = new ObservableCollection<object>();
            Day = new ObservableCollection<object>();
            Month = new ObservableCollection<object>();
            Year = new ObservableCollection<object>();
            Headers = new ObservableCollection<string>();
            Headers.Add("Tháng");
            Headers.Add("Ngày");
            Headers.Add("Năm");
            HeaderText = "Chọn ngày tháng";
            PopulateDateCollection();
            this.ItemsSource = Date;
            this.ColumnHeaderText = Headers;
            this.SelectionChanged += CustomDatePicker_SelectionChanged;
            ShowFooter = true;
            ShowHeader = true;
            ShowColumnHeader = true;


        }

        private void CustomDatePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDays(Date, e);
        }

        //Updatedays method is used to alter the Date collection as per selection change in Month column(if feb is Selected day collection has value from 1 to 28)

        public void UpdateDays(ObservableCollection<object> Date, SelectionChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (Date.Count == 3)
                {
                    bool isupdate = false;
                    if (e.OldValue != null && e.NewValue != null && (e.OldValue as ObservableCollection<object>).Count == (e.NewValue as ObservableCollection<object>).Count)
                    {
                        if (!object.Equals((e.OldValue as IList)[0], (e.NewValue as IList)[0]))
                        {
                            isupdate = true;
                        }
                        if (!object.Equals((e.OldValue as IList)[2], (e.NewValue as IList)[2]))
                        {
                            isupdate = true;
                        }
                    }

                    if (isupdate)
                    {

                        ObservableCollection<object> days = new ObservableCollection<object>();
                        int month = DateTime.ParseExact(Months[(e.NewValue as IList)[0].ToString()], "MMMM", CultureInfo.DefaultThreadCurrentCulture).Month;
                        int year = int.Parse((e.NewValue as IList)[2].ToString());
                        for (int j = 1; j <= DateTime.DaysInMonth(year, month); j++)
                        {
                            if (j < 10)
                            {
                                days.Add("0" + j);
                            }
                            else
                                days.Add(j.ToString());
                        }
                        ObservableCollection<object> oldvalue = new ObservableCollection<object>();

                        foreach (var item in e.NewValue as IList)
                        {
                            oldvalue.Add(item);
                        }
                        if (days.Count > 0)
                        {
                            Date.RemoveAt(1);
                            Date.Insert(1, days);
                        }

                        if ((Date[1] as IList).Contains(oldvalue[1]))
                        {
                            this.SelectedItem = oldvalue;
                        }
                        else
                        {
                            oldvalue[1] = (Date[1] as IList)[(Date[1] as IList).Count - 1];
                            this.SelectedItem = oldvalue;
                        }
                    }
                }
            });
        }

        private void PopulateDateCollection()
        {

            //populate months
            for (int i = 1; i < 13; i++)
            {
                if (!Months.ContainsKey(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i)))
                    Months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i), CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                Month.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
            }

            //populate year
            for (int i = 1990; i < 2050; i++)
            {
                Year.Add(i.ToString());
            }

            //populate Days
            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
            {
                if (i < 10)
                {
                    Day.Add("0" + i);
                }
                else
                    Day.Add(i.ToString());
            }

            Date.Add(Month);
            Date.Add(Day);
            Date.Add(Year);
        }
    }
}
