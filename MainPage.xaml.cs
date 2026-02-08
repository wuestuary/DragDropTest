namespace DragDropTest;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        SetupDrop();
    }

    private void SetupDrop()
    {
        // 尝试使用 .NET MAUI 内置拖放 API
        var dropGesture = new DropGestureRecognizer();
        
        dropGesture.DragOver += (s, e) =>
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
            DropLabel.Text = "释放以打开文件";
        };
        
        dropGesture.DragLeave += (s, e) =>
        {
            DropLabel.Text = "把文件拖到这里";
        };
        
        dropGesture.Drop += async (s, e) =>
        {
            try
            {
                if (e.Data.Properties.TryGetValue("FilePath", out var pathObj) && pathObj is string path)
                {
                    ResultLabel.Text = $"收到: {path}";
                }
                else if (e.Data.Properties.TryGetValue("Text", out var textObj) && textObj is string text)
                {
                    ResultLabel.Text = $"文本: {text}";
                }
                else
                {
                    // 尝试获取文件
                    var files = await e.Data.GetStorageItemsAsync();
                    if (files?.FirstOrDefault() is FileBase file)
                    {
                        ResultLabel.Text = $"文件: {file.FullPath}";
                    }
                    else
                    {
                        ResultLabel.Text = $"属性: {string.Join(", ", e.Data.Properties.Keys)}";
                    }
                }
            }
            catch (Exception ex)
            {
                ResultLabel.Text = $"错误: {ex.Message}";
            }
            
            DropLabel.Text = "把文件拖到这里";
        };

        // 添加到 Border
        DropArea.GestureRecognizers.Add(dropGesture);
    }
}