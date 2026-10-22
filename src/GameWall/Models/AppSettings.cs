using System;
using System.Collections.Generic;

namespace GameWall.Models;

/// <summary>视图模式</summary>
public enum WallViewMode
{
    /// <summary>大图墙（默认）</summary>
    Wall = 0,

    /// <summary>列表模式</summary>
    List = 1,

    /// <summary>紧凑模式</summary>
    Compact = 2,
}

/// <summary>
/// 全局配置，序列化到 %AppData%\GameWall\settings.json。
/// </summary>
public sealed class AppSettings
{
    // ── 外观 ────────────────────────────────────────────────
    /// <summary>主题：Dark / Light</summary>
    public string Theme { get; set; } = "Dark";

    /// <summary>当前视图模式</summary>
    public WallViewMode ViewMode { get; set; } = WallViewMode.Wall;

    /// <summary>大图墙卡片宽度（像素），高度按 3:4 自动计算</summary>
    public double CardWidth { get; set; } = 208;

    /// <summary>卡片是否显示名称文字</summary>
    public bool ShowLabels { get; set; } = true;

    /// <summary>自定义背景图片路径（留空则使用纯色 / 渐变）</summary>
    public string BackgroundImagePath { get; set; } = string.Empty;

    /// <summary>背景纯色（HEX，如 #101318）</summary>
    public string BackgroundColor { get; set; } = "#101318";

    /// <summary>背景图片不透明度 0~1</summary>
    public double BackgroundImageOpacity { get; set; } = 0.35;

    /// <summary>强调色（HEX）</summary>
    public string AccentColor { get; set; } = "#4F8CFF";

    // ── 窗口行为 ────────────────────────────────────────────
    public bool Topmost { get; set; }

    public bool StartFullscreen { get; set; }

    /// <summary>关闭窗口时最小化到托盘而不是退出</summary>
    public bool MinimizeToTray { get; set; } = true;

    /// <summary>开机自启</summary>
    public bool AutoStart { get; set; }

    /// <summary>移除游戏前二次确认</summary>
    public bool ConfirmOnRemove { get; set; } = true;

    /// <summary>启动游戏后自动最小化自己</summary>
    public bool MinimizeOnGameLaunch { get; set; }

    // ── 窗口位置 ────────────────────────────────────────────
    public double WindowWidth { get; set; } = 1280;
    public double WindowHeight { get; set; } = 820;

    // 用可空类型而不是 double.NaN：JSON 里 NaN 不是合法数字，
    // 而 null 能被正常序列化/反序列化，手改配置文件也不会读崩。
    public double? WindowLeft { get; set; }
    public double? WindowTop { get; set; }

    // ── 扫描 ────────────────────────────────────────────────
    /// <summary>自动扫描 / 搜索时优先遍历的目录</summary>
    public List<string> ScanPaths { get; set; } = new();

    /// <summary>扫描时递归的最大深度</summary>
    public int ScanDepth { get; set; } = 4;

    /// <summary>扫描时忽略小于该大小的 exe（字节），过滤掉辅助小工具</summary>
    public long MinExecutableSize { get; set; } = 40 * 1024;

    // ── 状态 ────────────────────────────────────────────────
    /// <summary>是否已经完成首次运行引导</summary>
    public bool FirstRunCompleted { get; set; }

    /// <summary>上次检查更新的时间（预留）</summary>
    public DateTime? LastCheckAt { get; set; }
}
