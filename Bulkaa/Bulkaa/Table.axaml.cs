using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows;

namespace Bulkaa;

public partial class Table : Window
{
    public Table()
    {
        InitializeComponent();
    ShowTable(fullTable);
        FillStatus();
    }
    string fullTable = "SELECT Продукция.ID, Продукция.Название, Продукция.Цена, Категории.Название_категории FROM Продукция JOIN Категории on Продукция.Категория = Категории.ID";

    private List<Продукция> orders;
    private List<Категория> stats;
    string connStr = "server=10.10.1.24;database=abd4;port=3306;User Id=user_01;password=user01pro";
    private MySqlConnection conn;

    public void ShowTable(string sql)
    {
        orders = new List<Продукция>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Client = new Продукция()
            {
                ID = reader.GetInt32("ID"),
                Название = reader.GetString("Название"),
                Цена = reader.GetInt32("Цена"),
                Название_категории = reader.GetString("Название_категории")
            };
            orders.Add(Client);
        }
        conn.Close();
        DataGrid.ItemsSource = orders;
    }
    
    private void SearchGoods(object? sender, TextChangedEventArgs e)
    {
        var gds = orders;
        gds = gds.Where(x => x.Название.Contains(Search_Goods.Text)).ToList();
        DataGrid.ItemsSource = gds;
    }
    
    private void Reset_OnClick(object? sender, RoutedEventArgs e)
    {
        ShowTable(fullTable);
        Search_Goods.Text = string.Empty;
    }

    private void Del(object? sender, RoutedEventArgs e)
    {
        try
        {
            Продукция usr = DataGrid.SelectedItem as Продукция;
            if (usr == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM Продукция WHERE ID = " + usr.ID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            orders.Remove(usr);
            ShowTable(fullTable);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    private void AddData(object? sender, RoutedEventArgs e)
    {
        Продукция newOrder = new Продукция();
        AddEd add = new AddEd(newOrder, orders);
        add.Show();
        this.Close();
    }
    
    private void Edit(object? sender, RoutedEventArgs e)
    {
        Продукция currenOrder = DataGrid.SelectedItem as Продукция;
        if (currenOrder == null)
            return;
        AddEd edit = new AddEd(currenOrder, orders);
        edit.Show();
        this.Close();
    }
    
    private void CmbStatus(object? sender, SelectionChangedEventArgs e)
    {
        var genderComboBox = (ComboBox)sender;
        var currentGender = genderComboBox.SelectedItem as Категория;
        var filteredUsers = orders
            .Where(x => x.Название_категории == currentGender.Название_категории)
            .ToList();
        DataGrid.ItemsSource = filteredUsers;
    }
    
    public void FillStatus()
    {
        stats = new List<Категория>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("select * from Категории", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentGender = new Категория()
            {
                ID = reader.GetInt32("ID"),
                Название_категории = reader.GetString("Название_категории"),

            };
            stats.Add(currentGender);
        }
        conn.Close();
        var genderComboBox = this.Find<ComboBox>("CmbGender");
        genderComboBox.ItemsSource = stats;
    }
}