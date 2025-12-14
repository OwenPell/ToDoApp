using System.Data;

namespace ToDoApp
{
    /// <summary>
    /// Main form for managing tasks (add, edit, delete, and view filtering).
    /// </summary>
    public partial class toDoList : Form
    {
        /// <summary>
        /// In-memory task store backing the UI.
        /// </summary>
        private readonly DataTable taskList = new();

        /// <summary>
        /// Database manager used to load and persist tasks.
        /// </summary>
        private DatabaseManager? db;

        /// <summary>
        /// Real row index currently being edited; -1 means none.
        /// </summary>
        private int editingIndex = -1;

        /// <summary>
        /// Real row index currently selected; -1 means none.
        /// </summary>
        private int selectedIndex = -1;

        /// <summary>
        /// Current view filter mode.
        /// </summary>
        private ViewMode currentView = ViewMode.All;

        /// <summary>
        /// Initializes the form.
        /// </summary>
        public toDoList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Supported filter modes for viewing tasks.
        /// </summary>
        private enum ViewMode
        {
            All,
            Daily,
            Weekly
        }

        /// <summary>
        /// Initializes storage, loads tasks, and wires UI behavior.
        /// </summary>
        private void toDoList_Load(object? sender, EventArgs e)
        {
            try
            {
                db = new DatabaseManager();

                if (taskList.Columns.Count == 0)
                {
                    taskList.Columns.Add("Title");
                    taskList.Columns.Add("Description");
                    taskList.Columns.Add("List");
                    taskList.Columns.Add("CreatedAt", typeof(DateTime));
                    taskList.Columns.Add("Completed", typeof(bool));
                }
                else
                {
                    if (!taskList.Columns.Contains("CreatedAt"))
                        taskList.Columns.Add("CreatedAt", typeof(DateTime));

                    if (!taskList.Columns.Contains("Completed"))
                        taskList.Columns.Add("Completed", typeof(bool));
                }

                LoadTasksFromDatabase();

                BackColor = Color.White;

                UiStyle.StylePrimaryButton(DailyBtn);
                UiStyle.StylePrimaryButton(WeeklyBtn);
                UiStyle.StylePrimaryButton(AllTasksBtn);
                UiStyle.StyleAddButton(newTaskBtn);
                UiStyle.StyleDeleteButton(deleteBtn);

                UiStyle.StyleTaskListPanel(taskListPanel);

                DailyBtn.Click -= DailyBtn_Click;
                WeeklyBtn.Click -= WeeklyBtn_Click;
                AllTasksBtn.Click -= AllTasksBtn_Click;
                deleteBtn.Click -= deleteBtn_Click;

                DailyBtn.Click += DailyBtn_Click;
                WeeklyBtn.Click += WeeklyBtn_Click;
                AllTasksBtn.Click += AllTasksBtn_Click;
                deleteBtn.Click += deleteBtn_Click;

                listSelectCmb.Items.Clear();
                listSelectCmb.Items.Add("All Tasks");
                listSelectCmb.Items.Add("Daily");
                listSelectCmb.Items.Add("Weekly");
                listSelectCmb.SelectedIndex = 0;

                newTaskBtn.Text = "Add";
                SetView(ViewMode.All);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during application load: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads tasks from the database into the DataTable.
        /// </summary>
        private void LoadTasksFromDatabase()
        {
            var savedTasks = db.LoadTasks();

            foreach (var task in savedTasks)
            {
                var row = taskList.NewRow();
                row["Title"] = task.Title;
                row["Description"] = task.Description;
                row["List"] = task.List;
                row["CreatedAt"] = task.CreatedAt;
                row["Completed"] = task.Completed;
                taskList.Rows.Add(row);
            }
        }

        /// <summary>
        /// Persists current tasks to the database, excluding completed tasks.
        /// </summary>
        private void SaveTasksToDatabase()
        {
            if (db == null) return;

            db.ClearAllTasks();

            foreach (DataRow row in taskList.Rows)
            {
                bool completed = row["Completed"] is bool b && b;
                if (!completed)
                {
                    db.SaveTask(
                        row["Title"]?.ToString() ?? string.Empty,
                        row["Description"]?.ToString() ?? string.Empty,
                        row["List"]?.ToString() ?? "All Tasks",
                        row["CreatedAt"] is DateTime dt ? dt : DateTime.UtcNow,
                        false
                    );
                }
            }
        }

        /// <summary>
        /// Saves tasks and closes the database connection during shutdown.
        /// </summary>
        /// <param name="e">Closing event args.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (db != null)
                {
                    SaveTasksToDatabase();
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during application shutdown: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            base.OnFormClosing(e);
        }

        /// <summary>
        /// Switches to the Daily view.
        /// </summary>
        private void DailyBtn_Click(object? sender, EventArgs e) => SetView(ViewMode.Daily);

        /// <summary>
        /// Switches to the Weekly view.
        /// </summary>
        private void WeeklyBtn_Click(object? sender, EventArgs e) => SetView(ViewMode.Weekly);

        /// <summary>
        /// Switches to the All Tasks view.
        /// </summary>
        private void AllTasksBtn_Click(object? sender, EventArgs e) => SetView(ViewMode.All);

        /// <summary>
        /// Applies the view filter and refreshes UI state.
        /// </summary>
        /// <param name="mode">The view mode to apply.</param>
        private void SetView(ViewMode mode)
        {
            currentView = mode;
            UpdateNavButtons();

            if (!IsRowVisible(selectedIndex))
            {
                selectedIndex = -1;
                editingIndex = -1;
                newTaskBtn.Text = "Add";
            }

            RefreshTaskBubbles();
        }

        /// <summary>
        /// Updates navigation button styling based on the active view.
        /// </summary>
        private void UpdateNavButtons()
        {
            UiStyle.SetNavButtonActive(DailyBtn, currentView == ViewMode.Daily, "Daily");
            UiStyle.SetNavButtonActive(WeeklyBtn, currentView == ViewMode.Weekly, "Weekly");
            UiStyle.SetNavButtonActive(AllTasksBtn, currentView == ViewMode.All, "All Tasks");
        }

        /// <summary>
        /// Determines whether a task row is visible in the current view.
        /// </summary>
        /// <param name="rowIndex">Real DataTable row index.</param>
        /// <returns>True if the row should be shown; otherwise false.</returns>
        private bool IsRowVisible(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= taskList.Rows.Count) return false;

            bool completed = taskList.Rows[rowIndex]["Completed"] is bool b && b;
            if (completed) return false;

            if (currentView == ViewMode.All) return true;

            string list = taskList.Rows[rowIndex]["List"]?.ToString() ?? "All Tasks";
            return currentView switch
            {
                ViewMode.Daily => list == "Daily",
                ViewMode.Weekly => list == "Weekly",
                _ => true
            };
        }

        /// <summary>
        /// Adds a new task or saves edits to an existing task.
        /// </summary>
        private void newTaskBtn_Click(object? sender, EventArgs e)
        {
            string title = titleTxt.Text.Trim();
            if (string.IsNullOrWhiteSpace(title)) return;

            string description = descriptionTxt.Text.Trim();
            string list = listSelectCmb.SelectedItem?.ToString() ?? "All Tasks";

            if (editingIndex >= 0 && editingIndex < taskList.Rows.Count)
            {
                taskList.Rows[editingIndex]["Title"] = title;
                taskList.Rows[editingIndex]["Description"] = description;
                taskList.Rows[editingIndex]["List"] = list;

                if (taskList.Rows[editingIndex]["Completed"] is DBNull)
                    taskList.Rows[editingIndex]["Completed"] = false;

                selectedIndex = editingIndex;
                editingIndex = -1;
            }
            else
            {
                var r = taskList.NewRow();
                r["Title"] = title;
                r["Description"] = description;
                r["List"] = list;
                r["CreatedAt"] = DateTime.UtcNow;
                r["Completed"] = false;

                taskList.Rows.Add(r);
                selectedIndex = taskList.Rows.Count - 1;
            }

            titleTxt.Clear();
            descriptionTxt.Clear();
            listSelectCmb.SelectedIndex = 0;

            if (!IsRowVisible(selectedIndex))
            {
                selectedIndex = -1;
                editingIndex = -1;
            }

            newTaskBtn.Text = "Add";
            RefreshTaskBubbles();
        }

        /// <summary>
        /// Deletes the currently selected task.
        /// </summary>
        private void deleteBtn_Click(object? sender, EventArgs e)
        {
            if (selectedIndex < 0 || selectedIndex >= taskList.Rows.Count) return;

            taskList.Rows.RemoveAt(selectedIndex);

            selectedIndex = -1;
            editingIndex = -1;

            titleTxt.Clear();
            descriptionTxt.Clear();
            listSelectCmb.SelectedIndex = 0;

            newTaskBtn.Text = "Add";
            RefreshTaskBubbles();
        }

        /// <summary>
        /// Rebuilds the task card UI list based on current view and sort order.
        /// </summary>
        private void RefreshTaskBubbles()
        {
            taskListPanel.SuspendLayout();
            taskListPanel.Controls.Clear();

            var rows = taskList.Select("", "CreatedAt ASC");

            foreach (var row in rows)
            {
                int realIndex = taskList.Rows.IndexOf(row);
                if (!IsRowVisible(realIndex)) continue;

                taskListPanel.Controls.Add(
                    CreateTaskBubble(
                        row["Title"]?.ToString() ?? string.Empty,
                        row["Description"]?.ToString() ?? string.Empty,
                        row["List"]?.ToString() ?? "All Tasks",
                        realIndex
                    )
                );
            }

            taskListPanel.ResumeLayout();
        }

        /// <summary>
        /// Creates a task card control for display in the task list panel.
        /// </summary>
        /// <param name="title">Task title.</param>
        /// <param name="description">Task description.</param>
        /// <param name="list">Task category/list.</param>
        /// <param name="rowIndex">Real DataTable row index.</param>
        /// <returns>A configured task card control.</returns>
        private Control CreateTaskBubble(string title, string description, string list, int rowIndex)
        {
            var card = new Panel();
            UiStyle.StyleTaskCard(card);

            card.Margin = new Padding(0, 0, 0, 10);
            card.Height = 86;
            card.Width = Math.Max(50, taskListPanel.ClientSize.Width - 24);

            UiStyle.SetTaskCardSelected(card, rowIndex == selectedIndex);

            var content = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(12),
                BackColor = Color.Transparent
            };

            bool completed = taskList.Rows[rowIndex]["Completed"] is bool b && b;

            var stripe = UiStyle.CreateTaskAccentStripe(
                list,
                completed,
                _ => MarkTaskCompleted(rowIndex, card)
            );

            var titleLbl = new Label { Dock = DockStyle.Top, Height = 24, Text = title };
            var descLbl = new Label { Dock = DockStyle.Fill, Text = description };

            var editBtn = new Button { Text = "Edit", Visible = false, Anchor = AnchorStyles.Top | AnchorStyles.Right };
            UiStyle.StyleTaskEditButton(editBtn);
            editBtn.Click += (_, _) => BeginEditTask(rowIndex);

            void LayoutEdit() => editBtn.Location = new Point(content.ClientSize.Width - editBtn.Width - 10, 10);

            void SelectThis()
            {
                if (editingIndex >= 0) return;

                selectedIndex = rowIndex;
                editingIndex = -1;
                newTaskBtn.Text = "Add";
                RefreshTaskBubbles();
            }

            card.Click += (_, _) => SelectThis();
            stripe.Click += (_, _) => SelectThis();
            content.Click += (_, _) => SelectThis();
            titleLbl.Click += (_, _) => SelectThis();
            descLbl.Click += (_, _) => SelectThis();

            void ShowEdit() => editBtn.Visible = true;
            void HideEdit() => editBtn.Visible = false;

            content.SizeChanged += (_, _) => LayoutEdit();

            content.Controls.Add(editBtn);
            content.Controls.Add(descLbl);
            content.Controls.Add(titleLbl);

            card.Controls.Add(content);
            card.Controls.Add(stripe);

            WireHoverSmart(card, ShowEdit, HideEdit);

            taskListPanel.SizeChanged += (_, _) =>
            {
                card.Width = Math.Max(50, taskListPanel.ClientSize.Width - 24);
                LayoutEdit();
            };

            LayoutEdit();
            return card;
        }

        /// <summary>
        /// Marks a task as completed and animates removal from the list.
        /// </summary>
        /// <param name="rowIndex">Real DataTable row index.</param>
        /// <param name="card">The UI card to animate.</param>
        private void MarkTaskCompleted(int rowIndex, Panel card)
        {
            if (rowIndex < 0 || rowIndex >= taskList.Rows.Count) return;

            taskList.Rows[rowIndex]["Completed"] = true;

            if (selectedIndex == rowIndex)
            {
                selectedIndex = -1;
                editingIndex = -1;
                newTaskBtn.Text = "Add";
            }

            int step = 6;
            var t = new System.Windows.Forms.Timer { Interval = 15 };
            t.Tick += (_, _) =>
            {
                int h = card.Height - step;
                if (h <= 0)
                {
                    t.Stop();
                    t.Dispose();
                    RefreshTaskBubbles();
                    return;
                }

                card.Height = h;
            };
            t.Start();
        }

        /// <summary>
        /// Wires hover behavior that avoids flicker when moving between nested controls.
        /// </summary>
        /// <param name="root">Root control to monitor.</param>
        /// <param name="show">Action to show hover UI.</param>
        /// <param name="hide">Action to hide hover UI.</param>
        private static void WireHoverSmart(Control root, Action show, Action hide)
        {
            var hideTimer = new System.Windows.Forms.Timer { Interval = 50 };

            hideTimer.Tick += (_, _) =>
            {
                hideTimer.Stop();
                var screenRect = root.RectangleToScreen(root.ClientRectangle);
                if (screenRect.Contains(Cursor.Position)) return;
                hide();
            };

            void OnEnter()
            {
                hideTimer.Stop();
                show();
            }

            void OnLeave()
            {
                hideTimer.Stop();
                hideTimer.Start();
            }

            void Hook(Control c)
            {
                c.MouseEnter += (_, _) => OnEnter();
                c.MouseLeave += (_, _) => OnLeave();

                foreach (Control child in c.Controls)
                    Hook(child);
            }

            Hook(root);
        }

        /// <summary>
        /// Starts editing an existing task by loading it into the input controls.
        /// </summary>
        /// <param name="index">Real DataTable row index.</param>
        private void BeginEditTask(int index)
        {
            if (index < 0 || index >= taskList.Rows.Count) return;

            bool completed = taskList.Rows[index]["Completed"] is bool b && b;
            if (completed) return;

            selectedIndex = index;
            editingIndex = index;

            titleTxt.Text = taskList.Rows[index]["Title"]?.ToString() ?? string.Empty;
            descriptionTxt.Text = taskList.Rows[index]["Description"]?.ToString() ?? string.Empty;

            string list = taskList.Rows[index]["List"]?.ToString() ?? "All Tasks";
            listSelectCmb.SelectedItem = list;

            newTaskBtn.Text = "Save";
            RefreshTaskBubbles();
        }

        /// <summary>
        /// Handles the task list panel paint event.
        /// </summary>
        private void taskListPanel_Paint(object? sender, PaintEventArgs e) { }
    }
}
