/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 07.01.2021
 * Time: 9:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ArcConfig
{
	partial class FormArcGinfo
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TextBox textBoxID;
		private System.Windows.Forms.TextBox textBoxDEPTH;
		private System.Windows.Forms.TextBox textBoxDEPTH_LOCAL;
		private System.Windows.Forms.TextBox textBoxCACHE_SIZE;
		private System.Windows.Forms.TextBox textBoxCACHE_TIMEOUT;
		private System.Windows.Forms.TextBox textBoxFLUSH_INTERVAL;
		private System.Windows.Forms.TextBox textBoxSTACK_INTERVAL;
		private System.Windows.Forms.TextBox textBoxRESTORE_INTERVAL;
		private System.Windows.Forms.TextBox textBoxRESTORE_TIME;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBoxID_TYPE;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.CheckBox checkBoxWRITE_MINMAX;
		private System.Windows.Forms.TextBox textBoxRESTORE_TIME_LOCAL;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.CheckedListBox checkedListBoxSTATE;
		private System.Windows.Forms.TextBox textBoxDEPTH_PARTITION;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.TextBox textBoxSTATE;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.TextBox textBoxN;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Data.DataSet dataSet1;
		private System.Windows.Forms.Button buttonCalc;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Button buttonCass;
		private System.Windows.Forms.PictureBox pictureBox1;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormArcGinfo));
			this.textBoxID = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBoxID_TYPE = new System.Windows.Forms.ComboBox();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button5 = new System.Windows.Forms.Button();
			this.buttonCalc = new System.Windows.Forms.Button();
			this.label26 = new System.Windows.Forms.Label();
			this.textBoxN = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.buttonCass = new System.Windows.Forms.Button();
			this.label19 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.textBoxSTACK_INTERVAL = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textBoxDEPTH_PARTITION = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxDEPTH_LOCAL = new System.Windows.Forms.TextBox();
			this.textBoxDEPTH = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label22 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.textBoxRESTORE_TIME_LOCAL = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxRESTORE_TIME = new System.Windows.Forms.TextBox();
			this.textBoxRESTORE_INTERVAL = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textBoxSTATE = new System.Windows.Forms.TextBox();
			this.checkBoxWRITE_MINMAX = new System.Windows.Forms.CheckBox();
			this.label14 = new System.Windows.Forms.Label();
			this.checkedListBoxSTATE = new System.Windows.Forms.CheckedListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.textBoxFLUSH_INTERVAL = new System.Windows.Forms.TextBox();
			this.textBoxCACHE_TIMEOUT = new System.Windows.Forms.TextBox();
			this.textBoxCACHE_SIZE = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.label27 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.dataSet1 = new System.Data.DataSet();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.groupBox1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// textBoxID
			// 
			this.textBoxID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxID.Location = new System.Drawing.Point(47, 16);
			this.textBoxID.Name = "textBoxID";
			this.textBoxID.ReadOnly = true;
			this.textBoxID.Size = new System.Drawing.Size(50, 20);
			this.textBoxID.TabIndex = 0;
			this.textBoxID.Text = "0";
			this.textBoxID.MouseHover += new System.EventHandler(this.TextBoxIDMouseHover);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(11, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(31, 16);
			this.label1.TabIndex = 11;
			this.label1.Text = "id =";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(423, 1);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(115, 17);
			this.label3.TabIndex = 13;
			this.label3.Text = "Вид записи данных";
			// 
			// comboBoxID_TYPE
			// 
			this.comboBoxID_TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxID_TYPE.FormattingEnabled = true;
			this.comboBoxID_TYPE.Items.AddRange(new object[] {
			"Непрерывный архив",
			"Запись по событию"});
			this.comboBoxID_TYPE.Location = new System.Drawing.Point(423, 15);
			this.comboBoxID_TYPE.Name = "comboBoxID_TYPE";
			this.comboBoxID_TYPE.Size = new System.Drawing.Size(157, 21);
			this.comboBoxID_TYPE.TabIndex = 14;
			this.comboBoxID_TYPE.SelectedIndexChanged += new System.EventHandler(this.ComboBoxID_TYPESelectedIndexChanged);
			this.comboBoxID_TYPE.MouseHover += new System.EventHandler(this.ComboBoxID_TYPEMouseHover);
			// 
			// textBoxName
			// 
			this.textBoxName.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.textBoxName.Location = new System.Drawing.Point(103, 16);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(301, 20);
			this.textBoxName.TabIndex = 25;
			this.textBoxName.MouseHover += new System.EventHandler(this.TextBoxNameMouseHover);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(127, 1);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(183, 15);
			this.label13.TabIndex = 27;
			this.label13.Text = "Наименование профиля архива:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.button5);
			this.groupBox1.Controls.Add(this.buttonCalc);
			this.groupBox1.Controls.Add(this.label26);
			this.groupBox1.Controls.Add(this.textBoxN);
			this.groupBox1.Controls.Add(this.label25);
			this.groupBox1.Controls.Add(this.label17);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.groupBox4);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textBoxDEPTH_LOCAL);
			this.groupBox1.Controls.Add(this.textBoxDEPTH);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.groupBox1.Location = new System.Drawing.Point(11, 294);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(295, 176);
			this.groupBox1.TabIndex = 33;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Основа";
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button5.Location = new System.Drawing.Point(238, 82);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(51, 23);
			this.button5.TabIndex = 46;
			this.button5.Text = "if>99";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.Button5Click);
			// 
			// buttonCalc
			// 
			this.buttonCalc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonCalc.Location = new System.Drawing.Point(238, 13);
			this.buttonCalc.Name = "buttonCalc";
			this.buttonCalc.Size = new System.Drawing.Size(51, 23);
			this.buttonCalc.TabIndex = 45;
			this.buttonCalc.Text = "Calc";
			this.buttonCalc.UseVisualStyleBackColor = true;
			this.buttonCalc.Click += new System.EventHandler(this.ButtonCalcClick);
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(179, 87);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(66, 18);
			this.label26.TabIndex = 44;
			this.label26.Text = "=D*60/Dp";
			// 
			// textBoxN
			// 
			this.textBoxN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxN.Location = new System.Drawing.Point(128, 84);
			this.textBoxN.Name = "textBoxN";
			this.textBoxN.ReadOnly = true;
			this.textBoxN.Size = new System.Drawing.Size(50, 20);
			this.textBoxN.TabIndex = 43;
			this.textBoxN.MouseHover += new System.EventHandler(this.TextBoxNMouseHover);
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(1, 87);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(121, 15);
			this.label25.TabIndex = 42;
			this.label25.Text = "Число партиций=1..63";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(148, 54);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(40, 20);
			this.label17.TabIndex = 41;
			this.label17.Text = "часов";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(148, 18);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(86, 29);
			this.label11.TabIndex = 40;
			this.label11.Text = "часов (min=24, max=0=100000)";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.buttonCass);
			this.groupBox4.Controls.Add(this.label19);
			this.groupBox4.Controls.Add(this.label18);
			this.groupBox4.Controls.Add(this.textBoxSTACK_INTERVAL);
			this.groupBox4.Controls.Add(this.label10);
			this.groupBox4.Controls.Add(this.textBoxDEPTH_PARTITION);
			this.groupBox4.Controls.Add(this.label15);
			this.groupBox4.Location = new System.Drawing.Point(7, 105);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(282, 65);
			this.groupBox4.TabIndex = 39;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Партиции для мгновенных";
			// 
			// buttonCass
			// 
			this.buttonCass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonCass.Location = new System.Drawing.Point(231, 42);
			this.buttonCass.Name = "buttonCass";
			this.buttonCass.Size = new System.Drawing.Size(51, 22);
			this.buttonCass.TabIndex = 43;
			this.buttonCass.Text = "Cass";
			this.buttonCass.UseVisualStyleBackColor = true;
			this.buttonCass.Click += new System.EventHandler(this.ButtonCassClick);
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label19.Location = new System.Drawing.Point(196, 42);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(28, 16);
			this.label19.TabIndex = 42;
			this.label19.Text = "сек";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(196, 13);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(80, 26);
			this.label18.TabIndex = 41;
			this.label18.Text = "мин (min=10, max= 525600)";
			// 
			// textBoxSTACK_INTERVAL
			// 
			this.textBoxSTACK_INTERVAL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxSTACK_INTERVAL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxSTACK_INTERVAL.Location = new System.Drawing.Point(134, 38);
			this.textBoxSTACK_INTERVAL.Name = "textBoxSTACK_INTERVAL";
			this.textBoxSTACK_INTERVAL.Size = new System.Drawing.Size(58, 20);
			this.textBoxSTACK_INTERVAL.TabIndex = 37;
			this.textBoxSTACK_INTERVAL.Text = "0";
			this.textBoxSTACK_INTERVAL.MouseHover += new System.EventHandler(this.TextBoxSTACK_INTERVALMouseHover);
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label10.Location = new System.Drawing.Point(2, 34);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(128, 26);
			this.label10.TabIndex = 36;
			this.label10.Text = "Период разбора (tp) ( накопления):";
			// 
			// textBoxDEPTH_PARTITION
			// 
			this.textBoxDEPTH_PARTITION.Location = new System.Drawing.Point(121, 13);
			this.textBoxDEPTH_PARTITION.Name = "textBoxDEPTH_PARTITION";
			this.textBoxDEPTH_PARTITION.Size = new System.Drawing.Size(70, 20);
			this.textBoxDEPTH_PARTITION.TabIndex = 33;
			this.textBoxDEPTH_PARTITION.Text = "0";
			this.textBoxDEPTH_PARTITION.MouseHover += new System.EventHandler(this.TextBoxDEPTH_PARTITIONMouseHover);
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(6, 16);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(119, 17);
			this.label15.TabIndex = 32;
			this.label15.Text = "Глубина (среза) (Dp):";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(7, 50);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(66, 29);
			this.label5.TabIndex = 31;
			this.label5.Text = "Глубина (dl) (локально):";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(2, 27);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 18);
			this.label4.TabIndex = 30;
			this.label4.Text = "Глубина(D):";
			// 
			// textBoxDEPTH_LOCAL
			// 
			this.textBoxDEPTH_LOCAL.Location = new System.Drawing.Point(76, 51);
			this.textBoxDEPTH_LOCAL.Name = "textBoxDEPTH_LOCAL";
			this.textBoxDEPTH_LOCAL.Size = new System.Drawing.Size(66, 20);
			this.textBoxDEPTH_LOCAL.TabIndex = 24;
			this.textBoxDEPTH_LOCAL.Text = "0";
			this.textBoxDEPTH_LOCAL.MouseHover += new System.EventHandler(this.TextBoxDEPTH_LOCALMouseHover);
			// 
			// textBoxDEPTH
			// 
			this.textBoxDEPTH.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxDEPTH.Location = new System.Drawing.Point(76, 25);
			this.textBoxDEPTH.Name = "textBoxDEPTH";
			this.textBoxDEPTH.Size = new System.Drawing.Size(66, 20);
			this.textBoxDEPTH.TabIndex = 23;
			this.textBoxDEPTH.Text = "0";
			this.textBoxDEPTH.MouseHover += new System.EventHandler(this.TextBoxDEPTHMouseHover);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label22);
			this.groupBox2.Controls.Add(this.label21);
			this.groupBox2.Controls.Add(this.label20);
			this.groupBox2.Controls.Add(this.label16);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.textBoxRESTORE_TIME_LOCAL);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.textBoxRESTORE_TIME);
			this.groupBox2.Controls.Add(this.textBoxRESTORE_INTERVAL);
			this.groupBox2.Location = new System.Drawing.Point(11, 486);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(245, 101);
			this.groupBox2.TabIndex = 34;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Опции для сервера восстановления архива";
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(211, 74);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(28, 20);
			this.label22.TabIndex = 45;
			this.label22.Text = "сек";
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(211, 48);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(28, 20);
			this.label21.TabIndex = 44;
			this.label21.Text = "сек";
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(211, 22);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(28, 20);
			this.label20.TabIndex = 43;
			this.label20.Text = "сек";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(6, 68);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(132, 26);
			this.label16.TabIndex = 38;
			this.label16.Text = "Период восстановления из локального:";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(7, 48);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(134, 20);
			this.label12.TabIndex = 37;
			this.label12.Text = "Период восстановления:";
			// 
			// textBoxRESTORE_TIME_LOCAL
			// 
			this.textBoxRESTORE_TIME_LOCAL.Location = new System.Drawing.Point(143, 74);
			this.textBoxRESTORE_TIME_LOCAL.Name = "textBoxRESTORE_TIME_LOCAL";
			this.textBoxRESTORE_TIME_LOCAL.Size = new System.Drawing.Size(66, 20);
			this.textBoxRESTORE_TIME_LOCAL.TabIndex = 36;
			this.textBoxRESTORE_TIME_LOCAL.Text = "0";
			this.textBoxRESTORE_TIME_LOCAL.MouseHover += new System.EventHandler(this.TextBoxRESTORE_TIME_LOCALMouseHover);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(7, 25);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(104, 20);
			this.label9.TabIndex = 35;
			this.label9.Text = "Период контроля:";
			// 
			// textBoxRESTORE_TIME
			// 
			this.textBoxRESTORE_TIME.Location = new System.Drawing.Point(143, 48);
			this.textBoxRESTORE_TIME.Name = "textBoxRESTORE_TIME";
			this.textBoxRESTORE_TIME.Size = new System.Drawing.Size(66, 20);
			this.textBoxRESTORE_TIME.TabIndex = 34;
			this.textBoxRESTORE_TIME.Text = "0";
			this.textBoxRESTORE_TIME.MouseHover += new System.EventHandler(this.TextBoxRESTORE_TIMEMouseHover);
			// 
			// textBoxRESTORE_INTERVAL
			// 
			this.textBoxRESTORE_INTERVAL.Location = new System.Drawing.Point(143, 22);
			this.textBoxRESTORE_INTERVAL.Name = "textBoxRESTORE_INTERVAL";
			this.textBoxRESTORE_INTERVAL.Size = new System.Drawing.Size(66, 20);
			this.textBoxRESTORE_INTERVAL.TabIndex = 33;
			this.textBoxRESTORE_INTERVAL.Text = "0";
			this.textBoxRESTORE_INTERVAL.MouseHover += new System.EventHandler(this.TextBoxRESTORE_INTERVALMouseHover);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textBoxSTATE);
			this.groupBox3.Controls.Add(this.checkBoxWRITE_MINMAX);
			this.groupBox3.Controls.Add(this.label14);
			this.groupBox3.Controls.Add(this.checkedListBoxSTATE);
			this.groupBox3.Location = new System.Drawing.Point(312, 294);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(368, 176);
			this.groupBox3.TabIndex = 35;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Дополнительные";
			// 
			// textBoxSTATE
			// 
			this.textBoxSTATE.Location = new System.Drawing.Point(97, 19);
			this.textBoxSTATE.Name = "textBoxSTATE";
			this.textBoxSTATE.ReadOnly = true;
			this.textBoxSTATE.Size = new System.Drawing.Size(83, 20);
			this.textBoxSTATE.TabIndex = 34;
			// 
			// checkBoxWRITE_MINMAX
			// 
			this.checkBoxWRITE_MINMAX.Location = new System.Drawing.Point(6, 155);
			this.checkBoxWRITE_MINMAX.Name = "checkBoxWRITE_MINMAX";
			this.checkBoxWRITE_MINMAX.Size = new System.Drawing.Size(254, 15);
			this.checkBoxWRITE_MINMAX.TabIndex = 33;
			this.checkBoxWRITE_MINMAX.Text = "Записывать минимальное и максимальное значение на интервале";
			this.checkBoxWRITE_MINMAX.UseVisualStyleBackColor = true;
			this.checkBoxWRITE_MINMAX.CheckedChanged += new System.EventHandler(this.CheckBoxWRITE_MINMAXCheckedChanged);
			this.checkBoxWRITE_MINMAX.MouseHover += new System.EventHandler(this.CheckBoxWRITE_MINMAXMouseHover);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(6, 21);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(85, 18);
			this.label14.TabIndex = 31;
			this.label14.Text = "опции архива =";
			// 
			// checkedListBoxSTATE
			// 
			this.checkedListBoxSTATE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left)));
			this.checkedListBoxSTATE.FormattingEnabled = true;
			this.checkedListBoxSTATE.Items.AddRange(new object[] {
			"1"});
			this.checkedListBoxSTATE.Location = new System.Drawing.Point(6, 42);
			this.checkedListBoxSTATE.Name = "checkedListBoxSTATE";
			this.checkedListBoxSTATE.Size = new System.Drawing.Size(356, 109);
			this.checkedListBoxSTATE.TabIndex = 30;
			this.checkedListBoxSTATE.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckedListBoxSTATEItemCheck);
			this.checkedListBoxSTATE.SelectedIndexChanged += new System.EventHandler(this.CheckedListBoxSTATESelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(11, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(154, 16);
			this.label2.TabIndex = 36;
			this.label2.Text = "Тип хранимых данных:";
			// 
			// textBoxFLUSH_INTERVAL
			// 
			this.textBoxFLUSH_INTERVAL.Location = new System.Drawing.Point(178, 68);
			this.textBoxFLUSH_INTERVAL.Name = "textBoxFLUSH_INTERVAL";
			this.textBoxFLUSH_INTERVAL.Size = new System.Drawing.Size(69, 20);
			this.textBoxFLUSH_INTERVAL.TabIndex = 37;
			this.textBoxFLUSH_INTERVAL.Text = "0";
			this.toolTip1.SetToolTip(this.textBoxFLUSH_INTERVAL, "FLUSH_INTERVAL = Период (сек) чистки устаревших данных. Значение 0 - не исполняет" +
		"ся");
			// 
			// textBoxCACHE_TIMEOUT
			// 
			this.textBoxCACHE_TIMEOUT.Location = new System.Drawing.Point(178, 45);
			this.textBoxCACHE_TIMEOUT.Name = "textBoxCACHE_TIMEOUT";
			this.textBoxCACHE_TIMEOUT.Size = new System.Drawing.Size(69, 20);
			this.textBoxCACHE_TIMEOUT.TabIndex = 36;
			this.textBoxCACHE_TIMEOUT.Text = "0";
			this.toolTip1.SetToolTip(this.textBoxCACHE_TIMEOUT, "CACHE_TIMEOUT  = Промежуток времени (сек), через который сервер записи архивов от" +
		"правляет данные на вставку серверу прямого доступа");
			// 
			// textBoxCACHE_SIZE
			// 
			this.textBoxCACHE_SIZE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxCACHE_SIZE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxCACHE_SIZE.Location = new System.Drawing.Point(178, 19);
			this.textBoxCACHE_SIZE.Name = "textBoxCACHE_SIZE";
			this.textBoxCACHE_SIZE.Size = new System.Drawing.Size(69, 20);
			this.textBoxCACHE_SIZE.TabIndex = 35;
			this.textBoxCACHE_SIZE.Text = "0";
			this.toolTip1.SetToolTip(this.textBoxCACHE_SIZE, "CACHE_SIZE = Объем данных (\"строк\"), при котором сервер записи архивов отправляет" +
		" данные на вставку серверу прямого доступа");
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(657, 58);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(23, 23);
			this.button1.TabIndex = 39;
			this.button1.Text = "+";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.label27);
			this.groupBox5.Controls.Add(this.label24);
			this.groupBox5.Controls.Add(this.label23);
			this.groupBox5.Controls.Add(this.label8);
			this.groupBox5.Controls.Add(this.label7);
			this.groupBox5.Controls.Add(this.label6);
			this.groupBox5.Controls.Add(this.textBoxFLUSH_INTERVAL);
			this.groupBox5.Controls.Add(this.textBoxCACHE_TIMEOUT);
			this.groupBox5.Controls.Add(this.textBoxCACHE_SIZE);
			this.groupBox5.Location = new System.Drawing.Point(276, 486);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(296, 101);
			this.groupBox5.TabIndex = 40;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Опции сервера записи архивов";
			// 
			// label27
			// 
			this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label27.Location = new System.Drawing.Point(248, 25);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(41, 15);
			this.label27.TabIndex = 47;
			this.label27.Text = "строк";
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(248, 71);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(28, 20);
			this.label24.TabIndex = 46;
			this.label24.Text = "сек";
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(248, 48);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(28, 20);
			this.label23.TabIndex = 45;
			this.label23.Text = "сек";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(10, 68);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(155, 28);
			this.label8.TabIndex = 40;
			this.label8.Text = "Период чистки устаревших данных :";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 48);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(157, 17);
			this.label7.TabIndex = 39;
			this.label7.Text = "Период отправки данных (ct):";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label6.Location = new System.Drawing.Point(2, 21);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(175, 20);
			this.label6.TabIndex = 38;
			this.label6.Text = "Кэш буфер-параметров (cs):";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button2.Location = new System.Drawing.Point(584, 476);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(86, 49);
			this.button2.TabIndex = 41;
			this.button2.Text = "Сохранить и завершить.";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// button3
			// 
			this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button3.Location = new System.Drawing.Point(584, 554);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(86, 33);
			this.button3.TabIndex = 42;
			this.button3.Text = "Отмена";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button4.Location = new System.Drawing.Point(605, 3);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 52);
			this.button4.TabIndex = 43;
			this.button4.Text = "Значения по умолчанию";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(12, 58);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(647, 217);
			this.dataGridView1.TabIndex = 44;
			this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1CellContentClick);
			// 
			// dataSet1
			// 
			this.dataSet1.DataSetName = "NewDataSet";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(76, 593);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(440, 107);
			this.pictureBox1.TabIndex = 45;
			this.pictureBox1.TabStop = false;
			// 
			// FormArcGinfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.CancelButton = this.button3;
			this.ClientSize = new System.Drawing.Size(682, 706);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.textBoxName);
			this.Controls.Add(this.comboBoxID_TYPE);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxID);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormArcGinfo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Опции архива..";
			this.Load += new System.EventHandler(this.FormArcGinfoLoad);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
