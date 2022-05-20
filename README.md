# yamaha-http

This is a port of: https://github.com/PSeitz/yamaha-nodejs  
With service discovery delivered by: https://github.com/Yortw/RSSDP 

Example XML output Basic Info from Yamaha Receiver
```xml
<YAMAHA_AV rsp="GET" RC="0">
    <Main_Zone>
        <Basic_Status>
            <Power_Control>
                <Power>On</Power>
                <Sleep>Off</Sleep>
            </Power_Control>
            <Volume>
                <Lvl>
                    <Val>-335</Val>
                    <Exp>1</Exp>
                    <Unit>dB</Unit>
                </Lvl>
                <Mute>Off</Mute>
            </Volume>
            <Input>
            <Input_Sel>AUDIO1</Input_Sel>
            <Input_Sel_Item_Info>
                <Param>AUDIO1</Param>
                <RW>RW</RW>
                <Title> AUDIO1 </Title>
                <Icon>
                    <On>/YamahaRemoteControl/Icons/icon002.png</On>
                    <Off></Off>
                </Icon>
                <Src_Name></Src_Name>
                <Src_Number>1</Src_Number>
            </Input_Sel_Item_Info></Input>
            <Surround>
                <Program_Sel>
                    <Current>
                        <Straight>Off</Straight>
                        <Enhancer>Off</Enhancer>
                        <Sound_Program>2ch Stereo</Sound_Program>
                    </Current>
                </Program_Sel>
                <_3D_Cinema_DSP>Off</_3D_Cinema_DSP>
            </Surround>
            <Party_Info>Off</Party_Info>
            <Sound_Video>
                <Tone>
                    <Bass>
                        <Val>0</Val>
                        <Exp>1</Exp>
                        <Unit>dB</Unit>
                    </Bass>
                    <Treble>
                        <Val>0</Val>
                        <Exp>1</Exp>
                        <Unit>dB</Unit>
                    </Treble>
                </Tone>
                <Direct>
                    <Mode>Off</Mode>
                </Direct>
                <HDMI>
                    <Standby_Through_Info>On</Standby_Through_Info><Output>
                        <OUT_1>On</OUT_1>
                    </Output>
                </HDMI>
                <Adaptive_DRC>Off</Adaptive_DRC>
            </Sound_Video>
        </Basic_Status>
    </Main_Zone>
</YAMAHA_AV>
```

Example output for System Config XML  
```xml
<YAMAHA_AV rsp="GET" RC="0">
    <System>
        <Config>
            <Model_Name>RX-S600D</Model_Name>
            <System_ID>005B8193</System_ID>
            <Version>1.09/2.06</Version>
            <Feature_Existence>
                <Main_Zone>1</Main_Zone>
                <Zone_2>1</Zone_2>
                <Zone_3>0</Zone_3>
                <Zone_4>0</Zone_4>
                <Tuner>0</Tuner>
                <DAB>1</DAB>
                <HD_Radio>0</HD_Radio>
                <Rhapsody>0</Rhapsody>
                <Napster>0</Napster>
                <SiriusXM>0</SiriusXM>
                <Spotify>1</Spotify>
                <Pandora>0</Pandora>
                <SERVER>1</SERVER>
                <NET_RADIO>1</NET_RADIO>
                <USB>1</USB>
                <iPod_USB>1</iPod_USB>
                <AirPlay>1</AirPlay> 
            </Feature_Existence>
            <Name><Input>
                <HDMI_1> PS3 </HDMI_1>
                <HDMI_2>Intel NUC</HDMI_2>
                <HDMI_3> MAC </HDMI_3>
                <HDMI_4> HDMI PC </HDMI_4>
                <HDMI_5> TV </HDMI_5>
                <AV_1> AV1 </AV_1>
                <AV_2> AV2 </AV_2>
                <AV_3> AV3 </AV_3>
                <AV_4> AV4 </AV_4>
                <AV_5> AV5 </AV_5>
                <V_AUX> V-AUX </V_AUX>
                <AUDIO_1> AUDIO1 </AUDIO_1>
                <AUDIO_2> CD </AUDIO_2>
                <USB> USB </USB></Input>
            </Name>
        </Config>
    </System>
</YAMAHA_AV>
```