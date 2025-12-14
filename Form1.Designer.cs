namespace ToDoApp
{
    partial class toDoList
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            layoutMain = new TableLayoutPanel();
            layoutTop = new TableLayoutPanel();
            DailyBtn = new Button();
            WeeklyBtn = new Button();
            AllTasksBtn = new Button();
            titleHost = new Panel();
            layoutTitle = new TableLayoutPanel();
            label1 = new Label();
            titleTxt = new TextBox();
            descHost = new Panel();
            layoutDesc = new TableLayoutPanel();
            label2 = new Label();
            descriptionTxt = new TextBox();
            layoutActionsRow = new TableLayoutPanel();
            newTaskBtn = new Button();
            deleteBtn = new Button();
            listHost = new Panel();
            layoutList = new TableLayoutPanel();
            taskListLbl = new Label();
            listSelectCmb = new ComboBox();
            taskListPanel = new FlowLayoutPanel();
            layoutMain.SuspendLayout();
            layoutTop.SuspendLayout();
            titleHost.SuspendLayout();
            layoutTitle.SuspendLayout();
            descHost.SuspendLayout();
            layoutDesc.SuspendLayout();
            layoutActionsRow.SuspendLayout();
            listHost.SuspendLayout();
            layoutList.SuspendLayout();
            SuspendLayout();
            // 
            // layoutMain
            // 
            layoutMain.ColumnCount = 1;
            layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMain.Controls.Add(layoutTop, 0, 0);
            layoutMain.Controls.Add(titleHost, 0, 1);
            layoutMain.Controls.Add(descHost, 0, 2);
            layoutMain.Controls.Add(layoutActionsRow, 0, 4);
            layoutMain.Controls.Add(listHost, 0, 3);
            layoutMain.Controls.Add(taskListPanel, 0, 5);
            layoutMain.Dock = DockStyle.Fill;
            layoutMain.Location = new Point(0, 0);
            layoutMain.Name = "layoutMain";
            layoutMain.Padding = new Padding(12);
            layoutMain.RowCount = 6;
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 71F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutMain.Size = new Size(418, 546);
            layoutMain.TabIndex = 0;
            // 
            // layoutTop
            // 
            layoutTop.ColumnCount = 3;
            layoutTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            layoutTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            layoutTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            layoutTop.Controls.Add(DailyBtn, 0, 0);
            layoutTop.Controls.Add(WeeklyBtn, 1, 0);
            layoutTop.Controls.Add(AllTasksBtn, 2, 0);
            layoutTop.Dock = DockStyle.Fill;
            layoutTop.Location = new Point(12, 12);
            layoutTop.Margin = new Padding(0);
            layoutTop.Name = "layoutTop";
            layoutTop.RowCount = 1;
            layoutTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTop.Size = new Size(394, 52);
            layoutTop.TabIndex = 0;
            // 
            // DailyBtn
            // 
            DailyBtn.BackColor = SystemColors.MenuHighlight;
            DailyBtn.Dock = DockStyle.Fill;
            DailyBtn.ForeColor = SystemColors.ButtonHighlight;
            DailyBtn.Location = new Point(0, 0);
            DailyBtn.Margin = new Padding(0, 0, 8, 0);
            DailyBtn.Name = "DailyBtn";
            DailyBtn.Size = new Size(123, 52);
            DailyBtn.TabIndex = 0;
            DailyBtn.Text = "Daily";
            DailyBtn.UseVisualStyleBackColor = false;
            // 
            // WeeklyBtn
            // 
            WeeklyBtn.BackColor = SystemColors.MenuHighlight;
            WeeklyBtn.Dock = DockStyle.Fill;
            WeeklyBtn.ForeColor = SystemColors.ButtonHighlight;
            WeeklyBtn.Location = new Point(139, 0);
            WeeklyBtn.Margin = new Padding(8, 0, 8, 0);
            WeeklyBtn.Name = "WeeklyBtn";
            WeeklyBtn.Size = new Size(115, 52);
            WeeklyBtn.TabIndex = 1;
            WeeklyBtn.Text = "Weekly";
            WeeklyBtn.UseVisualStyleBackColor = false;
            // 
            // AllTasksBtn
            // 
            AllTasksBtn.BackColor = SystemColors.MenuHighlight;
            AllTasksBtn.Dock = DockStyle.Fill;
            AllTasksBtn.ForeColor = SystemColors.ButtonHighlight;
            AllTasksBtn.Location = new Point(270, 0);
            AllTasksBtn.Margin = new Padding(8, 0, 0, 0);
            AllTasksBtn.Name = "AllTasksBtn";
            AllTasksBtn.Size = new Size(124, 52);
            AllTasksBtn.TabIndex = 2;
            AllTasksBtn.Text = "All Tasks";
            AllTasksBtn.UseVisualStyleBackColor = false;
            // 
            // titleHost
            // 
            titleHost.BackColor = Color.White;
            titleHost.BorderStyle = BorderStyle.FixedSingle;
            titleHost.Controls.Add(layoutTitle);
            titleHost.Dock = DockStyle.Fill;
            titleHost.Location = new Point(12, 72);
            titleHost.Margin = new Padding(0, 8, 0, 0);
            titleHost.Name = "titleHost";
            titleHost.Padding = new Padding(10, 8, 10, 8);
            titleHost.Size = new Size(394, 56);
            titleHost.TabIndex = 1;
            // 
            // layoutTitle
            // 
            layoutTitle.ColumnCount = 1;
            layoutTitle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTitle.Controls.Add(label1, 0, 0);
            layoutTitle.Controls.Add(titleTxt, 0, 1);
            layoutTitle.Dock = DockStyle.Fill;
            layoutTitle.Location = new Point(10, 8);
            layoutTitle.Margin = new Padding(0);
            layoutTitle.Name = "layoutTitle";
            layoutTitle.RowCount = 2;
            layoutTitle.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
            layoutTitle.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTitle.Size = new Size(372, 38);
            layoutTitle.TabIndex = 0;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(0, 0);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(372, 16);
            label1.TabIndex = 0;
            label1.Text = "Title";
            // 
            // titleTxt
            // 
            titleTxt.Dock = DockStyle.Fill;
            titleTxt.Location = new Point(0, 20);
            titleTxt.Margin = new Padding(0, 4, 0, 0);
            titleTxt.Name = "titleTxt";
            titleTxt.Size = new Size(372, 23);
            titleTxt.TabIndex = 1;
            // 
            // descHost
            // 
            descHost.BackColor = Color.White;
            descHost.BorderStyle = BorderStyle.FixedSingle;
            descHost.Controls.Add(layoutDesc);
            descHost.Dock = DockStyle.Fill;
            descHost.Location = new Point(12, 136);
            descHost.Margin = new Padding(0, 8, 0, 0);
            descHost.Name = "descHost";
            descHost.Padding = new Padding(10, 8, 10, 8);
            descHost.Size = new Size(394, 56);
            descHost.TabIndex = 2;
            // 
            // layoutDesc
            // 
            layoutDesc.ColumnCount = 1;
            layoutDesc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDesc.Controls.Add(label2, 0, 0);
            layoutDesc.Controls.Add(descriptionTxt, 0, 1);
            layoutDesc.Dock = DockStyle.Fill;
            layoutDesc.Location = new Point(10, 8);
            layoutDesc.Margin = new Padding(0);
            layoutDesc.Name = "layoutDesc";
            layoutDesc.RowCount = 2;
            layoutDesc.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
            layoutDesc.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDesc.Size = new Size(372, 38);
            layoutDesc.TabIndex = 0;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(0, 0);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(372, 16);
            label2.TabIndex = 0;
            label2.Text = "Description";
            // 
            // descriptionTxt
            // 
            descriptionTxt.Dock = DockStyle.Fill;
            descriptionTxt.Location = new Point(0, 20);
            descriptionTxt.Margin = new Padding(0, 4, 0, 0);
            descriptionTxt.Name = "descriptionTxt";
            descriptionTxt.Size = new Size(372, 23);
            descriptionTxt.TabIndex = 1;
            // 
            // layoutActionsRow
            // 
            layoutActionsRow.ColumnCount = 2;
            layoutActionsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutActionsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutActionsRow.Controls.Add(newTaskBtn, 0, 0);
            layoutActionsRow.Controls.Add(deleteBtn, 1, 0);
            layoutActionsRow.Dock = DockStyle.Fill;
            layoutActionsRow.Location = new Point(12, 271);
            layoutActionsRow.Margin = new Padding(0, 8, 0, 0);
            layoutActionsRow.Name = "layoutActionsRow";
            layoutActionsRow.RowCount = 1;
            layoutActionsRow.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutActionsRow.Size = new Size(394, 47);
            layoutActionsRow.TabIndex = 4;
            // 
            // newTaskBtn
            // 
            newTaskBtn.BackColor = SystemColors.MenuHighlight;
            newTaskBtn.Dock = DockStyle.Fill;
            newTaskBtn.ForeColor = SystemColors.ButtonHighlight;
            newTaskBtn.Location = new Point(2, 0);
            newTaskBtn.Margin = new Padding(2, 0, 8, 0);
            newTaskBtn.Name = "newTaskBtn";
            newTaskBtn.Size = new Size(187, 47);
            newTaskBtn.TabIndex = 0;
            newTaskBtn.Text = "Add";
            newTaskBtn.UseVisualStyleBackColor = false;
            newTaskBtn.Click += newTaskBtn_Click;
            // 
            // deleteBtn
            // 
            deleteBtn.BackColor = SystemColors.MenuHighlight;
            deleteBtn.Dock = DockStyle.Fill;
            deleteBtn.ForeColor = SystemColors.ButtonHighlight;
            deleteBtn.Location = new Point(205, 0);
            deleteBtn.Margin = new Padding(8, 0, 0, 0);
            deleteBtn.Name = "deleteBtn";
            deleteBtn.Size = new Size(189, 47);
            deleteBtn.TabIndex = 1;
            deleteBtn.Text = "Delete";
            deleteBtn.UseVisualStyleBackColor = false;
            // 
            // listHost
            // 
            listHost.BackColor = Color.White;
            listHost.BorderStyle = BorderStyle.FixedSingle;
            listHost.Controls.Add(layoutList);
            listHost.Dock = DockStyle.Fill;
            listHost.Location = new Point(12, 200);
            listHost.Margin = new Padding(0, 8, 0, 0);
            listHost.Name = "listHost";
            listHost.Padding = new Padding(10, 8, 10, 8);
            listHost.Size = new Size(394, 63);
            listHost.TabIndex = 3;
            // 
            // layoutList
            // 
            layoutList.ColumnCount = 1;
            layoutList.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutList.Controls.Add(taskListLbl, 0, 0);
            layoutList.Controls.Add(listSelectCmb, 0, 1);
            layoutList.Dock = DockStyle.Fill;
            layoutList.Location = new Point(10, 8);
            layoutList.Margin = new Padding(0);
            layoutList.Name = "layoutList";
            layoutList.RowCount = 2;
            layoutList.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
            layoutList.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutList.Size = new Size(372, 45);
            layoutList.TabIndex = 0;
            // 
            // taskListLbl
            // 
            taskListLbl.Dock = DockStyle.Fill;
            taskListLbl.Location = new Point(0, 0);
            taskListLbl.Margin = new Padding(0);
            taskListLbl.Name = "taskListLbl";
            taskListLbl.Size = new Size(372, 16);
            taskListLbl.TabIndex = 0;
            taskListLbl.Text = "Task List";
            // 
            // listSelectCmb
            // 
            listSelectCmb.Dock = DockStyle.Fill;
            listSelectCmb.DropDownStyle = ComboBoxStyle.DropDownList;
            listSelectCmb.FormattingEnabled = true;
            listSelectCmb.Location = new Point(0, 20);
            listSelectCmb.Margin = new Padding(0, 4, 0, 0);
            listSelectCmb.Name = "listSelectCmb";
            listSelectCmb.Size = new Size(372, 23);
            listSelectCmb.TabIndex = 1;
            // 
            // taskListPanel
            // 
            taskListPanel.AutoScroll = true;
            taskListPanel.BackColor = Color.White;
            taskListPanel.Dock = DockStyle.Fill;
            taskListPanel.FlowDirection = FlowDirection.TopDown;
            taskListPanel.Location = new Point(12, 326);
            taskListPanel.Margin = new Padding(0, 8, 0, 0);
            taskListPanel.Name = "taskListPanel";
            taskListPanel.Size = new Size(394, 208);
            taskListPanel.TabIndex = 5;
            taskListPanel.WrapContents = false;
            taskListPanel.Paint += taskListPanel_Paint;
            // 
            // toDoList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(418, 546);
            Controls.Add(layoutMain);
            MinimumSize = new Size(396, 500);
            Name = "toDoList";
            Text = "To Do List";
            Load += toDoList_Load;
            layoutMain.ResumeLayout(false);
            layoutTop.ResumeLayout(false);
            titleHost.ResumeLayout(false);
            layoutTitle.ResumeLayout(false);
            layoutTitle.PerformLayout();
            descHost.ResumeLayout(false);
            layoutDesc.ResumeLayout(false);
            layoutDesc.PerformLayout();
            layoutActionsRow.ResumeLayout(false);
            listHost.ResumeLayout(false);
            layoutList.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel layoutMain;
        private TableLayoutPanel layoutTop;
        private TableLayoutPanel layoutActionsRow;

        private Button DailyBtn;
        private Button WeeklyBtn;
        private Button AllTasksBtn;

        private Panel titleHost;
        private TableLayoutPanel layoutTitle;
        private Label label1;
        private TextBox titleTxt;

        private Panel descHost;
        private TableLayoutPanel layoutDesc;
        private Label label2;
        private TextBox descriptionTxt;

        private Panel listHost;
        private TableLayoutPanel layoutList;
        private Label taskListLbl;
        private ComboBox listSelectCmb;

        private Button newTaskBtn;
        private Button deleteBtn;

        private FlowLayoutPanel taskListPanel;
    }
}
