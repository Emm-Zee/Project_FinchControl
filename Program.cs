using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using FinchAPI;
using HidSharp.ReportDescriptors.Units;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control
    // Application Type: Console 
    // Description: S3 (Alarm System)
    // Author: Emily Crull
    // Dated Created: 10/2/2020
    // Last Modified: 11/7/2020
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

        #region MAIN MENU
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
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("\t\tEnter Choice: ");
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
                        LightAlarmSystemDisplayMenuScreen(finchRobot);
                        break;

                    case "e":
                        //UserProgrammingDisplayMenuScreen(finchRobot);
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

        #endregion

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
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
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


            Parallel.Invoke(
            () => ExploreLights(finchRobot),
            () => Tune(finchRobot));

            DisplayContinuePrompt();
            Console.Clear();

            DidYouLikeTheFinchSong(finchRobot);

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

            Console.WriteLine("\tWatch the Finch dance in circles for you!");

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
        static void MixingItUp(Finch finchRobot)
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
            Console.WriteLine();
            Console.WriteLine("\tThe Finch robot has been successfully disconnected.");
            Console.WriteLine();
            Console.WriteLine();
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
            Console.WriteLine();
            Console.WriteLine("\tPreparing to connect to the Finch robot.");
            Console.WriteLine();
            Console.WriteLine("\tPlease be sure the USB cable is connected to both the robot and the computer.");
            Console.WriteLine();
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
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control (S3: The Alarm System)");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\tThis application now has an alarm system feature.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
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
            Console.WriteLine("\t\tPress any key to continue.");
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
            finchRobot.wait(4000);
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

        #region DID YOU LIKE THE FINCH'S SONG

        static void DidYouLikeTheFinchSong(Finch finchRobot)

        {
            string userResponse;
            bool songLike = true;

            while (songLike)
            {
                System.Console.Write("Did you like the Finch's song and dance? ");
                userResponse = System.Console.ReadLine();

                if (userResponse == "yes")
                {
                    Console.WriteLine();
                    Console.WriteLine("Yay!");
                    Console.WriteLine();
                    Console.WriteLine("I'm glad you liked it.");
                    break;
                }
                else if (userResponse == "no")
                {
                    Console.WriteLine();
                    Console.WriteLine("Aww. Well, we are all entitled to our own opinions and like different music.");
                    break;
                }
                else
                {
                    Console.WriteLine();
                    System.Console.Write("I'm sorry, please answer the question more clearly by answering yes or no. ");
                }

            }
        }

        #endregion

        #region TEMPERATURE RECORDER MENU


        /// <summary>
        /// *****************************************************************
        /// *                     Temperature Recorder Menu                                 *
        /// *****************************************************************
        /// </summary>

        static void DataRecorderDisplayMenuScreen(Finch finchRobot)
        {

            int numberOfDataPoints = 0;
            double dataPointFrequency = 0;
            double[] temperatures = null;


            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;


            do
            {
                DisplayHeader("Temperature Recorder");
                Console.WriteLine("");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("");
                Console.WriteLine();
                Console.WriteLine();
                DisplayContinuePrompt();
                Console.Clear();
                //
                // get user menu choice
                //
                DisplayHeader("Temperature Recorder Menu");
                Console.WriteLine("\ta) Temperature Collection Number");
                Console.WriteLine("\tb) Temperature Collection Frequency");
                Console.WriteLine("\tc) Get Temperature Data");
                Console.WriteLine("\td) Show Temperature Data");
                Console.WriteLine("\tq) Main Menu");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("\t\tEnter Choice: ");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        dataPointFrequency = DataRecorderGetDataPointFrequency();
                        break;

                    case "c":
                        temperatures = DataRecorderDisplayGetNumberOfDataPoints(numberOfDataPoints, dataPointFrequency, finchRobot);
                        break;

                    case "d":
                        DataRecorderShowData(temperatures);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);

        }

        #endregion

        #region TEMPERATURE RECORDER: GET NUMBER OF DATA POINTS
        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            // get number of data points from user
            //returns the number of data points

            int numberOfDataPoints;
            string userResponse;

            DisplayHeader("Number of Data Points");

            Console.Write("\t Please enter how many times you would like to collect the temperature: ");
            userResponse = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("\t You have selected to collect the temperature {0} times", userResponse);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            DisplayContinuePrompt();
            Console.Clear();

            int.TryParse(userResponse, out numberOfDataPoints);
            //
            return numberOfDataPoints;
        }
        #endregion

        #region TEMPERATURE RECORDER: GET FREQUENCY POINTS
        static double DataRecorderGetDataPointFrequency()
        {
            // get number of frequency points from user
            //returns the number of frequency points

            double dataPointFrequency;
            string userResponse;
            double numericUserResponse;

            DisplayHeader("Data Point Frequency");

            Console.Write("\t In terms of seconds, enter how frequently you would like to collect the temperature: ");
            userResponse = Console.ReadLine();
            numericUserResponse = Convert.ToDouble(userResponse);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("\t You have selected to collect the temperature every {0} seconds", numericUserResponse);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            DisplayContinuePrompt();
            Console.Clear();

            double.TryParse(userResponse, out dataPointFrequency);
            //
            return dataPointFrequency;
        }

        #endregion

        #region TEMPERATURE RECORDER: GET DATA
        static double[] DataRecorderDisplayGetNumberOfDataPoints(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {

            double[] temperatures = new double[numberOfDataPoints];

            DisplayHeader("Get Data");

            Console.WriteLine($"\t Number of times the temperature is to be collected: {numberOfDataPoints}");
            Console.WriteLine();
            Console.WriteLine($"\t How often the temperature will be collected: {dataPointFrequency}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t The Finch robot is using these parameters to collect temperature data...");
            Console.WriteLine();
            for (int index = 0; index < numberOfDataPoints; index++)
            {

                temperatures[index] = (finchRobot.getTemperature() * 9 / 5 + 32);
                Console.WriteLine($"\t Reading {index + 1}: {temperatures[index].ToString("0\u00B0F")}");
                int waitInSeconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(waitInSeconds);
            }

            DisplayContinuePrompt();
            DisplayHeader("Table of Temperatures");

            Console.WriteLine();
            DataRecorderDisplayTable(temperatures);

            Console.WriteLine();
            DisplayContinuePrompt();

            return temperatures;

        }

        #endregion

        #region TEMPERATURE RECORDER: SHOW DATA
        static void DataRecorderShowData(double[] temperatures)
        {
            DisplayHeader("Temperature Results");

            //
            //display table header
            //

            DataRecorderDisplayTable(temperatures);

            DisplayContinuePrompt();

        }
        #endregion

        #region TEMPERATURE RECORDER DISPLAY TABLE

        static void DataRecorderDisplayTable(double[] temperatures)
        {
            Console.WriteLine(
                "Recording #".PadLeft(20) +
                "Temp".PadLeft(14)
                );
            Console.WriteLine(
                "-----------".PadLeft(20) +
                "----------".PadLeft(17)
                );

            //
            //display table data
            //

            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine(
                    (index + 1).ToString().PadLeft(15) +
                    temperatures[index].ToString("0\u00B0F").PadLeft(19)
                    );
            }
        }
        #endregion

        #region LIGHT ALARM SYSTEM MENU


        /// <summary>
        /// *****************************************************************
        /// *                      Light Alarm System Menu                        *
        /// *****************************************************************
        /// </summary>


        static void LightAlarmSystemDisplayMenuScreen(Finch finchRobot)
        {

            Console.CursorVisible = true;

            bool quitMenu = false;
            string lightAlarmMenuChoice;
            string sensorsToMonitor = "";
            string rangeType = "";
            int minMaxThreshold = 0;
            int timeToMonitor = 0;

            do
            {
                //
                // get user menu choice
                //
                DisplayHeader("The Light Alarm System Menu");
                Console.WriteLine();

                Console.WriteLine("\ta) Sensors to Monitor");
                Console.WriteLine("\tb) Range Type");
                Console.WriteLine("\tc) Set Minimum/Maximum Threshold Value");
                Console.WriteLine("\td) Set Time to Monitor");
                Console.WriteLine("\te) Set Alarm");
                Console.WriteLine("\tq) Main Menu");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("\t\tEnter Choice: ");
                lightAlarmMenuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (lightAlarmMenuChoice)
                {
                    case "a":
                        sensorsToMonitor = LightAlarmDisplaySetSensorsToMonitor(finchRobot);
                        break;

                    case "b":
                        rangeType = LightAlarmDisplaySetRangeType();
                        break;

                    case "c":
                        minMaxThreshold = LightAlarmSetMinMaxThresholdValue(rangeType, finchRobot);
                        break;

                    case "d":
                        timeToMonitor = LightAlarmSetTimeToMonitor();
                        break;

                    case "e":
                        LightAlarmSetAlarm(finchRobot, sensorsToMonitor, rangeType, minMaxThreshold, timeToMonitor);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);

        }
        #endregion

        #region LIGHT ALARM SYSTEM: LIGHT SENSORS TO MONITOR

        static string LightAlarmDisplaySetSensorsToMonitor(Finch finchRobot)
        {
            string sensorsToMonitor;
            DisplayHeader("Sensors To Monitor");

            Console.WriteLine();
            Console.Write("\tSensors to monitor (left, right, both): ");
            sensorsToMonitor = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine();
            if (sensorsToMonitor != "both")
            {
                Console.WriteLine("\tYou have selected to monitor the {0} sensor.", sensorsToMonitor);
                Console.WriteLine();
                CurrentLightSensorValue(finchRobot, sensorsToMonitor);
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\tYou have selected to monitor {0} sensors.", sensorsToMonitor);
                Console.WriteLine();
                CurrentLightSensorValue(finchRobot, sensorsToMonitor);
            };



            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            DisplayMenuPrompt("Light Alarm menu");

            return sensorsToMonitor;
        }

        #endregion

        #region LIGHT ALARM SYSTEM: CURRENT SENSOR VALUE

        static void CurrentLightSensorValue(Finch finchRobot, string sensorsToMonitor)
        {


            if (sensorsToMonitor == "left")
            {
                Console.WriteLine($"\tThe left light sensor's ambient value is currently {finchRobot.getLeftLightSensor()}.");
            }

            if (sensorsToMonitor == "right")
            {
                Console.WriteLine($"\tThe right light sensor's ambient value is currently {finchRobot.getRightLightSensor()}.");
            }

            if (sensorsToMonitor == "both")
            {
                Console.WriteLine($"\tThe left light sensor's ambient value is currently {finchRobot.getLeftLightSensor()} while the right light sensor's ambient value is {finchRobot.getRightLightSensor()}.");
            }
        }

        #endregion

        #region LIGHT ALARM SYSTEM: SET RANGE TYPE

        static string LightAlarmDisplaySetRangeType()
        {
            string rangeType;

            DisplayHeader("Set Range Type");

            Console.WriteLine();
            Console.Write("\tSet range type (minimum, maximum): ");
            rangeType = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\tYou have selected the {0} value.", rangeType);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            DisplayMenuPrompt("Light Alarm menu");

            return rangeType;
        }

        #endregion

        #region LIGHT ALARM SYSTEM : SET MIN/MAX THRESHOLD VALUE
        static int LightAlarmSetMinMaxThresholdValue(string rangeType, Finch finchRobot)
        {

            int minMaxThresholdValue;

            DisplayHeader("Minimum/Maximum Threshold Value");

            Console.WriteLine($"\tLeft light sensor ambient value: {finchRobot.getLeftLightSensor()}");
            Console.WriteLine($"\tRight light sensor ambient value: {finchRobot.getRightLightSensor()}");
            Console.WriteLine();
            Console.Write("\tSelect a {0} light sensor value: ", rangeType);
            int.TryParse(Console.ReadLine(), out minMaxThresholdValue);
            Console.WriteLine();
            Console.WriteLine("\tYou have chosen {0} as the {1} value.", minMaxThresholdValue, rangeType);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            DisplayMenuPrompt("Light Alarm menu");
            return minMaxThresholdValue;
        }

        #endregion

        #region LIGHT ALARM SYSTEM: SET TIME TO MONITOR

        static int LightAlarmSetTimeToMonitor()
        {

            int timeToMonitor;

            DisplayHeader("Set Time to Monitor");

            Console.Write("\t Enter the amount of time to monitor: ");
            int.TryParse(Console.ReadLine(), out timeToMonitor);
            Console.WriteLine();

            if (timeToMonitor == 1)
            {
                Console.WriteLine("\tYou have chosen to monitor for {0} second.", timeToMonitor);
            }
            else
            {
                Console.WriteLine("\tYou have chosen to monitor for {0} seconds.", timeToMonitor);
            };
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            DisplayMenuPrompt("Light Alarm menu");
            return timeToMonitor;
        }

        #endregion

        #region LIGHT ALARM SYSTEM: SET ALARM

        static void LightAlarmSetAlarm(
        Finch finchRobot,
        string sensorsToMonitor,
        string rangeType,
        int minMaxThreshold,
        int timeToMonitor)
        {

            int secondsElapsed = 0;
            bool thresholdExceeded = false;
            int currentLightSensorValue = 0;
            DisplayHeader("Set Alarm");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"\tSensors to monitor: " + sensorsToMonitor);
            Console.WriteLine($"\tRange Type: " + rangeType);
            Console.WriteLine($"\tMin/max threshold value: " + minMaxThreshold);
            Console.WriteLine($"\tMonitoring time: " + timeToMonitor);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue monitoring...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            DisplayHeader("Monitoring...(please wait)");
            Console.WriteLine();
            Console.WriteLine();

            while ((secondsElapsed < timeToMonitor) && !thresholdExceeded)
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(3, 15);
                Console.WriteLine($"Time Elapsed: " + (secondsElapsed + 1));
                DisplayLightLevelInFixedLocation(finchRobot);




                switch (sensorsToMonitor)
                {
                    case "left":
                        currentLightSensorValue = finchRobot.getLeftLightSensor();
                        break;

                    case "right":
                        currentLightSensorValue = finchRobot.getRightLightSensor();
                        break;

                    case "both":
                        currentLightSensorValue = (finchRobot.getLeftLightSensor() + finchRobot.getRightLightSensor()) / 2;
                        break;

                }


                switch (rangeType)
                {
                    case "minimum":
                        if (currentLightSensorValue < minMaxThreshold)
                        {
                            thresholdExceeded = true;
                            finchRobot.noteOn(1000);
                        }
                        break;

                    case "maximum":
                        if (currentLightSensorValue > minMaxThreshold)
                        {
                            thresholdExceeded = true;
                            finchRobot.noteOn(1000);

                        }
                        break;

                }

                finchRobot.wait(1000);
                secondsElapsed++;
            }


            Console.Clear();


            DisplayHeader("Results");
            Console.WriteLine();
            Console.WriteLine();
            if (thresholdExceeded)
            {
                finchRobot.noteOff();
                Console.WriteLine($"\tThe {rangeType} threshold value was exceeded by the current light sensor value of {currentLightSensorValue}.");
            }

            else
            {
                Console.WriteLine($"\tThe {rangeType} threshold value of {minMaxThreshold} was not exceeded by the current light sensor value of {currentLightSensorValue}.");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            DisplayMenuPrompt("The Light Alarm Menu");
        }

        #endregion

        #region LIGHT ALARM SYSTEM: DISPLAY LIGHT LEVEL IN FIXED LOCATION

        static void DisplayLightLevelInFixedLocation(Finch finchRobot)
        {
            int lightLevel;

            lightLevel = (finchRobot.getLeftLightSensor() + finchRobot.getRightLightSensor()) / 2;
            Console.SetCursorPosition(3, 16);
            Console.WriteLine("Current light level: {0}", lightLevel);
        }

        #endregion

        #region TALENT SHOW: USER PROGRAMMING
        static void userprogrammingdisplaymenuscreen(Finch finchRobot)
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

    }
}