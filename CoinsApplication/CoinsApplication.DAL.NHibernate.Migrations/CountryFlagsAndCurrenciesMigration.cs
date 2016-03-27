﻿using CoinsApplication.Misc;
using FluentMigrator;
using System.Collections.Generic;

namespace CoinsApplication.DAL.NHibernate.Migrations
{
    [Migration(250320161114)]
    public class CountryFlagsAndCurrenciesMigration : Migration
    {
        private readonly IList<dynamic> _currencies = new List<dynamic>
        {
            new { Code = "AED", Name = "United Arab Emirates Dirham" },
            new { Code = "AFN", Name = "Afghanistan Afghani" },
            new { Code = "ALL", Name = "Albania Lek" },
            new { Code = "AMD", Name = "Armenia Dram" },
            new { Code = "ANG", Name = "Netherlands Antilles Guilder" },
            new { Code = "AOA", Name = "Angola Kwanza" },
            new { Code = "ARS", Name = "Argentina Peso" },
            new { Code = "AUD", Name = "Australia Dollar" },
            new { Code = "AWG", Name = "Aruba Guilder" },
            new { Code = "AZN", Name = "Azerbaijan New Manat" },
            new { Code = "BAM", Name = "Bosnia and Herzegovina Convertible Marka" },
            new { Code = "BBD", Name = "Barbados Dollar" },
            new { Code = "BDT", Name = "Bangladesh Taka" },
            new { Code = "BGN", Name = "Bulgaria Lev" },
            new { Code = "BHD", Name = "Bahrain Dinar" },
            new { Code = "BIF", Name = "Burundi Franc" },
            new { Code = "BMD", Name = "Bermuda Dollar" },
            new { Code = "BND", Name = "Brunei Darussalam Dollar" },
            new { Code = "BOB", Name = "Bolivia Bolíviano" },
            new { Code = "BRL", Name = "Brazil Real" },
            new { Code = "BSD", Name = "Bahamas Dollar" },
            new { Code = "BTN", Name = "Bhutan Ngultrum" },
            new { Code = "BWP", Name = "Botswana Pula" },
            new { Code = "BYR", Name = "Belarus Ruble" },
            new { Code = "BZD", Name = "Belize Dollar" },
            new { Code = "CAD", Name = "Canada Dollar" },
            new { Code = "CDF", Name = "Congo/Kinshasa Franc" },
            new { Code = "CHF", Name = "Switzerland Franc" },
            new { Code = "CLP", Name = "Chile Peso" },
            new { Code = "CNY", Name = "China Yuan Renminbi" },
            new { Code = "COP", Name = "Colombia Peso" },
            new { Code = "CRC", Name = "Costa Rica Colon" },
            new { Code = "CUC", Name = "Cuba Convertible Peso" },
            new { Code = "CUP", Name = "Cuba Peso" },
            new { Code = "CVE", Name = "Cape Verde Escudo" },
            new { Code = "CZK", Name = "Czech Republic Koruna" },
            new { Code = "DJF", Name = "Djibouti Franc" },
            new { Code = "DKK", Name = "Denmark Krone" },
            new { Code = "DOP", Name = "Dominican Republic Peso" },
            new { Code = "DZD", Name = "Algeria Dinar" },
            new { Code = "EGP", Name = "Egypt Pound" },
            new { Code = "ERN", Name = "Eritrea Nakfa" },
            new { Code = "ETB", Name = "Ethiopia Birr" },
            new { Code = "EUR", Name = "Euro Member Countries" },
            new { Code = "FJD", Name = "Fiji Dollar" },
            new { Code = "FKP", Name = "Falkland Islands (Malvinas) Pound" },
            new { Code = "GBP", Name = "United Kingdom Pound" },
            new { Code = "GEL", Name = "Georgia Lari" },
            new { Code = "GGP", Name = "Guernsey Pound" },
            new { Code = "GHS", Name = "Ghana Cedi" },
            new { Code = "GIP", Name = "Gibraltar Pound" },
            new { Code = "GMD", Name = "Gambia Dalasi" },
            new { Code = "GNF", Name = "Guinea Franc" },
            new { Code = "GTQ", Name = "Guatemala Quetzal" },
            new { Code = "GYD", Name = "Guyana Dollar" },
            new { Code = "HKD", Name = "Hong Kong Dollar" },
            new { Code = "HNL", Name = "Honduras Lempira" },
            new { Code = "HRK", Name = "Croatia Kuna" },
            new { Code = "HTG", Name = "Haiti Gourde" },
            new { Code = "HUF", Name = "Hungary Forint" },
            new { Code = "IDR", Name = "Indonesia Rupiah" },
            new { Code = "ILS", Name = "Israel Shekel" },
            new { Code = "IMP", Name = "Isle of Man Pound" },
            new { Code = "INR", Name = "India Rupee" },
            new { Code = "IQD", Name = "Iraq Dinar" },
            new { Code = "IRR", Name = "Iran Rial" },
            new { Code = "ISK", Name = "Iceland Krona" },
            new { Code = "JEP", Name = "Jersey Pound" },
            new { Code = "JMD", Name = "Jamaica Dollar" },
            new { Code = "JOD", Name = "Jordan Dinar" },
            new { Code = "JPY", Name = "Japan Yen" },
            new { Code = "KES", Name = "Kenya Shilling" },
            new { Code = "KGS", Name = "Kyrgyzstan Som" },
            new { Code = "KHR", Name = "Cambodia Riel" },
            new { Code = "KMF", Name = "Comoros Franc" },
            new { Code = "KPW", Name = "Korea (North) Won" },
            new { Code = "KRW", Name = "Korea (South) Won" },
            new { Code = "KWD", Name = "Kuwait Dinar" },
            new { Code = "KYD", Name = "Cayman Islands Dollar" },
            new { Code = "KZT", Name = "Kazakhstan Tenge" },
            new { Code = "LAK", Name = "Laos Kip" },
            new { Code = "LBP", Name = "Lebanon Pound" },
            new { Code = "LKR", Name = "Sri Lanka Rupee" },
            new { Code = "LRD", Name = "Liberia Dollar" },
            new { Code = "LSL", Name = "Lesotho Loti" },
            new { Code = "LYD", Name = "Libya Dinar" },
            new { Code = "MAD", Name = "Morocco Dirham" },
            new { Code = "MDL", Name = "Moldova Leu" },
            new { Code = "MGA", Name = "Madagascar Ariary" },
            new { Code = "MKD", Name = "Macedonia Denar" },
            new { Code = "MMK", Name = "Myanmar (Burma) Kyat" },
            new { Code = "MNT", Name = "Mongolia Tughrik" },
            new { Code = "MOP", Name = "Macau Pataca" },
            new { Code = "MRO", Name = "Mauritania Ouguiya" },
            new { Code = "MUR", Name = "Mauritius Rupee" },
            new { Code = "MVR", Name = "Maldives (Maldive Islands) Rufiyaa" },
            new { Code = "MWK", Name = "Malawi Kwacha" },
            new { Code = "MXN", Name = "Mexico Peso" },
            new { Code = "MYR", Name = "Malaysia Ringgit" },
            new { Code = "MZN", Name = "Mozambique Metical" },
            new { Code = "NAD", Name = "Namibia Dollar" },
            new { Code = "NGN", Name = "Nigeria Naira" },
            new { Code = "NIO", Name = "Nicaragua Cordoba" },
            new { Code = "NOK", Name = "Norway Krone" },
            new { Code = "NPR", Name = "Nepal Rupee" },
            new { Code = "NZD", Name = "New Zealand Dollar" },
            new { Code = "OMR", Name = "Oman Rial" },
            new { Code = "PAB", Name = "Panama Balboa" },
            new { Code = "PEN", Name = "Peru Sol" },
            new { Code = "PGK", Name = "Papua New Guinea Kina" },
            new { Code = "PHP", Name = "Philippines Peso" },
            new { Code = "PKR", Name = "Pakistan Rupee" },
            new { Code = "PLN", Name = "Poland Zloty" },
            new { Code = "PYG", Name = "Paraguay Guarani" },
            new { Code = "QAR", Name = "Qatar Riyal" },
            new { Code = "RON", Name = "Romania New Leu" },
            new { Code = "RSD", Name = "Serbia Dinar" },
            new { Code = "RUB", Name = "Russia Ruble" },
            new { Code = "RWF", Name = "Rwanda Franc" },
            new { Code = "SAR", Name = "Saudi Arabia Riyal" },
            new { Code = "SBD", Name = "Solomon Islands Dollar" },
            new { Code = "SCR", Name = "Seychelles Rupee" },
            new { Code = "SDG", Name = "Sudan Pound" },
            new { Code = "SEK", Name = "Sweden Krona" },
            new { Code = "SGD", Name = "Singapore Dollar" },
            new { Code = "SHP", Name = "Saint Helena Pound" },
            new { Code = "SLL", Name = "Sierra Leone Leone" },
            new { Code = "SOS", Name = "Somalia Shilling" },
            new { Code = "SP", Name = "Seborga Luigino" },
            new { Code = "SRD", Name = "Suriname Dollar" },
            new { Code = "STD", Name = "São Tomé and Príncipe Dobra" },
            new { Code = "SVC", Name = "El Salvador Colon" },
            new { Code = "SYP", Name = "Syria Pound" },
            new { Code = "SZL", Name = "Swaziland Lilangeni" },
            new { Code = "THB", Name = "Thailand Baht" },
            new { Code = "TJS", Name = "Tajikistan Somoni" },
            new { Code = "TMT", Name = "Turkmenistan Manat" },
            new { Code = "TND", Name = "Tunisia Dinar" },
            new { Code = "TOP", Name = "Tonga Pa'anga" },
            new { Code = "TRY", Name = "Turkey Lira" },
            new { Code = "TTD", Name = "Trinidad and Tobago Dollar" },
            new { Code = "TVD", Name = "Tuvalu Dollar" },
            new { Code = "TWD", Name = "Taiwan New Dollar" },
            new { Code = "TZS", Name = "Tanzania Shilling" },
            new { Code = "UAH", Name = "Ukraine Hryvnia" },
            new { Code = "UGX", Name = "Uganda Shilling" },
            new { Code = "USD", Name = "United States Dollar" },
            new { Code = "UYU", Name = "Uruguay Peso" },
            new { Code = "UZS", Name = "Uzbekistan Som" },
            new { Code = "VEF", Name = "Venezuela Bolivar" },
            new { Code = "VND", Name = "Viet Nam Dong" },
            new { Code = "VUV", Name = "Vanuatu Vatu" },
            new { Code = "WST", Name = "Samoa Tala" },
            new { Code = "XCD", Name = "East Caribbean Dollar" },
            new { Code = "YER", Name = "Yemen Rial" },
            new { Code = "ZAR", Name = "South Africa Rand" },
            new { Code = "ZMW", Name = "Zambia Kwacha" },
            new { Code = "ZWD", Name = "Zimbabwe Dollar" }
        };

        private readonly IList<dynamic> _countries = new List<dynamic>()
        {
            new { Name = "Afghanistan", Flag = Properties.Resources.Afghanistan.ToByteArray() },
            new { Name = "African Union (OAS)", Flag = Properties.Resources.African_Union.ToByteArray() },
            new { Name = "Albania", Flag = Properties.Resources.Albania.ToByteArray() },
            new { Name = "Algeria", Flag = Properties.Resources.Algeria.ToByteArray() },
            new { Name = "American Samoa", Flag = Properties.Resources.American_Samoa.ToByteArray() },
            new { Name = "Andorra", Flag = Properties.Resources.Andorra.ToByteArray() },
            new { Name = "Angola", Flag = Properties.Resources.Angola.ToByteArray() },
            new { Name = "Anguilla", Flag = Properties.Resources.Anguilla.ToByteArray() },
            new { Name = "Antarctica", Flag = Properties.Resources.Antarctica.ToByteArray() },
            new { Name = "Antigua & Barbuda", Flag = Properties.Resources.Antigua___Barbuda.ToByteArray() },
            new { Name = "Arab League", Flag = Properties.Resources.Arab_League.ToByteArray() },
            new { Name = "Argentina", Flag = Properties.Resources.Argentina.ToByteArray() },
            new { Name = "Armenia", Flag = Properties.Resources.Armenia.ToByteArray() },
            new { Name = "Aruba", Flag = Properties.Resources.Aruba.ToByteArray() },
            new { Name = "ASEAN", Flag = Properties.Resources.ASEAN.ToByteArray() },
            new { Name = "Australia", Flag = Properties.Resources.Australia.ToByteArray() },
            new { Name = "Austria", Flag = Properties.Resources.Austria.ToByteArray() },
            new { Name = "Azerbaijan", Flag = Properties.Resources.Azerbaijan.ToByteArray() },
            new { Name = "Bahamas", Flag = Properties.Resources.Bahamas.ToByteArray() },
            new { Name = "Bahrain", Flag = Properties.Resources.Bahrain.ToByteArray() },
            new { Name = "Bangladesh", Flag = Properties.Resources.Bangladesh.ToByteArray() },
            new { Name = "Barbados", Flag = Properties.Resources.Barbados.ToByteArray() },
            new { Name = "Belarus", Flag = Properties.Resources.Belarus.ToByteArray() },
            new { Name = "Belgium", Flag = Properties.Resources.Belgium.ToByteArray() },
            new { Name = "Belize", Flag = Properties.Resources.Belize.ToByteArray() },
            new { Name = "Benin", Flag = Properties.Resources.Benin.ToByteArray() },
            new { Name = "Bermuda", Flag = Properties.Resources.Bermuda.ToByteArray() },
            new { Name = "Bhutan", Flag = Properties.Resources.Bhutan.ToByteArray() },
            new { Name = "Bolivia", Flag = Properties.Resources.Bolivia.ToByteArray() },
            new { Name = "Bosnia & Herzegovina", Flag = Properties.Resources.Bosnia___Herzegovina.ToByteArray() },
            new { Name = "Botswana", Flag = Properties.Resources.Botswana.ToByteArray() },
            new { Name = "Brazil", Flag = Properties.Resources.Brazil.ToByteArray() },
            new { Name = "Brunei", Flag = Properties.Resources.Brunei.ToByteArray() },
            new { Name = "Bulgaria", Flag = Properties.Resources.Bulgaria.ToByteArray() },
            new { Name = "Burkina Faso", Flag = Properties.Resources.Burkina_Faso.ToByteArray() },
            new { Name = "Burundi", Flag = Properties.Resources.Burundi.ToByteArray() },
            new { Name = "Cambodja", Flag = Properties.Resources.Cambodja.ToByteArray() },
            new { Name = "Cameroon", Flag = Properties.Resources.Cameroon.ToByteArray() },
            new { Name = "Canada", Flag = Properties.Resources.Canada.ToByteArray() },
            new { Name = "Cape Verde", Flag = Properties.Resources.Cape_Verde.ToByteArray() },
            new { Name = "CARICOM", Flag = Properties.Resources.CARICOM.ToByteArray() },
            new { Name = "Cayman Islands", Flag = Properties.Resources.Cayman_Islands.ToByteArray() },
            new { Name = "Central African Republic", Flag = Properties.Resources.Central_African_Republic.ToByteArray() },
            new { Name = "Chad", Flag = Properties.Resources.Chad.ToByteArray() },
            new { Name = "Chile", Flag = Properties.Resources.Chile.ToByteArray() },
            new { Name = "China", Flag = Properties.Resources.China.ToByteArray() },
            new { Name = "CIS", Flag = Properties.Resources.CIS.ToByteArray() },
            new { Name = "Colombia", Flag = Properties.Resources.Colombia.ToByteArray() },
            new { Name = "Commonwealth", Flag = Properties.Resources.Commonwealth.ToByteArray() },
            new { Name = "Comoros", Flag = Properties.Resources.Comoros.ToByteArray() },
            new { Name = "Congo-Brazzaville", Flag = Properties.Resources.Congo_Brazzaville.ToByteArray() },
            new { Name = "Congo-Kinshasa", Flag = Properties.Resources.Congo_Kinshasa_Zaire_.ToByteArray() },
            new { Name = "Cook Islands", Flag = Properties.Resources.Cook_Islands.ToByteArray() },
            new { Name = "Costa Rica", Flag = Properties.Resources.Costa_Rica.ToByteArray() },
            new { Name = "Cote d'Ivoire", Flag = Properties.Resources.Cote_d_Ivoire.ToByteArray() },
            new { Name = "Croatia", Flag = Properties.Resources.Croatia.ToByteArray() },
            new { Name = "Cuba", Flag = Properties.Resources.Cuba.ToByteArray() },
            new { Name = "Cyprus", Flag = Properties.Resources.Cyprus.ToByteArray() },
            new { Name = "Czech Republic", Flag = Properties.Resources.Czech_Republic.ToByteArray() },
            new { Name = "Denmark", Flag = Properties.Resources.Denmark.ToByteArray() },
            new { Name = "Djibouti", Flag = Properties.Resources.Djibouti.ToByteArray() },
            new { Name = "Dominica", Flag = Properties.Resources.Dominica.ToByteArray() },
            new { Name = "Dominican Republic", Flag = Properties.Resources.Dominican_Republic.ToByteArray() },
            new { Name = "Egypt", Flag = Properties.Resources.Egypt.ToByteArray() },
            new { Name = "El Salvador", Flag = Properties.Resources.El_Salvador.ToByteArray() },
            new { Name = "England", Flag = Properties.Resources.England.ToByteArray() },
            new { Name = "Equador", Flag = Properties.Resources.Ecuador.ToByteArray() },
            new { Name = "Equatorial Guinea", Flag = Properties.Resources.Equatorial_Guinea.ToByteArray() },
            new { Name = "Eritrea", Flag = Properties.Resources.Eritrea.ToByteArray() },
            new { Name = "Estonia", Flag = Properties.Resources.Estonia.ToByteArray() },
            new { Name = "Ethiopia", Flag = Properties.Resources.Ethiopia.ToByteArray() },
            new { Name = "European Union", Flag = Properties.Resources.European_Union.ToByteArray() },
            new { Name = "Faroes", Flag = Properties.Resources.Faroes.ToByteArray() },
            new { Name = "Fiji", Flag = Properties.Resources.Fiji.ToByteArray() },
            new { Name = "Finland", Flag = Properties.Resources.Finland.ToByteArray() },
            new { Name = "France", Flag = Properties.Resources.France.ToByteArray() },
            new { Name = "Gabon", Flag = Properties.Resources.Gabon.ToByteArray() },
            new { Name = "Gambia", Flag = Properties.Resources.Gambia.ToByteArray() },
            new { Name = "Georgia", Flag = Properties.Resources.Georgia.ToByteArray() },
            new { Name = "Germany", Flag = Properties.Resources.Germany.ToByteArray() },
            new { Name = "Ghana", Flag = Properties.Resources.Ghana.ToByteArray() },
            new { Name = "Gibraltar", Flag = Properties.Resources.Gibraltar.ToByteArray() },
            new { Name = "Greece", Flag = Properties.Resources.Greece.ToByteArray() },
            new { Name = "Greenland", Flag = Properties.Resources.Greenland.ToByteArray() },
            new { Name = "Grenada", Flag = Properties.Resources.Grenada.ToByteArray() },
            new { Name = "Guadeloupe", Flag = Properties.Resources.Guadeloupe.ToByteArray() },
            new { Name = "Guam", Flag = Properties.Resources.Guam.ToByteArray() },
            new { Name = "Guatemala", Flag = Properties.Resources.Guademala.ToByteArray() },
            new { Name = "Guernsey", Flag = Properties.Resources.Guernsey.ToByteArray() },
            new { Name = "Guinea-Bissau", Flag = Properties.Resources.Guinea_Bissau.ToByteArray() },
            new { Name = "Guinea", Flag = Properties.Resources.Guinea.ToByteArray() },
            new { Name = "Guyana", Flag = Properties.Resources.Guyana.ToByteArray() },
            new { Name = "Haiti", Flag = Properties.Resources.Haiti.ToByteArray() },
            new { Name = "Honduras", Flag = Properties.Resources.Honduras.ToByteArray() },
            new { Name = "Hong Kong", Flag = Properties.Resources.Hong_Kong.ToByteArray() },
            new { Name = "Hungary", Flag = Properties.Resources.Hungary.ToByteArray() },
            new { Name = "Iceland", Flag = Properties.Resources.Iceland.ToByteArray() },
            new { Name = "India", Flag = Properties.Resources.India.ToByteArray() },
            new { Name = "Indonesia", Flag = Properties.Resources.Indonesia.ToByteArray() },
            new { Name = "Iran", Flag = Properties.Resources.Iran.ToByteArray() },
            new { Name = "Iraq", Flag = Properties.Resources.Iraq.ToByteArray() },
            new { Name = "Ireland", Flag = Properties.Resources.Ireland.ToByteArray() },
            new { Name = "Islamic Conference", Flag = Properties.Resources.Islamic_Conference.ToByteArray() },
            new { Name = "Isle of Man", Flag = Properties.Resources.Isle_of_Man.ToByteArray() },
            new { Name = "Israel", Flag = Properties.Resources.Israel.ToByteArray() },
            new { Name = "Italy", Flag = Properties.Resources.Italy.ToByteArray() },
            new { Name = "Jamaica", Flag = Properties.Resources.Jamaica.ToByteArray() },
            new { Name = "Japan", Flag = Properties.Resources.Japan.ToByteArray() },
            new { Name = "Jersey", Flag = Properties.Resources.Jersey.ToByteArray() },
            new { Name = "Jordan", Flag = Properties.Resources.Jordan.ToByteArray() },
            new { Name = "Kazakhstan", Flag = Properties.Resources.Kazakhstan.ToByteArray() },
            new { Name = "Kenya", Flag = Properties.Resources.Kenya.ToByteArray() },
            new { Name = "Kiribati", Flag = Properties.Resources.Kiribati.ToByteArray() },
            new { Name = "Kosovo", Flag = Properties.Resources.Kosovo.ToByteArray() },
            new { Name = "Kuwait", Flag = Properties.Resources.Kuwait.ToByteArray() },
            new { Name = "Kyrgyzstan", Flag = Properties.Resources.Kyrgyzstan.ToByteArray() },
            new { Name = "Laos", Flag = Properties.Resources.Laos.ToByteArray() },
            new { Name = "Latvia", Flag = Properties.Resources.Latvia.ToByteArray() },
            new { Name = "Lebanon", Flag = Properties.Resources.Lebanon.ToByteArray() },
            new { Name = "Lesotho", Flag = Properties.Resources.Lesotho.ToByteArray() },
            new { Name = "Liberia", Flag = Properties.Resources.Liberia.ToByteArray() },
            new { Name = "Libya", Flag = Properties.Resources.Libya.ToByteArray() },
            new { Name = "Liechtenstein", Flag = Properties.Resources.Liechtenstein.ToByteArray() },
            new { Name = "Lithuania", Flag = Properties.Resources.Lithuania.ToByteArray() },
            new { Name = "Luxembourg", Flag = Properties.Resources.Luxembourg.ToByteArray() },
            new { Name = "Macao", Flag = Properties.Resources.Macao.ToByteArray() },
            new { Name = "Macedonia", Flag = Properties.Resources.Macedonia.ToByteArray() },
            new { Name = "Madagascar", Flag = Properties.Resources.Madagascar.ToByteArray() },
            new { Name = "Malawi", Flag = Properties.Resources.Malawi.ToByteArray() },
            new { Name = "Malaysia", Flag = Properties.Resources.Malaysia.ToByteArray() },
            new { Name = "Maldives", Flag = Properties.Resources.Maldives.ToByteArray() },
            new { Name = "Mali", Flag = Properties.Resources.Mali.ToByteArray() },
            new { Name = "Malta", Flag = Properties.Resources.Malta.ToByteArray() },
            new { Name = "Marshall Islands", Flag = Properties.Resources.Marshall_Islands.ToByteArray() },
            new { Name = "Martinique", Flag = Properties.Resources.Martinique.ToByteArray() },
            new { Name = "Mauritania", Flag = Properties.Resources.Mauritania.ToByteArray() },
            new { Name = "Mauritius", Flag = Properties.Resources.Mauritius.ToByteArray() },
            new { Name = "Mexico", Flag = Properties.Resources.Mexico.ToByteArray() },
            new { Name = "Micronesia", Flag = Properties.Resources.Micronesia.ToByteArray() },
            new { Name = "Moldova", Flag = Properties.Resources.Moldova.ToByteArray() },
            new { Name = "Monaco", Flag = Properties.Resources.Monaco.ToByteArray() },
            new { Name = "Mongolia", Flag = Properties.Resources.Mongolia.ToByteArray() },
            new { Name = "Montenegro", Flag = Properties.Resources.Montenegro.ToByteArray() },
            new { Name = "Montserrat", Flag = Properties.Resources.Montserrat.ToByteArray() },
            new { Name = "Morocco", Flag = Properties.Resources.Morocco.ToByteArray() },
            new { Name = "Mozambique", Flag = Properties.Resources.Mozambique.ToByteArray() },
            new { Name = "Myanmar (Burma)", Flag = Properties.Resources.Myanmar_Burma_.ToByteArray() },
            new { Name = "Namibia", Flag = Properties.Resources.Namibia.ToByteArray() },
            new { Name = "NATO", Flag = Properties.Resources.NATO.ToByteArray() },
            new { Name = "Nauru", Flag = Properties.Resources.Nauru.ToByteArray() },
            new { Name = "Nepal", Flag = Properties.Resources.Nepal.ToByteArray() },
            new { Name = "Netherlands Antilles", Flag = Properties.Resources.Netherlands_Antilles.ToByteArray() },
            new { Name = "Netherlands", Flag = Properties.Resources.Netherlands.ToByteArray() },
            new { Name = "New Caledonia", Flag = Properties.Resources.New_Caledonia.ToByteArray() },
            new { Name = "New Zealand", Flag = Properties.Resources.New_Zealand.ToByteArray() },
            new { Name = "Nicaragua", Flag = Properties.Resources.Nicaragua.ToByteArray() },
            new { Name = "Niger", Flag = Properties.Resources.Niger.ToByteArray() },
            new { Name = "Nigeria", Flag = Properties.Resources.Nigeria.ToByteArray() },
            new { Name = "North Korea", Flag = Properties.Resources.North_Korea.ToByteArray() },
            new { Name = "Northern Cyprus", Flag = Properties.Resources.Northern_Cyprus.ToByteArray() },
            new { Name = "Northern Ireland", Flag = Properties.Resources.Northern_Ireland.ToByteArray() },
            new { Name = "Norway", Flag = Properties.Resources.Norway.ToByteArray() },
            new { Name = "Oman", Flag = Properties.Resources.Oman.ToByteArray() },
            new { Name = "Pakistan", Flag = Properties.Resources.Pakistan.ToByteArray() },
            new { Name = "Palau", Flag = Properties.Resources.Palau.ToByteArray() },
            new { Name = "Palestine", Flag = Properties.Resources.Palestine.ToByteArray() },
            new { Name = "Panama", Flag = Properties.Resources.Panama.ToByteArray() },
            new { Name = "Papua New Guinea", Flag = Properties.Resources.Papua_New_Guinea.ToByteArray() },
            new { Name = "Paraguay", Flag = Properties.Resources.Paraguay.ToByteArray() },
            new { Name = "Peru", Flag = Properties.Resources.Peru.ToByteArray() },
            new { Name = "Philippines", Flag = Properties.Resources.Philippines.ToByteArray() },
            new { Name = "Poland", Flag = Properties.Resources.Poland.ToByteArray() },
            new { Name = "Portugal", Flag = Properties.Resources.Portugal.ToByteArray() },
            new { Name = "Puerto Rico", Flag = Properties.Resources.Puerto_Rico.ToByteArray() },
            new { Name = "Qatar", Flag = Properties.Resources.Qatar.ToByteArray() },
            new { Name = "Romania", Flag = Properties.Resources.Romania.ToByteArray() },
            new { Name = "Russian Federation", Flag = Properties.Resources.Russian_Federation.ToByteArray() },
            new { Name = "Rwanda", Flag = Properties.Resources.Rwanda.ToByteArray() },
            new { Name = "Saint Lucia", Flag = Properties.Resources.Saint_Lucia.ToByteArray() },
            new { Name = "Samoa", Flag = Properties.Resources.Samoa.ToByteArray() },
            new { Name = "San Marino", Flag = Properties.Resources.San_Marino.ToByteArray() },
            new { Name = "Sao Tome & Principe", Flag = Properties.Resources.Sao_Tome___Principe.ToByteArray() },
            new { Name = "Saudi Arabia", Flag = Properties.Resources.Saudi_Arabia.ToByteArray() },
            new { Name = "Scotland", Flag = Properties.Resources.Scotland.ToByteArray() },
            new { Name = "Senegal", Flag = Properties.Resources.Senegal.ToByteArray() },
            new { Name = "Serbia (Yugoslavia)", Flag = Properties.Resources.Serbia.ToByteArray() },
            new { Name = "Seyshelles", Flag = Properties.Resources.Seyshelles.ToByteArray() },
            new { Name = "Sierra Leone", Flag = Properties.Resources.Sierra_Leone.ToByteArray() },
            new { Name = "Singapore", Flag = Properties.Resources.Singapore.ToByteArray() },
            new { Name = "Slovakia", Flag = Properties.Resources.Slovakia.ToByteArray() },
            new { Name = "Slovenia", Flag = Properties.Resources.Slovenia.ToByteArray() },
            new { Name = "Solomon Islands", Flag = Properties.Resources.Solomon_Islands.ToByteArray() },
            new { Name = "Somalia", Flag = Properties.Resources.Somalia.ToByteArray() },
            new { Name = "Somaliland", Flag = Properties.Resources.Somaliland.ToByteArray() },
            new { Name = "South Africa", Flag = Properties.Resources.South_Afriica.ToByteArray() },
            new { Name = "South Korea", Flag = Properties.Resources.South_Korea.ToByteArray() },
            new { Name = "Spain", Flag = Properties.Resources.Spain.ToByteArray() },
            new { Name = "Sri Lanka", Flag = Properties.Resources.Sri_Lanka.ToByteArray() },
            new { Name = "St Kitts & Nevis", Flag = Properties.Resources.St_Kitts___Nevis.ToByteArray() },
            new { Name = "St Vincent & the Grenadines", Flag = Properties.Resources.St_Vincent___the_Grenadines.ToByteArray() },
            new { Name = "Sudan", Flag = Properties.Resources.Sudan.ToByteArray() },
            new { Name = "Suriname", Flag = Properties.Resources.Suriname.ToByteArray() },
            new { Name = "Swaziland", Flag = Properties.Resources.Swaziland.ToByteArray() },
            new { Name = "Sweden", Flag = Properties.Resources.Sweden.ToByteArray() },
            new { Name = "Switzerland", Flag = Properties.Resources.Switzerland.ToByteArray() },
            new { Name = "Syria", Flag = Properties.Resources.Syria.ToByteArray() },
            new { Name = "Tahiti (French Polinesia)", Flag = Properties.Resources.Tahiti_French_Polinesia_.ToByteArray() },
            new { Name = "Taiwan", Flag = Properties.Resources.Taiwan.ToByteArray() },
            new { Name = "Tajikistan", Flag = Properties.Resources.Tajikistan.ToByteArray() },
            new { Name = "Tanzania", Flag = Properties.Resources.Tanzania.ToByteArray() },
            new { Name = "Thailand", Flag = Properties.Resources.Thailand.ToByteArray() },
            new { Name = "Timor-Leste", Flag = Properties.Resources.Timor_Leste.ToByteArray() },
            new { Name = "Togo", Flag = Properties.Resources.Togo.ToByteArray() },
            new { Name = "Tonga", Flag = Properties.Resources.Tonga.ToByteArray() },
            new { Name = "Trinidad & Tobago", Flag = Properties.Resources.Trinidad___Tobago.ToByteArray() },
            new { Name = "Tunisia", Flag = Properties.Resources.Tunisia.ToByteArray() },
            new { Name = "Turkey", Flag = Properties.Resources.Turkey.ToByteArray() },
            new { Name = "Turkmenistan", Flag = Properties.Resources.Turkmenistan.ToByteArray() },
            new { Name = "Turks and Caicos Islands", Flag = Properties.Resources.Turks_and_Caicos_Islands.ToByteArray() },
            new { Name = "Tuvalu", Flag = Properties.Resources.Tuvalu.ToByteArray() },
            new { Name = "Uganda", Flag = Properties.Resources.Uganda.ToByteArray() },
            new { Name = "Ukraine", Flag = Properties.Resources.Ukraine.ToByteArray() },
            new { Name = "United Arab Emirates", Flag = Properties.Resources.United_Arab_Emirates.ToByteArray() },
            new { Name = "United Kingdom (Great Britain)", Flag = Properties.Resources.United_Kingdom_Great_Britain_.ToByteArray() },
            new { Name = "United States of America (USA)", Flag = Properties.Resources.United_States_of_America.ToByteArray() },
            new { Name = "Uruguay", Flag = Properties.Resources.Uruguay.ToByteArray() },
            new { Name = "Uzbekistan", Flag = Properties.Resources.Uzbekistan.ToByteArray() },
            new { Name = "Vanuatu", Flag = Properties.Resources.Vanutau.ToByteArray() },
            new { Name = "Vatican City", Flag = Properties.Resources.Vatican_City.ToByteArray() },
            new { Name = "Venezuela", Flag = Properties.Resources.Venezuela.ToByteArray() },
            new { Name = "Viet Nam", Flag = Properties.Resources.Viet_Nam.ToByteArray() },
            new { Name = "Virgin Islands British", Flag = Properties.Resources.Virgin_Islands_British.ToByteArray() },
            new { Name = "Virgin Islands US", Flag = Properties.Resources.Virgin_Islands_US.ToByteArray() },
            new { Name = "Wales", Flag = Properties.Resources.Wales.ToByteArray() },
            new { Name = "Western Sahara", Flag = Properties.Resources.Western_Sahara.ToByteArray() },
            new { Name = "Yemen", Flag = Properties.Resources.Yemen.ToByteArray() },
            new { Name = "Zambia", Flag = Properties.Resources.Zambia.ToByteArray() },
            new { Name = "Zimbabwe", Flag = Properties.Resources.Zimbabwe.ToByteArray() }
        };

        public override void Up()
        {
            Execute.WithConnection((connection, transaction) =>
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Country(Id, Name, Flag) VALUES(@id, @name, @flag)";

                    var id = command.CreateParameter();
                    command.Parameters.Add(id);
                    id.ParameterName = "id";

                    var name = command.CreateParameter();
                    command.Parameters.Add(name);
                    name.ParameterName = "name";

                    var flag = command.CreateParameter();
                    command.Parameters.Add(flag);
                    flag.ParameterName = "flag";

                    int number = 1;
                    foreach (var country in _countries)
                    {
                        name.Value = country.Name;
                        flag.Value = country.Flag;
                        id.Value = number++;

                        command.ExecuteNonQuery();
                    }
                }
            });

            foreach (var currency in _currencies)
            {
                Insert.IntoTable("Currency").Row(new
                {
                    Name = currency.Name,
                    Code = currency.Code
                });
            }
        }

        public override void Down()
        {
            Delete.Table("Country");
            Delete.Table("Currency");
        }
    }
}