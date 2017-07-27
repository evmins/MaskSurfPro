using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

using MaskSurfPro.Resources;

namespace MaskSurfPro.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public int TorPort;
        public int TorControlPort;
        public int PolipoPort;
        public bool IsStartOnBoot;

        public string PortsTitleText { get; set; }
        public string TorPortLabelText { get; set; }
        public string TorComPortText { get; set; }
        public string PolipoPortText { get; set; }
        public string ApplySettingsBtnText { get; set; }
        public string CancelSettingsBtnText { get; set; }

        public SettingsViewModel()
        {
            TorPort = 9050;
            TorControlPort = 9051;
            PolipoPort = 8000;
            IsStartOnBoot = false;

            Translate();
        }
        public bool ApplySettings()
        {
            StringBuilder strConfig = new StringBuilder();
            StringBuilder ExcludedCodesResult = new StringBuilder();
            int nStart;

            strConfig.Append(Tor.ReadTorrc());
            if (!String.IsNullOrWhiteSpace(strConfig.ToString()))
            {
                if (TorPort != 0)
                {
                    nStart = strConfig.ToString().IndexOf("SOCKSPort");
                    if (nStart != -1)
                    {
                        int nEnd = strConfig.ToString().IndexOf(Environment.NewLine, nStart + 1);
                        if (nEnd < 0)
                        {
                            strConfig.Remove(nStart, strConfig.Length - nStart);
                        }
                        else
                        {
                            strConfig.Remove(nStart, nEnd - nStart);
                        }
                    }
                    strConfig.Append("SOCKSPort " + TorPort.ToString() + Environment.NewLine);
                    Settings.SetInt("Tor port", TorPort);
                }
                if (TorControlPort != 0)
                {
                    nStart = strConfig.ToString().IndexOf("ControlPort");
                    if (nStart != -1)
                    {
                        int nEnd = strConfig.ToString().IndexOf(Environment.NewLine, nStart + 1);
                        if (nEnd < 0)
                        {
                            strConfig.Remove(nStart, strConfig.Length - nStart);
                        }
                        else
                        {
                            strConfig.Remove(nStart, nEnd - nStart);
                        }
                    }
                    strConfig.Append("ControlPort " + TorControlPort.ToString() + Environment.NewLine);
                    Settings.SetInt("Tor control port", TorControlPort);
                }
                Tor.WriteTorrc(strConfig.ToString());
            }
            StringBuilder strPolipoConfig = new StringBuilder();
            strPolipoConfig.Append(Tor.ReadPolipoConf());
            if (PolipoPort != 0)
            {
                nStart = strPolipoConfig.ToString().IndexOf("proxyPort=");
                if (nStart != -1)
                {
                    int nEnd = strPolipoConfig.ToString().IndexOf(Environment.NewLine, nStart + 1);
                    if (nEnd < 0)
                    {
                        strPolipoConfig.Remove(nStart, strPolipoConfig.Length - nStart);
                    }
                    else
                    {
                        strPolipoConfig.Remove(nStart, nEnd - nStart);
                    }
                }
                strPolipoConfig.Append("proxyPort=" + PolipoPort.ToString() + Environment.NewLine);

                //socksParentProxy
                nStart = strPolipoConfig.ToString().IndexOf("socksParentProxy=");
                if (nStart != -1)
                {
                    int nEnd = strPolipoConfig.ToString().IndexOf(Environment.NewLine, nStart + 1);
                    if (nEnd < 0)
                    {
                        strPolipoConfig.Remove(nStart, strPolipoConfig.Length - nStart);
                    }
                    else
                    {
                        strPolipoConfig.Remove(nStart, nEnd - nStart);
                    }
                }
                strPolipoConfig.Append("socksParentProxy=\"localhost\\:" + TorPort.ToString() + "\"" + Environment.NewLine);

                Tor.WritePolipoConf(strPolipoConfig.ToString());
                Settings.SetInt("Polipo port", PolipoPort);
            }
            /*
            if (IsStartOnBoot != Settings.GetBool("Start on boot", false)) //Applys changes
            {
                if (IsStartOnBoot)
                {
                    //
                }
                else
                {
                    //
                }
            }
            */
            //смена цепи и перезагрузка настроек tor
            bool bResult;

            Task t = Task.Run(() =>
            {
                bResult = Tor.SendSimpleSignal("SIGNAL RELOAD\r\n");
                bResult = Tor.SendSimpleSignal("SIGNAL NEWNYM\r\n");
            });

            return true;
        }
        public void Translate()
        {
            PortsTitleText = AppStrings.Ports;
            TorPortLabelText = AppStrings.TorListeningPort;
            TorComPortText = AppStrings.TorControlPort;
            PolipoPortText = AppStrings.PolipoListeningPort;
            ApplySettingsBtnText = AppStrings.Apply;
            CancelSettingsBtnText = AppStrings.Cancel;
        }
    }
}
