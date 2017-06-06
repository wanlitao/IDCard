using System;
using System.Windows.Forms;
using IDCard.Reader.Synjones;

namespace IDCard.Reader.Test
{
    public partial class SynjonesTestForm : Form
    {
        private IIDCardReader idCardReader = new SynjonesIDCardReader();

        public SynjonesTestForm()
        {
            InitializeComponent();
        }

        private void btnReadTextPhotoInfo_Click(object sender, EventArgs e)
        {
            var result = idCardReader.ReadBaseTextPhotoInfo();

            TraceResult(result);
        }

        private void TraceResult(IDCardActionResult result)
        {
            var message = result.flag ? "操作成功" : result.msg;

            tbxResultInfo.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}：{message}");
            tbxResultInfo.AppendText(Environment.NewLine);
        }
    }
}
