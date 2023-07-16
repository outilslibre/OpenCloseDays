using OpenCloseDays.CustomPublicHolidays;
using PublicHoliday;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCloseDays
{
    public class PublicHolidaysService
    {
        private static IDictionary<string, (string name, IPublicHolidays holidays)> _publicHolidays =
            new Dictionary<string, (string name, IPublicHolidays holidays)>()
        {
                { "aus", ("Australie", new AustraliaPublicHoliday()) },
                { "at", ("Autriche", new AustriaPublicHoliday()) },
                { "be", ("Belgique", new BelgiumPublicHoliday()) },
                { "ca", ("Canada", new CanadaPublicHoliday()) },
                { "cz", ("République Tchèque", new CzechRepublicPublicHoliday()) },
                { "da", ("Danemark", new DenmarkPublicHoliday()) },
                { "nl", ("Pays-Bas", new DutchPublicHoliday()) },
                { "ee", ("Estonie", new EstoniaPublicHoliday()) },
                { "fr", ("France", new FrancePublicHoliday()) },
                { "de", ("Allemagne", new GermanPublicHoliday()) },
                { "irl", ("Irlande", new IrelandPublicHoliday()) },
                { "it", ("Italie", new ItalyPublicHoliday()) },
                { "ja", ("Japon", new JapanPublicHoliday()) },
                { "kz", ("Kazakhstan", new KazakhstanPublicHoliday()) },
                { "lt", ("Lituanie", new LithuaniaPublicHoliday()) },
                { "lu", ("Luxembourg", new LuxembourgPublicHoliday()) },
                { "nz", ("Nouvelle Zélande", new NewZealandPublicHoliday()) },
                { "no", ("Norvège", new NorwayPublicHoliday()) },
                { "pl", ("Pologne", new PolandPublicHoliday()) },
                { "ro", ("Roumanie", new RomanianPublicHoliday()) },
                { "sk", ("Slovaquie", new SlovakiaPublicHoliday()) },
                { "za", ("Afrique du Sud", new SouthAfricaPublicHoliday()) },
                { "es", ("Espagne", new SpainPublicHoliday()) },
                { "se", ("Suède", new SwedenPublicHoliday()) },
                { "ch", ("Suisse", new SwitzerlandPublicHoliday()) },
                { "uk", ("Angleterre", new UKBankHoliday()) },
                { "us", ("USA", new USAPublicHoliday()) },
                { "pt", ("Portugal", new PortuguesePublicHoliday()) },
                { "br", ("Brésil", new BrazilPublicHoliday()) },
                { "zz", ("- Autre -", new NoPublicHoliday()) },
        };

        public IEnumerable<(string code, string name)> GetHandledCountries() =>
            _publicHolidays.OrderBy(kv => kv.Value.name).Select(kv => (kv.Key, kv.Value.name));

        public string GetCountryName(string country_code)
        {
            if (!_publicHolidays.TryGetValue(country_code, out var country))
                return country_code;

            return country.name;
        }

        public IDictionary<DateTime, string> GetPublicHolidaysForYear(string country_code, int year)
        {
            if (!_publicHolidays.TryGetValue(country_code, out var country))
                throw new NotImplementedException($"Country {country_code} is not supported");

            return country.holidays.PublicHolidayNames(year);
        }

        public IEnumerable<(DateTime Date, string Name)> GetPublicHolidaysForPeriod(
            string country_code, DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("endDate must be after startDate", nameof(endDate));

            if (!_publicHolidays.TryGetValue(country_code, out var country))
                throw new NotImplementedException($"Country {country_code} is not supported");

            var yearsInPeriod = Enumerable.Range(startDate.Year, endDate.Year - startDate.Year + 1);
            return yearsInPeriod.SelectMany(y => country.holidays.PublicHolidayNames(y))
                .Where(h => startDate.Date <= h.Key && h.Key <= endDate.Date).Select(h => (h.Key, h.Value)).OrderBy(h => h.Key).ToArray();
        }
    }
}
