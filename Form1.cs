using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections.Specialized;

namespace IpseityLauncher
{
    public partial class Form1 : Form
    {
        private String stringIpseityRootFolder;
        private String stringPrologFolder;
        private Boolean boolIpseityFoundOnStart;
        private Boolean boolPrologFoundOnStart;
        private String stringCurrentDirectory;
           
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The goal of this method is to find automatically the 
        /// pathe needed by ipseity, it we don't find it, it will fallback
        /// to the classical way : asking the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                stringCurrentDirectory = Directory.GetCurrentDirectory();

                //detect if ipseity is in the correct folder (root folder is the same than the launcher)
                boolIpseityFoundOnStart = this.findIpseity();

                //Same here but for prolog in the program files folder
                boolPrologFoundOnStart = this.findProlog();


                //if prolog not found, try to find ?
                boolPrologFoundOnStart = this.findPrologRecursively();

                //if prolog not found, execute installer ?
                boolPrologFoundOnStart = this.installProlog();

                //if all is detected start ipseity
                if (boolPrologFoundOnStart && boolIpseityFoundOnStart)
                {
                    textBox_Status.Text = textBox_Status.Text + "Automatic Launch. ";
                    this.launch();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("The launcher does not have enough right to access the current directory" + ex.ToString());
            }
            catch (Exception generalEx)
            {
                MessageBox.Show("Something really bad happened an exception was raised" + generalEx.ToString());
            }
        }

        /// <summary>
        /// we try to install prolog
        /// </summary>
        /// <returns>if prolog is succesful in installing return true, false otherwise</returns>
        private bool installProlog()
        {
            if (this.askIfUserWantToInstallProlog())
            {
                if (!boolPrologFoundOnStart && File.Exists(stringCurrentDirectory + @"\software\SWIProlog5_6_49.exe"))
                {
                    Process process = Process.Start(stringCurrentDirectory + @"\software\SWIProlog5_6_49.exe");
                    process.WaitForExit();
                    process.Close();
                    textBox_Status.Text = textBox_Status.Text + "Prolog not found, try to install. ";
                    return this.findProlog();
                }
            }
            return false;
        }

        /// <summary>
        /// if not found on start in the program files path we ask the user if the program
        /// can search prolog from the root direcgtory of ipseity
        /// </summary>
        /// <returns>true of prolog found, false otherwise</returns>

        private bool findPrologRecursively()
        {
            String prologOnRecursiveSearch = null;
            if (!boolPrologFoundOnStart && boolIpseityFoundOnStart && this.askIfUserWantToSearchProlog())
            {
                prologOnRecursiveSearch = this.directorySearch(stringIpseityRootFolder, "plcon.exe");
                if (prologOnRecursiveSearch != null)
                {
                    //Hiding all that is unnecessary and put the correct path
                    textBox_PrologFolder.Text = prologOnRecursiveSearch;
                    label_PrologFolder.Visible = false;
                    textBox_PrologFolder.Visible = false;
                    textBox_Status.Text = textBox_Status.Text + "Prolog found. ";
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// try to find ipseity
        /// </summary>
        /// <returns>A bollean, true if ipseity is found</returns>
        Boolean findIpseity()
        {
            // Get the current directory in the case of the launcher present in the ipseity folder and
            //test the validity of the asumption.
            String stringCurrentDirectory = Directory.GetCurrentDirectory();
            if (File.Exists(stringCurrentDirectory + @"\app\IpseityProject\binary\1.2.2\win\Ipseity.exe"))
            {
                //Hiding all that is unnecessary and put the correct path
                textBox_RootFolder.Text = stringCurrentDirectory;
                label_IpseityRootFolder.Visible = false;
                textBox_RootFolder.Visible = false;
                textBox_Status.Text = textBox_Status.Text + "Ipseity found. ";
                return true;
            }
            return false;
        }

        /// <summary>
        /// Try to find prolog in the standart instalation folder.
        /// </summary>
        /// <returns>A boolean, true if prolog is found</returns>
        Boolean findProlog()
        {
            //Same here but for prolog in the program files folder
            String string32BitProgramFilesFolder = this.Get32bitProgramFiles();
            if (File.Exists(string32BitProgramFilesFolder + @"\pl\bin\plcon.exe"))
            {
                //Hiding all that is unnecessary and put the correct path
                textBox_PrologFolder.Text = string32BitProgramFilesFolder + @"\pl";
                label_PrologFolder.Visible = false;
                textBox_PrologFolder.Visible = false;
                textBox_Status.Text = textBox_Status.Text + "Prolog found. ";
                return true;
            }
            return false;
        }

        /// <summary>
        /// This method will get the path given by the user
        /// It will do some test to check the validity of the paths
        /// Then we make the adequate path and we run ipseity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void launch() 
        {
            //By security we desactivate te textbox while working
            textBox_RootFolder.Enabled = false;
            textBox_PrologFolder.Enabled = false;

            //We get the text of the textbox
            stringIpseityRootFolder = textBox_RootFolder.Text;
            stringPrologFolder = textBox_PrologFolder.Text;

            if (//Test if the path are correct by checking on of the essential file that must be contained
                File.Exists(stringIpseityRootFolder + @"\app\IpseityProject\binary\1.2.2\win\Ipseity.exe") &&
                File.Exists(stringPrologFolder + @"\bin\plcon.exe")
                )
            {
                //Concatenate all the path correctly without overwriting the content of the original
                Environment.SetEnvironmentVariable(
                                                    "PATH", Environment.GetEnvironmentVariable("PATH") +
                    /*The Lib to know :*/ stringIpseityRootFolder + @"\lib\win\Qt_Libraries" +
                                                    ";" +
                    /*prolog path :*/     stringPrologFolder + @"\bin" +
                                                    ";");
                //Launching ipseity
                //Process.Start(stringIpseityRootFolder + @"\app\IpseityProject\binary\1.2.2\win\Ipseity.exe");

                ProcessStartInfo processIpseity = new ProcessStartInfo(stringIpseityRootFolder + @"\app\IpseityProject\binary\1.2.2\win\Ipseity.exe");

                processIpseity.UseShellExecute = false; //mandatory if not false then process.start throw exception


                //working on dictionarry
                processIpseity.EnvironmentVariables.Remove("PATH"); //remove the previous path

                //get the local path, reinject it into the path variable and add the other path needed by ipseity
                processIpseity.EnvironmentVariables.Add(
                                                        "PATH",
                                                        Environment.GetEnvironmentVariable("PATH") +
                    /*The Lib to know :*/ stringIpseityRootFolder + @"\lib\win\Qt_Libraries" +
                                                        ";" +
                    /*prolog path :*/     stringPrologFolder + @"\bin" +
                                                        ";");

                //so that ipseity know where he is working
                processIpseity.WorkingDirectory = stringIpseityRootFolder + @"\app\IpseityProject\binary\1.2.2\win";
                
                //Inform the user :
                textBox_Status.Text = textBox_Status.Text + "Launching. ";

                //Finally launching
                Process.Start(processIpseity);
                

            }
            else
            {
                //Signal that the path are inccorect
                MessageBox.Show("The Ipseity folder or the prolog folder seems to be wrong \n");
            }
            //Get back to normal
            textBox_RootFolder.Enabled = true;
            textBox_PrologFolder.Enabled = true;
        }

        /// <summary>
        /// Try to launch ipseity on click of the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_LaunchIpseity_Click(object sender, EventArgs e)
        {
            this.launch();
        }


        /// <summary>
        /// The goal of this function is to get the path of the 
        /// 32bit Program Files Folder, whereas you are or not 
        /// in a 64bit os. Not so much usefull....
        /// </summary>
        /// <returns>A string refering to the places of the 32 Bit Program Files</returns>
        private String Get32bitProgramFiles()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            }
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles);
        }

        /// <summary>
        /// This function ask the user if he want the program to search automattically
        /// prolog
        /// </summary>
        /// <returns>If the user answer yes then it return true</returns>
        private Boolean askIfUserWantToSearchProlog()
        {
            String message = "Prolog not found in the classical path, do you want the software to try to find it automatically?";
            String caption = "Warning : Prolog not clearly find";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            return MessageBox.Show(message, caption, buttons)==System.Windows.Forms.DialogResult.Yes;
        }

        /// <summary>
        /// This function ask the user if he want to install prolog
        /// prolog
        /// </summary>
        /// <returns>If the user answer yes then it return true</returns>
        
        private Boolean askIfUserWantToInstallProlog()
        {
            //TODO : factorize it with askifuserwantto searchprolog
            String message = "Prolog not found, do you want to install it?";
            String caption = "Warning : Prolog not clearly find";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            return MessageBox.Show(message, caption, buttons) == System.Windows.Forms.DialogResult.Yes;
        }

        /// <summary>
        /// This fucntion search recursively fileToSearch from the root folder dirTosearch
        /// </summary>
        /// <param name="dirToSearch"></param>
        /// <param name="fileToSearch"></param>
        /// <returns>the path where the file is</returns>
        private String directorySearch(String dirToSearch, String fileToSearch)
        {
            try
            {
                foreach (String f in Directory.GetFiles(dirToSearch, fileToSearch))
                {
                    Console.WriteLine(fileToSearch + " found in " + dirToSearch);
                    return dirToSearch;
                }

                foreach (String d in Directory.GetDirectories(dirToSearch))
                {
                    foreach (String f in Directory.GetFiles(d, fileToSearch))
                    {
                        Console.WriteLine(fileToSearch + " found in " + d);
                        return d;
                    }
                    return this.directorySearch(d, fileToSearch);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Something Really Bad append :(" + e.Message);
            }
            return null;
        }

    }
}
