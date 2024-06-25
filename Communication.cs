using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Data;
using System.Windows.Forms;

namespace MedicalGasPlantAutomation.Class
{
    class Communication
    {
        #region GLOBAL DEĞİŞKENLER
        public static int f=1;
        public static string COMName;
        public static int[] plantType = new int[255];
        public static int[] panelNo = new int[255];
        public static int[,] holdingRegister = new int[255, 100];
        public static byte[,] coil = new byte[255, 300];
        public static byte[] dataRcv = new byte[100];
        public static byte[] dataSend = new byte[8];
        public static int[] slaveid = new int[255];
        public static int dataRcvCnt, panelCnt;
        
        
        public static bool COMState;
        public static int subID, subID2, subID11,subID22, subID3, subID33, subID4, subID44;
        public static int[] COMErrCnt = new int[255];
        public static bool[] COMErr = new bool[255];
        public static int[] COMErrCntLKD = new int[255];
        public static bool[] COMErrLKD = new bool[255];
        public static bool[] OkCOM = new bool[255];
        public static int[] tankNo = new int[255];

        public static DataTable tablo = new DataTable();
        #endregion

        #region VAKUM DEĞİŞKENLERİ
        public static int[] modbusAdressVAC = new int[255];
        public static int[] modbusBaudrateVAC = new int[255];
        public static int[] vakumTankVAC = new int[40];
        public static int[] vakumHatVAC = new int[40];
        public static int[] vakumStartVAC = new int[40];
        public static int[] vakumStopVAC = new int[40];
        public static int[] startTimeP2VAC = new int[40];
        public static int[] startTimeP3VAC = new int[40];
        public static int[] startTimeP4VAC = new int[40];
        public static int[] startTimeAlarmVAC = new int[40];
        public static int[] StatPumpVAC = new int[40];
        public static int[] esYaslanmaGunVAC = new int[40];
        public static int[] esYasDutyVAC = new int[40];
        public static int[] saatPompa1VAC = new int[40];
        public static int[] dakikaPompa1VAC = new int[40];
        public static int[] saatPompa2VAC = new int[40];
        public static int[] dakikaPompa2VAC = new int[40];
        public static int[] saatPompa3VAC = new int[40];
        public static int[] dakikaPompa3VAC = new int[40];
        public static int[] saatPompa4VAC = new int[40];
        public static int[] dakikaPompa4VAC = new int[40];
        public static int[] StatP1VAC = new int[60];
        public static int[] StatP2VAC = new int[60];
        public static int[] StatP3VAC = new int[60];
        public static int[] StatP4VAC = new int[60];
        public static int[] pompaSicaklik1VAC = new int[40];
        public static int[] pompaSicaklik2VAC = new int[40];
        public static int[] pompaSicaklik3VAC = new int[40];
        public static int[] pompaSicaklik4VAC = new int[40];
        public static int[] pompaSicaklikMaxVAC = new int[40];
        public static int[] pompaSicaklikMinVAC = new int[40];
        public static int[] sicaklikOlcumVAC = new int[40];
        public static int[] birimVAC = new int[40];

        public static bool[] pumpDuty1VAC = new bool[40];
        public static bool[] pumpDuty2VAC = new bool[40];
        public static bool[] pumpDuty3VAC = new bool[40];
        public static bool[] pumpDuty4VAC = new bool[40];
        public static bool[] pumpEn1VAC = new bool[40];
        public static bool[] pumpEn2VAC = new bool[40];
        public static bool[] pumpEn3VAC = new bool[40];
        public static bool[] pumpEn4VAC = new bool[40];
        public static bool[] faultTermik1VAC = new bool[40];
        public static bool[] faultTermik2VAC = new bool[40];
        public static bool[] faultTermik3VAC = new bool[40];
        public static bool[] faultTermik4VAC = new bool[40];
        public static bool[] faultSicaklik1VAC = new bool[40];
        public static bool[] faultSicaklik2VAC = new bool[40];
        public static bool[] faultSicaklik3VAC = new bool[40];
        public static bool[] faultSicaklik4VAC = new bool[40];
        public static bool[] faultKontaktor1VAC = new bool[40];
        public static bool[] faultKontaktor2VAC = new bool[40];
        public static bool[] faultKontaktor3VAC = new bool[40];
        public static bool[] faultKontaktor4VAC = new bool[40];
        public static bool[] faultPlantEmergencyVAC = new bool[40];
        public static bool[] faultPlantPressureVAC = new bool[40];
        public static bool[] faultPlantVAC = new bool[40];
        public static bool[] faultFilterVAC = new bool[40];
        public static bool[] faultPlantTimeVAC = new bool[40];
        public static bool[] faultSensorTankVAC = new bool[40];
        public static bool[] faultSensorHatVAC = new bool[40];
        public static bool[] AlarmVAC = new bool[40];
        public static bool[] MuteVAC = new bool[40];

        #endregion

        #region HAVA DEĞİŞKENLERİ

        public static int[] basıncTankAIR = new int[40];
        public static int[] basıncHatAIR = new int[40];
        public static int[] basıncStartAIR = new int[40];
        public static int[] basıncStopAIR = new int[40];
        public static int[] startTimeP1AIR = new int[40];
        public static int[] startTimeP2AIR = new int[40];
        public static int[] startTimeP3AIR = new int[40];
        public static int[] startTimeP4AIR = new int[40];
        public static int[] startTimeAlarmAIR = new int[40];
        public static int[] StatPumpAIR = new int[40];
        public static int[] esYaslanmaGunAIR = new int[40];
        public static int[] esYasDutyAIR = new int[40];
        public static int[] saatPompa1AIR = new int[40];
        public static int[] dakikaPompa1AIR = new int[40];
        public static int[] saatPompa2AIR = new int[40];
        public static int[] dakikaPompa2AIR = new int[40];
        public static int[] saatPompa3AIR = new int[40];
        public static int[] dakikaPompa3AIR = new int[40];
        public static int[] saatPompa4AIR = new int[40];
        public static int[] dakikaPompa4AIR = new int[40];
        public static int[] StatP1AIR = new int[60];
        public static int[] StatP2AIR = new int[60];
        public static int[] StatP3AIR = new int[60];
        public static int[] StatP4AIR = new int[60];
        public static int[] pompaSicaklik1AIR = new int[40];
        public static int[] pompaSicaklik2AIR = new int[40];
        public static int[] pompaSicaklik3AIR = new int[40];
        public static int[] pompaSicaklik4AIR = new int[40];
        public static int[] pompaSicaklikMaxAIR = new int[40];
        public static int[] pompaSicaklikMinAIR = new int[40];
        public static int[] dewTemp = new int[40];
        public static int[] dewHum = new int[40];
        public static int[] dewPoint = new int[40];
        public static int[] dutyDryTime = new int[40];
        public static int[] drayerGrup = new int[60];
        public static int[] statDrayer1 = new int[40];
        public static int[] statDrayer2 = new int[40];
        public static int[] statDrayer3 = new int[40];
        public static int[] statDrayer4 = new int[40];
        public static int[] vakumLinePresFault = new int[40];
        public static int[] birimAIR = new int[40];

        public static bool[] pumpDuty1AIR = new bool[40];
        public static bool[] pumpDuty2AIR = new bool[40];
        public static bool[] pumpDuty3AIR = new bool[40];
        public static bool[] pumpDuty4AIR = new bool[40];
        public static bool[] pumpEn1AIR = new bool[40];
        public static bool[] pumpEn2AIR = new bool[40];
        public static bool[] pumpEn3AIR = new bool[40];
        public static bool[] pumpEn4AIR = new bool[40];
        public static bool[] faultTermik1AIR = new bool[40];
        public static bool[] faultTermik2AIR = new bool[40];
        public static bool[] faultTermik3AIR = new bool[40];
        public static bool[] faultTermik4AIR = new bool[40];
        public static bool[] faultSicaklik1AIR = new bool[40];
        public static bool[] faultSicaklik2AIR = new bool[40];
        public static bool[] faultSicaklik3AIR = new bool[40];
        public static bool[] faultSicaklik4AIR = new bool[40];
        public static bool[] faultKontaktor1AIR = new bool[40];
        public static bool[] faultKontaktor2AIR = new bool[40];
        public static bool[] faultKontaktor3AIR = new bool[40];
        public static bool[] faultKontaktor4AIR = new bool[40];
        public static bool[] faultPlantEmergencyAIR = new bool[40];
        public static bool[] faultPlantPressureAIR = new bool[40];
        public static bool[] faultPlantAIR = new bool[40];
        public static bool[] faultDewPointAIR = new bool[40];
        public static bool[] faultPlantTimeAIR = new bool[40];
        public static bool[] faultSensorTankAIR = new bool[40];
        public static bool[] faultSensorHatAIR = new bool[40];
        public static bool[] AlarmAIR = new bool[40];
        public static bool[] MuteAIR = new bool[40];
        public static bool[] dutyDry1 = new bool[40];
        public static bool[] dutyDry2 = new bool[40];
        public static bool[] dutyDry3 = new bool[40];
        public static bool[] dutyDry4 = new bool[40];

        #endregion

        #region OKSİJEN DEĞİŞKENLERİ

        public static int[] valReserveLowO2 = new int[40];
        public static int[] valChangeCylinderO2 = new int[40];
        public static int[] rampaO2 = new int[40];
        public static int[] birimO2 = new int[40];
        public static int[] valLinePressLowO2 = new int[40];
        public static int[] valLinePressHighO2 = new int[40];
        public static int[] cylinderPressLeftO2 = new int[40];
        public static int[] cylinderPressRightO2 = new int[40];
        public static int[] linePressO2 = new int[40];

        public static bool[] inDutyLeftO2 = new bool[60];
        public static bool[] inDutyRightO2 = new bool[60];
        public static bool[] reserveLowLeftO2 = new bool[60];
        public static bool[] reserveLowRightO2 = new bool[60];
        public static bool[] changeCylinderLeftO2 = new bool[60];
        public static bool[] changeCylinderRightO2 = new bool[60];
        public static bool[] changeImmediatelyLeftO2 = new bool[60];
        public static bool[] changeImmediatelyRightO2 = new bool[60];
        public static bool[] normalO2 = new bool[40];
        public static bool[] linePressLowO2 = new bool[40];
        public static bool[] linePressHighO2 = new bool[40];
        public static bool[] pressureFaultO2 = new bool[40];
        public static bool[] plantFaultLeftO2 = new bool[60];
        public static bool[] plantFaultRightO2 = new bool[60];
        public static bool[] alarmO2 = new bool[40];
        public static bool[] muteO2 = new bool[40];
        #endregion

        #region LİKİT DEĞİŞKENLERİ
        public static int[,] tankLevel = new int[50,5];
        #endregion

        #region AGSS DEĞİŞKENLERİ

        public static int[] vakumAGSS = new int[40];
        public static int[] StatP1AGSS = new int[40];
        public static int[] StatP2AGSS = new int[40];
        public static int[] saatPompa1AGSS = new int[40];
        public static int[] dakikaPompa1AGSS = new int[40];
        public static int[] saatPompa2AGSS = new int[40];
        public static int[] dakikaPompa2AGSS = new int[40];
        public static int[] birimAGSS = new int[40];

        public static bool[,] ButtonAGSS = new bool[50,50];

        public static bool[] pump1CutinAGSS = new bool[40]; 
        public static bool[] pump1CutOutAGSS = new bool[40];
        public static bool[] pump2CutinAGSS = new bool[40];
        public static bool[] pump2CutOutAGSS = new bool[40];
        public static bool[] contactFault1AGSS = new bool[40];
        public static bool[] contactFault2AGSS = new bool[40];
        public static bool[] airflowAGSS = new bool[40];
        public static bool[] faultPlantEmergencyAGSS = new bool[40];
        public static bool[] faultPlantAGSS = new bool[40];
        public static bool[] thermicFault1AGSS = new bool[40];
        public static bool[] thermicFault2AGSS = new bool[40];
        public static bool[] Passive1AGSS = new bool[40];
        public static bool[] Passive2AGSS = new bool[40];        

        public static bool[] AlarmAGSS = new bool[40];
        public static bool[] MuteAGSS = new bool[40];

        #endregion

        public static int[] dizi = new int[255];
        public static crc16 crc16 = new crc16();
        public static SerialPort _serialPort;
        public static Thread readThread = new Thread(new ThreadStart(Read)); 
        public static void _Main()
        {
            try
            {
                tablo = Class.Database.GetDataTable();
                for (int i = 0; i < Class.Database.plantCount; i++)
                {
                    plantType[i] = Convert.ToInt32(tablo.Rows[i]["SANTRAL TİPİ"]); //1:O2 2:VAC, 3:AIR
                    slaveid[i] = Convert.ToInt32(tablo.Rows[i]["SANTRAL ID"]);
                    tankNo[i] = Convert.ToInt32(tablo.Rows[i]["LİKİT TANK NO"]);
                    panelNo[i] = Convert.ToInt32(tablo.Rows[i]["Kimlik"]);
                }
                // Create a new SerialPort object with default settings.
                _serialPort = new SerialPort();
                // Allow the user to set the appropriate properties.
                _serialPort.PortName = COMName;
                _serialPort.BaudRate = 9600;
                _serialPort.Parity = Parity.None;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = StopBits.One;
                _serialPort.Handshake = Handshake.None;
                _serialPort.ReadBufferSize = 2400;
                // Set the read/write timeouts
                _serialPort.ReadTimeout = 1000;
                _serialPort.WriteTimeout = 1000;
                readThread.Start();                                               

            }
            catch(OleDbException ex)
 
            {
                
                MessageBox.Show(" " + ex);
            }
        }
        public static void _Main1()
        {
            try
            {
                tablo = Class.Database.GetDataTable();
                for (int i = 0; i < Class.Database.plantCount; i++)
                {
                    plantType[i] = Convert.ToInt32(tablo.Rows[i]["SANTRAL TİPİ"]); //1:O2 2:VAC, 3:AIR
                    slaveid[i] = Convert.ToInt32(tablo.Rows[i]["SANTRAL ID"]);
                    tankNo[i] = Convert.ToInt32(tablo.Rows[i]["LİKİT TANK NO"]);
                    panelNo[i] = Convert.ToInt32(tablo.Rows[i]["Kimlik"]);
                } 
            }
            catch (OleDbException ex)

            {

                MessageBox.Show(" " + ex);
            }
        }              


        public static void Read()
        {
            
            while (true)
            {
                
                #region SERİ PORTU HAZIRLA

                if (!_serialPort.IsOpen)
                {
                    COMState = false;
                    try
                    {
                        _serialPort.PortName = COMName;
                        _serialPort.Open();
                    }
                    catch
                    {
                        goto readEnd;
                    }
                }
                else
                {
                    COMState = true;
                    if (_serialPort.PortName != COMName)
                    {
                        _serialPort.Close();
                        _serialPort.DiscardInBuffer();
                        _serialPort.PortName = COMName;
                        try
                        {
                            
                            _serialPort.Open();
                        }
                        catch { }
                    }
                }
                //if (_serialPort.IsOpen) COMState = true;
                // else COMState = false;

                #endregion

                #region SERİ PORTTAN DATA AL

                try
                {
                    while (_serialPort.BytesToRead > 0)
                    {
                        dataRcv[dataRcvCnt] = Convert.ToByte(_serialPort.ReadByte());
                        // haberlesmeDurum += dataRcv[dataRcvCnt].ToString() + " ";
                        dataRcvCnt++;                        
                    }
                }
                catch { }


                #endregion

                #region HOLDING REGISTER OKUMA
                if (dataRcvCnt == 23 && dataRcv[1] == 3 && crc16.getCrc16(dataRcv, 21) == dataRcv[21] * 256 + dataRcv[22])
                {
                    COMErrCnt[panelCnt] = 0;
                    OkCOM[slaveid[panelCnt]] = true;
                    for (int i = 0; i < 9; i++)
                    {
                        if (plantType[panelCnt] == 2) holdingRegister[slaveid[panelCnt], (subID22 - 1) * 9 + i] = (dataRcv[2 * i + 3] * 256 + dataRcv[2 * i + 4]);
                        if (plantType[panelCnt] == 1) holdingRegister[slaveid[panelCnt], (subID11 - 1) * 9 + i] = (dataRcv[2 * i + 3] * 256 + dataRcv[2 * i + 4]);                        
                        if (plantType[panelCnt] == 3) holdingRegister[slaveid[panelCnt], (subID33 - 1) * 9 + i] = (dataRcv[2 * i + 3] * 256 + dataRcv[2 * i + 4]);
                        if (plantType[panelCnt] == 5) holdingRegister[slaveid[panelCnt], (subID44 - 1) * 9 + i] = (dataRcv[2 * i + 3] * 256 + dataRcv[2 * i + 4]);

                    }
                    holdingRegArray();
                }


                ///likit için 
                else if (dataRcvCnt == 19 && slaveid[panelCnt] == (dataRcv[3] * 256 + dataRcv[4]) &&  dataRcv[1] == 3 && crc16.getCrc16(dataRcv, 17) == dataRcv[17] * 256 + dataRcv[18])
                {
                    COMErrCntLKD[slaveid[panelCnt]] = 0;
                    OkCOM[slaveid[panelCnt]] = true;

                    //holdingRegister[slaveid[panelCnt], 0] = dataRcv[3] * 256 + dataRcv[4]; //ID
                    //holdingRegister[slaveid[panelCnt], 1] = dataRcv[5] * 256 + dataRcv[6]; //BR
                    holdingRegister[slaveid[panelCnt], 0] = dataRcv[7] * 256 + dataRcv[8]; //tank1
                    holdingRegister[slaveid[panelCnt], 1] = dataRcv[9] * 256 + dataRcv[10]; //tank2
                    holdingRegister[slaveid[panelCnt], 2] = dataRcv[11] * 256 + dataRcv[12]; //tank3
                    holdingRegister[slaveid[panelCnt], 3] = dataRcv[13] * 256 + dataRcv[14]; //tank4
                    holdingRegister[slaveid[panelCnt], 4] = dataRcv[15] * 256 + dataRcv[16]; //tank5
                    holdingRegArray();
                }
                #endregion

                #region COIL OKUMA

                else if (dataRcvCnt == 7 && dataRcv[1] == 1 && crc16.getCrc16(dataRcv, 5) == dataRcv[5] * 256 + dataRcv[6])
                {
                    COMErrCnt[panelCnt] = 0;
                    OkCOM[slaveid[panelCnt]] = true;
                    switch (plantType[panelCnt])
                    {
                        case 1://O2
                            coil[slaveid[panelCnt], 0] = dataRcv[3];
                            coil[slaveid[panelCnt], 1] = dataRcv[4];
                            break; 
                        case 2://VAC
                            coil[slaveid[panelCnt], subID2 * 2 - 10] = dataRcv[3];
                            coil[slaveid[panelCnt], subID2 * 2 - 9] = dataRcv[4];
                            break;
                        case 3://AIR
                            coil[slaveid[panelCnt], subID3 * 2 - 12] = dataRcv[3];
                            coil[slaveid[panelCnt], subID3 * 2 - 11] = dataRcv[4];                           
                            break;
                        case 4:

                            break;
                        case 5://AGSS
                            coil[slaveid[panelCnt], subID4 * 2 - 4] = dataRcv[3]; 
                            coil[slaveid[panelCnt], subID4 * 2 - 3] = dataRcv[4];  
                            break;
                    }
                    coilArray();
                }

                #endregion

                dataRcvCnt = 0;

                #region SORGU YOLLA                

                switch (plantType[panelCnt])
                {
                    case 1://O2
                        subID++;
                        if (subID == 3)
                        {
                            subID = 0;
                            COMErrCnt[panelCnt]++;
                            if (COMErrCnt[panelCnt] == 7)
                            {
                                COMErrCnt[panelCnt] = 6;
                                COMErr[panelCnt] = true;
                            }
                            else
                            {
                                COMErr[panelCnt] = false;
                            }
                            panelCnt++;
                        }
                        switch (subID)
                        {
                            case 1:
                                readHolReg(slaveid[panelCnt], 3, 9); //3. hr den itibaren 9 hr okur.
                                subID11 = 1;
                                break;
                            case 2:
                                readCoil(slaveid[panelCnt], 1, 16);                                
                                break;
                        }
                        break;
                    case 2://VAC
                        subID++;
                        if (subID == 7)
                        {
                            subID = 0;
                            COMErrCnt[panelCnt]++;
                            if (COMErrCnt[panelCnt] == 7)
                            {
                                COMErrCnt[panelCnt] = 6;
                                COMErr[panelCnt] = true;
                            }
                            else
                            {
                                COMErr[panelCnt] = false;
                            }
                            panelCnt++;
                        }
                        switch (subID)
                        {
                            case 1:
                                readHolReg(slaveid[panelCnt], 3, 9);
                                subID22 = 1;
                                break;
                            case 2:
                                readHolReg(slaveid[panelCnt], 12, 9);
                                subID22 = 2;
                                break;
                            case 3:
                                readHolReg(slaveid[panelCnt], 21, 9);
                                subID22 = 3;
                                break;
                            case 4:
                                readHolReg(slaveid[panelCnt], 30, 9);
                                subID22 = 4;
                                break;
                            case 5:
                                readCoil(slaveid[panelCnt], 1, 16);
                                subID2 = 5;
                                break;
                            case 6:
                                readCoil(slaveid[panelCnt], 17, 16);
                                subID2 = 6;
                                break;
                        }
                        break;
                    case 3://AIR
                        subID++;
                        if (subID == 9)
                        {
                            subID = 0;
                            COMErrCnt[panelCnt]++;
                            if (COMErrCnt[panelCnt] == 7)
                            {
                                COMErrCnt[panelCnt] = 6;
                                COMErr[panelCnt] = true;
                            }
                            else
                            {
                                COMErr[panelCnt] = false;
                            }
                            panelCnt++;
                        }
                        switch (subID)
                        {
                            case 1:
                                readHolReg(slaveid[panelCnt], 3, 9);
                                subID33 = 1;
                                break;
                            case 2:
                                readHolReg(slaveid[panelCnt], 12, 9);
                                subID33 = 2;
                                break;
                            case 3:
                                readHolReg(slaveid[panelCnt], 21, 9);
                                subID33 = 3;
                                break;
                            case 4:
                                readHolReg(slaveid[panelCnt], 30, 9);
                                subID33 = 4;
                                break;
                            case 5:
                                readHolReg(slaveid[panelCnt], 39, 9);
                                subID33 = 5;
                                break;
                            case 6:
                                readCoil(slaveid[panelCnt], 1, 16);
                                subID3 = 6;
                                break;
                            case 7:
                                readCoil(slaveid[panelCnt], 17, 16);
                                subID3 = 7;
                                break;
                            case 8:
                                readCoil(slaveid[panelCnt], 33, 16);
                                subID3 = 8;
                                break;
                        }
                        break;

                    case 4://likit
                        readHolReg(slaveid[panelCnt], 1, 7); //1 HR OKUR.
                       
                        COMErrCntLKD[slaveid[panelCnt]]++;
                        if (COMErrCntLKD[slaveid[panelCnt]] == 15)
                        {
                            COMErrCntLKD[slaveid[panelCnt]] = 14;
                            COMErrLKD[slaveid[panelCnt]] = true;
                        }
                        else
                        {
                            COMErrLKD[slaveid[panelCnt]] = false;
                        }
                     
                        panelCnt++;
                        break;

                    case 5://AGSS
                        subID++;
                        if (subID == 8)
                        {
                            subID = 0;
                            COMErrCnt[panelCnt]++;
                            if (COMErrCnt[panelCnt] == 7)
                            {
                                COMErrCnt[panelCnt] = 6;
                                COMErr[panelCnt] = true;
                            }
                            else
                            {
                                COMErr[panelCnt] = false;
                            }
                            panelCnt++;
                        }
                        switch (subID)
                        {
                            case 1:
                                readHolReg(slaveid[panelCnt], 3, 9);
                                subID44 = 1;
                                break;                           
                            case 2:
                                readCoil(slaveid[panelCnt], 1, 16);
                                subID4 = 2;
                                break;
                            case 3:
                                readCoil(slaveid[panelCnt], 17, 16);
                                subID4 = 3;
                                break;                        
                            case 4:
                                readCoil(slaveid[panelCnt], 33, 16);
                                subID4 = 4;
                                break;
                            case 5:
                                readCoil(slaveid[panelCnt], 49, 16);
                                subID4 = 5;
                                break;
                            case 6:
                                readCoil(slaveid[panelCnt], 65, 16);
                                subID4 = 6;
                                break;
                            case 7:
                                readCoil(slaveid[panelCnt], 81, 16);
                                subID4 = 7;
                                break;
                        }
                        break;

                }
                            
                #endregion

                if (panelCnt == Class.Database.plantCount) panelCnt = 0; //kayıt sayısının 1 eksiğine kadar artar.
                readEnd:
                try
                {
                    _serialPort.DiscardOutBuffer();
                }
                catch { }

                Thread.Sleep(400);
            }         
        }

        #region HR VE COIL DATALARINI DİZİYE YAZ
        public static void holdingRegArray()
        {

            switch (plantType[panelCnt])
            {
                case 1://O2
                    
                    valReserveLowO2[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 0];
                    valChangeCylinderO2[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 1];
                    rampaO2[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 2];
                    birimO2[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 3];
                    valLinePressLowO2[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 4];
                    valLinePressHighO2[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 5];
                    cylinderPressLeftO2[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 6];
                    cylinderPressRightO2[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 7];
                    linePressO2[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 8];
                    break;

                case 2://VAC
                    //degisken = 10;
                    vakumTankVAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 0];
                    vakumHatVAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 1];
                    vakumStartVAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 2];
                    vakumStopVAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 3];
                    startTimeP2VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 4];
                    startTimeP3VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 5];
                    startTimeP4VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 6];
                    startTimeAlarmVAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 7];
                    StatPumpVAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 8];//pompa sayısı

                    esYaslanmaGunVAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 9];
                    esYasDutyVAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 10];
                    saatPompa1VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 11];
                    dakikaPompa1VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 12];
                    saatPompa2VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 13];
                    dakikaPompa2VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 14];
                    saatPompa3VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 15];
                    dakikaPompa3VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 16];
                    saatPompa4VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 17];

                    dakikaPompa4VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 18];
                    StatP1VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 19];
                    StatP2VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 20];
                    StatP3VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 21];
                    StatP4VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 22];
                    pompaSicaklik1VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 23];
                    pompaSicaklik2VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 24];
                    pompaSicaklik3VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 25];
                    pompaSicaklik4VAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 26];

                    pompaSicaklikMaxVAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 27];
                    pompaSicaklikMinVAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 28];
                    sicaklikOlcumVAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 30];
                    birimVAC[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 31];
                    break;

                case 3://AIR
                    basıncTankAIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 0];
                    basıncHatAIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 1];
                    basıncStartAIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 2];
                    basıncStopAIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 3];
                    startTimeP2AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 4];
                    startTimeP3AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 5];
                    startTimeP4AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 6];
                    startTimeAlarmAIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 7];
                    StatPumpAIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 8];//pompa sayısı

                    esYaslanmaGunAIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 9];
                    esYasDutyAIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 10];
                    saatPompa1AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 11];
                    dakikaPompa1AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 12];
                    saatPompa2AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 13];
                    dakikaPompa2AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 14];
                    saatPompa3AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 15];
                    dakikaPompa3AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 16];
                    saatPompa4AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 17];

                    dakikaPompa4AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 18];
                    StatP1AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 19];
                    StatP2AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 20];
                    StatP3AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 21];
                    StatP4AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 22];
                    pompaSicaklik1AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 23];
                    pompaSicaklik2AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 24];
                    pompaSicaklik3AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 25];
                    pompaSicaklik4AIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 26];

                    pompaSicaklikMaxAIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 27];
                    pompaSicaklikMinAIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 28];

                    birimAIR[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 29];
                    /*dewTemp[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 30];
                    dewHum[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 31];*/

                    dewPoint[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 32];
                    dutyDryTime[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 33];
                    drayerGrup[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 34];
                    statDrayer1[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 35];
                    statDrayer2[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 36];
                    statDrayer3[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 37];
                    statDrayer4[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 38];
                    vakumLinePresFault[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 39];
                    break;

                case 4:
                    tankLevel[slaveid[panelCnt], 0] = holdingRegister[slaveid[panelCnt], 0];
                    tankLevel[slaveid[panelCnt], 1] = holdingRegister[slaveid[panelCnt], 1];
                    tankLevel[slaveid[panelCnt], 2] = holdingRegister[slaveid[panelCnt], 2];
                    tankLevel[slaveid[panelCnt], 3] = holdingRegister[slaveid[panelCnt], 3];
                    tankLevel[slaveid[panelCnt], 4] = holdingRegister[slaveid[panelCnt], 4];
                    break;  

                case 5://AGSS
                    vakumAGSS[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 0];
                    saatPompa1AGSS[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 1];
                    saatPompa2AGSS[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 2];
                    StatP1AGSS[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt],3];
                    StatP2AGSS[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt],4];
                    
                    dakikaPompa1AGSS[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 6];
                    
                    dakikaPompa2AGSS[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 8];
                    birimAGSS[slaveid[panelCnt]] = holdingRegister[slaveid[panelCnt], 9];
                    break;
            }

            
        }
        public static void coilArray()
        {
            switch (plantType[panelCnt])
            {
                case 1:
                    if ((coil[slaveid[panelCnt], 0] & 1) == 1) inDutyLeftO2[slaveid[panelCnt]] = true;
                    else inDutyLeftO2[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 2) == 2) inDutyRightO2[slaveid[panelCnt]] = true;
                    else inDutyRightO2[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 4) == 4) reserveLowLeftO2[slaveid[panelCnt]] = true;
                    else reserveLowLeftO2[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 8) == 8) reserveLowRightO2[slaveid[panelCnt]] = true;
                    else reserveLowRightO2[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 16) == 16) changeCylinderLeftO2[slaveid[panelCnt]] = true;
                    else changeCylinderLeftO2[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 32) == 32) changeCylinderRightO2[slaveid[panelCnt]] = true;
                    else changeCylinderRightO2[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 64) == 64) changeImmediatelyLeftO2[slaveid[panelCnt]] = true;
                    else changeImmediatelyLeftO2[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 128) == 128) changeImmediatelyRightO2[slaveid[panelCnt]] = true;
                    else changeImmediatelyRightO2[slaveid[panelCnt]] = false;

                    if ((coil[slaveid[panelCnt], 1] & 1) == 1) normalO2[slaveid[panelCnt]] = true;
                    else normalO2[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 2) == 2) linePressLowO2[slaveid[panelCnt]] = true;
                    else linePressLowO2[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 4) == 4) linePressHighO2[slaveid[panelCnt]] = true;
                    else linePressHighO2[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 8) == 8) pressureFaultO2[slaveid[panelCnt]] = true;
                    else pressureFaultO2[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 16) == 16) plantFaultLeftO2[slaveid[panelCnt]] = true;
                    else plantFaultLeftO2[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 32) == 32) plantFaultRightO2[slaveid[panelCnt]] = true;
                    else plantFaultRightO2[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 64) == 64) alarmO2[slaveid[panelCnt]] = true;
                    else alarmO2[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 128) == 128) muteO2[slaveid[panelCnt]] = true;
                    else muteO2[slaveid[panelCnt]] = false;
                    break;

                case 3:
                    if ((coil[slaveid[panelCnt], 0] & 1) == 1) pumpDuty1AIR[panelCnt] = true;
                    else pumpDuty1AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 2) == 2) pumpDuty2AIR[panelCnt] = true;
                    else pumpDuty2AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 4) == 4) pumpDuty3AIR[panelCnt] = true;
                    else pumpDuty3AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 8) == 8) pumpDuty4AIR[panelCnt] = true;
                    else pumpDuty4AIR[slaveid[panelCnt]] = false;
                    if ((coil[panelCnt, 0] & 16) == 16) pumpEn1AIR[panelCnt] = true;
                    else pumpEn1AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 32) == 32) pumpEn2AIR[panelCnt] = true;
                    else pumpEn2AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 64) == 64) pumpEn3AIR[panelCnt] = true;
                    else pumpEn3AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 128) == 128) pumpEn4AIR[panelCnt] = true;
                    else pumpEn4AIR[slaveid[panelCnt]] = false;

                    if ((coil[slaveid[panelCnt], 1] & 1) == 1) faultTermik1AIR[slaveid[panelCnt]] = true;
                    else faultTermik1AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 2) == 2) faultTermik2AIR[slaveid[panelCnt]] = true;
                    else faultTermik2AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 4) == 4) faultTermik3AIR[slaveid[panelCnt]] = true;
                    else faultTermik3AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 8) == 8) faultTermik4AIR[slaveid[panelCnt]] = true;
                    else faultTermik4AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 16) == 16) faultSicaklik1AIR[slaveid[panelCnt]] = true;
                    else faultSicaklik1AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 32) == 32) faultSicaklik2AIR[slaveid[panelCnt]] = true;
                    else faultSicaklik2AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 64) == 64) faultSicaklik3AIR[slaveid[panelCnt]] = true;
                    else faultSicaklik3AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 128) == 128) faultSicaklik4AIR[slaveid[panelCnt]] = true;
                    else faultSicaklik4AIR[slaveid[panelCnt]] = false;

                    if ((coil[slaveid[panelCnt], 2] & 1) == 1) faultKontaktor1AIR[slaveid[panelCnt]] = true;
                    else faultKontaktor1AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 2] & 2) == 2) faultKontaktor2AIR[slaveid[panelCnt]] = true;
                    else faultKontaktor2AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 2] & 4) == 4) faultKontaktor3AIR[slaveid[panelCnt]] = true;
                    else faultKontaktor3AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 2] & 8) == 8) faultKontaktor4AIR[slaveid[panelCnt]] = true;
                    else faultKontaktor4AIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 2] & 16) == 16) faultPlantEmergencyAIR[slaveid[panelCnt]] = true;
                    else faultPlantEmergencyAIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 2] & 32) == 32) faultPlantPressureAIR[slaveid[panelCnt]] = true;//line press fault
                    else faultPlantPressureAIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 2] & 64) == 64) faultPlantAIR[slaveid[panelCnt]] = true;
                    else faultPlantAIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 2] & 128) == 128) faultDewPointAIR[slaveid[panelCnt]] = true;
                    else faultDewPointAIR[slaveid[panelCnt]] = false;

                    if ((coil[slaveid[panelCnt], 3] & 1) == 1) faultPlantTimeAIR[slaveid[panelCnt]] = true;
                    else faultPlantTimeAIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 3] & 2) == 2) faultSensorTankAIR[slaveid[panelCnt]] = true;
                    else faultSensorTankAIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 3] & 4) == 4) faultSensorHatAIR[slaveid[panelCnt]] = true;
                    else faultSensorHatAIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 3] & 8) == 8) AlarmAIR[slaveid[panelCnt]] = true;
                    else AlarmAIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 3] & 16) == 16) MuteAIR[slaveid[panelCnt]] = true; 
                    else MuteAIR[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 3] & 32) == 32) dutyDry1[slaveid[panelCnt]] = true;
                    else dutyDry1[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 3] & 64) == 64) dutyDry2[slaveid[panelCnt]] = true;
                    else dutyDry2[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 3] & 128) == 128) dutyDry3[slaveid[panelCnt]] = true;
                    else dutyDry3[slaveid[panelCnt]] = false;

                    if ((coil[slaveid[panelCnt], 4] & 1) == 1) dutyDry4[slaveid[panelCnt]] = true;
                    else dutyDry4[slaveid[panelCnt]] = false;
                    break;

                case 2:
                    if ((coil[slaveid[panelCnt], 0] & 1) == 1) pumpDuty1VAC[slaveid[panelCnt]] = true;
                    else pumpDuty1VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 2) == 2) pumpDuty2VAC[slaveid[panelCnt]] = true;
                    else pumpDuty2VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 4) == 4) pumpDuty3VAC[slaveid[panelCnt]] = true;
                    else pumpDuty3VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 8) == 8) pumpDuty4VAC[slaveid[panelCnt]] = true;
                    else pumpDuty4VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 16) == 16) pumpEn1VAC[slaveid[panelCnt]] = true;
                    else pumpEn1VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 32) == 32) pumpEn2VAC[slaveid[panelCnt]] = true;
                    else pumpEn2VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 64) == 64) pumpEn3VAC[slaveid[panelCnt]] = true;
                    else pumpEn3VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 0] & 128) == 128) pumpEn4VAC[slaveid[panelCnt]] = true;
                    else pumpEn4VAC[slaveid[panelCnt]] = false;

                    if ((coil[slaveid[panelCnt], 1] & 1) == 1) faultTermik1VAC[slaveid[panelCnt]] = true;
                    else faultTermik1VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 2) == 2) faultTermik2VAC[slaveid[panelCnt]] = true;
                    else faultTermik2VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 4) == 4) faultTermik3VAC[slaveid[panelCnt]] = true;
                    else faultTermik3VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 8) == 8) faultTermik4VAC[slaveid[panelCnt]] = true;
                    else faultTermik4VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 16) == 16) faultSicaklik1VAC[slaveid[panelCnt]] = true;
                    else faultSicaklik1VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 32) == 32) faultSicaklik2VAC[slaveid[panelCnt]] = true;
                    else faultSicaklik2VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 64) == 64) faultSicaklik3VAC[slaveid[panelCnt]] = true;
                    else faultSicaklik3VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 1] & 128) == 128) faultSicaklik4VAC[slaveid[panelCnt]] = true;
                    else faultSicaklik4VAC[slaveid[panelCnt]] = false;

                    if ((coil[slaveid[panelCnt], 2] & 1) == 1) faultKontaktor1VAC[slaveid[panelCnt]] = true;
                    else faultKontaktor1VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 2] & 2) == 2) faultKontaktor2VAC[slaveid[panelCnt]] = true;
                    else faultKontaktor2VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 2] & 4) == 4) faultKontaktor3VAC[slaveid[panelCnt]] = true;
                    else faultKontaktor3VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 2] & 8) == 8) faultKontaktor4VAC[slaveid[panelCnt]] = true;
                    else faultKontaktor4VAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 2] & 16) == 16) faultPlantEmergencyVAC[slaveid[panelCnt]] = true;
                    else faultPlantEmergencyVAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 2] & 32) == 32) faultPlantPressureVAC[slaveid[panelCnt]] = true;//line press fault
                    else faultPlantPressureVAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 2] & 64) == 64) faultPlantVAC[slaveid[panelCnt]] = true;
                    else faultPlantVAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 2] & 128) == 128) faultFilterVAC[slaveid[panelCnt]] = true;
                    else faultFilterVAC[slaveid[panelCnt]] = false;

                    if ((coil[slaveid[panelCnt], 3] & 1) == 1) faultPlantTimeVAC[slaveid[panelCnt]] = true;
                    else faultPlantTimeVAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 3] & 2) == 2) faultSensorTankVAC[slaveid[panelCnt]] = true;
                    else faultSensorTankVAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 3] & 4) == 4) faultSensorHatVAC[slaveid[panelCnt]] = true;
                    else faultSensorHatVAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 3] & 8) == 8) AlarmVAC[slaveid[panelCnt]] = true;
                    else AlarmVAC[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 3] & 16) == 16) MuteVAC[slaveid[panelCnt]] = true;
                    else MuteVAC[slaveid[panelCnt]] = false;
                    break;
                      
                case 5:  
                    
                    if ((coil[slaveid[panelCnt], 0] & 2) == 2) ButtonAGSS[slaveid[panelCnt],1] = true;
                    else ButtonAGSS[slaveid[panelCnt],1] = false;
                    if ((coil[slaveid[panelCnt], 0] & 4) == 4) ButtonAGSS[slaveid[panelCnt],2] = true;
                    else ButtonAGSS[slaveid[panelCnt],2] = false;
                    if ((coil[slaveid[panelCnt], 0] & 8) == 8) ButtonAGSS[slaveid[panelCnt],3] = true;
                    else ButtonAGSS[slaveid[panelCnt],3] = false;
                    if ((coil[slaveid[panelCnt], 0] & 16) == 16) ButtonAGSS[slaveid[panelCnt],4] = true;
                    else ButtonAGSS[slaveid[panelCnt],4] = false;
                    if ((coil[slaveid[panelCnt], 0] & 32) == 32) ButtonAGSS[slaveid[panelCnt],5] = true;
                    else ButtonAGSS[slaveid[panelCnt],5] = false;
                    if ((coil[slaveid[panelCnt], 0] & 64) == 64) ButtonAGSS[slaveid[panelCnt],6] = true;
                    else ButtonAGSS[slaveid[panelCnt],6] = false;
                    if ((coil[slaveid[panelCnt], 0] & 128) == 128) ButtonAGSS[slaveid[panelCnt],7] = true;
                    else ButtonAGSS[slaveid[panelCnt],7] = false;


                    if ((coil[slaveid[panelCnt], 1] & 1) == 1) ButtonAGSS[slaveid[panelCnt],8] = true;
                    else ButtonAGSS[slaveid[panelCnt],8] = false;
                    if ((coil[slaveid[panelCnt], 1] & 2) == 2) ButtonAGSS[slaveid[panelCnt],9] = true;
                    else ButtonAGSS[slaveid[panelCnt],9] = false;
                    if ((coil[slaveid[panelCnt], 1] & 4) == 4) ButtonAGSS[slaveid[panelCnt],10] = true;
                    else ButtonAGSS[slaveid[panelCnt],10] = false;
                    if ((coil[slaveid[panelCnt], 1] & 8) == 8) ButtonAGSS[slaveid[panelCnt],11] = true;
                    else ButtonAGSS[slaveid[panelCnt],11] = false;
                    if ((coil[slaveid[panelCnt], 1] & 16) == 16) ButtonAGSS[slaveid[panelCnt],12] = true;
                    else ButtonAGSS[slaveid[panelCnt],12] = false;
                    if ((coil[slaveid[panelCnt], 1] & 32) == 32) ButtonAGSS[slaveid[panelCnt],13] = true;
                    else ButtonAGSS[slaveid[panelCnt],13] = false;
                    if ((coil[slaveid[panelCnt], 1] & 64) == 64) ButtonAGSS[slaveid[panelCnt],14] = true;
                    else ButtonAGSS[slaveid[panelCnt],14] = false;
                    if ((coil[slaveid[panelCnt], 1] & 128) == 128) ButtonAGSS[slaveid[panelCnt],15] = true;
                    else ButtonAGSS[slaveid[panelCnt],15] = false;


                    if ((coil[slaveid[panelCnt], 2] & 1) == 1) ButtonAGSS[slaveid[panelCnt],16] = true;
                    else ButtonAGSS[slaveid[panelCnt],16] = false;
                    if ((coil[slaveid[panelCnt], 2] & 2) == 2) ButtonAGSS[slaveid[panelCnt],17] = true;
                    else ButtonAGSS[slaveid[panelCnt],17] = false;
                    if ((coil[slaveid[panelCnt], 2] & 4) == 4) ButtonAGSS[slaveid[panelCnt],18] = true;
                    else ButtonAGSS[slaveid[panelCnt],18] = false;
                    if ((coil[slaveid[panelCnt], 2] & 8) == 8) ButtonAGSS[slaveid[panelCnt],19] = true;
                    else ButtonAGSS[slaveid[panelCnt],19] = false;
                    if ((coil[slaveid[panelCnt], 2] & 16) == 16) ButtonAGSS[slaveid[panelCnt],20] = true;
                    else ButtonAGSS[slaveid[panelCnt],20] = false;
                    if ((coil[slaveid[panelCnt], 2] & 32) == 32) ButtonAGSS[slaveid[panelCnt],21] = true;
                    else ButtonAGSS[slaveid[panelCnt],21] = false;
                    if ((coil[slaveid[panelCnt], 2] & 64) == 64) ButtonAGSS[slaveid[panelCnt],22] = true;
                    else ButtonAGSS[slaveid[panelCnt],22] = false;
                    if ((coil[slaveid[panelCnt], 2] & 128) == 128) ButtonAGSS[slaveid[panelCnt],23] = true;
                    else ButtonAGSS[slaveid[panelCnt],23] = false;

                    if ((coil[slaveid[panelCnt], 3] & 1) == 1) ButtonAGSS[slaveid[panelCnt],24] = true;
                    else ButtonAGSS[slaveid[panelCnt],24] = false;
                    if ((coil[slaveid[panelCnt], 3] & 2) == 2) ButtonAGSS[slaveid[panelCnt],25] = true;
                    else ButtonAGSS[slaveid[panelCnt],25] = false;
                    if ((coil[slaveid[panelCnt], 3] & 4) == 4) ButtonAGSS[slaveid[panelCnt],26] = true;
                    else ButtonAGSS[slaveid[panelCnt],26] = false;
                    if ((coil[slaveid[panelCnt], 3] & 8) == 8) ButtonAGSS[slaveid[panelCnt],27] = true;
                    else ButtonAGSS[slaveid[panelCnt],27] = false;
                    if ((coil[slaveid[panelCnt], 3] & 16) == 16) ButtonAGSS[slaveid[panelCnt],28] = true;
                    else ButtonAGSS[slaveid[panelCnt],28] = false;
                    if ((coil[slaveid[panelCnt], 3] & 32) == 32) ButtonAGSS[slaveid[panelCnt],29] = true;
                    else ButtonAGSS[slaveid[panelCnt],29] = false;
                    if ((coil[slaveid[panelCnt], 3] & 64) == 64) ButtonAGSS[slaveid[panelCnt],30] = true;
                    else ButtonAGSS[slaveid[panelCnt],30] = false;
                    if ((coil[slaveid[panelCnt], 3] & 128) == 128) ButtonAGSS[slaveid[panelCnt],31] = true; 
                    else ButtonAGSS[slaveid[panelCnt],31] = false;

                    if ((coil[slaveid[panelCnt], 4] & 1) == 1) ButtonAGSS[slaveid[panelCnt],32] = true;
                    else ButtonAGSS[slaveid[panelCnt],32] = false;
                    if ((coil[slaveid[panelCnt], 4] & 2) == 2) ButtonAGSS[slaveid[panelCnt],33] = true;
                    else ButtonAGSS[slaveid[panelCnt],33] = false;
                    if ((coil[slaveid[panelCnt], 4] & 4) == 4) ButtonAGSS[slaveid[panelCnt],34] = true;
                    else ButtonAGSS[slaveid[panelCnt],34] = false;
                    if ((coil[slaveid[panelCnt], 4] & 8) == 8) ButtonAGSS[slaveid[panelCnt],35] = true;
                    else ButtonAGSS[slaveid[panelCnt],35] = false;
                    if ((coil[slaveid[panelCnt], 4] & 16) == 16) ButtonAGSS[slaveid[panelCnt],36] = true;
                    else ButtonAGSS[slaveid[panelCnt],36] = false;
                    if ((coil[slaveid[panelCnt], 4] & 32) == 32) ButtonAGSS[slaveid[panelCnt],37] = true;
                    else ButtonAGSS[slaveid[panelCnt],37] = false;
                    if ((coil[slaveid[panelCnt], 4] & 64) == 64) ButtonAGSS[slaveid[panelCnt],38] = true;
                    else ButtonAGSS[slaveid[panelCnt],38] = false;
                    if ((coil[slaveid[panelCnt], 4] & 128) == 128) ButtonAGSS[slaveid[panelCnt],39] = true;
                    else ButtonAGSS[slaveid[panelCnt],39] = false;

                    if ((coil[slaveid[panelCnt], 5] & 1) == 1) ButtonAGSS[slaveid[panelCnt],40] = true;
                    else ButtonAGSS[slaveid[panelCnt],40] = false;
                    if ((coil[slaveid[panelCnt], 5] & 2) == 2) ButtonAGSS[slaveid[panelCnt],41] = true;
                    else ButtonAGSS[slaveid[panelCnt],41] = false;
                    if ((coil[slaveid[panelCnt], 5] & 4) == 4) ButtonAGSS[slaveid[panelCnt],42] = true;
                    else ButtonAGSS[slaveid[panelCnt],42] = false;
                    if ((coil[slaveid[panelCnt], 5] & 8) == 8) ButtonAGSS[slaveid[panelCnt],43] = true;
                    else ButtonAGSS[slaveid[panelCnt],43] = false;
                    if ((coil[slaveid[panelCnt], 5] & 16) == 16) ButtonAGSS[slaveid[panelCnt],44] = true;
                    else ButtonAGSS[slaveid[panelCnt],44] = false;
                    if ((coil[slaveid[panelCnt], 5] & 32) == 32) ButtonAGSS[slaveid[panelCnt],45] = true;
                    else ButtonAGSS[slaveid[panelCnt],45] = false;
                    if ((coil[slaveid[panelCnt], 5] & 64) == 64) ButtonAGSS[slaveid[panelCnt],46] = true;
                    else ButtonAGSS[slaveid[panelCnt],46] = false;
                    if ((coil[slaveid[panelCnt], 5] & 128) == 128) ButtonAGSS[slaveid[panelCnt],47] = true;
                    else ButtonAGSS[slaveid[panelCnt],47] = false;

                    if ((coil[slaveid[panelCnt], 6] & 1) == 1) ButtonAGSS[slaveid[panelCnt],48] = true;
                    else ButtonAGSS[slaveid[panelCnt],48] = false;
                    if ((coil[slaveid[panelCnt], 6] & 2) == 2) ButtonAGSS[slaveid[panelCnt],49] = true;
                    else ButtonAGSS[slaveid[panelCnt],49] = false;

                    if ((coil[slaveid[panelCnt], 6] & 4) == 4) pump1CutinAGSS[slaveid[panelCnt]] = true;
                    else pump1CutinAGSS[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 6] & 8) == 8) pump1CutOutAGSS[slaveid[panelCnt]] = true;
                    else pump1CutOutAGSS[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 6] & 16) == 16) pump2CutinAGSS[slaveid[panelCnt]] = true;
                    else pump2CutinAGSS[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 6] & 32) == 32) pump2CutOutAGSS[slaveid[panelCnt]] = true;
                    else pump2CutOutAGSS[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 6] & 64) == 64) contactFault1AGSS[slaveid[panelCnt]] = true;
                    else contactFault1AGSS[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 6] & 128) == 128) contactFault2AGSS[slaveid[panelCnt]] = true;
                    else contactFault2AGSS[slaveid[panelCnt]] = false;                   
             
                       
                    if ((coil[slaveid[panelCnt], 7] & 1) == 1) airflowAGSS[slaveid[panelCnt]] = true;
                    else airflowAGSS[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 7] & 2) == 2) faultPlantEmergencyAGSS[slaveid[panelCnt]] = true;//line press fault
                    else faultPlantEmergencyAGSS[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 7] & 4) == 4) faultPlantAGSS[slaveid[panelCnt]] = true;
                    else faultPlantAGSS[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 7] & 8) == 8) thermicFault1AGSS[slaveid[panelCnt]] = true;//line press fault
                    else thermicFault1AGSS[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 7] & 16) == 16) thermicFault2AGSS[slaveid[panelCnt]] = true;
                    else thermicFault2AGSS[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 7] & 32) == 32) Passive1AGSS[slaveid[panelCnt]] = true;//
                    else Passive1AGSS[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 7] & 64) == 64) Passive2AGSS[slaveid[panelCnt]] = true;
                    else Passive2AGSS[slaveid[panelCnt]] = false;
                    if ((coil[slaveid[panelCnt], 7] & 128) == 128) AlarmAGSS[slaveid[panelCnt]] = true;
                    else AlarmAGSS[slaveid[panelCnt]] = false;

                    if ((coil[slaveid[panelCnt], 8] & 1) == 1) MuteAGSS[slaveid[panelCnt]] = true;
                    else MuteAGSS[slaveid[panelCnt]] = false;
                    break;

                    
            }

        }
        #endregion

        #region MODBUS MASTER KOMUTLARI

        public static void readHolReg(int ID, int reg, int Lng)
        {
            reg--;
            dataSend[0] = Convert.ToByte(ID);
            dataSend[1] = 3;
            dataSend[2] = Convert.ToByte(reg / 256);
            dataSend[3] = Convert.ToByte(reg % 256);
            dataSend[4] = Convert.ToByte(Lng / 256);
            dataSend[5] = Convert.ToByte(Lng % 256);
            int crc = crc16.getCrc16(dataSend, 6);
            dataSend[6] = Convert.ToByte(crc / 256);
            dataSend[7] = Convert.ToByte(crc % 256);

            try
            {
                _serialPort.Write(dataSend, 0, 8);
            }
            catch { /*HataDurum = "Haberleşme Kesik";*/ }
        }
    /*    public static void writeHolReg(int ID, int reg, int data)
        {
            reg--;
            dataSend[0] = Convert.ToByte(ID);
            dataSend[1] = 6;
            dataSend[2] = Convert.ToByte(reg / 256);
            dataSend[3] = Convert.ToByte(reg % 256);
            dataSend[4] = Convert.ToByte(data / 256);
            dataSend[5] = Convert.ToByte(data % 256);
            int crc = crc16.getCrc16(dataSend, 6);
            dataSend[6] = Convert.ToByte(crc / 256);
            dataSend[7] = Convert.ToByte(crc % 256);

            try
            {
                _serialPort.Write(dataSend, 0, 8);
            }
            catch { /*HataDurum = "Haberleşme Kesik"; }
        }*/
        public static void readCoil(int ID, int reg, int lng)
        {
            reg--;
            dataSend[0] = Convert.ToByte(ID);
            dataSend[1] = 1;
            dataSend[2] = Convert.ToByte(reg / 256);
            dataSend[3] = Convert.ToByte(reg % 256);
            dataSend[4] = Convert.ToByte(lng / 256);
            dataSend[5] = Convert.ToByte(lng % 256);
            int crc = crc16.getCrc16(dataSend, 6);
            dataSend[6] = Convert.ToByte(crc / 256);
            dataSend[7] = Convert.ToByte(crc % 256);

            try
            {
                _serialPort.Write(dataSend, 0, 8);
            }
            catch {/* HataDurum = "Haberleşme Kesik";*/ }
        }
      /*  public static void writeCoil(int ID, int reg, bool data)
        {
            reg--;
            dataSend[0] = Convert.ToByte(ID);
            dataSend[1] = 5;
            dataSend[2] = Convert.ToByte(reg / 256);
            dataSend[3] = Convert.ToByte(reg % 256);
            if (data) dataSend[4] = 255;
            else dataSend[4] = 0;
            dataSend[5] = 0;
            int crc = crc16.getCrc16(dataSend, 6);
            dataSend[6] = Convert.ToByte(crc / 256);
            dataSend[7] = Convert.ToByte(crc % 256);

            try
            {
                _serialPort.Write(dataSend, 0, 8);
            }
            catch { /*HataDurum = "Haberleşme Kesik"; }
        }*/

        #endregion
    }

    public class crc16
    {

        static byte[] aCRCHi = new byte[256] {
              0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
              0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
              0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
              0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
              0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
              0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
              0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
              0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
              0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
              0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
              0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
              0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
              0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
              0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
              0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
              0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
              0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
              0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
              0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
              0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
              0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
              0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
              0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
              0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
              0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
              0x80, 0x41, 0x00, 0xC1, 0x81, 0x40};
          
        static byte[] aCRCLo = new byte[256] {
              0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06,
              0x07, 0xC7, 0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD,
              0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09,
              0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A,
              0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC, 0x14, 0xD4,
              0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
              0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3,
              0xF2, 0x32, 0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4,
              0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A,
              0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29,
              0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF, 0x2D, 0xED,
              0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
              0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60,
              0x61, 0xA1, 0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67,
              0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F,
              0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68,
              0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA, 0xBE, 0x7E,
              0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
              0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71,
              0x70, 0xB0, 0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92,
              0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C,
              0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B,
              0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89, 0x4B, 0x8B,
              0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
              0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42,
              0x43, 0x83, 0x41, 0x81, 0x80, 0x40 };

        public int getCrc16(byte[] msg, int len)
        {
            int CRCHi = 0xFF; // high CRC byte initialized
            int CRCLo = 0xFF; // low CRC byte initialized
            int index, ii = 0;        // will index into CRC lookup table

            while (len != 0)
            {
                index = (CRCHi ^ msg[ii]) & 0XFF;            // calculate the CRC
                CRCHi = (CRCLo ^ aCRCHi[index]) & 0XFF;
                CRCLo = aCRCLo[index] & 0XFF;
                len--;
                ii++;
            }

            return (CRCHi * 256 + CRCLo) & 0XFFFF;
        }
    }
}
