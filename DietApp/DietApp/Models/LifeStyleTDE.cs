

using System.ComponentModel;

namespace DietApp.Models
{
    public enum LifeStyleTDE
    {
        [Description("Nieaktywny/siedzący: prawie brak aktywności w ciągu dnia")]
        Sedentary =1,
        [Description("Lekki: 1-3 treningów w tygodniu, lub praca z lekką aktywnością")]
        Light,
        [Description("Średni: 3-5 treningów w tygodniu")]
        Moderate,
        [Description("Wysoki: 6-7 treningów w tygodniu")]
        High,
        [Description("Bardzo Wysoki: Ciężka praca fizyczna lub treningi dwa razy w tygodniu")]
        Extreme
    }
}
