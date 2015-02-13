using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MiniBotV2
{
    public class TextBoxWriter : TextWriter
    {
        TextBox _output = null;
        public TextBoxWriter(TextBox output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            _output.AppendText(value.ToString()); // TODO: overide the writer in IRC so that it saves the console output to a static class which is then accessed by the main thread to update the text box
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
