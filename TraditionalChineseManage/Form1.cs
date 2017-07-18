using System.Windows.Forms;
using System.Collections.Generic;
using TraditionalChinese;
using System.Text;


namespace TraditionalChineseManage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetMyPinyin_Click(object sender, System.EventArgs e)
        {
            if (txtPinyin1.TextLength == 0)
                return;

            IList<string> myPinyin;

            var success = Traditional.TryGetPinMyPin(txtPinyin1.Text, out myPinyin);

            txtMyPinyin1.Text = success ? myPinyin[0] : @"Not Found";
        }

        private void btnGetTraditional_Click(object sender, System.EventArgs e)
        {
            if (txtChar1.TextLength == 0)
                return;

            IList<string> mySimplified;

            var success = Traditional.TryGetCharSimp(txtChar1.Text, out mySimplified);

            txtSimplified.Text = success ? mySimplified[0] : @"Not Found";
        }

        private void GetPinyinMyPinyin_Click(object sender, System.EventArgs e)
        {
            if (txtBopo1.TextLength == 0)
                return;

            txtMyPinyin2.Text = "";

            IList<string> pinyin;

            var success = Traditional.TryGetBoPin(txtBopo1.Text, out pinyin);

            if (success)
            {
                txtPinyin2.Text = pinyin[0];

                IList<string> myPinyin;

                var success2 = Traditional.TryGetPinMyPin(txtPinyin2.Text, out myPinyin);

                txtMyPinyin2.Text = success2 ? myPinyin[0] : @"Not Found";        
            }

            else
            {
                txtMyPinyin2.Text = @"Not Found";
            }

        }

        private void btnGetBopo_Click(object sender, System.EventArgs e)
        {
            if (txtChar1.TextLength == 0)
                return;

            IList<string> bopo;

            var success = Traditional.TryGetCharBo(txtChar1.Text, out bopo);

            if (success)
            {
                lstBoPo2.Items.Clear();
                var working = new StringBuilder("");
                for (var x = 0; x <= bopo.Count; x++)
                {
                    if (x == 0)
                    {
                        working = new StringBuilder(bopo[x] + " ");
                        continue;
                    }

                    if (x == bopo.Count)
                    {
                        if (working.Length == 0)
                        {
                            continue;
                        }
                        lstBoPo2.Items.Add(working);
                        continue;
                    }

                    if (((x / 5) * 5) == x)
                    {
                        lstBoPo2.Items.Add(working);
                        working = new StringBuilder(bopo[x] + " ");
                    }
                    else
                    {
                        working.Append(bopo[x] + " ");
                    }
                }
            }
            else
            {
                lstBoPo2.Items.Clear();
                lstBoPo2.Items.Add("Not Found");
            }
        }

        private void GetChars_Click(object sender, System.EventArgs e)
        {
            if (txtBopo1.TextLength == 0)
                return;

            IList<string> chars;

            var success = Traditional.TryGetBoChar(txtBopo1.Text, out chars);

            if (success)
            {
                lstChars1.Items.Clear();
                var working = new StringBuilder("");
                for (var x = 0; x <= chars.Count; x++)
                {
                    if (x == 0)
                    {
                        working = new StringBuilder(chars[x] + " ");
                        continue;
                    }

                    if (x == chars.Count)
                    {
                        if (working.Length == 0)
                        {
                            continue;
                        }
                        lstChars1.Items.Add(working);
                        continue;
                    }

                    if (((x / 5) * 5) == x)
                    {
                        lstChars1.Items.Add(working);
                        working = new StringBuilder(chars[x] + " ");
                    }
                    else
                    {
                        working.Append(chars[x] + " ");
                    }
                }
            }
            else
            {
                lstChars1.Items.Clear();
                lstChars1.Items.Add("Not Found");
            }
        }

        private void GetPinyinMyPinyin2_Click(object sender, System.EventArgs e)
        {
            if (txtChar1.TextLength == 0)
                return;

            IList<string> pinyin;

            var success = Traditional.TryGetCharPin(txtChar1.Text, out pinyin);

            if (success)
            {
                lstPin1.Items.Clear();
                lstMyPin1.Items.Clear();
                var working = new StringBuilder("");
                var working2 = new StringBuilder("");
                for (var x = 0; x <= pinyin.Count; x++)
                {
                    IList<string> myPinyin;
                    if (x == 0)
                    {
                        working = new StringBuilder(pinyin[x] + " ");

                        var success2 = Traditional.TryGetPinMyPin(pinyin[x], out myPinyin);
                        if (success2)
                        {
                            working2 = new StringBuilder(myPinyin[0] + " ");
                        }

                        continue;
                    }

                    if (x == pinyin.Count)
                    {
                        if (working.Length == 0)
                        {
                            continue;
                        }
                        lstPin1.Items.Add(working);
                        lstMyPin1.Items.Add(working2);
                        continue;
                    }

                    if (((x / 3) * 3) == x)
                    {
                        lstPin1.Items.Add(working);
                        lstMyPin1.Items.Add(working2);
                        working = new StringBuilder(pinyin[x] + " ");
                        var success2 = Traditional.TryGetPinMyPin(pinyin[x], out myPinyin);
                        if (success2)
                        {
                            working2 = new StringBuilder(myPinyin[0] + " ");
                        }
                    }
                    else
                    {
                        working.Append(pinyin[x] + " ");
                        var success2 = Traditional.TryGetPinMyPin(pinyin[x], out myPinyin);
                        if (success2)
                        {
                            working2.Append(myPinyin[0] + " ");
                        }
                    }
                }
            }
            else
            {
                lstPin1.Items.Clear();
                lstMyPin1.Items.Clear();
                lstPin1.Items.Add("Not Found");
                lstMyPin1.Items.Add("Not Found");
            }
        }

        private void GetCji_Click(object sender, System.EventArgs e)
        {
            if (txtChar1.TextLength == 0)
                return;

            IList<string> cji;

            var success = Traditional.TryGetCharCji(txtChar1.Text, out cji);

            txtCji.Text = success ? cji[0] : @"Not Found";
        }

        private void btnGetBopo2_Click(object sender, System.EventArgs e)
        {
            if (txtPinyin1.TextLength == 0)
                return;

            IList<string> bopo;

            var success = Traditional.TryGetPinBo(txtPinyin1.Text, out bopo);

            txtBopo4.Text = success ? bopo[0] : @"Not Found";
        }

        private void btnGetFEI_Click(object sender, System.EventArgs e)
        {
            if (txtChar1.TextLength == 0)
                return;

            IList<string> fei;

            var success = Traditional.TryGetCharFei(txtChar1.Text, out fei);

            txtFEIndex.Text = success ? fei[0] : @"Not Found";
        }

        private void GetEnglish_Click(object sender, System.EventArgs e)
        {
            if (txtChar1.TextLength == 0)
                return;

            IList<string> english;

            var success = Traditional.TryGetCharEnglish(txtChar1.Text, out english);

            txtEnglish.Text = success ? english[0] : @"Not Found";
        }

		private void GetFromCji_Click(object sender, System.EventArgs e)
		{
			listChars2.Items.Clear();
			if (txtCji1.Text.Length < 1)
				return;

			IList<char> fromCjis;

			var success = Traditional.TryGetCjiChar(txtCji1.Text, out fromCjis);

			if (!success)
				return;

            listChars2.Items.Clear();
		    foreach (var chinese in fromCjis)
		    {
		        listChars2.Items.Add(chinese);
		        listChars2.Items.Add(" ");
		    }
		}

        private void GetPinChars_Click(object sender, System.EventArgs e)
        {
            if (txtPinyin1.TextLength == 0)
                return;

            IList<string> chars;

            var success = Traditional.TryGetPinChar(txtPinyin1.Text, out chars);

            if (success)
            {
                listChars3.Items.Clear();
                listChars3.LabelEdit = true;
                listChars3.BeginUpdate();
                string working = "";
                for (var x = 0; x <= chars.Count; x++)
                {
                    if (x == 0)
                    {
                        working = chars[x] + " ";
                        continue;
                    }

                    if (x == chars.Count)
                    {
                        if (working.Length == 0)
                        {
                            continue;
                        }
                        listChars3.Items.Add(working);
                        continue;
                    }

                    if (((x / 5) * 5) == x)
                    {
                        listChars3.Items.Add(working);
                        working = chars[x] + " ";
                    }
                    else
                    {
                        working = working + chars[x] + " ";
                    }
                }
            }
            else
            {
                listChars3.Items.Clear();
                listChars3.Items.Add("Not Found");
            }
            listChars3.EndUpdate();
        }

        private void GetPinCrit_Click(object sender, System.EventArgs e)
        {
            if (txtPinyin1.TextLength == 0)
                return;

            IList<string> crit;

            var success = Traditional.TryGetPinCrit(txtPinyin1.Text, out crit);

            txtCrit1.Text = success ? crit[0] : @"Not Found";
        }

        private void btnGetCharCrit_Click(object sender, System.EventArgs e)
        {
            if (txtChar1.TextLength > 1)
            {
                txtChar1.Text = @"Max 1 Char";
                return;
            }

            IList<string> crit;
            bool found = Traditional.TryGetCharCrit(txtChar1.Text, out crit);

            if (!found)
            {
                listCrits1.Items.Clear();
                listCrits1.Items.Add("Not Found");
                return;
            }

            foreach (var onecrit in crit)
            {
                listCrits1.Items.Add(onecrit);
            }
        }

        private void btnGetBopoCrit_Click(object sender, System.EventArgs e)
        {
            var result = "";
            if (txtBopo1.TextLength == 0 || txtBopo1.TextLength > 4)
            {
                txtCrit2.Text = @"Invalid Bopo";
                return;
            }

            bool found = Traditional.TryGetBopoCrit(txtBopo1.Text,  out result);

            if (!found)
            {
                txtCrit2.Text = @"Not found";
                return;
            }
            txtCrit2.Text = result;
        }

        private void btnGetChars1_Click(object sender, System.EventArgs e)
        {
            listChars4.Items.Clear();
            if (txtCrit3.TextLength == 0 || txtCrit3.TextLength > 5)
            {
                listChars4.Items.Add("Invalid Crit");
                return;
            }
            IList<string> charList = new List<string>();
            bool found = Traditional.TryGetCritChar(txtCrit3.Text, out charList);

            if (!found)
            {
                listChars4.Items.Clear();
                listChars4.Items.Add("Not found");
                return;
            }

            foreach (var oneChar in charList)
            {
                listChars4.Items.Add(oneChar);
            }
            return;
        }

        private void btnCritBopo_Click(object sender, System.EventArgs e)
        {
            var result = "";
            if (txtCrit3.TextLength == 0 || txtCrit3.TextLength > 4)
            {
                txtCritBopo.Text = @"Invalid Bopo";
                return;
            }

            bool found = Traditional.TryGetCritBopo(txtCrit3.Text, out result);

            if (!found)
            {
                txtCritBopo.Text = @"Not found";
                return;
            }
            txtCritBopo.Text = result;
        }

        private void btnCritPin_Click(object sender, System.EventArgs e)
        {
            var result = "";
            if (txtCrit3.TextLength == 0 || txtCrit3.TextLength > 4)
            {
                txtCritPin.Text = @"Invalid Bopo";
                return;
            }

            bool found = Traditional.TryGetCritPin(txtCrit3.Text, out result);

            if (!found)
            {
                txtCrit3.Text = @"Not found";
                return;
            }
            txtCritPin.Text = result;
        }

    }
}
