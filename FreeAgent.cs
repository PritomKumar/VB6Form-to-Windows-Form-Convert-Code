using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace vb6Convert
{
    class FreeAgent
    {
        public static string designerContent = "";
        public static string csContent = "";
        public void Work()
        {

            var basePath = "F:\\Therapie Plus Project\\TherapiePlusNewWorking\\TherapiePlus.Net\\TherapiePlus\\UI\\";
            var desingnerExtention = ".Designer.cs";
            var csExtension = ".cs";



            var formName = "frmXInternetUpdate";

            var desingerFilePath = basePath + formName + desingnerExtention;
            var csFilePath = basePath + formName + csExtension; ;

            //ReplaceInFile("F:\\C# Tutorial Practice\\TutorialApplication\\Program.cs");    
            //ReplaceInFile("F:\\Therapie Plus Project\\TherapiePlus.Net\\TherapiePlus\\UI\\frmRgEinzahlungBuchen.Designer.cs");

            var FILE_PATH =
                @"F:\Therapie Plus Project\TherapiePlusNewWorking\TherapiePlus.Net\TherapiePlus\UI\frmSTBehand.cs";


            FixInFile(FILE_PATH);

        }



        public void FixInFile(string filePath)
        {

            csContent = string.Empty;
            string line = string.Empty;
            using (StreamReader reader = new StreamReader(filePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.TrimStart();
                    if ((
                        line.Contains(@";"))
                        && !line.Contains(@"//"))
                    {
                        //line = line.TrimStart();
                        //line = line + @"//" ;

                    }
                    if (!line.StartsWith(@"//") && line.Contains(@"frmWarten.DefInstance.lbl"))
                    {
                        //line = line.TrimStart();
                        line = "\n//TODO:Label\n" + "//" + line;

                    }

                    //if (!line.StartsWith(@"//") && line.Contains(@"VB6Helpers.Unload(frmDruckSuche.DefInstance);"))
                    //{
                    //    line = line.Replace(@"VB6Helpers.Unload(frmDruckSuche.DefInstance);", @"");

                    //}

                    //if (line.Contains(@""))
                    //{
                    //    line = line.Replace(@"", @"");
                    //}

                    //if (line.Contains(@""))
                    //{
                    //    line = line.Replace(@"", @"");
                    //}

                    //if (line.Contains(@""))
                    //{
                    //    line = line.Replace(@"", @"");
                    //}

                    //if (line.Contains(@""))
                    //{
                    //    line = line.Replace(@"", @"");
                    //}

                    //if (line.Contains(@""))
                    //{
                    //    line = line.Replace(@"", @"");
                    //}

                    //if (line.Contains(@"[]") && line.Contains(@"("))
                    //{
                    //    line = Regex.Replace(line, @"(", @"{");                      
                    //}
                    //if (line.Contains(@"[]") && line.Contains(@")"))
                    //{
                    //    line = Regex.Replace(line, @")", @"}");          
                    //}
                    csContent = csContent + line + "\n";
                }
                //csContent = reader.ReadToEnd();
                reader.Close();
            }


            csContent = Regex.Replace(csContent, @"", @"");
            csContent = Regex.Replace(csContent, @"", @"");
            csContent = Regex.Replace(csContent, @"", @"");
            csContent = Regex.Replace(csContent, @"", @"");

            using (StringReader reader = new StringReader(csContent))
            {

                reader.Close();
            }


            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(csContent);
                writer.Close();
            }
        }
    }

}

