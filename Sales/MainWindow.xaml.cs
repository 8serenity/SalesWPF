using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Collections.ObjectModel;

namespace Sales
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DbConnection newConnection = Connection;

            newConnection.Open();

            DataTable dt = newConnection.GetSchema("Tables");

            List<string> tableNames = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                if (row[3].ToString().Equals("BASE TABLE", StringComparison.OrdinalIgnoreCase))
                {
                    string tableName = row[2].ToString();
                    tableNames.Add(tableName);
                }
            }

            comboBox.ItemsSource = tableNames;
            newConnection.Close();
        }

        private void ChangedTable(object sender, SelectionChangedEventArgs e)
        {
            DbCommand command = Connection.CreateCommand();

            command.CommandText = "select * from" + " " + comboBox.SelectedValue.ToString();

            DbDataAdapter adapter = Factory.CreateDataAdapter();

            adapter.SelectCommand = command;

            DataTable tableToDisplay = new DataTable();

            adapter.Fill(tableToDisplay);

            listData.ItemsSource = tableToDisplay.DefaultView;
        }

        public static DbConnection Connection
        {
            get
            {
                DbProviderFactory providerFactory = DbProviderFactories.GetFactory(ConfigurationManager
                                            .ConnectionStrings["SalesConnection"].ProviderName);
                DbConnection connection = providerFactory.CreateConnection();

                connection.ConnectionString = ConfigurationManager.ConnectionStrings["SalesConnection"].ConnectionString;
                return connection;
            }
        }

        public DbProviderFactory Factory
        {
            get
            {
                DbProviderFactory providerFactory = DbProviderFactories.GetFactory(ConfigurationManager
                                            .ConnectionStrings["SalesConnection"].ProviderName);
                return providerFactory;
            }
        }
    }
}
