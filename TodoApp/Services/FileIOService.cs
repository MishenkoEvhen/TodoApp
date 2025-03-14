using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;
using TodoApp.Models;

namespace TodoApp.Services;

public class FileIOService
{
    private readonly string PATH;
    public BindingList<TodoModel> LoadData()
    {
        var fileExists = File.Exists(PATH);
        if (!fileExists)
        {
            File.CreateText(PATH).Dispose();
            return new BindingList<TodoModel>();
        }

        using (var reader = File.OpenText(PATH))
        {
            var fileText = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<BindingList<TodoModel>>(fileText);
        }
    }

    public void SaveData(BindingList<TodoModel> todoDataList)
    {
        using (StreamWriter writer = File.CreateText(PATH))
        {
            string output = JsonConvert.SerializeObject(todoDataList);
            writer.Write(output);
        }
    }
}