using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PolyglotFramework.Preferences;

namespace PolyglotFramework
{
    public partial class PolyglotComposer : Form
    {
        private ModulesConfiguration config;

        private PolyglotEngine engine;

        private const string filefilter = "Polyglot config files (*.pcf)|*.pcf";
        private string filename = "";
        private bool suspectChecks = false;

        public PolyglotComposer()
        {
            InitializeComponent();
            try
            {
                engine = PolyglotEngine.NewEngine();
                engine.Stopped += engine_Stopped;
            }
            catch (ModuleImplementationException mie)
            {
                MessageBox.Show(mie.Message, "Module Implementation Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        private void PolyglotComposer_Load(object sender, EventArgs e)
        {
            foreach (IAcquisition acquisitionModule in engine.AcquisitionModules) acquisitionComboBox.Items.Add(acquisitionModule.ModuleName);
            foreach (IPointer pointerModule in engine.PointerModules) pointerComboBox.Items.Add(pointerModule.ModuleName);
            foreach (ITransformation transformationModule in engine.TransformationModules) transformationComboBox.Items.Add(transformationModule.ModuleName);
            foreach (IOutput outputModule in engine.OutputModules) outputComboBox.Items.Add(outputModule.ModuleName);

            this.RecheckStuff();
        }

        void engine_Stopped(object sender, EngineStoppedEventArgs e)
        {
            engine = null;
            engine = PolyglotEngine.NewEngine();
            ModuleSelectionGroup.Enabled = true;
        }

        private void UseTheseButton_click(object sender, EventArgs e)
        {
            if (CheckAllComboboxes())
            {
                //Console.WriteLine("asking engine to run");
                engine.Run(
                    acquisitionComboBox.SelectedIndex,
                    pointerComboBox.SelectedIndex,
                    transformationComboBox.SelectedIndex,
                    outputComboBox.SelectedIndex
                    );
                ModuleSelectionGroup.Enabled = false;
            }
        }

        private void RecheckStuff(object sender, EventArgs e)
        {
            this.RecheckStuff();
        }

        private void RecheckStuff()
        {
            if (!this.suspectChecks)
            {
                bool ok = CheckAllComboboxes();
                UseTheseButton.Enabled = ok;
                saveToolStripMenuItem.Enabled = ok;
                resetToolStripMenuItem.Enabled = CanReset();
                HandleStar();
            }
        }

        private void HandleStar()
        {
            // no point checking if all values are blank
            if (CanReset())
            {
                string title = ModuleSelectionGroup.Text;
                bool[] check = new bool[2];
                check[0] = (title.Length > 0) ? (title[title.Length - 1] == '*') : false;
                check[1] = SameAsOnDisk();
                if (check[0] && check[1]) title = title.Substring(0, title.Length - 1);
                if (!check[0] && !check[1]) title += "*";
                ModuleSelectionGroup.Text = title;
            }
        }

        private bool CanReset()
        {
            if (acquisitionComboBox.SelectedIndex != -1) return true;
            if (pointerComboBox.SelectedIndex != -1) return true;
            if (transformationComboBox.SelectedIndex != -1) return true;
            if (outputComboBox.SelectedIndex != -1) return true;

            return false;
        }

        private bool SameAsOnDisk()
        {
            ModulesConfiguration mc = this.GenerateConfigFromControls();
            return (this.config == mc);
        }

        private bool CheckAllComboboxes()
        {
            return (acquisitionComboBox.SelectedIndex != -1) &&
                                (pointerComboBox.SelectedIndex != -1) &&
                                (transformationComboBox.SelectedIndex != -1) &&
                                (outputComboBox.SelectedIndex != -1);
        }

        private void PolyglotComposer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (engine !=null) engine.Stop();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.suspectChecks = true;
            ModuleSelectionGroup.Text = "";
            acquisitionComboBox.SelectedIndex = -1;
            pointerComboBox.SelectedIndex = -1;
            transformationComboBox.SelectedIndex = -1;
            outputComboBox.SelectedIndex = -1;
            this.suspectChecks = false;
            this.RecheckStuff();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.suspectChecks = true;
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = filefilter;
                open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                if (open.ShowDialog() == DialogResult.OK)
                {
                    ModuleSelectionGroup.Text = Path.GetFileName(open.FileName);
                    this.filename = open.FileName;

                    try
                    {
                        this.config = ModulesConfiguration.LoadConfiguration(open.FileName);
                        bool loaded = false;

                        for (int i = 0; i < engine.AcquisitionModules.Count(); i++)
                        {
                            if (config.AcquisitionModule.ModuleID == engine.AcquisitionModules.ElementAt(i).ModuleID)
                            {
                                loaded = true;
                                acquisitionComboBox.SelectedIndex = i;
                                break;
                            }
                        }
                        for (int i = 0; i < engine.PointerModules.Count(); i++)
                        {
                            if (config.PointerModule.ModuleID == engine.PointerModules.ElementAt(i).ModuleID)
                            {
                                loaded = true;
                                pointerComboBox.SelectedIndex = i;
                                break;
                            }
                        }
                        for (int i = 0; i < engine.TransformationModules.Count(); i++)
                        {
                            if (config.TransformationModule.ModuleID == engine.TransformationModules.ElementAt(i).ModuleID)
                            {
                                loaded = true;
                                transformationComboBox.SelectedIndex = i;
                                break;
                            }
                        }
                        for (int i = 0; i < engine.OutputModules.Count(); i++)
                        {
                            if (config.OutputModule.ModuleID == engine.OutputModules.ElementAt(i).ModuleID)
                            {
                                loaded = true;
                                outputComboBox.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        this.suspectChecks = false;
                        this.RecheckStuff();
                    }
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed loading config file");
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = filefilter;
            if (this.filename.Length > 0) save.FileName = this.filename; 
            save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            if (save.ShowDialog() == DialogResult.OK)
            {
                ModulesConfiguration mc = GenerateConfigFromControls();

                if (mc.SaveConfiguration(save.FileName))
                {
                    ModuleSelectionGroup.Text = Path.GetFileName(save.FileName);
                    this.config = mc;
                }
            }
        }

        private ModulesConfiguration GenerateConfigFromControls()
        {
            ModulesConfiguration mc = new ModulesConfiguration();
            if (acquisitionComboBox.SelectedIndex >= 0)
            {
                mc.AcquisitionModule = new ModuleMetaData();
                mc.AcquisitionModule.ModuleID = engine.AcquisitionModules.ElementAt(acquisitionComboBox.SelectedIndex).ModuleID;
                mc.AcquisitionModule.ModuleName = engine.AcquisitionModules.ElementAt(acquisitionComboBox.SelectedIndex).ModuleName;
            }
            if (pointerComboBox.SelectedIndex >= 0)
            {
                mc.PointerModule = new ModuleMetaData();
                mc.PointerModule.ModuleID = engine.PointerModules.ElementAt(pointerComboBox.SelectedIndex).ModuleID;
                mc.PointerModule.ModuleName = engine.PointerModules.ElementAt(pointerComboBox.SelectedIndex).ModuleName;
            }
            if (transformationComboBox.SelectedIndex >= 0)
            {
                mc.TransformationModule = new ModuleMetaData();
                mc.TransformationModule.ModuleID = engine.TransformationModules.ElementAt(transformationComboBox.SelectedIndex).ModuleID;
                mc.TransformationModule.ModuleName = engine.TransformationModules.ElementAt(transformationComboBox.SelectedIndex).ModuleName;
            }
            if (outputComboBox.SelectedIndex >= 0)
            {
                mc.OutputModule = new ModuleMetaData();
                mc.OutputModule.ModuleID = engine.OutputModules.ElementAt(outputComboBox.SelectedIndex).ModuleID;
                mc.OutputModule.ModuleName = engine.OutputModules.ElementAt(outputComboBox.SelectedIndex).ModuleName;
            }
            return mc;
        }
    }
}
