using FCP.Util;
using IDCard.Reader.Synjones;
using System;
using System.Windows.Forms;

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
            TraceResult("读卡", result);

            if (result.flag)
            {
                var cardInfo = idCardReader.ParseTextInfo();
                TraceDataMessage("身份证信息", cardInfo);
            }
        }

        private void btnGetBmpPhoto_Click(object sender, EventArgs e)
        {
            var result = idCardReader.ParsePhotoInfo();
            TraceResult("生成Bmp照片", result);
        }

        private void TraceResult(string message, IDCardActionResult result)
        {
            var actionMessage = $"{message}:{(result.flag ? "操作成功" : result.msg)}";

            TraceMessage(actionMessage);
        }

        private void TraceDataMessage<TData>(string message, TData data)
        {
            var dataMessage = $"{message}:{SerializerFactory.JsonSerializer.SerializeString(data)}" ;

            TraceMessage(dataMessage);
        }

        private void TraceMessage(string message)
        {
            tbxResultInfo.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}：{message}");
            tbxResultInfo.AppendText(Environment.NewLine);
        }
    }
}
