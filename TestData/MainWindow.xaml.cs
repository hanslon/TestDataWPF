﻿using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TestData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataProcessor processor = new();
        private int quantity;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void fetchNumber(Numbers.NumberType numberType)
        {
            var number = new Numbers(numberType);
            var outputString = "";

            if (testifIntInQty_boxValue())
            {
                Message_box.Text = "illegal quantity";
            }
            else
            {
                quantity = Convert.ToInt32(Qty_box.Text);
                outputString = number.GetData(number.DataType, quantity, processor.OutputMethod);
                Message_box.Text = outputString;
            }
        }

        private bool testifIntInQty_boxValue()
        {
            var failedTest = false;

            try
            {
                Convert.ToInt32(Qty_box.Text);
            }
            catch (Exception)
            {

                failedTest = true;
            }

            return failedTest;
        }


        private void Imei_button_Click(object sender, RoutedEventArgs e)
        {
            fetchNumber(Numbers.NumberType.Imei);
        }

        private void Serial_button_Click(object sender, RoutedEventArgs e)
        {
            fetchNumber(Numbers.NumberType.Serial);
        }

        private void VATNO_button_Click(object sender, RoutedEventArgs e)
        {
            fetchNumber(Numbers.NumberType.Vatno);
        }

        private void VATSE_button_Click(object sender, RoutedEventArgs e)
        {
            fetchNumber(Numbers.NumberType.Vatse);
        }

        private void VATDK_button_Click(object sender, RoutedEventArgs e)
        {
            fetchNumber(Numbers.NumberType.Vatdk);

        }


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var currentRadio = (RadioButton) sender;
            

            if (Qty_box != null)
            {
                if (currentRadio.Name == "File_radio")
                {
                    Qty_box.IsEnabled = true;
                    Qty_box.Focus();
                    Qty_box.SelectAll();
                    processor.OutputMethod = DataProcessor.OutputType.File;
                }
                else if (currentRadio.Name == "Display_radio")
                {
                    quantity = 1;
                    Qty_box.Text = quantity.ToString(); 
                    Qty_box.IsEnabled = false;
                    processor.OutputMethod = DataProcessor.OutputType.TextOnly;

                }
                else if (currentRadio.Name == "Copy_radio")
                {
                    quantity = 1;
                    Qty_box.Text = quantity.ToString();
                    Qty_box.IsEnabled = false;
                    processor.OutputMethod = DataProcessor.OutputType.Clipboard;
                }
            }
            
        }

        private void Qty_box_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
