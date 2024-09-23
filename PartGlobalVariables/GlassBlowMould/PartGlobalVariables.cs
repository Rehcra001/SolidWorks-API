using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace GlassBlowMould
{
    public partial class PartGlobalVariables : Form
    {
        private string _filepath = "";
        private SldWorks _swApp;
        private ModelDoc2? _swModel = default(ModelDoc2);
        private EquationMgr? _swEquationMgr = default(EquationMgr);
        private int _fileerror;
        private int _filewarning;
        private List<(string equationName, double value, int index)> _globalVariableData = new List<(string equationName, double value, int index)>();
        private List<TextBox> _textBoxes = new List<TextBox>();
        private List<Label> _labels = new List<Label>();

        public PartGlobalVariables()
        {
            InitializeComponent();
        }

        private void OpenSolidWorksPartFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Reset();

                _filepath = openFileDialog.FileName;

                filePathTextBox.Text = _filepath;

                OpenInSolidWorks();

                ReadInGlobalVariables();

                GenerateTextBoxes(_globalVariableData.Count);
            }
        }

        private void Reset()
        {
            _filepath = "";
            _swModel = default;
            _swEquationMgr = default;
            _globalVariableData.Clear();
            _labels.Clear();
            _textBoxes.Clear();
            int count = Controls.Count;
            List<Control> toRemove = Controls.OfType<Control>().ToList();

            foreach (var control in toRemove)
            {
                if (control.Name.Contains("swLabel") || control.Name.Contains("swText"))
                {
                    Controls.Remove(control);
                    control.Dispose();
                }
            }
        }

        private void GenerateTextBoxes(int count)
        {
            int ySpacing = 30;
            int labelStart = 25;
            int labelWidth = 150;
            int spacing = 10;
            for (int i = 0; i < count; i++)
            {
                Label label = new Label();
                label.Location = new Point(labelStart, ySpacing * i + ySpacing);
                label.Text = _globalVariableData[i].equationName;
                label.TextAlign = ContentAlignment.MiddleRight;
                label.Width = labelWidth;
                label.Name = "swLabel" + i;
                _labels.Add(label);

                TextBox textBox = new TextBox();
                textBox.Location = new Point(labelStart + labelWidth + spacing, ySpacing * i + ySpacing);
                textBox.Text = _globalVariableData[i].value.ToString();
                textBox.TextAlign = HorizontalAlignment.Right;
                textBox.Name = "swText" + i;
                _textBoxes.Add(textBox);
            }

            foreach (Label lab in _labels)
            {
                this.Controls.Add(lab);
            }

            foreach (TextBox box in _textBoxes)
            {
                this.Controls.Add(box);
            }
        }

        private void ReadInGlobalVariables()
        {
            _swEquationMgr = (EquationMgr)_swModel.GetEquationMgr();

            int equationCount = _swEquationMgr.GetCount();

            //_globalVariableData = new (string equationName, double value, int index)[equationCount];

            int index = 0;

            for (int i = 0; i < equationCount; i++)
            {
                if (_swEquationMgr.GlobalVariable[i])
                {
                    _globalVariableData.Add(SplitEquation(i));

                    index++;
                }
            }

        }

        private (string equationName, double value, int index) SplitEquation(int index)
        {

            string[] str = _swEquationMgr.Equation[index].Split("=");

            str[0] = str[0].Replace('"', ' ').Trim();

            str[1] = new String(str[1].Where(c => (Char.IsDigit(c) || c == '.')).ToArray());

            return (str[0], Convert.ToDouble(str[1]), index);
        }

        private void OpenInSolidWorks()
        {
            _swApp = SolidWorksSingleton.GetApplication();

            // open part
            _swModel = _swApp.OpenDoc6(_filepath, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref _fileerror, ref _filewarning);

            // set the current working directory
            _swApp.SetCurrentWorkingDirectory(_swModel.GetPathName().Substring(0, _swModel.GetPathName().LastIndexOf("\\")));
        }

        private void RebuildButton_Click(object sender, EventArgs e)
        {
            StoreNewValues();

            UpdateSolidWorks();

            _swModel.ForceRebuild3(false);
        }

        private void UpdateSolidWorks()
        {
            if (_swEquationMgr is not null)
            {
                for (int index = 0; index < _globalVariableData.Count; index++)
                {
                    _swEquationMgr.Equation[_globalVariableData[index].index] = "\"" + _globalVariableData[index].equationName + "\"" + "=" + _globalVariableData[index].value + "mm";
                }
            }

        }

        private void StoreNewValues()
        {
            for (int index = 0; index < _textBoxes.Count; index++)
            {
                _globalVariableData[index] = (_globalVariableData[index].equationName, Convert.ToDouble(_textBoxes[index].Text), _globalVariableData[index].index);
            }
        }
    }
}
