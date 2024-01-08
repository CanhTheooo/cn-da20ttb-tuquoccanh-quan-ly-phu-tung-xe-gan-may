using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VehiclePartStore.Ultils
{
    public static class Ultils
    {
        public static DialogResult ShowInfoOKMess(string content = "", string label = "")
        {
            
            return MessageBox.Show(string.IsNullOrWhiteSpace(content) ? "Thành công!": content, string.IsNullOrWhiteSpace(label) ? "Thông Báo" : label, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static DialogResult ShowInfoYesNoMess(string content = "", string label = "")
        {

            return MessageBox.Show(string.IsNullOrWhiteSpace(content) ? "Thành công!" : content, string.IsNullOrWhiteSpace(label) ? "Thông Báo" : label, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

        }
        public static DialogResult ShowErrorOKMess(string content = "", string label = "")
        {
            return MessageBox.Show(string.IsNullOrWhiteSpace(content) ? "Thất bại!\nXin vui lòng thử lại" : content, string.IsNullOrWhiteSpace(label) ? "Lỗi" : label, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static bool IsEqual<T>(T entityBefore, T entityAfter)
        {
            bool isEqual = true;

            string t1 = JsonConvert.SerializeObject(entityBefore);
            string t2 = JsonConvert.SerializeObject(entityAfter);

            if (t1 != t2)
            {
                isEqual = false;
            }
            return isEqual;
        }
    }
}
