using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace MUISModifier_new
{
	public partial class Form1 : Form
	{

		bool SaveFile(string ffile)
		{
			string sssst = "";

			for (int n = 0; n < 64; n = n + 1)
			{
				string DA = "0000" + D_ScenID[n];
				string DB = "0000" + D_ButtonID[n];
				string DC = "0000" + D_ObjTextID[n];
				string DD = "0000" + D_TutTextID[n];
				string DE = "0000" + D_DriftID[n];

				D_ScenID[n] = DA.Substring(DA.Length - 4);
				D_ButtonID[n] = DB.Substring(DB.Length - 4);
				D_ObjTextID[n] = DC.Substring(DC.Length - 4);
				D_TutTextID[n] = DD.Substring(DD.Length - 4);
				D_DriftID[n] = DE.Substring(DE.Length - 4);

				sssst = sssst + D_ScenID[n] + D_ButtonID[n] + D_ObjTextID[n] + D_TutTextID[n] + D_DriftID[n];
			}

			byte[] Savebytes = null;
			Savebytes = new byte[sssst.Length/2];

			for (int n = 0; n < sssst.Length; n = n + 2)
			{
				string ffd = sssst.Substring(n, 2);
				Savebytes[n / 2] = Convert.ToByte(ffd, 16);
			}
			

			File.WriteAllBytes(ffile, Savebytes);

			Text_ScenID.Text = D_ScenID[MMission];
			Text_ButtonID.Text = D_ButtonID[MMission];
			Text_ObjTextID.Text = D_ObjTextID[MMission];
			Text_TutTextID.Text = D_TutTextID[MMission];
			Text_DriftID.Text = D_DriftID[MMission];

			return true;
		}

		public Form1()
		{
			InitializeComponent();
		}

		

		private void ChangeRadio(object sender, EventArgs e)
		{
			int ML = 0;
			int MM = 0;

			if (RadioLevel_0.Checked == true) { ML = 0; }
			if (RadioLevel_1.Checked == true) { ML = 1; }
			if (RadioLevel_2.Checked == true) { ML = 2; }
			if (RadioLevel_3.Checked == true) { ML = 3; }
			if (RadioLevel_4.Checked == true) { ML = 4; }
			if (RadioLevel_5.Checked == true) { ML = 5; }
			if (RadioLevel_6.Checked == true) { ML = 6; }
			if (RadioLevel_7.Checked == true) { ML = 7; }

			if (RadioMission_0.Checked == true) { MM = 0; }
			if (RadioMission_1.Checked == true) { MM = 1; }
			if (RadioMission_2.Checked == true) { MM = 2; }
			if (RadioMission_3.Checked == true) { MM = 3; }
			if (RadioMission_4.Checked == true) { MM = 4; }
			if (RadioMission_5.Checked == true) { MM = 5; }
			if (RadioMission_6.Checked == true) { MM = 6; }
			if (RadioMission_7.Checked == true) { MM = 7; }

			MMission = ML * 8 + MM;

			Text_ScenID.Text = D_ScenID[MMission];
			Text_ButtonID.Text = D_ButtonID[MMission];
			Text_ObjTextID.Text = D_ObjTextID[MMission];
			Text_TutTextID.Text = D_TutTextID[MMission];
			Text_DriftID.Text = D_DriftID[MMission];
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult = MessageBox.Show("Any unsaved changes will be lost. Is this OK?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (DialogResult == DialogResult.Yes)
			{
				Application.Exit();
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Text_ScenID.Text = D_ScenID[MMission];
			Text_ButtonID.Text = D_ButtonID[MMission];
			Text_ObjTextID.Text = D_ObjTextID[MMission];
			Text_TutTextID.Text = D_TutTextID[MMission];
			Text_DriftID.Text = D_DriftID[MMission];
			Text = OpenF + " - " + Pname;
		}

		private void Text_ScenID_TextChanged(object sender, EventArgs e)
		{
			Text_ScenID.Text=ToHexUint16(Text_ScenID.Text);
			D_ScenID[MMission]= Text_ScenID.Text;
		}

		private void Text_ButtonID_TextChanged(object sender, EventArgs e)
		{
			Text_ButtonID.Text = ToHexUint16(Text_ButtonID.Text);
			D_ButtonID[MMission] = Text_ButtonID.Text;
		}

		private void Text_ObjTextID_TextChanged(object sender, EventArgs e)
		{
			Text_ObjTextID.Text = ToHexUint16(Text_ObjTextID.Text);
			D_ObjTextID[MMission] = Text_ObjTextID.Text;
		}

		private void Text_TutTextID_TextChanged(object sender, EventArgs e)
		{
			Text_TutTextID.Text = ToHexUint16(Text_TutTextID.Text);
			D_TutTextID[MMission] = Text_TutTextID.Text;
		}

		private void Text_DriftID_TextChanged(object sender, EventArgs e)
		{
			Text_DriftID.Text = ToHexUint16(Text_DriftID.Text);
			D_DriftID[MMission] = Text_DriftID.Text;
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
			SaveFileDialog1.InitialDirectory = "c:\\";
			SaveFileDialog1.Filter = "BIN File (*.bin)|*.bin";
			SaveFileDialog1.FilterIndex = 1;
			SaveFileDialog1.RestoreDirectory = true;
			if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					SaveFile(SaveFileDialog1.FileName);
					OpenF = SaveFileDialog1.FileName; Text = OpenF + " - " + Pname;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (File.Exists(OpenF))
			{
				SaveFile(OpenF);
				Text = OpenF + " - " + Pname;
			}
			else
			{
				SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
				SaveFileDialog1.InitialDirectory = "c:\\";
				SaveFileDialog1.Filter = "BIN File (*.bin)|*.bin";
				SaveFileDialog1.FilterIndex = 1;
				SaveFileDialog1.RestoreDirectory = true;
				if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					try
					{
						SaveFile(SaveFileDialog1.FileName);
						OpenF = SaveFileDialog1.FileName; Text = OpenF + " - " + Pname;
					}
					catch (Exception ex)
					{
						MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
					}
				}
			}
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult = MessageBox.Show("Any unsaved changes will be lost. Is this OK?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (DialogResult == DialogResult.Yes)
			{
				OpenF = "Untitled"; Text = OpenF + " - " + Pname;

				string[] D_ScenID = {
					"0000", "0001", "0002", "0003", "0004", "0005", "0006", "0007",
					"0008", "0009", "000A", "000B", "000C", "000D", "000E", "000F",
					"0010", "0011", "0012", "0013", "0014", "0015", "0016", "0017",
					"0018", "0019", "001A", "001B", "001C", "001D", "001E", "001F",
					"0020", "0021", "0022", "0023", "0024", "0025", "0026", "0027",
					"0028", "0029", "002A", "002B", "002C", "002D", "002E", "002F",
					"0030", "0031", "0032", "0033", "0034", "0035", "0036", "0037",
					"0038", "0039", "003A", "003B", "003C", "003D", "003E", "003F"
				};

				string[] D_ButtonID = {
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000"
				};

				string[] D_ObjTextID = {
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000"
				};

				string[] D_TutTextID = {
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000"
				};

				string[] D_DriftID = {
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000",
					"0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000"
				};

				Text_ScenID.Text = D_ScenID[MMission];
				Text_ButtonID.Text = D_ButtonID[MMission];
				Text_ObjTextID.Text = D_ObjTextID[MMission];
				Text_TutTextID.Text = D_TutTextID[MMission];
				Text_DriftID.Text = D_DriftID[MMission];
			}
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult = MessageBox.Show("Any unsaved changes will be lost. Is this OK?", "Warning",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (DialogResult == DialogResult.Yes)
			{
				OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
				OpenFileDialog1.InitialDirectory = "c:\\";
				OpenFileDialog1.Filter = "BIN File (*.bin)|*.bin";
				OpenFileDialog1.FilterIndex = 1;
				OpenFileDialog1.RestoreDirectory = true;
				if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
				{
					try
					{
						//MessageBox.Show(OpenFileDialog1.FileName);
						string Ofile = OpenFileDialog1.FileName;
						OpenF = Ofile; Text = OpenF + " - " + Pname;
						using (BinaryReader b = new BinaryReader(File.Open(OpenFileDialog1.FileName, FileMode.Open)))
						{

							int length = (int)b.BaseStream.Length;

							if (length == 640)
							{
								for (int nn = 0; nn < 64; nn = nn + 1)
								{
									byte[] Filebyte = null;
									Filebyte = new byte[10];
									Filebyte[0] = b.ReadByte();
									Filebyte[1] = b.ReadByte();
									Filebyte[2] = b.ReadByte();
									Filebyte[3] = b.ReadByte();
									Filebyte[4] = b.ReadByte();
									Filebyte[5] = b.ReadByte();
									Filebyte[6] = b.ReadByte();
									Filebyte[7] = b.ReadByte();
									Filebyte[8] = b.ReadByte();
									Filebyte[9] = b.ReadByte();
									string entry = BitConverter.ToString(Filebyte);
									entry = entry.Replace("-", "").ToUpper();
									D_ScenID[nn]= entry.Substring(0, 4);
									D_ButtonID[nn]= entry.Substring(4, 4);
									D_ObjTextID[nn]= entry.Substring(8, 4);
									D_TutTextID[nn]= entry.Substring(12, 4);
									D_DriftID[nn]= entry.Substring(16, 4);
								}
								Text_ScenID.Text = D_ScenID[MMission];
								Text_ButtonID.Text = D_ButtonID[MMission];
								Text_ObjTextID.Text = D_ObjTextID[MMission];
								Text_TutTextID.Text = D_TutTextID[MMission];
								Text_DriftID.Text = D_DriftID[MMission];
							}
							else
							{
								if (length < 640)
									MessageBox.Show("Files must be 640 bytes in size.", "File too small", MessageBoxButtons.OK, MessageBoxIcon.Error);
								if (length > 640)
									MessageBox.Show("Files must be 640 bytes in size.", "File too large", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
					}
				}
			}
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Creator: TheZACAtac\nVersion: Beta\nWiki: wiki.tockdom.com/wiki/MUIS_Modifier", "About MUIS Modifier");
		}
	}
}
