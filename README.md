# yamaha-http

This is a port of: https://github.com/PSeitz/yamaha-nodejs  
With service discovery delivered by: https://github.com/Yortw/RSSDP 

Example XML output from Yamaha Receiver
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
            </Volume><Input>
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