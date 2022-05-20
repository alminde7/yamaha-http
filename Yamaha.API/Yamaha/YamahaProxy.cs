using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Linq;
using System.Collections.Generic;

namespace Yamaha.API.Yamaha
{
    public class YamahaProxy
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<YamahaProxy> _logger;
        private readonly string _defaultZone = "Main_Zone";

        public YamahaProxy(HttpClient httpClient, ILogger<YamahaProxy> logger)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ReceiverStatus> GetBasicInfo(string zone)
        {
            var xmlString = await SendRequest($"<YAMAHA_AV cmd=\"GET\"><{zone}><Basic_Status>GetParam</Basic_Status></{zone}></YAMAHA_AV>");
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            return new ReceiverStatus(xmlDocument);
        }

        public async Task<ReceiverInformation> GetSystemConfig()
        {
            var xmlString = await SendRequest($"<YAMAHA_AV cmd=\"GET\"><System><Config>GetParam</Config></System></YAMAHA_AV>");
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            return new ReceiverInformation(xmlDocument);
        }
        
        //public async Task<HeadphoneStatus> GetHeadphoneConnectedStatus()
        //{

        //}

        #region Power controls
        public Task PowerOn(string zone)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{zone}><Power_Control><Power>On</Power></Power_Control></{zone}></YAMAHA_AV>");
        }

        public Task PowerOff(string zone)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{zone}><Power_Control><Power>Standby</Power></Power_Control></{zone}></YAMAHA_AV>");
        }

        public Task Sleep(string zone, int time)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Volume controls
        public Task Mute(string zone)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{zone}><Volume><Mute>On</Mute></Volume></{zone}></YAMAHA_AV>");
        }

        public Task Unmute(string zone)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{zone}><Volume><Mute>Off</Mute></Volume></{zone}></YAMAHA_AV>");
        }

        public Task SetVolume(string zone, int volume)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{zone}><Volume><Lvl><Val>{volume}</Val><Exp>1</Exp><Unit>dB</Unit></Lvl></Volume></{zone}></YAMAHA_AV>");
        }

        public async Task TurnVolumeUpBy(string zone, int volumeAmount)
        {
            // Get Basic info
        }

        public async Task TurnVolumeDownBy(string zone, int volumeAmount)
        {

        }

        /// <summary>
        /// Only avaliable in Main_Zone
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Task SetSubwooferTrim(int level)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{_defaultZone}><Volume><Subwoofer_Trim><Val>{level}</Val><Exp>1</Exp><Unit>dB</Unit></Subwoofer_Trim></Volume></{_defaultZone}></YAMAHA_AV>");
        }
        #endregion

        #region Play controls
        public Task Play(string zone)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{zone}><Play_Control><Playback>Play</Playback></Play_Control></{zone}></YAMAHA_AV>");
        }

        public Task Pause(string zone)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{zone}><Play_Control><Playback>Pause</Playback></Play_Control></{zone}></YAMAHA_AV>");
        }

        public Task Stop(string zone)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{zone}><Play_Control><Playback>Stop</Playback></Play_Control></{zone}></YAMAHA_AV>");
        }

        public Task Skip(string zone)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{zone}><Play_Control><Playback>Skip Fwd</Playback></Play_Control></{zone}></YAMAHA_AV>");
        }

        public Task Rewind(string zone)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{zone}><Play_Control><Playback>Skip Rev</Playback></Play_Control></{zone}></YAMAHA_AV>");
        }
        #endregion

        #region PartyMode controls
        public Task PartyModeOn()
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><System><Party_Mode><Mode>On</Mode></Party_Mode></System></YAMAHA_AV>");
        }

        public Task PartyModeOff()
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><System><Party_Mode><Mode>Off</Mode></Party_Mode></System></YAMAHA_AV>");
        }

        public Task PartyModeIncreaseVolume()
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><System><Party_Mode><Volume><Lvl>Up</Lvl></Volume><</Party_Mode></System></YAMAHA_AV>");
        }

        public Task PartyModeDecreaseVolume()
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><System><Party_Mode><Volume><Lvl>Down</Lvl></Volume><</Party_Mode></System></YAMAHA_AV>");
        }
        #endregion

        #region Input controls
        public Task SetInput(string zone, string input)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{zone}><Input><Input_Sel>{input}</Input_Sel></Input></{zone}></YAMAHA_AV>");
        }
        #endregion

        #region Scene controls
        public Task SetScene(string zone, string scene)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{zone}><Scene><Scene_Sel>Scene {scene}</Scene_Sel></Scene></{zone}></YAMAHA_AV>");
        }
        #endregion

        #region Cusor controls
        public Task RemoteCursorAction(string zone, string cursorAction)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{zone}><Cursor_Control><Cursor>{cursorAction}</Cursor></Cursor_Control></{zone}></YAMAHA_AV>");
        }

        public Task RemoveMenuAction(string zone, string menuAction)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{zone}><Cursor_Control><Cursor>{menuAction}</Cursor></Cursor_Control></{zone}></YAMAHA_AV>");
        }
        #endregion

        #region Sound/Video controls
        public Task SetPureDirect(bool enabled)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{_defaultZone}><Sound_Video><Pure_Direct><Mode>{(enabled ? "On" : "Off")}</Mode></Pure_Direct></Sound_Video></{_defaultZone}></YAMAHA_AV>");
        }

        /// <summary>
        /// Only avaliable in Main_Zone
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Task SetBass(int level)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{_defaultZone}><Sound_Video><Tone><Bass><Val>{level}</Val><Exp>1</Exp><Unit>dB</Unit></Bass></Tone></Sound_Video></{_defaultZone}></YAMAHA_AV>");
        }

        /// <summary>
        /// Only avaliable in Main_Zone
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Task SetTreble(int level)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{_defaultZone}><Sound_Video><Tone><Treble><Val>{level}</Val><Exp>1</Exp><Unit>dB</Unit></Treble></Tone></Sound_Video></{_defaultZone}></YAMAHA_AV>");
        }

        /// <summary>
        /// Only avaliable in Main_Zone
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Task SetDialogLift(int level)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{_defaultZone}><Sound_Video><Dialogue_Adjust><Dialogue_Lift>{level}</Dialogue_Lift></Dialogue_Adjust></Sound_Video></{_defaultZone}></YAMAHA_AV>");
        }

        /// <summary>
        /// Only avaliable in Main_Zone
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Task SetDialogLevel(int level)
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{_defaultZone}><Sound_Video><Dialogue_Adjust><Dialogue_Lvl>{level}</Dialogue_Lvl></Dialogue_Adjust></Sound_Video></{_defaultZone}></YAMAHA_AV>");
        }

        /// <summary>
        /// Only avaliable in Main_Zone
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Task YPAOVolumeOn()
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{_defaultZone}><Sound_Video><YPAO_Volume>Auto</YPAO_Volume></Sound_Video></{_defaultZone}></YAMAHA_AV>");
        }

        /// <summary>
        /// Only avaliable in Main_Zone
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Task YPAOVolumeOff()
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{_defaultZone}><Sound_Video><YPAO_Volume>Off</YPAO_Volume></Sound_Video></{_defaultZone}></YAMAHA_AV>");
        }

        /// <summary>
        /// Only avaliable in Main_Zone
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Task ExtraBassOn()
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{_defaultZone}><Sound_Video><Extra_Bass>Auto</Extra_Bass></Sound_Video></{_defaultZone}></YAMAHA_AV>");
        }

        /// <summary>
        /// Only avaliable in Main_Zone
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Task ExtraBassOff()
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{_defaultZone}><Sound_Video><Extra_Bass>Off</Extra_Bass></Sound_Video></{_defaultZone}></YAMAHA_AV>");
        }

        /// <summary>
        /// Only avaliable in Main_Zone
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Task AdaptiveDRCOn()
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{_defaultZone}><Sound_Video><Adaptive_DRC>Auto</Adaptive_DRC></Sound_Video></{_defaultZone}></YAMAHA_AV>");
        }

        /// <summary>
        /// Only avaliable in Main_Zone
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Task AdaptiveDRCOff()
        {
            return SendCommand($"<YAMAHA_AV cmd=\"PUT\"><{_defaultZone}><Sound_Video><Adaptive_DRC>Off</Adaptive_DRC></Sound_Video></{_defaultZone}></YAMAHA_AV>");
        }
        #endregion

        private async Task SendCommand(string xmlRequest)
        {
            var stringContent = new StringContent(xmlRequest, Encoding.UTF8, "application/xml");
            var result = await _httpClient.PostAsync("/YamahaRemoteControl/ctrl", stringContent);
        }

        private async Task<string> SendRequest(string xmlRequest)
        {
            var stringContent = new StringContent(xmlRequest, Encoding.UTF8, "application/xml");
            var result = await _httpClient.PostAsync("/YamahaRemoteControl/ctrl", stringContent);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return content;
            }
            return null;
        }
    }

    public static class CursorActions
    {
        public static string Up = "Up";
        public static string Down = "Down";
        public static string Right = "Right";
        public static string Left = "Left";
        public static string Return = "Return";
        public static string Select = "Sel";
    }

    public static class MenuActions
    {
        public static string Option = "Option";
        public static string Display = "Display";
    }

    public enum HeadphoneStatus
    {
        Connected,
        NotConnected,
        NotAvailable
    }

    public static class BasicInfoParser
    {
        public static int GetVolume(XmlDocument xml)
        {
            var volumeString = xml.DocumentElement.SelectSingleNode("/YAMAHA_AV/Main_Zone/Basic_Status/Volume/Lvl/Val").InnerText;
            return int.TryParse(volumeString, out var volume) ? volume : -999;
        }

        public static int GetBass(XmlDocument xml)
        {
            var volumeString = xml.DocumentElement.SelectSingleNode("/YAMAHA_AV/Main_Zone/Basic_Status/Sound_Video/Tone/Bass/Val").InnerText;
            return int.TryParse(volumeString, out var volume) ? volume : -999;
        }

        public static int GetTreble(XmlDocument xml)
        {
            var volumeString = xml.DocumentElement.SelectSingleNode("/YAMAHA_AV/Main_Zone/Basic_Status/Sound_Video/Tone/Treble/Val").InnerText;
            return int.TryParse(volumeString, out var volume) ? volume : -999;
        }

        public static bool IsMuted(XmlDocument xml)
        {
            var isMutedAsString = xml.DocumentElement.SelectSingleNode("/YAMAHA_AV/Main_Zone/Basic_Status/Volume/Mute").InnerText;
            return isMutedAsString == "On";
        }

        public static bool IsOn(XmlDocument xml)
        {
            var isOnAsString = xml.DocumentElement.SelectSingleNode("/YAMAHA_AV/Main_Zone/Basic_Status/Power_Control/Power").InnerText;
            return isOnAsString == "On";
        }

        public static bool IsSleep(XmlDocument xml)
        {
            var isOnAsString = xml.DocumentElement.SelectSingleNode("/YAMAHA_AV/Main_Zone/Basic_Status/Power_Control/Sleep").InnerText;
            return isOnAsString == "On";
        }

        public static string GetCurrentInput(XmlDocument xml)
        {
            return xml.DocumentElement.SelectSingleNode("/YAMAHA_AV/Main_Zone/Basic_Status/Input/Input_Sel").InnerText;
        }

        public static string GetCurrentInputTitle(XmlDocument xml)
        {
            return xml.DocumentElement.SelectSingleNode("/YAMAHA_AV/Main_Zone/Basic_Status/Input/Input_Sel_Item_Info/Title").InnerText;
        }

        public static bool IsPartyModeEnabled(XmlDocument xml)
        {
            var isOnAsString = xml.DocumentElement.SelectSingleNode("/YAMAHA_AV/Main_Zone/Basic_Status/Party_Info").InnerText;
            return isOnAsString == "On";
        }

        public static bool IsPureDirectEnabled(XmlDocument xml)
        {
            var isOnAsString = xml.DocumentElement.SelectSingleNode("/YAMAHA_AV/Main_Zone/Basic_Status/Sound_Video/Direct/Mode").InnerText;
            return isOnAsString == "On";
        }
    }

    public class ReceiverStatus
    {
        public ReceiverStatus()
        {

        }

        public ReceiverStatus(XmlDocument xml)
        {
            IsOn = BasicInfoParser.IsOn(xml);
            IsMuted = BasicInfoParser.IsMuted(xml);
            Volume = BasicInfoParser.GetVolume(xml);
            InputStatus = new InputStatus(xml);
        }

        public bool IsOn { get; init; }
        public bool IsMuted { get; init; }
        public int Volume { get; init; }
        public InputStatus InputStatus { get; init; }
    }

    public class InputStatus
    {
        public InputStatus(XmlDocument xml)
        {
            SelectedInput = BasicInfoParser.GetCurrentInput(xml);
            Title = BasicInfoParser.GetCurrentInputTitle(xml);
        }

        public string SelectedInput { get; }
        public string Title { get; }
    }

    public class ReceiverInformation
    {
        public ReceiverInformation(XmlDocument xml)
        {
            Model = ConfigInfoParser.GetModelName(xml);
            SystemId = ConfigInfoParser.GetSystemId(xml);
            Version = ConfigInfoParser.GetVersion(xml);
            Features = ConfigInfoParser.GetAvailableFeatures(xml);
            Inputs = ConfigInfoParser.GetAvailableInputs(xml);
        }

        public string Model { get; }
        public string SystemId { get; }
        public string Version { get; }
        public dynamic Inputs { get; }
        public dynamic Features { get; }
    }

    public static class ConfigInfoParser
    {
        public static dynamic GetAvailableInputs(XmlDocument xml)
        {
            var inputEnumerator = xml.DocumentElement.SelectSingleNode("/YAMAHA_AV/System/Config/Name/Input").ChildNodes.GetEnumerator();

            var list = new List<dynamic>();

            while (inputEnumerator.MoveNext())
            {
                var input = inputEnumerator.Current as XmlNode;
                if (input == null)
                    continue;

                list.Add(new { key = input.Name.Replace("_", ""), value = input.InnerText.Trim() });
            }

            return list;
        }

        public static dynamic GetAvailableFeatures(XmlDocument xml)
        {
            var inputEnumerator = xml.DocumentElement.SelectSingleNode("/YAMAHA_AV/System/Config/Feature_Existence").ChildNodes.GetEnumerator();

            var list = new List<dynamic>();

            while (inputEnumerator.MoveNext())
            {
                var input = inputEnumerator.Current as XmlNode;
                if (input == null)
                    continue;

                list.Add(new { key = input.Name, value = input.InnerText == "1" });
            }

            return list;
        }

        public static string GetModelName(XmlDocument xml)
        {
            return xml.DocumentElement.SelectSingleNode("/YAMAHA_AV/System/Config/Model_Name").InnerText;
        }

        public static string GetSystemId(XmlDocument xml)
        {
            return xml.DocumentElement.SelectSingleNode("/YAMAHA_AV/System/Config/System_ID").InnerText;
        }

        public static string GetVersion(XmlDocument xml)
        {
            return xml.DocumentElement.SelectSingleNode("/YAMAHA_AV/System/Config/Version").InnerText;
        }
    }


}
