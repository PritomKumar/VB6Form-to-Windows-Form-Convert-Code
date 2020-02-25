using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace vb6Convert
{
    class Program
    {

        public static string designerContent = "";
        public static string csContent = "";
        public static List<string> eventHandlerList = new List<string>();
        public static List<string> keyPressEventHandlerList = new List<string>();
        public static List<string> queryUnloadEventHandlerList = new List<string>();
        public static List<string> keyEventHandlerList = new List<string>();
        public static List<string> mouseEventHandlerList = new List<string>();
        public static List<string> formClosedEventHandlerList = new List<string>();
        public static List<string> allTODO_ProblemList = new List<string>();
        public static List<string> allTODO_ProblemListDesignerFile = new List<string>();
        public static List<List<String>> allVariableNameAndType = new List<List<string>>();

        static void Main(String[] args)
        {

            //var basePath = "F:\\Therapie Plus Project\\TherapiePlus.Net\\TherapiePlus\\UI\\";
            var basePath = "F:\\Termin Plus Project\\TerminPlus\\TerminPlus.Net\\TerminPlus\\";
            var desingnerExtention = ".Designer.cs";
            var csExtension = ".cs";
















            var formName = "frmTerminSuche";

            var desingerFilePath = basePath + formName + desingnerExtention;
            var csFilePath = basePath + formName + csExtension; ;

            //ReplaceInFile("F:\\C# Tutorial Practice\\TutorialApplication\\Program.cs");    
            //ReplaceInFile("F:\\Therapie Plus Project\\TherapiePlus.Net\\TherapiePlus\\UI\\frmRgEinzahlungBuchen.Designer.cs");

            ReplaceInDesignerFile(desingerFilePath);
            ReplaceInDesignerFileTODOWorks(desingerFilePath);   
            ReplaceInCsFile(csFilePath);
            ReplaceInCsFileTODOWorks(csFilePath);

            //FreeAgent freeAgent = new FreeAgent();
            //freeAgent.Work();

        }

        public static void ReplaceInDesignerFile(string filePath)
        {

            designerContent = string.Empty;
            string line = string.Empty;
            using (StreamReader reader = new StreamReader(filePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if ((line.Contains(@".BackStyle = CodeArchitects.VB6Library.VB6BackStyleConstants.Transparent;")
                        || line.Contains(@".Style = CodeArchitects.VB6Library.VBRUN.ButtonConstants.vbButtonGraphical;")
                        || line.Contains(@".Value2 = ")
                        || line.Contains(@".OcxState = ")
                        || line.Contains(@".OLEDropMode = ")
                        || line.Contains(@".ParentForm = this;")
                        || line.Contains(@".Name6 = ")
                        || line.Contains(@".Name = ")
                        || line.Contains(@".Value2 = ")
                        || line.Contains(@".HelpContextID = "))
                        && !line.Contains(@"//"))
                    {
                        line = line.TrimStart();
                        line = @"//" + line;

                    }
                    else if (line.Contains(@"InitializeComponents();"))
                    {
                        line = "InitializeComponent();";
                    }

                    if (line.Contains(@"CodeArchitects.VB6Library.VB6ControlArrayCS"))
                    {
                        line = line.Replace("(", "{");
                        line = line.Replace(")", "}");
                    }
                    if (line.Contains(@"VB6ControlArrayCS"))
                    {
                        line = line.Replace("(", "{");
                        line = line.Replace(")", "}");
                    }


                    //if (line.Contains(@"[]") && line.Contains(@"("))
                    //{
                    //    line = Regex.Replace(line, @"(", @"{");                      
                    //}
                    //if (line.Contains(@"[]") && line.Contains(@")"))
                    //{
                    //    line = Regex.Replace(line, @")", @"}");          
                    //}

                    if (line.Contains(@"CodeArchitects.VB6Library.Events.VB6EventHandler"))
                    {
                        try
                        {
                            //Console.WriteLine("Line = " + line);
                            string firstBracket = "(this.";
                            char secondBracket = ')';
                            int startIndex = (int)(line.LastIndexOf(firstBracket) + firstBracket.Length);
                            //Console.WriteLine("Line length = " + line.Length);
                            //Console.WriteLine("Start Index = " + startIndex);
                            string eventName = line.Substring((int)(startIndex),
                                line.LastIndexOf(secondBracket) - startIndex);

                            eventHandlerList.Add(eventName);
                            //Console.WriteLine("EventName = " + eventName);
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }

                    if (line.Contains(@"CodeArchitects.VB6Library.Events.VB6MouseEventHandler"))
                    {
                        try
                        {
                            //Console.WriteLine("Line = " + line);
                            string firstBracket = "(this.";
                            char secondBracket = ')';
                            int startIndex = (int)(line.LastIndexOf(firstBracket) + firstBracket.Length);
                            //Console.WriteLine("Line length = " + line.Length);
                            //Console.WriteLine("Start Index = " + startIndex);
                            string eventName = line.Substring((int)(startIndex),
                                line.LastIndexOf(secondBracket) - startIndex);

                            mouseEventHandlerList.Add(eventName);
                            //Console.WriteLine("EventName = " + eventName);
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }

                    if (line.Contains(@"CodeArchitects.VB6Library.Events.VB6KeyPressEventHandler"))
                    {
                        try
                        {
                            //Console.WriteLine("Line = " + line);
                            string firstBracket = "(this.";
                            char secondBracket = ')';
                            int startIndex = (int)(line.LastIndexOf(firstBracket) + firstBracket.Length);
                            //Console.WriteLine("Line length = " + line.Length);
                            //Console.WriteLine("Start Index = " + startIndex);
                            string eventName = line.Substring((int)(startIndex),
                                line.LastIndexOf(secondBracket) - startIndex);

                            keyPressEventHandlerList.Add(eventName);
                            //Console.WriteLine("EventName = " + eventName);
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }

                    if (line.Contains(@"CodeArchitects.VB6Library.Events.VB6QueryUnloadEventHandler"))
                    {
                        try
                        {
                            //Console.WriteLine("Line = " + line);
                            string firstBracket = "(this.";
                            char secondBracket = ')';
                            int startIndex = (int)(line.LastIndexOf(firstBracket) + firstBracket.Length);
                            //Console.WriteLine("Line length = " + line.Length);
                            //Console.WriteLine("Start Index = " + startIndex);
                            string eventName = line.Substring((int)(startIndex),
                                line.LastIndexOf(secondBracket) - startIndex);

                            queryUnloadEventHandlerList.Add(eventName);
                            //Console.WriteLine("EventName = " + eventName);
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }

                    if (line.Contains(@"CodeArchitects.VB6Library.Events.VB6KeyEventHandler"))
                    {
                        try
                        {
                            //Console.WriteLine("Line = " + line);
                            string firstBracket = "(this.";
                            char secondBracket = ')';
                            int startIndex = (int)(line.LastIndexOf(firstBracket) + firstBracket.Length);
                            //Console.WriteLine("Line length = " + line.Length);
                            //Console.WriteLine("Start Index = " + startIndex);
                            string eventName = line.Substring((int)(startIndex),
                                line.LastIndexOf(secondBracket) - startIndex);

                            keyEventHandlerList.Add(eventName);
                            //Console.WriteLine("EventName = " + eventName);
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }

                    if (line.Contains(@"CodeArchitects.VB6Library.Events.VB6ByrefShortEventHandler"))
                    {
                        try
                        {
                            //Console.WriteLine("Line = " + line);
                            string firstBracket = "(this.";
                            char secondBracket = ')';
                            int startIndex = (int)(line.LastIndexOf(firstBracket) + firstBracket.Length);
                            //Console.WriteLine("Line length = " + line.Length);
                            //Console.WriteLine("Start Index = " + startIndex);
                            string eventName = line.Substring((int)(startIndex),
                                line.LastIndexOf(secondBracket) - startIndex);

                            formClosedEventHandlerList.Add(eventName);
                            //Console.WriteLine("EventName = " + eventName);
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }


                    designerContent = designerContent + line + "\n";
                }
                //designerContent = reader.ReadToEnd();
                reader.Close();
            }

            //foreach (List<string> subList in allVariableNameAndType)
            //{
            //    Console.WriteLine(subList[0] +"  " +  subList[1]);
            //}

            #region Prefix Change

            designerContent = Regex.Replace(designerContent, @".Appearance = CodeArchitects.VB6Library.VB6AppearanceConstants.Flat;",
                ".FlatStyle = FlatStyle.Flat;");
            designerContent = Regex.Replace(designerContent, @"QueryUnload += new CodeArchitects.VB6Library.Events.VB6QueryUnloadEventHandler",
                @"FormClosing += new FormClosingEventHandler");
            designerContent = Regex.Replace(designerContent, @"CodeArchitects.VB6Library.Events.VB6QueryUnloadEventHandler", @"FormClosingEventHandler");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6Form", "Form");
            designerContent = Regex.Replace(designerContent, @".Change += new CodeArchitects.VB6Library.Events.VB6EventHandler",
                @".TextChanged += new System.EventHandler");
            designerContent = Regex.Replace(designerContent, ".Caption", ".Text");
            designerContent = Regex.Replace(designerContent, @".BorderStyle = CodeArchitects.VB6Library.VBRUN.FormBorderStyleConstants.vbFixedDouble;",
                @".FormBorderStyle = FormBorderStyle.Fixed3D;");
            designerContent = Regex.Replace(designerContent, ".MaxButton", ".MaximizeBox");
            designerContent = Regex.Replace(designerContent, ".MinButton", ".MinimizeBox");
            designerContent = Regex.Replace(designerContent, ".StartUpPosition = CodeArchitects.VB6Library.VBRUN.StartUpPositionConstants.vbStartUpManual;",
                ".StartPosition = FormStartPosition.Manual;");
            designerContent = Regex.Replace(designerContent, ".Unload += new CodeArchitects.VB6Library.Events.VB6ByrefShortEventHandler",
                ".FormClosed += new FormClosedEventHandler    ");
            designerContent = Regex.Replace(designerContent, "Activate += new CodeArchitects.VB6Library.Events.VB6EventHandler",
                "Activated += new System.EventHandler");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.Events.VB6EventHandler", "System.EventHandler");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6CommandButton", "Button");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6PictureBox", "PictureBox");
            designerContent = Regex.Replace(designerContent, @".AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;",
                @".SizeMode = PictureBoxSizeMode.AutoSize;");
            designerContent = Regex.Replace(designerContent, ".AutoRedraw = true;", ".Refresh();");
            designerContent = Regex.Replace(designerContent, "SoftPlus.MigratedControls.SP_ComboDrop", "ComboBox");
            designerContent = Regex.Replace(designerContent, ".ListIndex", ".SelectedIndex");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6Label", "Label");
            designerContent = Regex.Replace(designerContent, @".Alignment = CodeArchitects.VB6Library.VBRUN.AlignmentConstants.vbRightJustify;",
                @".Anchor = AnchorStyles.Right;");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6Frame", "GroupBox");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6CheckBox", "CheckBox");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6BorderStyleConstants", "BorderStyle");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6TextBox", "TextBox");
            designerContent = Regex.Replace(designerContent, @".ScrollBars = CodeArchitects.VB6Library.VBRUN.ScrollBarConstants.vbVertical;",
                @".ScrollBars = ScrollBars.Vertical;");
            designerContent = Regex.Replace(designerContent, "SP_ComboDrop", "ComboBox");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.TDBGrid", "C1.Win.C1TrueDBGrid.C1TrueDBGrid");
            designerContent = Regex.Replace(designerContent, ".ReBind();", ".Rebind();");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6ImageList", "ImageList");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6DTPicker", "DateTimePicker");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6ListView", "ListView");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6Timer", "Timer");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6OptionButton", "RadioButton");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6StatusBar", "StatusBar");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6CommonDialog", "OpenFileDialog");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6WebBrowser", "WebBrowser");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6MainMenu", "MenuStrip");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6Menu", "ToolStripMenuItem");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6FrameNoBorder", "GroupBox");
            designerContent = Regex.Replace(designerContent, "SoftPlus.MigratedControls.VB6PVDate2", "DateTimePicker");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6ControlArrayCS<", "");
            designerContent = Regex.Replace(designerContent, "VB6ControlArrayCS<", "");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.Events.VB6KeyEventHandler", "KeyEventHandler");
            designerContent = Regex.Replace(designerContent, "SoftPlus.MigratedControls.VB6TDBGrid", "C1.Win.C1TrueDBGrid.C1TrueDBGrid");
            designerContent = Regex.Replace(designerContent, @".Alignment = CodeArchitects.VB6Library.VBRUN.AlignmentConstants.vbCenter;",
                @".Anchor = AnchorStyles.None;");
            designerContent = Regex.Replace(designerContent, "VB6Project.EnsureVB6Library", "//VB6Project.EnsureVB6Library");
            designerContent = Regex.Replace(designerContent, "VB6PVDate2", "DateTimePicker");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.Events.VB6KeyPressEventHandler", "KeyPressEventHandler");
            designerContent = Regex.Replace(designerContent, "VB6TDBGrid", "C1.Win.C1TrueDBGrid.C1TrueDBGrid");
            designerContent = Regex.Replace(designerContent, @"CodeArchitects.VB6Library.Events.VB6ByrefShortEventHandler",
                @"FormClosedEventHandler");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6ProgressBar", "ProgressBar");
            designerContent = Regex.Replace(designerContent, @"InitializeComponents();", @"InitializeComponent();");
            //designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VBRUN.ButtonConstants.vbButtonGraphical;", "//.*$");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.MSComCtl2.FormatConstant.dtpShortDate;", "DateTimePickerFormat.Short;");
            designerContent = Regex.Replace(designerContent, "CodeArchitects.VB6Library.VB6TreeView", "TreeView");
            designerContent = Regex.Replace(designerContent, @".StartUpPosition = CodeArchitects.VB6Library.VBRUN.StartUpPositionConstants.vbStartUpOwner;",
                @".StartPosition = FormStartPosition.CenterParent;");
            designerContent = Regex.Replace(designerContent, @"SP_ComboDrop", @"ComboBox");
            designerContent = Regex.Replace(designerContent, @">", @"[]");
            designerContent = Regex.Replace(designerContent, @".Align = CodeArchitects.VB6Library.VBRUN.AlignConstants.vbAlignNone;",
                @".Anchor = AnchorStyles.None;");
            designerContent = Regex.Replace(designerContent, @".BorderStyle = CodeArchitects.VB6Library.VBRUN.FormBorderStyleConstants.vbFixedSingle;",
                @".FormBorderStyle = FormBorderStyle.FixedSingle;");
            designerContent = Regex.Replace(designerContent, @"CodeArchitects.VB6Library.MSComCtl2.FormatConstant.dtpLongDate;",
                @"DateTimePickerFormat.Long;");
            designerContent = Regex.Replace(designerContent, @".StartUpPosition = CodeArchitects.VB6Library.VBRUN.StartUpPositionConstants.vbStartUpWindowsDefault;",
                @".StartPosition = FormStartPosition.WindowsDefaultLocation;");
            designerContent = Regex.Replace(designerContent, @"SoftPlus.MigratedControls.VB6PVTime", @"DateTimePicker");
            designerContent = Regex.Replace(designerContent, @"QueryUnload ", @"FormClosing ");
            designerContent = Regex.Replace(designerContent, @"Activate ", @"Activated ");
            designerContent = Regex.Replace(designerContent, @"Unload ", @"FormClosed    ");
            designerContent = Regex.Replace(designerContent, @"StartUpPosition = CodeArchitects.VB6Library.VBRUN.StartUpPositionConstants.vbStartUpScreen;",
                @"StartPosition = FormStartPosition.CenterScreen;");
            designerContent = Regex.Replace(designerContent, @"CodeArchitects.VB6Library.VB6SSTab", @"DevExpress.XtraTab.XtraTabControl");
            designerContent = Regex.Replace(designerContent, @"System.Windows.Forms.TabPage", @"DevExpress.XtraTab.XtraTabPage");
            designerContent = Regex.Replace(designerContent, @".ShowToolTips = true;", @".ShowToolTips = DevExpress.Utils.DefaultBoolean.True;");
            designerContent = Regex.Replace(designerContent, @"CodeArchitects.VB6Library.Events.VB6MouseEventHandler", @"MouseEventHandler");
            designerContent = Regex.Replace(designerContent, @"CodeArchitects.VB6Library.VB6ListBox", @"ListBox");
            designerContent = Regex.Replace(designerContent, @".DblClick ", @".DoubleClick ");
            designerContent = Regex.Replace(designerContent, @"CodeArchitects.VB6Library.VB6CheckedListBox", @"ListBox");
            designerContent = Regex.Replace(designerContent, @".Multiline = true;", @".MultiLine = DefaultBoolean.True;");
            designerContent = Regex.Replace(designerContent, @"SoftPlus.MigratedControls.SP_ComboText", @"ComboBox");
            designerContent = Regex.Replace(designerContent, @"SP_ComboText", @"ComboBox");
            designerContent = Regex.Replace(designerContent, @"VB6PVTime", @"DateTimePicker");
            designerContent = Regex.Replace(designerContent, @"CodeArchitects.VB6Library.VB6ComboBox", @"ComboBox");
            designerContent = Regex.Replace(designerContent, @"", @"");
            designerContent = Regex.Replace(designerContent, @"", @"");
            designerContent = Regex.Replace(designerContent, @"", @"");
            designerContent = Regex.Replace(designerContent, @"", @"");
            designerContent = Regex.Replace(designerContent, @"", @"");

            #endregion


            using (StringReader reader = new StringReader(designerContent))
            {
                reader.Close();
            }


            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(designerContent);
                writer.Close();
            }
        }

        public static void ReplaceInCsFile(string filePath)
        {

            csContent = string.Empty;
            string line = string.Empty;
            string previousLine = String.Empty;

            using (StreamReader reader = new StreamReader(filePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if ((
                        line.Contains(@""))
                        && !line.Contains(@"//"))
                    {
                        //line = line.TrimStart();
                        //line = @"//" + line;

                    }
                    if (line.Contains(".Default = true;"))
                    {
                        string trimmedLine = line.TrimStart();
                        string modifiedLine = @"this.AcceptButton = " +
                                              trimmedLine.Substring(0, trimmedLine.LastIndexOf(".Default")) + ";";
                        line = modifiedLine;
                    }

                    if (line.Contains(@"RefetchCol"))
                    {
                        line = line.Replace("(", "[");
                        line = line.Replace(")", "]");
                        string oldLine = line.Substring(0, line.LastIndexOf(".")) + ".Columns" +
                                         line.Substring(line.LastIndexOf("["),
                                             line.LastIndexOf("]") - line.LastIndexOf("[") + 1)
                                         + ".Refetch();";
                        //Console.WriteLine("Refetch = " + oldLine );
                        line = oldLine;
                    }

                    if (line.Contains(@"UnloadMode == (int) VBRUN.QueryUnloadConstants.vbFormControlMenu") && line.Contains(@"("))
                    {
                        line = Regex.Replace(line, @"UnloadMode == (int) VBRUN.QueryUnloadConstants.vbFormControlMenu",
                            @"e.CloseReason == (int) VBRUN.QueryUnloadConstants.vbFormControlMenu");
                    }

                    if (line.Contains(@".ToolTipText = "))
                    {
                        string newLine = line;
                        newLine = newLine.TrimStart();
                        newLine = newLine.Replace(".ToolTipText = ", ", ");
                        newLine = newLine.Replace(";", "");
                        newLine = "this.ToolTip1.SetToolTip(this." + newLine + ");";
                        line = newLine;
                        //Console.WriteLine("New Line = " + newLine);
                    }

                    if (line.Contains(@"Cancel = 1;"))
                    {
                        line = line.Replace(@"Cancel = 1;", @"e.Cancel = true;");
                    }
                    if (line.Contains(@"internal partial class "))
                    {
                        previousLine = @"internal partial class ";
                        //line = line.Replace( @"", @"");
                    }
                    if (previousLine.Equals("internal partial class ") && line.Contains("{") && !line.Contains(@"//"))
                    {
                        line = line + @"//
		#region Developer Work
		// Search and Check TODO
		//In Designer.cs


		//In code.cs

				
		#endregion

"
                            ;
                        previousLine = string.Empty;
                        //line = line.Replace( @"", @"");
                    }

                    if (line.Contains(@"{"))
                    {
                        previousLine = String.Empty;
                        //line = line.Replace( @"", @"");
                    }
                    if (line.Contains(@"KeyCode == (int)VBRUN.KeyCodeConstants.vbKeyEscape"))
                    {
                        line = line.Replace(@"KeyCode == (int)VBRUN.KeyCodeConstants.vbKeyEscape", @"e.KeyCode == Keys.Escape");
                    }
                    if (line.Contains(@"DefInstance.Show((int)VBRUN.FormShowConstants.vbModal);"))
                    {
                        line = line.Replace(@"DefInstance.Show((int)VBRUN.FormShowConstants.vbModal);", @"DefInstance.ShowDialog();");
                    }
                    if (line.Contains(@".Clear();")
                        && !line.Contains(@".Items.Clear();")
                        && !line.Contains(@".Nodes.Clear();")
                        && !line.Contains(@".Err.Clear();"))
                    {
                        line = line.Replace(@".Clear();", @".Items.Clear();");
                    }

                    string checkComment = line.TrimStart();

                    if ((line.Contains(@".ZOrder") && !checkComment.StartsWith(@"//"))
                        || (line.Contains(@".AddItem(") && !checkComment.StartsWith(@"//"))
                        || (line.Contains(".Array = mxaListe;") && !checkComment.StartsWith(@"//"))
                        || (line.Contains("].HeadFont.") && !checkComment.StartsWith(@"//"))
                        || (line.Contains("modXUtilities.WindowGetPosition(this);") && !checkComment.StartsWith(@"//"))
                        || (line.Contains("modXUtilities.WindowSavePosition(this);") && !checkComment.StartsWith(@"//"))
                        || (line.Contains(".Separator = ") && !checkComment.StartsWith(@"//"))
                        || (line.Contains("modC_StdPlan_Draw.StdPlan_Init(") && !checkComment.StartsWith(@"//"))
                        || (line.Contains("modC_StdPlan_Draw.StdPlan_Draw(") && !checkComment.StartsWith(@"//"))
                        || (line.Contains("modC_StdPlan_Vorschau.Plan_Vorschau_Draw(") && !checkComment.StartsWith(@"//"))
                        || (line.Contains("modC_StdPlan_Draw.StdPlan_ValuePoint(") && !checkComment.StartsWith(@"//"))
                        || (line.Contains("modC_StdPlan_Data.StdPlan_Data_Merge(") && !checkComment.StartsWith(@"//"))
                        || (line.Contains("modXControls.Form_Controls_Disable(this);") && !checkComment.StartsWith(@"//"))
                        || (line.Contains(@"Nodes.Add(") && !checkComment.StartsWith(@"//"))
                        || (line.Contains(@"mod_FITplus.LoadFITplusMitarbeiter(") && !checkComment.StartsWith(@"//"))
                        || (line.Contains(@"mod_FITplus.LoadFITplusAbos(") && !checkComment.StartsWith(@"//"))
                        || (line.Contains(@"modXControls.Form_Controls_Reable(this)") && !checkComment.StartsWith(@"//"))
                        || (line.Contains(@"modXUtilities.WindowGetPosition(") && !checkComment.StartsWith(@"//"))
                        || (line.Contains(@"modT_Settings.SetAlternateGridColor(") && !checkComment.StartsWith(@"//"))
                        || (line.Contains(@".AddItem(") && !checkComment.StartsWith(@"//"))
                        || (line.Contains(@".AddItem(") && !checkComment.StartsWith(@"//"))
                        || (line.Contains(@".AddItem(") && !checkComment.StartsWith(@"//"))
                        || (line.Contains(@"mod_Language.SetCurrentLanguage(this);") && !checkComment.StartsWith(@"//"))

                    )
                    {
                        allTODO_ProblemList.Add(line);
                    }

                    foreach (var sublist in allVariableNameAndType)
                    {
                        if (sublist[0].Equals("PictureBox"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("DrawMode");
                            deprecatedAttributeList.Add("DrawStyle");
                            deprecatedAttributeList.Add("Line");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                var todoChange = sublist[1] + "." + deprecatedAttribute;
                                if ((line.Contains(todoChange) && !line.Contains(@"//"))

                                )
                                {
                                    allTODO_ProblemList.Add(line);
                                }
                            }
                        }

                        if (sublist[0].Equals("Button[]"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("Appearance");
                            deprecatedAttributeList.Add("FontBold");
                        
                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                
                                var todoChange = sublist[1];
                                var commentCheck = line.TrimStart();
                                if ((line.Contains(todoChange) && !commentCheck.StartsWith(@"//") 
                                                               && (line.Contains(deprecatedAttribute))

                                ))
                                {
                                    allTODO_ProblemList.Add(line);
                                }
                            }
                        }

                        if (sublist[0].Equals("Label[]"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("BackStyle");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {

                                var todoChange = sublist[1];
                                var commentCheck = line.TrimStart();
                                if ((line.Contains(todoChange) && !commentCheck.StartsWith(@"//")
                                                               && (line.Contains(deprecatedAttribute))

                                    ))
                                {
                                    allTODO_ProblemList.Add(line);
                                }
                            }
                        }

                        if (sublist[0].Equals("RadioButton[]"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("Value");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                if (deprecatedAttribute.Equals("Value"))
                                {
                                    var variableName = sublist[1];
                                    var changedValue =  "Checked";
                                    var cautionValue =  "Value2";
                                    if ((line.Contains(variableName)
                                         && line.Contains(deprecatedAttribute)
                                         && !line.Contains(@"//")
                                         && !line.Contains(changedValue)
                                         && !line.Contains(cautionValue)

                                        ))
                                    {
                                        line = line.Replace(deprecatedAttribute, changedValue);
                                    }
                                }
                            }
                        }

                        if (sublist[0].Equals("ComboBox[]"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("Value");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                if (deprecatedAttribute.Equals("Value"))
                                {
                                    var variableName = sublist[1];
                                    var changedValue = "SelectedValue";
                                    var cautionValue = "Value2";
                                    if ((line.Contains(variableName)
                                         && line.Contains(deprecatedAttribute)
                                         && !line.Contains(@"//")
                                         && !line.Contains(changedValue)
                                         && !line.Contains(cautionValue)

                                        ))
                                    {
                                        line = line.Replace(deprecatedAttribute, changedValue);
                                    }
                                }
                            }
                        }

                        if (sublist[0].Equals("ComboBox"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("Value2");
                            deprecatedAttributeList.Add("set_ItemData(");
                            deprecatedAttributeList.Add("get_ItemData(");
                            deprecatedAttributeList.Add("ListCount");
                            deprecatedAttributeList.Add("ListIndex");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                var todoChange = sublist[1] + "." + deprecatedAttribute;
                                if ((line.Contains(todoChange) && !line.Contains(@"//"))

                                    )
                                {
                                    allTODO_ProblemList.Add(line);
                                }
                            }
                        }

                        if (sublist[0].Equals("Label"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("BackStyle");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                var todoChange = sublist[1] + "." + deprecatedAttribute;
                                if ((line.Contains(todoChange) && !line.Contains(@"//"))

                                )
                                {
                                    allTODO_ProblemList.Add(line);
                                }
                            }
                        }

                        if (sublist[0].Equals("TextBox"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("Locked");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                var todoChange = sublist[1] + "." + deprecatedAttribute;
                                if ((line.Contains(todoChange) && !line.Contains(@"//"))

                                )
                                {
                                    allTODO_ProblemList.Add(line);
                                }
                            }
                        }

                        if (sublist[0].Equals("C1.Win.C1TrueDBGrid.C1TrueDBGrid")) //Normal TrueDBGrid
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("Array");
                            deprecatedAttributeList.Add("SelBookmarks");
                            deprecatedAttributeList.Add("RowDividerStyle");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                var todoChange = sublist[1] + "." + deprecatedAttribute;
                                if ((line.Contains(todoChange) && !line.Contains(@"//"))

                                )
                                {
                                    allTODO_ProblemList.Add(line);
                                }
                            }

                            deprecatedAttributeList.Add("ApproxCount");
                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                var changeAttribute = sublist[1] + "." + deprecatedAttribute;
                                var changedValue = sublist[1] + "." + "RowCount";
                                var cautionValue = sublist[1] + "." + "Value2";
                                if ((line.Contains("ApproxCount")
                                     && !line.Contains(@"//")
                                     && !line.Contains(changedValue)
                                     && !line.Contains(cautionValue)

                                    ))
                                {
                                    line = line.Replace(changeAttribute, changedValue);
                                }
                            }
                        }

                        if (sublist[0].Equals("C1.Win.C1TrueDBGrid.C1TrueDBGrid")) //TrueDBGrid.Columns[]
                        {
                            var deprecatedAttributeList = new List<String>();

                            if (line.Contains(@"Columns["))
                            {
                                var beforeAttribute = line.Substring(0, line.IndexOf(".")) + ".Columns" +
                                                                line.Substring(line.LastIndexOf("["),
                                                          line.LastIndexOf("]") - line.LastIndexOf("[") + 1);

                                //Console.WriteLine("Before Attribute = " + beforeAttribute);
                                deprecatedAttributeList.Add("FetchStyle");
                                deprecatedAttributeList.Add("Locked");
                                deprecatedAttributeList.Add("HeadingStyle");

                                foreach (var deprecatedAttribute in deprecatedAttributeList)
                                {
                                    var todoChange = beforeAttribute + "." + deprecatedAttribute;

                                    if ((line.Contains(todoChange) && !line.Contains(@"//"))

                                    )
                                    {
                                        allTODO_ProblemList.Add(line);
                                    }
                                }
                            }
                        }

                        if (sublist[0].Equals("ComboBox"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("Value");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                var changeAttribute = sublist[1] + "." + deprecatedAttribute;
                                var changedValue = sublist[1] + "." + "SelectedValue";
                                var cautionValue = sublist[1] + "." + "Value2";
                                if ((line.Contains(changeAttribute)
                                     && !line.Contains(@"//")
                                     && !line.Contains(changedValue)
                                     && !line.Contains(cautionValue)

                                    ))
                                {
                                    line = line.Replace(changeAttribute, changedValue);
                                }
                            }
                        }

                        if (sublist[0].Equals("TextBox"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("Change");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                var changeAttribute = sublist[1] + "." + deprecatedAttribute;
                                var changedValue = sublist[1] + "." + "TextChanged";
                                var cautionValue = sublist[1] + "." + "HeightChange";
                                if ((line.Contains(changeAttribute)
                                     && !line.Contains(@"//")
                                     && !line.Contains(changedValue)
                                     && !line.Contains(cautionValue)

                                    ))
                                {
                                    line = line.Replace(changeAttribute, changedValue);
                                }
                            }
                        }

                        if (sublist[0].Equals("CheckBox"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("Value");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                if (deprecatedAttribute.Equals("Value"))
                                {
                                    var changeAttribute = sublist[1] + "." + deprecatedAttribute;
                                    var changedValue = sublist[1] + "." + "Checked";
                                    var cautionValue = sublist[1] + "." + "Value2";
                                    if ((line.Contains(changeAttribute)
                                         && !line.Contains(@"//")
                                         && !line.Contains(changedValue)
                                         && !line.Contains(cautionValue)

                                        ))
                                    {
                                        line = line.Replace(changeAttribute, changedValue);
                                    }
                                }
                            }
                        }

                        if (sublist[0].Equals("ProgressBar"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("Max");
                            deprecatedAttributeList.Add("Min");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                if (deprecatedAttribute.Equals("Max"))
                                {
                                    var changeAttribute = sublist[1] + "." + deprecatedAttribute;
                                    var changedValue = sublist[1] + "." + "Maximum";

                                    if ((line.Contains(changeAttribute)
                                         && !line.Contains(@"//")
                                         && !line.Contains(changedValue)

                                        ))
                                    {
                                        line = line.Replace(changeAttribute, changedValue);
                                    }
                                }

                                if (deprecatedAttribute.Equals("Min"))
                                {
                                    var changeAttribute = sublist[1] + "." + deprecatedAttribute;
                                    var changedValue = sublist[1] + "." + "Minimum";

                                    if ((line.Contains(changeAttribute)
                                         && !line.Contains(@"//")
                                         && !line.Contains(changedValue)

                                        ))
                                    {
                                        line = line.Replace(changeAttribute, changedValue);
                                    }
                                }
                            }
                        }

                        if (sublist[0].Equals("DateTimePicker")) //Normal TrueDBGrid
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("DateString");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                var todoChange = sublist[1] + "." + deprecatedAttribute;
                                if ((line.Contains(todoChange) && !line.Contains(@"//"))

                                )
                                {
                                    allTODO_ProblemList.Add(line);
                                }
                            }
                            deprecatedAttributeList.Add("Time");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                if (deprecatedAttribute.Equals("Time"))
                                {
                                    var changeAttribute = sublist[1] + "." + deprecatedAttribute;
                                    var changedValue = sublist[1] + "." + "Value";
                                    var cautionValue = sublist[1] + "." + "Value2";
                                    if ((line.Contains(changeAttribute)
                                         && !line.Contains(@"//")
                                         && !line.Contains(changedValue)
                                         && !line.Contains(cautionValue)

                                        ))
                                    {
                                        line = line.Replace(changeAttribute, changedValue);
                                    }
                                }
                            }
                        }

                    }


                    if (line.Contains(@".FetchRowStyle") && !line.Contains(@".FetchRowStyles"))
                    {
                        line = line.Replace(@".FetchRowStyle", @".FetchRowStyles");
                    }
                    if (line.Contains(@"Cancel = (short)(true ? -1 : 0);"))
                    {
                        line = line.Replace(@"Cancel = (short)(true ? -1 : 0);",
                            @"e.Cancel = (true ? true : false);");
                    }
                    if (line.Contains(@""))
                    {
                        //line = line.Replace( @"", @"");
                    }
                    if (line.Contains(@""))
                    {
                        //line = line.Replace( @"", @"");
                    }


                    //if (line.Contains(@"[]") && line.Contains(@"("))
                    //{
                    //    line = Regex.Replace(line, @"(", @"{");                      
                    //}

                    //if (line.Contains(@"[]") && line.Contains(@")"))
                    //{
                    //    line = Regex.Replace(line, @")", @"}");          
                    //}

                    foreach (var eventString in eventHandlerList)
                    {
                        string str = "private void " + eventString;
                        if (line.Contains(str) && line.Contains(@"(") && !line.Contains(@"object sender , EventArgs e"))
                        {
                            string firstPart = line.Substring(0, line.LastIndexOf('(') + 1);
                            string modifiedLine = firstPart + " object sender , EventArgs e )";
                            line = modifiedLine;
                            //Console.WriteLine("Modified Line = " + modifiedLine);
                            break;

                            //line = Regex.Replace(line, @"(", @"{");
                        }
                    }

                    foreach (var eventString in keyPressEventHandlerList)
                    {
                        string str = "private void " + eventString;
                        if (line.Contains(str) && line.Contains(@"(") && !line.Contains(@"object sender , KeyPressEventArgs e"))
                        {
                            string firstPart = line.Substring(0, line.LastIndexOf('(') + 1);
                            string modifiedLine = firstPart + " object sender , KeyPressEventArgs e )";
                            line = modifiedLine;
                            break;
                            // Console.WriteLine("Modified Line = " + modifiedLine);
                            //line = Regex.Replace(line, @"(", @"{");
                        }
                    }

                    foreach (var eventString in queryUnloadEventHandlerList)
                    {
                        string str = "private void " + eventString;
                        if (line.Contains(str) && line.Contains(@"(") && !line.Contains(@"object sender , FormClosingEventArgs e"))
                        {
                            string firstPart = line.Substring(0, line.LastIndexOf('(') + 1);
                            string modifiedLine = firstPart + " object sender , FormClosingEventArgs e )";
                            line = modifiedLine;
                            //Console.WriteLine("Modified Line = " + modifiedLine);
                            break;
                            //line = Regex.Replace(line, @"(", @"{");
                        }
                    }

                    foreach (var eventString in keyEventHandlerList)
                    {
                        string str = "private void " + eventString;
                        if (line.Contains(str) && line.Contains(@"(") && !line.Contains(@"object sender , KeyEventArgs e"))
                        {
                            string firstPart = line.Substring(0, line.LastIndexOf('(') + 1);
                            string modifiedLine = firstPart + " object sender , KeyEventArgs e )";
                            line = modifiedLine;
                            //Console.WriteLine("Modified Line = " + modifiedLine);
                            break;

                            //line = Regex.Replace(line, @"(", @"{");
                        }
                    }

                    foreach (var eventString in mouseEventHandlerList)
                    {
                        string str = "private void " + eventString;
                        if (line.Contains(str) && line.Contains(@"(") && !line.Contains(@"object sender , MouseEventArgs e"))
                        {
                            string firstPart = line.Substring(0, line.LastIndexOf('(') + 1);
                            string modifiedLine = firstPart + " object sender , MouseEventArgs e )";
                            line = modifiedLine;
                            //Console.WriteLine("Modified Line = " + modifiedLine);
                            break;

                            //line = Regex.Replace(line, @"(", @"{");
                        }
                    }

                    foreach (var eventString in formClosedEventHandlerList)
                    {
                        string str = "private void " + eventString;
                        if (line.Contains(str) && line.Contains(@"(") && !line.Contains(@"object sender , FormClosedEventArgs e"))
                        {
                            string firstPart = line.Substring(0, line.LastIndexOf('(') + 1);
                            string modifiedLine = firstPart + " object sender , FormClosedEventArgs e )";
                            line = modifiedLine;
                            //Console.WriteLine("Modified Line = " + modifiedLine);
                            break;

                            //line = Regex.Replace(line, @"(", @"{");
                        }
                    }

                    if (line.Contains(@"") && line.Contains(@"("))
                    {
                        //line = Regex.Replace(line, @"(", @"{");
                    }

                    if (line.Contains(@"DefInstance.Show(1);"))
                    {
                        line = line.Replace(@"DefInstance.Show(1);", @"DefInstance.ShowDialog();");

                    }

                    if (line.Contains(@"KeyCode == (int) VBRUN.KeyCodeConstants.vbKeyEscape"))
                    {
                        line = line.Replace(@"KeyCode == (int) VBRUN.KeyCodeConstants.vbKeyEscape",
                            @"e.KeyCode == Keys.Escape");
                        //Console.WriteLine("LALALALA");
                    }

                    if (line.Contains(@"KeyAscii == (int) VBRUN.KeyCodeConstants.vbKeyEscape"))
                    {
                        line = line.Replace(@"KeyAscii == (int) VBRUN.KeyCodeConstants.vbKeyEscape", @"e.KeyChar == (char)Keys.Escape");
                        //Console.WriteLine("LALALALA");
                    }
                    if (line.Contains(@"KeyAscii == (int)VBRUN.KeyCodeConstants.vbKeyEscape"))
                    {
                        line = line.Replace(@"KeyAscii == (int)VBRUN.KeyCodeConstants.vbKeyEscape", @"e.KeyChar == (char)Keys.Escape");
                        //Console.WriteLine("LALALALA");
                    }


                    csContent = csContent + line + "\n";
                }
                //csContent = reader.ReadToEnd();
                reader.Close();
            }

            csContent = Regex.Replace(csContent, @".Caption", @".Text");
            csContent = Regex.Replace(csContent, "DefInstance.Show(1);", @"DefInstance.ShowDialog();");
            csContent = Regex.Replace(csContent, @".SetFocus", @".Focus");
            csContent = Regex.Replace(csContent, @"Value == VBRUN.CheckBoxConstants.vbUnchecked", @"Checked == false");
            csContent = Regex.Replace(csContent, @"Value == VBRUN.CheckBoxConstants.vbChecked", @"Checked == true");
            csContent = Regex.Replace(csContent, @"Controls6", @"Controls");
            csContent = Regex.Replace(csContent, @"Value = VBRUN.CheckBoxConstants.vbUnchecked", @"Checked = false");
            csContent = Regex.Replace(csContent, @"Value = VBRUN.CheckBoxConstants.vbChecked", @"Checked = true");
            csContent = Regex.Replace(csContent, @".ReBind", @".Rebind");
            csContent = Regex.Replace(csContent, @".AlternatingRowStyle", @".AlternatingRows");
            csContent = Regex.Replace(csContent, @"this.hWnd", @"(int)this.Handle");
            csContent = Regex.Replace(csContent, @".SelStart", @".SelectionStart");
            csContent = Regex.Replace(csContent, @".SelLength", @".SelectionLength");
            csContent = Regex.Replace(csContent, @"VBRUN.CheckBoxConstants.vbChecked", @"true");
            csContent = Regex.Replace(csContent, @"VBRUN.CheckBoxConstants.vbUnchecked", @"false");
            csContent = Regex.Replace(csContent, @"float", @"int");
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

        public static void ReplaceInDesignerFileTODOWorks(string filePath)
        {

            csContent = string.Empty;
            string line = string.Empty;
            string previousLine = String.Empty;

            using (StreamReader reader = new StreamReader(filePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if ((
                        line.Contains(@""))
                        && !line.Contains(@"//"))
                    {
                        //line = line.TrimStart();
                        //line = @"//" + line;

                    }

                    if (line.Contains(@"public") && line.EndsWith(@";") )
                    {
                        string oldLine = line;
                        oldLine = oldLine.Trim();
                        oldLine = oldLine.Replace(";", "");
                        var words = oldLine.Split(" ");

                        // Console.WriteLine(oldLine);
                        foreach (var word in words)
                        {
                            //Console.WriteLine(word);
                        }

                        //Console.WriteLine("Word Count = "+ words.Length);

                        allVariableNameAndType.Add(new List<string> { words[1], words[2] });

                    }

                    foreach (var sublist in allVariableNameAndType)
                    {
                        if (sublist[0].Equals("ImageList"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("BackColor");
                            deprecatedAttributeList.Add("MaskColor");
                            deprecatedAttributeList.Add("ImageList");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                var todoChange = sublist[1] + "." + deprecatedAttribute;
                                if ((line.Contains(todoChange) && !line.Contains(@"//"))

                                )
                                {
                                    allTODO_ProblemListDesignerFile.Add(line);
                                }
                            }


                        }

                        if (sublist[0].Equals("PictureBox"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("Picture");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                var changeAttribute = "this." + sublist[1] + "." + deprecatedAttribute;
                                var changedValue = "this." + sublist[1] + "." + "Image";
                                //var cautionValue = sublist[1] + "." + "Value2";
                                if ((line.Contains(changeAttribute)
                                     && !line.Contains(@"//")
                                     && !line.Contains(changedValue)
                                        //&& !line.Contains(cautionValue)

                                    ))
                                {
                                    line = line.Replace(changeAttribute, changedValue);
                                }
                            }
                        }

                        if (sublist[0].Equals("TreeView"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("LineStyle");
                            deprecatedAttributeList.Add("Style");
                            deprecatedAttributeList.Add("Indentation");
                            deprecatedAttributeList.Add("NodeClick");
                            deprecatedAttributeList.Add("SingleSel");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                var todoChange = sublist[1] + "." + deprecatedAttribute;
                                if ((line.Contains(todoChange) && !line.Contains(@"//"))

                                )
                                {
                                    allTODO_ProblemListDesignerFile.Add(line);
                                }
                            }
                        }

                        if (sublist[0].Equals("Timer"))
                        {
                            var deprecatedAttributeList = new List<String>();
                            deprecatedAttributeList.Add("Timer");

                            foreach (var deprecatedAttribute in deprecatedAttributeList)
                            {
                                var changeAttribute = sublist[1] + "." + deprecatedAttribute;
                                var changedValue = sublist[1] + "." + "Tick";
                                //var cautionValue = sublist[1] + "." + "Value2";
                                if ((line.Contains(changeAttribute)
                                     && !line.Contains(@"//")
                                     && !line.Contains(changedValue)
                                     //&& !line.Contains(cautionValue)

                                     ))
                                {
                                    line = line.Replace(changeAttribute, changedValue);
                                }
                            }
                        }
                    }
                    if (line.Contains(@""))
                    {
                        //line = line.Replace( @"", @"");
                    }
                    if (line.Contains(@""))
                    {
                        //line = line.Replace( @"", @"");
                    }
                    if (line.Contains(@""))
                    {
                        //line = line.Replace( @"", @"");
                    }
                    if (line.Contains(@""))
                    {
                        //line = line.Replace( @"", @"");
                    }

                    foreach (var todo in allTODO_ProblemListDesignerFile)
                    {
                        if (line.Equals(todo))
                        {
                            line = @"//TODO" + "\n" + @"//" + line.TrimStart();
                        }
                    }


                    csContent = csContent + line + "\n";
                }
                //csContent = reader.ReadToEnd();
                reader.Close();
            }

            csContent = Regex.Replace(csContent, @"", @"");
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
        public static void ReplaceInCsFileTODOWorks(string filePath)
        {

            csContent = string.Empty;
            string line = string.Empty;
            string previousLine = String.Empty;

            using (StreamReader reader = new StreamReader(filePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if ((
                        line.Contains(@""))
                        && !line.Contains(@"//"))
                    {
                        //line = line.TrimStart();
                        //line = @"//" + line;

                    }

                    
                    if (line.Contains(@"//In code.cs"))
                    {
                        var todoComment = String.Empty;
                        foreach (var TODO in allTODO_ProblemList)
                        {
                            todoComment = todoComment + "\n" + @"//" + TODO.TrimStart();
                        }

                        //Console.WriteLine("TODOcommect = " + todoComment);
                        line = line + todoComment;
                    }

                    if (line.Contains(@"//In Designer.cs"))
                    {
                        var todoComment = String.Empty;
                        foreach (var TODO in allTODO_ProblemListDesignerFile)
                        {
                            todoComment = todoComment + "\n" + @"//" + TODO.TrimStart();
                        }

                        //Console.WriteLine("TODOcommect = " + todoComment);
                        line = line + todoComment;
                    }
                    if (line.Contains(@""))
                    {
                        //line = line.Replace( @"", @"");
                    }
                    if (line.Contains(@""))
                    {
                        //line = line.Replace( @"", @"");
                    }
                    if (line.Contains(@""))
                    {
                        //line = line.Replace( @"", @"");
                    }
                    if (line.Contains(@""))
                    {
                        //line = line.Replace( @"", @"");
                    }

                    foreach (var todo in allTODO_ProblemList)
                    {
                        if (line.Equals(todo))
                        {
                            line = @"//TODO" + "\n" + @"//" + line.TrimStart();
                        }
                    }


                    csContent = csContent + line + "\n";
                }
                //csContent = reader.ReadToEnd();
                reader.Close();
            }

            allTODO_ProblemList.Clear();
            allTODO_ProblemListDesignerFile.Clear();

            csContent = Regex.Replace(csContent, @"", @"");
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

