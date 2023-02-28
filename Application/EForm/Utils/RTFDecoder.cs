namespace EForm.Utils
{
    public class RTFDecoder
    {
        public static string Decode(string input)
        {
            using (System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox())
            {
                rtBox.Rtf = input;
                return rtBox.Text;
            }
        }
    }
}