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
        var dropGesture = new DropGestureRecognizer();
        
        dropGesture.DragOver += (s, e) =>
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
            DropLabel.Text = "释放以打开文件";
            DropLabel.TextColor = Colors.Yellow;
        };
        
        dropGesture.DragLeave += (s, e) =>
        {
            DropLabel.Text = "把文件拖到这里";
            DropLabel.TextColor = Colors.White;
        };
        
        dropGesture.Drop += (s, e) =>
        {
            try
            {
                // 检查所有可用属性
                var props = e.Data.Properties;
                var result = new List<string>();
                
                foreach (var key in props.Keys)
                {
                    if (props.TryGetValue(key, out var value))
                    {
                        result.Add($"{key}={value?.GetType().Name}:{value}");
                    }
                }
                
                ResultLabel.Text = string.Join("\n", result);
            }
            catch (Exception ex)
            {
                ResultLabel.Text = $"错误: {ex.Message}";
            }
            
            DropLabel.Text = "把文件拖到这里";
            DropLabel.TextColor = Colors.White;
        };

        DropArea.GestureRecognizers.Add(dropGesture);
    }
}