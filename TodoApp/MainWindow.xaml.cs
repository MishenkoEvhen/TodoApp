using System.Windows;
using System.ComponentModel;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly string PATH = $"{Environment.CurrentDirectory}\\todoDataList.json";
    private BindingList<TodoModel> _todoDateList;
    private FileIOService _fileIOService;
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        _fileIOService = new FileIOService(PATH);

        try
        {
            _todoDateList = _fileIOService.LoadData();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            Close();
        }

        dgTodoList.ItemsSource = _todoDateList;
        _todoDateList.ListChanged += _todoDataList_ListChanged;
    }

    private void _todoDataList_ListChanged(object sender, ListChangedEventArgs e)
    {
        if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted ||
            e.ListChangedType == ListChangedType.ItemChanged)
        {
            try
            {
                _fileIOService.SaveData(sender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }
    }
}