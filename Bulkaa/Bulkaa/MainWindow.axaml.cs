using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Bulkaa;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    string connectionString = "";

    public void Autorizacia(object? sender, RoutedEventArgs e)
    {
        if (Login.Text == "user" && Password.Text == "www")
        {
            Table tbl = new Table();
            this.Hide();
            tbl.Show();
        }
    }
    
    
    public void Exit_PR(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}