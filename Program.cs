﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control
    // Application Type: Console 
    // Description: S1 (Talent Show)
    // Author: Emily Crull
    // Dated Created: 10/2/2020
    // Last Modified:
    //
    // **************************************************

    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMainMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.Yellow;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMainMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();

            do
            {
                DisplayHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tg) Exit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        DisplayTalentShowMenuScreen(finchRobot);
                        break;

                    case "c":
                        DataRecorderDisplayMenuScreen(finchRobot);
                        break;

                    case "d":
                        AlarmSystemDisplayMenuScreen(finchRobot);
                        break;

                    case "e":
                        UserProgrammingDisplayMenuScreen(finchRobot);
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "g":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region TALENT SHOW MENU

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void DisplayTalentShowMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayHeader("The Talent Show");
                Console.WriteLine("");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("(This module is currently under development.)");
                Console.WriteLine("");
                Console.WriteLine();
                Console.WriteLine();
                DisplayContinuePrompt();
                Console.Clear();
                //
                // get user menu choice
                //
                DisplayHeader("Talent Show Menu");
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) Dance");
                Console.WriteLine("\tc) Mixing It Up");
                Console.WriteLine("\td) Return to Main Menu");
                //Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayLightAndSound(finchRobot);
                        break;

                    case "b":
                        Dance(finchRobot);
                        break;

                    case "c":
                        MixingItUp(finchRobot);
                        break;

                    case "d":
                        quitTalentShowMenu = true;
                        break;

                    //case "q":
                    //    quitTalentShowMenu = true;
                    //    break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }
        #endregion

        #region TALENT SHOW: LIGHT AND SOUND
        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayLightAndSound(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot will now show off its glowing talent and play a tune for you!");
            DisplayContinuePrompt();


            Parallel.Invoke( 
            () => ExploreLights(finchRobot), 
            () => Tune(finchRobot));

            DisplayMenuPrompt("Talent Show Menu");
        }

        #endregion

        #region TALENT SHOW: DANCE
        /// *****************************************************************
        /// *                          Talent Show > Dance                  *
        /// *****************************************************************
        /// <param name="finchRobot">finch robot object</param>
        static void Dance(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayHeader("Dance");

            Console.WriteLine("\tThe Finch robot will now boogie!");
           
            DisplayContinuePrompt();

            ExploreMovement(finchRobot);
            
            DisplayMenuPrompt("Talent Show Menu");
        }

        #endregion


        #region TALENT SHOW: MIXERS
        /// <summary>
        /// *****************************************************************
        /// *                   Talent Show > Mixing It Up                  *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void MixingItUp (Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayHeader("Mixing It Up");

            Console.WriteLine("\tThe Finch robot will now light up the room while it boogies to some tunes!");
            DisplayContinuePrompt();


            Parallel.Invoke(
            () => DanceTwo(finchRobot),
            () => ExploreLightsTwo(finchRobot),
            () => TuneTwo(finchRobot));

            DisplayMenuPrompt("Talent Show Menu");
        }

        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayHeader("Disconnecting Finch Robot");

            Console.WriteLine("\tPreparing to disconnect from the Finch robot.");
            finchRobot.disConnect();
            DisplayContinuePrompt();
            Console.Clear();
            Console.WriteLine("\tThe Finch robot is now disconnected.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayHeader("Connecting to the Finch Robot");

            Console.WriteLine("\tPreparing to connect to the Finch robot. Please be sure the USB cable is connected to both the robot and the computer.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();
            finchRobot.noteOn(300);
            finchRobot.wait(100);
            finchRobot.noteOn(450);
            finchRobot.wait(100);
            finchRobot.noteOn(600);
            finchRobot.wait(100);
            finchRobot.noteOn(750);
            finchRobot.wait(200);
            finchRobot.noteOff();
            Console.Clear();
            // TODO test connection and provide user feedback - text, lights, sounds

            Console.WriteLine();
            Console.WriteLine();
            DisplayMenuPrompt("Main Menu");
            Console.WriteLine();
            Console.WriteLine();

            //
            // reset finch robot
            //
            finchRobot.setLED(0, 0, 0);

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control S1: The Talent Show");
            Console.WriteLine();
            Console.WriteLine("This will feature portions of the application involving 'The Talent Show'");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for exploring what the Finch can do!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName}.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion

        #region TALENT SHOW: LIGHTS
        static void ExploreLights(Finch finchRobot)
        {
                for (int ledValue = 0; ledValue < 255; ledValue++)
                {
                finchRobot.setLED(ledValue, 0, 0);
                }
            
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(3600);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(4600);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(3000);
            for (int ledValue = 255; ledValue > 0; ledValue--)
            {
                finchRobot.setLED(ledValue, 0, 0);
            }


            //for (int ledValue = 255; ledValue > 40; ledValue--)
            //{
            //    finchRobot.setLED(ledValue, 0, 0);
            //}
            /*}*/
        }

            #endregion

        #region TALENT SHOW: DANCE
            static void ExploreMovement(Finch finchRobot)
            {
                //finchRobot.setMotors(150, 150);
                //finchRobot.wait(1000);
                //finchRobot.setMotors(255, 0);
                //finchRobot.wait(500);
                //finchRobot.wait(1000);
                //finchRobot.setMotors(0, 255);
                //finchRobot.wait(500);
                //finchRobot.setMotors(150, 150);
                //finchRobot.wait(1000);
                //finchRobot.setMotors(255, 0);
                //finchRobot.wait(500);
                //finchRobot.setMotors(150, 150);
                //finchRobot.wait(1000);
                //finchRobot.setMotors(0, 255);
                //finchRobot.wait(500);
                //finchRobot.setMotors(150, 150);
                //finchRobot.wait(1000);
                //finchRobot.setMotors(0, 0);


                finchRobot.setMotors(200, 125);
                finchRobot.wait(10000);
                finchRobot.setMotors(100, 62);
                finchRobot.wait(15000);
                finchRobot.setMotors(200, 125);
                finchRobot.wait(10000);
                finchRobot.setMotors(0, 0);

            }
            #endregion

        #region TALENT SHOW: DATA RECORDER
            static void DataRecorderDisplayMenuScreen(Finch finchRobot)
            {
                DisplayHeader("Data Recorder");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("\t(Currently under development.)");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                DisplayContinuePrompt();
            }
            #endregion

        #region TALENT SHOW: ALARM SYSTEM
            static void AlarmSystemDisplayMenuScreen(Finch finchRobot)
            {
                DisplayHeader("Alarm System");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("\t(Currently under development.)");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                DisplayContinuePrompt();
            }
            #endregion

        #region TALENT SHOW: USER PROGRAMMING
            static void UserProgrammingDisplayMenuScreen(Finch finchRobot)
            {
                DisplayHeader("User Programming");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("\t(Currently under development.)");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                DisplayContinuePrompt();
            }
        #endregion

        #region TALENT SHOW: TUNE 
        static void Tune(Finch finchRobot)
        {
            for (int frequency = 0; frequency < 300; frequency++)
            {
                finchRobot.noteOn(frequency);
                finchRobot.wait(2);
                finchRobot.noteOff();
            }

            for (int buzz = 0; buzz < 7; buzz++)
            {
                finchRobot.noteOn(50);
                finchRobot.wait(200);
                finchRobot.noteOff();

                finchRobot.noteOn(500);
                finchRobot.wait(200);
                finchRobot.noteOff();

                finchRobot.noteOn(20);
                finchRobot.wait(200);
                finchRobot.noteOff();
                finchRobot.wait(20);
            }
            for (int frequency = 0; frequency < 200; frequency++)
            {
                finchRobot.noteOn(frequency);
                finchRobot.wait(2);
                finchRobot.noteOff();
            }
            for (int buzz = 0; buzz < 4; buzz++)
            {
                finchRobot.noteOn(70);
                finchRobot.wait(200);
                finchRobot.noteOff();

                finchRobot.noteOn(700);
                finchRobot.wait(200);
                finchRobot.noteOff();

                finchRobot.noteOn(30);
                finchRobot.wait(200);
                finchRobot.noteOff();
                finchRobot.wait(20);
            }

            for (int buzz = 0; buzz < 5; buzz++)
            {
                finchRobot.noteOn(50);
                finchRobot.wait(200);
                finchRobot.noteOff();

                finchRobot.noteOn(500);
                finchRobot.wait(200);
                finchRobot.noteOff();

                finchRobot.noteOn(20);
                finchRobot.wait(200);
                finchRobot.noteOff();
                finchRobot.wait(20);
            }
                for (int frequency = 300; frequency > 0; frequency--)
                {
                    finchRobot.noteOn(frequency);
                    finchRobot.wait(2);
                    finchRobot.noteOff();
                }
        }
        #endregion




        #region TALENT SHOW: TUNE 2 
        static void TuneTwo(Finch finchRobot)
        {
            finchRobot.noteOn(600);
            finchRobot.wait(200);
            finchRobot.noteOff();

            finchRobot.noteOn(700);
            finchRobot.wait(200);
            finchRobot.noteOff();

            finchRobot.noteOn(800);
            finchRobot.wait(200);
            finchRobot.noteOff();

            finchRobot.noteOn(700);
            finchRobot.wait(200);
            finchRobot.noteOff();

            finchRobot.noteOn(600);
            finchRobot.wait(200);

            finchRobot.wait(3000);
           
            finchRobot.noteOn(600);
            finchRobot.wait(200);
            finchRobot.noteOff();

            finchRobot.noteOn(700);
            finchRobot.wait(200);
            finchRobot.noteOff();

            finchRobot.noteOn(800);
            finchRobot.wait(200);
            finchRobot.noteOff();

            finchRobot.noteOn(700);
            finchRobot.wait(200);
            finchRobot.noteOff();

            finchRobot.noteOn(600);
            finchRobot.wait(200);
            finchRobot.noteOff();

            finchRobot.wait(3000);

            finchRobot.noteOn(600);
            finchRobot.wait(200);
            finchRobot.noteOff();

            finchRobot.noteOn(700);
            finchRobot.wait(200);
            finchRobot.noteOff();

            finchRobot.noteOn(800);
            finchRobot.wait(200);
            finchRobot.noteOff();

            finchRobot.noteOn(700);
            finchRobot.wait(200);
            finchRobot.noteOff();

            finchRobot.noteOn(600);
            finchRobot.wait(500);
            finchRobot.noteOff();


            //for (int frequency = 1200; frequency < 1600; frequency++)
            //{
            //    finchRobot.noteOn(frequency);
            //    finchRobot.wait(1);
            //    finchRobot.noteOff();
            //}
        }
        #endregion



        #region TALENT SHOW: DANCE 2
        static void DanceTwo(Finch finchRobot)
        {


            finchRobot.setMotors(0, 0);
            finchRobot.wait(1000);
            
            finchRobot.setMotors(150, 150);
            finchRobot.wait(3000);

            finchRobot.setMotors(0, 0);
            finchRobot.wait(1000);

            finchRobot.setMotors(-150, -150);
            finchRobot.wait(3000);
            finchRobot.setMotors(0, 0);


            //finchRobot.setMotors(150, 150);
            //finchRobot.wait(1000);
            //finchRobot.setMotors(255, 0);
            //finchRobot.wait(500);
            //finchRobot.wait(1000);
            //finchRobot.setMotors(0, 255);
            //finchRobot.wait(500);
            //finchRobot.setMotors(150, 150);
            //finchRobot.wait(1000);
            //finchRobot.setMotors(255, 0);
            //finchRobot.wait(500);
            //finchRobot.setMotors(150, 150);
            //finchRobot.wait(1000);
            //finchRobot.setMotors(0, 255);
            //finchRobot.wait(500);
            //finchRobot.setMotors(150, 150);
            //finchRobot.wait(1000);
            //finchRobot.setMotors(0, 0);

        }
        #endregion


        #region TALENT SHOW: LIGHTS 2
        static void ExploreLightsTwo(Finch finchRobot)
        {
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(1000);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(3000);
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(1000);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(3000);
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(1000);
        }
        #endregion
    }

}