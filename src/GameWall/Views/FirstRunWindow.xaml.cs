using System.Windows;
using System.Windows.Input;

namespace GameWall.Views;

/// <summary>
/// 首次运行引导。只是说明页，点了按钮就关闭。
/// </summary>
public partial class FirstRunWindow : Window
{
    public FirstRunWindow()
    {
        InitializeComponent();
    }

    /// <summary>用户点了「开始添加游戏」，主窗口据此自动弹出选择文件对话框。</summary>
    public bool ShouldAddGame { get; private set; }

    private void Start_Click(object sender, RoutedEventArgs e)
    {
        ShouldAddGame = true;
        DialogResult = true;
        Close();
    }

    private void Skip_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}
