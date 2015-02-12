using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace MiniBotV2
{
    class TextBoxStreamWriter : TextWriter
    {
        TextBox _output = null;
        public TextBoxStreamWriter(TextBox output)
        {
            _output = output;
        }

        public override void Writer(char value)
        {
            base.Writer(value);
            _output.AppendText(value.ToString());
        }

        public override Encoding Encoding
        {
            get { return System.Text.Enconding.UTF8; }
        }

    }


}
