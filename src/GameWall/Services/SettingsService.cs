using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameWall.Models;

namespace GameWall.Services;

/// <summary>
/// 设置的读写与默认值补全。
/// </summary>
public sealed class SettingsService
{
    public AppSettings Current { get; private set; } = new();

    public void Load()
    {
        AppPaths.EnsureCreated();
        var loaded = JsonStore.Load<AppSettings>(AppPaths.SettingsFile) ?? new AppSettings();

        // 老版本配置可能缺字段，这里补齐合理默认值
        if (loaded.ScanPaths is null || loaded.ScanPaths.Count == 0)
        {
            loaded.ScanPaths = GameScanner.DefaultScanRoots();
        }

        if (loaded.CardWidth < 120 || loaded.CardWidth > 420)
        {
            loaded.CardWidth = 208;
        }

        if (string.IsNullOrWhiteSpace(loaded.AccentColor) || !IsValidHex(loaded.AccentColor))
        {
            loaded.AccentColor = "#4F8CFF";
        }

        if (string.IsNullOrWhiteSpace(loaded.BackgroundColor) || !IsValidHex(loaded.BackgroundColor))
        {
            loaded.BackgroundColor = "#101318";
        }

        Current = loaded;

        // 首次运行（或配置文件被杀掉）时立刻落盘一份，
        // 这样用户一启动就能在 %AppData%\GameWall\ 里看到 settings.json，
        // 也避免「改了设置但进程被杀、配置还没写过」的情况。
        if (!File.Exists(AppPaths.SettingsFile))
        {
            Save();
        }
    }

    public void Save()
    {
        JsonStore.Save(AppPaths.SettingsFile, Current);
    }

    /// <summary>把设置恢复成出厂默认（保留首次运行标记，避免再次弹引导）。</summary>
    public void ResetToDefaults()
    {
        var firstRunDone = Current.FirstRunCompleted;
        Current = new AppSettings
        {
            ScanPaths = GameScanner.DefaultScanRoots(),
            FirstRunCompleted = firstRunDone,
        };
        Save();
    }

    public static bool IsValidHex(string? hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
        {
            return false;
        }

        try
        {
            var color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(hex);
            return color.A > 0 || true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>过滤出当前真实存在的扫描根目录。</summary>
    public List<string> ExistingScanRoots() =>
        Current.ScanPaths.Where(p => !string.IsNullOrWhiteSpace(p) && Directory.Exists(p)).ToList();
}
