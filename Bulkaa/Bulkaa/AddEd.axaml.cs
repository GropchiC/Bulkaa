using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;

namespace Bulkaa;

public partial class AddEd : Window
{
    private List<Продукция> Orders;
    private Продукция CurrenOrder;
    public AddEd(Продукция currentOrder, List<Продукция> orders)
    {
        InitializeComponent();
        CurrenOrder = currentOrder;
        this.DataContext = currentOrder;
        Orders = orders;
    }
    
    private MySqlConnection conn;
    string connStr = "server=10.10.1.24;database=abd4;port=3306;User Id=user_01;password=user01pro";

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var usr = Orders.FirstOrDefault(x => x.ID == CurrenOrder.ID);
        if (usr == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO Продукция VALUES (" + Convert.ToInt32(id.Text) + ", '" + id_user.Text + "', '" + date_create.Text + "', '" + stat.Text + "');";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE Продукция SET Название = '" + id_user.Text + "', Цена = '" +  date_create.Text + "', Категория = " + Convert.ToInt32(stat.Text) + " WHERE ID = " + Convert.ToInt32(id.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        Table back = new Table();
        this.Close();
        back.Show(); 
    }
}