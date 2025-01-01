using System;
using Dalamud.Game;
using Dalamud.Plugin.Services;
using Lumina.Text;

namespace GatherBuddy.Utility;

public readonly struct MultiString(string cn)
{
    public static string ParseSeStringLumina(SeString? luminaString)
        => luminaString == null ? string.Empty : Dalamud.Game.Text.SeStringHandling.SeString.Parse(luminaString.RawData).TextValue;

    public readonly string English  = string.Empty;
    public readonly string German   = string.Empty;
    public readonly string French   = string.Empty;
    public readonly string Japanese = string.Empty;
    public readonly string Chinese  = cn;

    public string this[ClientLanguage lang]
        => Name(lang);

    public override string ToString()
        => Name(ClientLanguage.ChineseSimplified);

    public string ToWholeString()
        => $"{English}|{German}|{French}|{Japanese}|{Chinese}";

    public static MultiString FromPlaceName(IDataManager gameData, uint id)
    {
        var cn = ParseSeStringLumina(gameData.GetExcelSheet<Lumina.Excel.GeneratedSheets.PlaceName>(ClientLanguage.ChineseSimplified)!.GetRow(id)?.Name);
        return new MultiString(cn);
    }

    public static MultiString FromItem(IDataManager gameData, uint id)
    {
        var cn = ParseSeStringLumina(gameData.GetExcelSheet<Lumina.Excel.GeneratedSheets.Item>(ClientLanguage.ChineseSimplified)!.GetRow(id)?.Name);
        return new MultiString(cn);
    }

    private string Name(ClientLanguage lang)
        => lang switch
        {
            ClientLanguage.English  => English,
            ClientLanguage.German   => German,
            ClientLanguage.Japanese => Japanese,
            ClientLanguage.French   => French,
            ClientLanguage.ChineseSimplified => Chinese,
            _                       => throw new ArgumentException(),
        };

    public static readonly MultiString Empty = new(string.Empty);
}
