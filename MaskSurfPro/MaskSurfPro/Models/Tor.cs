using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace MaskSurfPro
{
    static public class Tor
    {
        static private string RouterStatuses;

        static public string GetCurrentIP()
        {
            return DependencyService.Get<ITorController>().GetCurrentIP();
        }
        static public void ChangeIP()
        {
            DependencyService.Get<ITorController>().ChangeIP();
        }
        static public string ReadTorrc()
        {
           return DependencyService.Get<ITorController>().ReadTorrc();
        }
        static public string ReadPolipoConf()
        {
            return DependencyService.Get<ITorController>().ReadPolipoConf();
        }
        static public void WriteTorrc(string TextToWrite)
        {
            DependencyService.Get<ITorController>().WriteTorrc(TextToWrite);
        }
        static public void WritePolipoConf(string TextToWrite)
        {
            DependencyService.Get<ITorController>().WritePolipoConf(TextToWrite);
        }
        static public bool SendSimpleSignal(string Signal)
        {
            return DependencyService.Get<ITorController>().SendSimpleSignal(Signal);
        }
        static public bool SendReload()
        {
            return DependencyService.Get<ITorController>().SendReload();
        }
        static public string SendSignal(string Signal)
        {
            return DependencyService.Get<ITorController>().SendSignal(Signal);
        }
        static public string GetCountries()
        {
            if (String.IsNullOrEmpty(RouterStatuses))
            {
                RouterStatuses = DependencyService.Get<ITorController>().GetCountries();
            }
            return RouterStatuses;
        }
        static public string GetCountry(string IP)
        {
            return DependencyService.Get<ITorController>().GetCountry(IP);
        }
        static public void GetBigData(string Signal)
        {
        }
        static public void GetBigData(string Signal, int ReceiveNumber)
        {
        }
        static public void Connect()
        {
            DependencyService.Get<ITorController>().Connect();
        }

        static public string CodeToCountry(string CountryCode)
        {
            if (CountryCode[0]== 'A')
            {
                if (CountryCode == "AE")
                    return "United Arab Emirates";
                if (CountryCode == "AF")
                    return "Afghanistan";
                if (CountryCode == "AX")
                    return "Aland islands";
                if (CountryCode == "AL")
                    return "Albania";
                if (CountryCode == "AN")
                    return "Netherlands Antilles";
                if (CountryCode == "AS")
                    return "American Samoa";
                if (CountryCode == "AD")
                    return "Andorra";
                if (CountryCode == "AO")
                    return "Angola";
                if (CountryCode == "AI")
                    return "Anguilla";
                if (CountryCode == "AQ")
                    return "Antarctica";
                if (CountryCode == "AG")
                    return "Antigua and Barbuda";
                if (CountryCode == "AR")
                    return "Argentina";
                if (CountryCode == "AM")
                    return "Armenia";
                if (CountryCode == "AW")
                    return "Aruba";
                if (CountryCode == "AU")
                    return "Australia";
                if (CountryCode == "AT")
                    return "Austria";
                if (CountryCode == "AZ")
                    return "Azerbaijan";
            }

            if (CountryCode[0]== 'B')
            {
                if (CountryCode == "BS")
                    return "Bahamas";
                if (CountryCode == "BH")
                    return "Bahrain";
                if (CountryCode == "BD")
                    return "Bangladesh";
                if (CountryCode == "BB")
                    return "Barbados";
                if (CountryCode == "BL")
                    return "Saint Barthelemy";
                if (CountryCode == "BY")
                    return "Belarus";
                if (CountryCode == "BE")
                    return "Belgium";
                if (CountryCode == "BZ")
                    return "Belize";
                if (CountryCode == "BJ")
                    return "Benin";
                if (CountryCode == "BM")
                    return "Bermuda";
                if (CountryCode == "BT")
                    return "Bhutan";
                if (CountryCode == "BO")
                    return "Bolivia";
                if (CountryCode == "BA")
                    return "Bosnia and Herzegovina";
                if (CountryCode == "BW")
                    return "Botswana";
                if (CountryCode == "BV")
                    return "Bouvet Island";
                if (CountryCode == "BR")
                    return "Brasil";
                if (CountryCode == "BN")
                    return "Brunei Darussalam";
                if (CountryCode == "BG")
                    return "Bulgaria";
                if (CountryCode == "BF")
                    return "Burkina Faso";
                if (CountryCode == "BI")
                    return "Burundi";
            }

            if (CountryCode[0]== 'C')
            {
                if (CountryCode == "CM")
                    return "Cameroon";
                if (CountryCode == "CA")
                    return "Canada";
                if (CountryCode == "CH")
                    return "Switzerland";
                if (CountryCode == "CV")
                    return "Cape Verde";
                if (CountryCode == "CF")
                    return "Central African Republic";
                if (CountryCode == "CL")
                    return "Chile";
                if (CountryCode == "CN")
                    return "China";
                if (CountryCode == "CX")
                    return "Christmas Island";
                if (CountryCode == "CC")
                    return "Cocos Islands";
                if (CountryCode == "CO")
                    return "Colombia";
                if (CountryCode == "CG")
                    return "Congo";
                if (CountryCode == "CD")
                    return "Congo, The Democratic Republic of the";
                if (CountryCode == "CK")
                    return "Cook Islands";
                if (CountryCode == "CR")
                    return "Costa Rica";
                if (CountryCode == "CI")
                    return "Cote D'ivoire";
                if (CountryCode == "CU")
                    return "Cuba";
                if (CountryCode == "CY")
                    return "Cyprus";
                if (CountryCode == "CZ")
                    return "Czech Republic";
            }

            if (CountryCode[0]== 'D')
            {
                if (CountryCode == "DE")
                    return "Germany";
                if (CountryCode == "DK")
                    return "Denmark";
                if (CountryCode == "DJ")
                    return "Djibouti";
                if (CountryCode == "DM")
                    return "Dominica";
                if (CountryCode == "DO")
                    return "Dominican Republic";
                if (CountryCode == "DZ")
                    return "Algeria";
            }

            if (CountryCode[0]== 'E')
            {
                if (CountryCode == "EC")
                    return "Ecuador";
                if (CountryCode == "EG")
                    return "Egypt";
                if (CountryCode == "ER")
                    return "Eritrea";
                if (CountryCode == "EE")
                    return "Estonia";
                if (CountryCode == "ES")
                    return "Spain";
                if (CountryCode == "ET")
                    return "Ethiopia";
                if (CountryCode == "EH")
                    return "Western Sahara";
            }

            if (CountryCode[0]== 'F')
            {
                if (CountryCode == "FK")
                    return "Falkland Islands(Malvinas)";
                if (CountryCode == "FM")
                    return "Micronesia, Federated States of";
                if (CountryCode == "FO")
                    return "Faroe Islands";
                if (CountryCode == "FJ")
                    return "Fiji";
                if (CountryCode == "FI")
                    return "Finland";
                if (CountryCode == "FR")
                    return "France";
            }

            if (CountryCode[0]== 'G')
            {
                if (CountryCode == "GA")
                    return "Gabon";
                if (CountryCode == "GB")
                    return "United Kingdom";
                if (CountryCode == "GF")
                    return "French Guiana";
                if (CountryCode == "GM")
                    return "Gambia";
                if (CountryCode == "GE")
                    return "Georgia";
                if (CountryCode == "GH")
                    return "Ghana";
                if (CountryCode == "GI")
                    return "Gibraltar";
                if (CountryCode == "GR")
                    return "Greece";
                if (CountryCode == "GL")
                    return "Greenland";
                if (CountryCode == "GD")
                    return "Grenada";
                if (CountryCode == "GP")
                    return "Guadeloupe";
                if (CountryCode == "GU")
                    return "Guam";
                if (CountryCode == "GT")
                    return "Guatemala";
                if (CountryCode == "GG")
                    return "Guernsey";
                if (CountryCode == "GN")
                    return "Guinea";
                if (CountryCode == "GQ")
                    return "Equatorial Guinea";
                if (CountryCode == "GW")
                    return "Guinea-Bissau";
                if (CountryCode == "GY")
                    return "Guyana";
                if (CountryCode == "GS")
                    return "South Georgia and the South Sandwich Islands";
            }

            if (CountryCode[0]== 'H')
            {
                if (CountryCode == "HT")
                    return "Haiti";
                if (CountryCode == "HM")
                    return "Heard Island and McDonald Islands";
                if (CountryCode == "HN")
                    return "Honduras";
                if (CountryCode == "HK")
                    return "Hong Kong";
                if (CountryCode == "HR")
                    return "Croatia";
                if (CountryCode == "HU")
                    return "Hungary";
            }

            if (CountryCode[0]== 'I')
            {
                if (CountryCode == "IS")
                    return "Iceland";
                if (CountryCode == "IN")
                    return "India";
                if (CountryCode == "ID")
                    return "Indonesia";
                if (CountryCode == "IO")
                    return "British Indian ocean territory";
                if (CountryCode == "IR")
                    return "Iran";
                if (CountryCode == "IQ")
                    return "Iraq";
                if (CountryCode == "IE")
                    return "Ireland";
                if (CountryCode == "IM")
                    return "Isle of Man";
                if (CountryCode == "IL")
                    return "Israel";
                if (CountryCode == "IT")
                    return "Italy";
            }

            if (CountryCode[0]== 'J')
            {
                if (CountryCode == "JM")
                    return "Jamaica";
                if (CountryCode == "JP")
                    return "Japan";
                if (CountryCode == "JE")
                    return "Jersey";
                if (CountryCode == "JO")
                    return "Jordan";
            }

            if (CountryCode[0]== 'K')
            {
                if (CountryCode == "KZ")
                    return "Kazakhstan";
                if (CountryCode == "KE")
                    return "Kenya";
                if (CountryCode == "KH")
                    return "Cambodia";
                if (CountryCode == "KI")
                    return "Kiribati";
                if (CountryCode == "KN")
                    return "Saint Kitts and Nevis";
                if (CountryCode == "KP")
                    return "Korea, Democratic Peoples Republic of";
                if (CountryCode == "KR")
                    return "Korea, Republic of";
                if (CountryCode == "KW")
                    return "Kuwait";
                if (CountryCode == "KG")
                    return "Kyrgyzstan";
                if (CountryCode == "KY")
                    return "Cayman Islands";
            }

            if (CountryCode[0]== 'L')
            {
                if (CountryCode == "LA")
                    return "Lao People's Democratic Republic";
                if (CountryCode == "LV")
                    return "Latvia";
                if (CountryCode == "LB")
                    return "Lebanon";
                if (CountryCode == "LC")
                    return "Saint Lucia";
                if (CountryCode == "LK")
                    return "Sri Lanka";
                if (CountryCode == "LM")
                    return "Comoros";
                if (CountryCode == "LS")
                    return "Lesotho";
                if (CountryCode == "LR")
                    return "Liberia";
                if (CountryCode == "LY")
                    return "Libyan Arab Jamahiriya";
                if (CountryCode == "LI")
                    return "Liechtenstein";
                if (CountryCode == "LT")
                    return "Lithuania";
                if (CountryCode == "LU")
                    return "Luxembourg";
            }

            if (CountryCode[0]== 'M')
            {
                if (CountryCode == "MO")
                    return "Macao";
                if (CountryCode == "MK")
                    return "Macedonia";
                if (CountryCode == "MF")
                    return "Saint Martin";
                if (CountryCode == "MG")
                    return "Madagascar";
                if (CountryCode == "MW")
                    return "Malawi";
                if (CountryCode == "MY")
                    return "Malaysia";
                if (CountryCode == "MV")
                    return "Maldives";
                if (CountryCode == "ML")
                    return "Mali";
                if (CountryCode == "MT")
                    return "Malta";
                if (CountryCode == "MH")
                    return "Marshall Islands";
                if (CountryCode == "MQ")
                    return "Martinique";
                if (CountryCode == "MP")
                    return "Northern Mariana Islands";
                if (CountryCode == "MR")
                    return "Mauritania";
                if (CountryCode == "MU")
                    return "Mauritius";
                if (CountryCode == "MX")
                    return "Mexico";
                if (CountryCode == "MD")
                    return "Moldova";
                if (CountryCode == "MC")
                    return "Monaco";
                if (CountryCode == "MN")
                    return "Mongolia";
                if (CountryCode == "ME")
                    return "Montenegro";
                if (CountryCode == "MS")
                    return "Montserrat";
                if (CountryCode == "MA")
                    return "Morocco";
                if (CountryCode == "MZ")
                    return "Mozambique";
                if (CountryCode == "MM")
                    return "Myanmar";
            }

            if (CountryCode[0]== 'N')
            {
                if (CountryCode == "NA")
                    return "Namibia";
                if (CountryCode == "NR")
                    return "Nauru";
                if (CountryCode == "NP")
                    return "Nepal";
                if (CountryCode == "NL")
                    return "Netherlands";
                if (CountryCode == "NC")
                    return "New Caledonia";
                if (CountryCode == "NZ")
                    return "New Zeland";
                if (CountryCode == "NI")
                    return "Nicaragua";
                if (CountryCode == "NE")
                    return "Niger";
                if (CountryCode == "NG")
                    return "Nigeria";
                if (CountryCode == "NU")
                    return "Niue";
                if (CountryCode == "NF")
                    return "Norfolk Island";
                if (CountryCode == "NO")
                    return "Norway";
            }


            if (CountryCode[0]== 'O')
            {
                if (CountryCode == "OM")
                    return "Oman";
            }

            if (CountryCode[0]== 'P')
            {
                if (CountryCode == "PK")
                    return "Pakistan";
                if (CountryCode == "PW")
                    return "Palau";
                if (CountryCode == "PS")
                    return "Palestinian territory";
                if (CountryCode == "PA")
                    return "Panama";
                if (CountryCode == "PG")
                    return "Papua New Guinea";
                if (CountryCode == "PY")
                    return "Paraguay";
                if (CountryCode == "PE")
                    return "Peru";
                if (CountryCode == "PF")
                    return "French Polynesia";
                if (CountryCode == "PH")
                    return "Philippines";
                if (CountryCode == "PN")
                    return "Pitcairn";
                if (CountryCode == "PL")
                    return "Poland";
                if (CountryCode == "PT")
                    return "Portugal";
                if (CountryCode == "PR")
                    return "Puerto Rico";
                if (CountryCode == "PM")
                    return "Saint Pierreand Miquelon";
            }

            if (CountryCode[0]== 'Q')
            {
                if (CountryCode == "QA")
                    return "QATAR";
            }

            if (CountryCode[0]== 'R')
            {
                if (CountryCode == "RE")
                    return "Reunion";
                if (CountryCode == "RO")
                    return "Romania";
                if (CountryCode == "RS")
                    return "Serbia";
                if (CountryCode == "RU")
                    return "Russian Federation";
                if (CountryCode == "RW")
                    return "Rwanda";
            }

            if (CountryCode[0]== 'S')
            {
                if (CountryCode == "SH")
                    return "Saint Helena";
                if (CountryCode == "SM")
                    return "San Marino";
                if (CountryCode == "ST")
                    return "Sao Tome and Principe";
                if (CountryCode == "SA")
                    return "Saudi Arabia";
                if (CountryCode == "SN")
                    return "Senegal";
                if (CountryCode == "SC")
                    return "Seychelles";
                if (CountryCode == "SL")
                    return "Sierra Leone";
                if (CountryCode == "SG")
                    return "Singapore";
                if (CountryCode == "SK")
                    return "Slovakia";
                if (CountryCode == "SI")
                    return "Slovenia";
                if (CountryCode == "SB")
                    return "Solomon Islands";
                if (CountryCode == "SO")
                    return "Somalia";
                if (CountryCode == "SD")
                    return "Sudan";
                if (CountryCode == "SR")
                    return "Suriname";
                if (CountryCode == "SJ")
                    return "Svalbard and Jan Mayen";
                if (CountryCode == "SZ")
                    return "Swaziland";
                if (CountryCode == "SE")
                    return "Sweden";
                if (CountryCode == "SV")
                    return "El Salvador";
                if (CountryCode == "SY")
                    return "Syrian Arab Republic";
            }

            if (CountryCode[0]== 'T')
            {
                if (CountryCode == "TW")
                    return "Taiwan";
                if (CountryCode == "TJ")
                    return "Tajikistan";
                if (CountryCode == "TZ")
                    return "Tanzania, United Republic of";
                if (CountryCode == "TH")
                    return "Thailand";
                if (CountryCode == "TL")
                    return "Timor-Leste";
                if (CountryCode == "TG")
                    return "Togo";
                if (CountryCode == "TK")
                    return "Tokelau";
                if (CountryCode == "TO")
                    return "Tonga";
                if (CountryCode == "TT")
                    return "Trinidad and Tobago";
                if (CountryCode == "TN")
                    return "Tunisia";
                if (CountryCode == "TR")
                    return "Turkey";
                if (CountryCode == "TM")
                    return "Turkmenistan";
                if (CountryCode == "TC")
                    return "Turks and Caicos Islands";
                if (CountryCode == "TD")
                    return "Chad";
                if (CountryCode == "TF")
                    return "French Southern Territories";
                if (CountryCode == "TV")
                    return "Tuvalu";
            }

            if (CountryCode[0]== 'U')
            {
                if (CountryCode == "UG")
                    return "Uganda";
                if (CountryCode == "UA")
                    return "Ukraine";
                if (CountryCode == "US")
                    return "United States";
                if (CountryCode == "UM")
                    return "United States Minor Outlying Islands";
                if (CountryCode == "UY")
                    return "Uruguay";
                if (CountryCode == "UZ")
                    return "Uzbekistan";
            }

            if (CountryCode[0]== 'V')
            {
                if (CountryCode == "VU")
                    return "Vanuatu";
                if (CountryCode == "VE")
                    return "Venezuela";
                if (CountryCode == "VN")
                    return "Viet Nam";
                if (CountryCode == "VA")
                    return "Vatican City State";
                if (CountryCode == "VG")
                    return "Virgin Islands, British";
                if (CountryCode == "VI")
                    return "Virgin Islands, U.S.";
                if (CountryCode == "VC")
                    return "Saint Vincent and the Grenadines";
            }

            if (CountryCode[0]== 'W')
            {
                if (CountryCode == "WF")
                    return "Wallis and Futuna";
                if (CountryCode == "WS")
                    return "Samoa";
            }

            if (CountryCode[0]== 'Y')
            {
                if (CountryCode == "YE")
                    return "Yemen";
                if (CountryCode == "YT")
                    return "Mayotte";
            }

            if (CountryCode[0]== 'Z')
            {
                if (CountryCode == "ZA")
                    return "South Africa";
                if (CountryCode == "ZM")
                    return "Zambia";
                if (CountryCode == "ZW")
                    return "Zimbabwe";
            }

            return CountryCode;
        }
        static public string CountryToCode(string CountryName)
        {
            if (CountryName[0]== 'A')
            {
                if (CountryName == "Afghanistan")
                    return "AF";
                if (CountryName == "Aland islands")
                    return "AX";
                if (CountryName == "Albania")
                    return "AL";
                if (CountryName == "Algeria")
                    return "DZ";
                if (CountryName == "American Samoa")
                    return "AS";
                if (CountryName == "Andorra")
                    return "AD";
                if (CountryName == "Angola")
                    return "AO";
                if (CountryName == "Anguilla")
                    return "AI";
                if (CountryName == "Antarctica")
                    return "AQ";
                if (CountryName == "Antigua and Barbuda")
                    return "AG";
                if (CountryName == "Argentina")
                    return "AR";
                if (CountryName == "Armenia")
                    return "AM";
                if (CountryName == "Aruba")
                    return "AW";
                if (CountryName == "Australia")
                    return "AU";
                if (CountryName == "Austria")
                    return "AT";
                if (CountryName == "Azerbaijan")
                    return "AZ";
            }

            if (CountryName[0]== 'B')
            {
                if (CountryName == "Bahamas")
                    return "BS";
                if (CountryName == "Bahrain")
                    return "BH";
                if (CountryName == "Bangladesh")
                    return "BD";
                if (CountryName == "Barbados")
                    return "BB";
                if (CountryName == "Belarus")
                    return "BY";
                if (CountryName == "Belgium")
                    return "BE";
                if (CountryName == "Belize")
                    return "BZ";
                if (CountryName == "Benin")
                    return "BJ";
                if (CountryName == "Bermuda")
                    return "BM";
                if (CountryName == "Bhutan")
                    return "BT";
                if (CountryName == "Bolivia")
                    return "BO";
                if (CountryName == "Bosnia and Herzegovina")
                    return "BA";
                if (CountryName == "Botswana")
                    return "BW";
                if (CountryName == "Bouvet Island")
                    return "BV";
                if (CountryName == "Brasil")
                    return "BR";
                if (CountryName == "British Indian ocean territory")
                    return "IO";
                if (CountryName == "Brunei Darussalam")
                    return "BN";
                if (CountryName == "Bulgaria")
                    return "BG";
                if (CountryName == "Burkina Faso")
                    return "BF";
                if (CountryName == "Burundi")
                    return "BI";
            }

            if (CountryName[0]== 'C')
            {
                if (CountryName == "Cameroon")
                    return "CM";
                if (CountryName == "Cambodia")
                    return "KH";
                if (CountryName == "Canada")
                    return "CA";
                if (CountryName == "Cape Verde")
                    return "CV";
                if (CountryName == "Cayman Islands")
                    return "KY";
                if (CountryName == "Central African Republic")
                    return "CF";
                if (CountryName == "Chad")
                    return "TD";
                if (CountryName == "Chile")
                    return "CL";
                if (CountryName == "China")
                    return "CN";
                if (CountryName == "Christmas Island")
                    return "CX";
                if (CountryName == "Cocos Islands")
                    return "CC";
                if (CountryName == "Colombia")
                    return "CO";
                if (CountryName == "Comoros")
                    return "LM";
                if (CountryName == "Congo")
                    return "CG";
                if (CountryName == "Congo, The Democratic Republic of the")
                    return "CD";
                if (CountryName == "Cook Islands")
                    return "CK";
                if (CountryName == "Costa Rica")
                    return "CR";
                if (CountryName == "Cote D'ivoire")
                    return "CI";
                if (CountryName == "Croatia")
                    return "HR";
                if (CountryName == "Cuba")
                    return "CU";
                if (CountryName == "Cyprus")
                    return "CY";
                if (CountryName == "Czech Republic")
                    return "CZ";
            }

            if (CountryName[0]== 'D')
            {
                if (CountryName == "Denmark")
                    return "DK";
                if (CountryName == "Djibouti")
                    return "DJ";
                if (CountryName == "Dominica")
                    return "DM";
                if (CountryName == "Dominican Republic")
                    return "DO";
            }

            if (CountryName[0]== 'E')
            {
                if (CountryName == "Ecuador")
                    return "EC";
                if (CountryName == "El Salvador")
                    return "SV";
                if (CountryName == "Egypt")
                    return "EG";
                if (CountryName == "Eritrea")
                    return "ER";
                if (CountryName == "Equatorial Guinea")
                    return "GQ";
                if (CountryName == "Estonia")
                    return "EE";
                if (CountryName == "Ethiopia")
                    return "ET";
            }

            if (CountryName[0]== 'F')
            {
                if (CountryName == "Falkland Islands(Malvinas)")
                    return "FK";
                if (CountryName == "Faroe Islands")
                    return "FO";
                if (CountryName == "Fiji")
                    return "FJ";
                if (CountryName == "Finland")
                    return "FI";
                if (CountryName == "France")
                    return "FR";
                if (CountryName == "French Guiana")
                    return "GF";
                if (CountryName == "French Polynesia")
                    return "PF";
                if (CountryName == "French Southern Territories")
                    return "TF";
            }

            if (CountryName[0]== 'G')
            {
                if (CountryName == "Gabon")
                    return "GA";
                if (CountryName == "Gambia")
                    return "GM";
                if (CountryName == "Georgia")
                    return "GE";
                if (CountryName == "Germany")
                    return "DE";
                if (CountryName == "Ghana")
                    return "GH";
                if (CountryName == "Gibraltar")
                    return "GI";
                if (CountryName == "Greece")
                    return "GR";
                if (CountryName == "Greenland")
                    return "GL";
                if (CountryName == "Grenada")
                    return "GD";
                if (CountryName == "Guadeloupe")
                    return "GP";
                if (CountryName == "Guam")
                    return "GU";
                if (CountryName == "Guatemala")
                    return "GT";
                if (CountryName == "Guernsey")
                    return "GG";
                if (CountryName == "Guinea")
                    return "GN";
                if (CountryName == "Guinea-Bissau")
                    return "GW";
                if (CountryName == "Guyana")
                    return "GY";
            }

            if (CountryName[0]== 'H')
            {
                if (CountryName == "Haiti")
                    return "HT";
                if (CountryName == "Heard Island and McDonald Islands")
                    return "HM";
                if (CountryName == "Honduras")
                    return "HN";
                if (CountryName == "Hong Kong")
                    return "HK";
                if (CountryName == "Hungary")
                    return "HU";
            }

            if (CountryName[0]== 'I')
            {
                if (CountryName == "Iceland")
                    return "IS";
                if (CountryName == "India")
                    return "IN";
                if (CountryName == "Indonesia")
                    return "ID";
                if (CountryName == "Iran")
                    return "IR";
                if (CountryName == "Iraq")
                    return "IQ";
                if (CountryName == "Ireland")
                    return "IE";
                if (CountryName == "Isle of Man")
                    return "IM";
                if (CountryName == "Israel")
                    return "IL";
                if (CountryName == "Italy")
                    return "IT";
            }

            if (CountryName[0]== 'J')
            {
                if (CountryName == "Jamaica")
                    return "JM";
                if (CountryName == "Japan")
                    return "JP";
                if (CountryName == "Jersey")
                    return "JE";
                if (CountryName == "Jordan")
                    return "JO";
            }

            if (CountryName[0]== 'K')
            {
                if (CountryName == "Kazakhstan")
                    return "KZ";
                if (CountryName == "Kenya")
                    return "KE";
                if (CountryName == "Kiribati")
                    return "KI";
                if (CountryName == "Korea, Democratic Peoples Republic of")
                    return "KP";
                if (CountryName == "Korea, Republic of")
                    return "KR";
                if (CountryName == "Kuwait")
                    return "KW";
                if (CountryName == "Kyrgyzstan")
                    return "KG";
            }

            if (CountryName[0]== 'L')
            {
                if (CountryName == "Lao People's Democratic Republic")
                    return "LA";
                if (CountryName == "Latvia")
                    return "LV";
                if (CountryName == "Lebanon")
                    return "LB";
                if (CountryName == "Lesotho")
                    return "LS";
                if (CountryName == "Liberia")
                    return "LR";
                if (CountryName == "Libyan Arab Jamahiriya")
                    return "LY";
                if (CountryName == "Liechtenstein")
                    return "LI";
                if (CountryName == "Lithuania")
                    return "LT";
                if (CountryName == "Luxembourg")
                    return "LU";
            }

            if (CountryName[0]== 'M')
            {
                if (CountryName == "Macao")
                    return "MO";
                if (CountryName == "Macedonia")
                    return "MK";
                if (CountryName == "Madagascar")
                    return "MG";
                if (CountryName == "Malawi")
                    return "MW";
                if (CountryName == "Malaysia")
                    return "MY";
                if (CountryName == "Maldives")
                    return "MV";
                if (CountryName == "Mali")
                    return "ML";
                if (CountryName == "Malta")
                    return "MT";
                if (CountryName == "Marshall Islands")
                    return "MH";
                if (CountryName == "Martinique")
                    return "MQ";
                if (CountryName == "Mauritania")
                    return "MR";
                if (CountryName == "Mauritius")
                    return "MU";
                if (CountryName == "Mayotte")
                    return "YT";
                if (CountryName == "Mexico")
                    return "MX";
                if (CountryName == "Micronesia, Federated States of")
                    return "FM";
                if (CountryName == "Moldova")
                    return "MD";
                if (CountryName == "Monaco")
                    return "MC";
                if (CountryName == "Mongolia")
                    return "MN";
                if (CountryName == "Montenegro")
                    return "ME";
                if (CountryName == "Montserrat")
                    return "MS";
                if (CountryName == "Morocco")
                    return "MA";
                if (CountryName == "Mozambique")
                    return "MZ";
                if (CountryName == "Myanmar")
                    return "MM";
            }

            if (CountryName[0]== 'N')
            {
                if (CountryName == "Namibia")
                    return "NA";
                if (CountryName == "Nauru")
                    return "NR";
                if (CountryName == "Nepal")
                    return "NP";
                if (CountryName == "Netherlands")
                    return "NL";
                if (CountryName == "Netherlands Antilles")
                    return "AN";
                if (CountryName == "New Caledonia")
                    return "NC";
                if (CountryName == "New Zeland")
                    return "NZ";
                if (CountryName == "Nicaragua")
                    return "NI";
                if (CountryName == "Niger")
                    return "NE";
                if (CountryName == "Nigeria")
                    return "NG";
                if (CountryName == "Niue")
                    return "NU";
                if (CountryName == "Norfolk Island")
                    return "NF";
                if (CountryName == "Northern Mariana Islands")
                    return "MP";
                if (CountryName == "Norway")
                    return "NO";
            }


            if (CountryName[0]== 'O')
            {
                if (CountryName == "Oman")
                    return "OM";
            }

            if (CountryName[0]== 'P')
            {
                if (CountryName == "Pakistan")
                    return "PK";
                if (CountryName == "Palau")
                    return "PW";
                if (CountryName == "Palestinian territory")
                    return "PS";
                if (CountryName == "Panama")
                    return "PA";
                if (CountryName == "Papua New Guinea")
                    return "PG";
                if (CountryName == "Paraguay")
                    return "PY";
                if (CountryName == "Peru")
                    return "PE";
                if (CountryName == "Philippines")
                    return "PH";
                if (CountryName == "Pitcairn")
                    return "PN";
                if (CountryName == "Poland")
                    return "PL";
                if (CountryName == "Portugal")
                    return "PT";
                if (CountryName == "Puerto Rico")
                    return "PR";
            }

            if (CountryName[0]== 'Q')
            {
                if (CountryName == "QATAR")
                    return "QA";
            }

            if (CountryName[0]== 'R')
            {
                if (CountryName == "Reunion")
                    return "RE";
                if (CountryName == "Romania")
                    return "RO";
                if (CountryName == "Russian Federation")
                    return "RU";
                if (CountryName == "Rwanda")
                    return "RW";
            }

            if (CountryName[0]== 'S')
            {
                if (CountryName == "Saint Barthelemy")
                    return "BL";
                if (CountryName == "Saint Helena")
                    return "SH";
                if (CountryName == "Saint Kitts and Nevis")
                    return "KN";
                if (CountryName == "Saint Lucia")
                    return "LC";
                if (CountryName == "Saint Martin")
                    return "MF";
                if (CountryName == "Saint Pierreand Miquelon")
                    return "PM";
                if (CountryName == "Saint Vincent and the Grenadines")
                    return "VC";
                if (CountryName == "Samoa")
                    return "WS";
                if (CountryName == "San Marino")
                    return "SM";
                if (CountryName == "Sao Tome and Principe")
                    return "ST";
                if (CountryName == "Saudi Arabia")
                    return "SA";
                if (CountryName == "Senegal")
                    return "SN";
                if (CountryName == "Serbia")
                    return "RS";
                if (CountryName == "Seychelles")
                    return "SC";
                if (CountryName == "Sierra Leone")
                    return "SL";
                if (CountryName == "Singapore")
                    return "SG";
                if (CountryName == "Slovakia")
                    return "SK";
                if (CountryName == "Slovenia")
                    return "SI";
                if (CountryName == "Solomon Islands")
                    return "SB";
                if (CountryName == "Somalia")
                    return "SO";
                if (CountryName == "South Africa")
                    return "ZA";
                if (CountryName == "South Georgia and the South Sandwich Islands")
                    return "GS";
                if (CountryName == "Spain")
                    return "ES";
                if (CountryName == "Sri Lanka")
                    return "LK";
                if (CountryName == "Sudan")
                    return "SD";
                if (CountryName == "Suriname")
                    return "SR";
                if (CountryName == "Svalbard and Jan Mayen")
                    return "SJ";
                if (CountryName == "Swaziland")
                    return "SZ";
                if (CountryName == "Sweden")
                    return "SE";
                if (CountryName == "Switzerland")
                    return "CH";
                if (CountryName == "Syrian Arab Republic")
                    return "SY";
            }

            if (CountryName[0]== 'T')
            {
                if (CountryName == "Taiwan")
                    return "TW";
                if (CountryName == "Tajikistan")
                    return "TJ";
                if (CountryName == "Tanzania, United Republic of")
                    return "TZ";
                if (CountryName == "Thailand")
                    return "TH";
                if (CountryName == "Timor-Leste")
                    return "TL";
                if (CountryName == "Togo")
                    return "TG";
                if (CountryName == "Tokelau")
                    return "TK";
                if (CountryName == "Tonga")
                    return "TO";
                if (CountryName == "Trinidad and Tobago")
                    return "TT";
                if (CountryName == "Tunisia")
                    return "TN";
                if (CountryName == "Turkey")
                    return "TR";
                if (CountryName == "Turkmenistan")
                    return "TM";
                if (CountryName == "Turks and Caicos Islands")
                    return "TC";
                if (CountryName == "Tuvalu")
                    return "TV";
            }

            if (CountryName[0]== 'U')
            {
                if (CountryName == "Uganda")
                    return "UG";
                if (CountryName == "Ukraine")
                    return "UA";
                if (CountryName == "United Kingdom")
                    return "GB";
                if (CountryName == "United States")
                    return "US";
                if (CountryName == "United States Minor Outlying Islands")
                    return "UM";
                if (CountryName == "United Arab Emirates")
                    return "AE";
                if (CountryName == "Uruguay")
                    return "UY";
                if (CountryName == "Uzbekistan")
                    return "UZ";
            }

            if (CountryName[0]== 'V')
            {
                if (CountryName == "Vanuatu")
                    return "VU";
                if (CountryName == "Venezuela")
                    return "VE";
                if (CountryName == "Viet Nam")
                    return "VN";
                if (CountryName == "Vatican City State")
                    return "VA";
                if (CountryName == "Virgin Islands, British")
                    return "VG";
                if (CountryName == "Virgin Islands, U.S.")
                    return "VI";
            }

            if (CountryName[0]== 'W')
            {
                if (CountryName == "Wallis and Futuna")
                    return "WF";
                if (CountryName == "Western Sahara")
                    return "EH";
            }

            if (CountryName[0]== 'Y')
            {
                if (CountryName == "Yemen")
                    return "YE";
            }

            if (CountryName[0]== 'Z')
            {
                if (CountryName == "Zambia")
                    return "ZM";
                if (CountryName == "Zimbabwe")
                    return "ZW";
            }

            /*if(isalpha(CountryName.ElementAt(0)))
            {
                return CountryName;
            }
            else
            {
                CountryName.Empty();
                return CountryName;
            }*/
            return CountryName;
        }
    }
    public interface ITorController
    {
        void Connect();
        string ReadTorrc();
        string ReadPolipoConf();
        void WriteTorrc(string TorrcText);
        void WritePolipoConf(string PolipoConfText);
        string GetCurrentIP();
        void ChangeIP();
        string GetCountries();
        string GetCountry(string IP);
        string SendSignal(string Signal);
        bool SendSimpleSignal(string Signal);
        bool SendReload();
    }

    public struct Country
    {
        public string Name { get; set; }
        public uint Number { get; set; }
    }
    public class CountriesList : ObservableCollection<Country>
    {
    }
}
