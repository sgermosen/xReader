using Newtonsoft.Json;
using System.Collections.Generic;

namespace News.Client.Models
{
    public class Country
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("topLevelDomain", Required = Required.Always)]
        public List<string> TopLevelDomain { get; set; }

        [JsonProperty("alpha2Code", Required = Required.Always)]
        public string Alpha2Code { get; set; }

        [JsonProperty("alpha3Code", Required = Required.Always)]
        public string Alpha3Code { get; set; }

        [JsonProperty("callingCodes", Required = Required.Always)]
        public List<string> CallingCodes { get; set; }

        [JsonProperty("capital", Required = Required.Always)]
        public string Capital { get; set; }

        [JsonProperty("altSpellings", Required = Required.Always)]
        public List<string> AltSpellings { get; set; }

        [JsonProperty("region", Required = Required.Always)]
        public string Region { get; set; }

        [JsonProperty("subregion", Required = Required.Always)]
        public string Subregion { get; set; }

        [JsonProperty("population", Required = Required.Always)]
        public long Population { get; set; }

        [JsonProperty("latlng", Required = Required.Always)]
        public List<double> Latlng { get; set; }

        [JsonProperty("demonym", Required = Required.Always)]
        public string Demonym { get; set; }

        [JsonProperty("area", Required = Required.AllowNull)]
        public double? Area { get; set; }

        [JsonProperty("gini", Required = Required.AllowNull)]
        public double? Gini { get; set; }

        [JsonProperty("timezones", Required = Required.Always)]
        public List<string> Timezones { get; set; }

        [JsonProperty("borders", Required = Required.Always)]
        public List<string> Borders { get; set; }

        [JsonProperty("nativeName", Required = Required.Always)]
        public string NativeName { get; set; }

        [JsonProperty("numericCode", Required = Required.AllowNull)]
        public string NumericCode { get; set; }

        [JsonProperty("currencies", Required = Required.Always)]
        public List<Currency> Currencies { get; set; }

        [JsonProperty("languages", Required = Required.Always)]
        public List<Language> Languages { get; set; }

        [JsonProperty("translations", Required = Required.Always)]
        public Translations Translations { get; set; }

        [JsonProperty("flag", Required = Required.Always)]
        public string Flag { get; set; }

        [JsonProperty("regionalBlocs", Required = Required.Always)]
        public List<RegionalBloc> RegionalBlocs { get; set; }

        [JsonProperty("cioc", Required = Required.AllowNull)]
        public string Cioc { get; set; }
    }


    //public enum Region { Africa, Americas, Asia, Empty, Europe, Oceania, Polar };

    //public enum Acronym { Al, Asean, Au, Cais, Caricom, Cefta, Eeu, Efta, Eu, Nafta, Pa, Saarc, Usan };

    ////public enum Name { AfricanUnion, ArabLeague, AssociationOfSoutheastAsianNations, CaribbeanCommunity, CentralAmericanIntegrationSystem, CentralEuropeanFreeTradeAgreement, EurasianEconomicUnion, EuropeanFreeTradeAssociation, EuropeanUnion, NorthAmericanFreeTradeAgreement, PacificAlliance, SouthAsianAssociationForRegionalCooperation, UnionOfSouthAmericanNations };

    //public enum OtherAcronym { Eaeu, Sica, Unasul, Unasur, Uzan };

    //public enum OtherName { AccordDeLibreÉchangeNordAméricain, AlianzaDelPacífico, CaribischeGemeenschap, CommunautéCaribéenne, ComunidadDelCaribe, JāmiAtAdDuwalAlArabīyah, LeagueOfArabStates, SistemaDeLaIntegraciónCentroamericana, SouthAmericanUnion, TratadoDeLibreComercioDeAméricaDelNorte, UmojaWaAfrika, UnieVanZuidAmerikaanseNaties, UnionAfricaine, UniãoAfricana, UniãoDeNaçõesSulAmericanas, UniónAfricana, UniónDeNacionesSuramericanas, الاتحادالأفريقي, جامعةالدولالعربية };

}
