using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ToDoApp
{
    /// <summary>
    /// Provides centralized UI styling helpers for buttons, task cards, panels, and accent elements.
    /// </summary>
    public static class UiStyle
    {
        /// <summary>
        /// Accent color for the different Tasks list.
        /// </summary>
        private static readonly Color AccentAll = Color.FromArgb(214, 196, 235); // pastel purple
        private static readonly Color AccentDaily = Color.FromArgb(180, 224, 196); // pastel green
        private static readonly Color AccentWeekly = Color.FromArgb(170, 196, 235); // pastel blue

        /// <summary>
        /// Accent color for the different Tasks action buttons.
        /// </summary>
        private static readonly Color AccentAdd = Color.FromArgb(180, 224, 196); // pastel green
        private static readonly Color AccentDelete = Color.FromArgb(235, 180, 180); // pastel red

        /// <summary>
        /// Applies primary button styling with rounded corners.
        /// </summary>
        /// <param name="b">The button to style.</param>
        public static void StylePrimaryButton(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.UseVisualStyleBackColor = false;
            b.ForeColor = Color.Black;
            b.Padding = new Padding(6, 0, 6, 0);

            b.SizeChanged -= PrimaryButton_SizeChanged;
            b.SizeChanged += PrimaryButton_SizeChanged;

            b.Region = Rounded(b, 12);
        }

        /// <summary>
        /// Updates the rounded region of a primary button when its size changes.
        /// </summary>
        private static void PrimaryButton_SizeChanged(object? sender, EventArgs e)
        {
            if (sender is Control c)
                c.Region = Rounded(c, 12);
        }

        /// <summary>
        /// Sets the active visual state of a navigation button.
        /// </summary>
        /// <param name="b">The navigation button.</param>
        /// <param name="active">Whether the button is active.</param>
        /// <param name="viewName">The associated view name.</param>
        public static void SetNavButtonActive(Button b, bool active, string viewName)
        {
            Color accent = GetListAccentColor(viewName);

            b.FlatStyle = FlatStyle.Flat;
            b.UseVisualStyleBackColor = false;
            b.BackColor = active ? accent : ControlPaint.Light(accent, 0.55f);
            b.ForeColor = Color.Black;

            b.FlatAppearance.BorderSize = active ? 4 : 0;
            b.FlatAppearance.BorderColor = accent;

            b.Padding = active
                ? new Padding(b.Padding.Left, b.Padding.Top + 4, b.Padding.Right, b.Padding.Bottom)
                : new Padding(6, 0, 6, 0);
        }

        /// <summary>
        /// Sets the active visual state of a navigation button using its text.
        /// </summary>
        /// <param name="b">The navigation button.</param>
        /// <param name="active">Whether the button is active.</param>
        public static void SetNavButtonActive(Button b, bool active)
        {
            SetNavButtonActive(b, active, b.Text);
        }

        /// <summary>
        /// Styles a task list panel for vertical scrolling content.
        /// </summary>
        /// <param name="p">The panel to style.</param>
        public static void StyleTaskListPanel(FlowLayoutPanel p)
        {
            p.BackColor = Color.White;
            p.AutoScroll = true;
            p.WrapContents = false;
            p.FlowDirection = FlowDirection.TopDown;
        }

        /// <summary>
        /// Applies rounded corners to a control and maintains them on resize. Reusable.
        /// </summary>
        /// <param name="c">The control.</param>
        /// <param name="radius">Corner radius.</param>
        public static void ApplyRounded(Control c, int radius)
        {
            c.SizeChanged -= Rounded_SizeChanged;
            c.SizeChanged += Rounded_SizeChanged;

            c.Tag = radius;
            c.Region = Rounded(c, radius);
        }

        /// <summary>
        /// Reapplies rounded corners when a control is resized.
        /// </summary>
        private static void Rounded_SizeChanged(object? sender, EventArgs e)
        {
            if (sender is Control c && c.Tag is int r)
                c.Region = Rounded(c, r);
        }

        /// <summary>
        /// Styles a task card panel.
        /// </summary>
        /// <param name="card">The task card panel.</param>
        public static void StyleTaskCard(Panel card)
        {
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.None;

            ApplyRounded(card, 14);

            card.Paint -= TaskCard_Paint;
            card.Paint += TaskCard_Paint;
        }

        /// <summary>
        /// Sets the visual selected state of a task card.
        /// </summary>
        /// <param name="card">The task card.</param>
        /// <param name="selected">Whether the card is selected.</param>
        public static void SetTaskCardSelected(Panel card, bool selected)
        {
            card.BackColor = selected ? Color.FromArgb(245, 248, 255) : Color.White;
            card.Invalidate();
        }

        /// <summary>
        /// Creates an accent stripe with an interactive completion toggle.
        /// </summary>
        /// <param name="listName">The task list name.</param>
        /// <param name="completed">Whether the task is completed.</param>
        /// <param name="onToggle">Callback invoked when toggled.</param>
        /// <returns>The accent stripe panel.</returns>
        public static Panel CreateTaskAccentStripe(string listName, bool completed, Action<Panel> onToggle)
        {
            var stripe = new Panel
            {
                Dock = DockStyle.Left,
                Width = 30,
                Margin = new Padding(0),
                Padding = new Padding(0),
                BackColor = GetListAccentColor(listName)
            };

            var chk = new Panel
            {
                Size = new Size(16, 16),
                Margin = new Padding(0),
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                Tag = completed
            };

            ApplyRounded(chk, 8);

            void LayoutCheck()
            {
                chk.Location = new Point(
                    Math.Max(0, (stripe.ClientSize.Width - chk.Width) / 2),
                    Math.Max(0, (stripe.ClientSize.Height - chk.Height) / 2)
                );
            }

            stripe.SizeChanged += (_, _) => LayoutCheck();

            chk.Paint += (_, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                var rect = new Rectangle(0, 0, chk.Width - 1, chk.Height - 1);

                using var pen = new Pen(Color.FromArgb(160, 120, 120, 120), 1);
                e.Graphics.DrawEllipse(pen, rect);

                if (chk.Tag is bool done && done)
                {
                    using var fill = new SolidBrush(Color.FromArgb(140, 0, 0, 0));
                    e.Graphics.FillEllipse(fill, new Rectangle(4, 4, chk.Width - 8, chk.Height - 8));
                }
            };

            chk.Click += (_, _) =>
            {
                chk.Tag = true;
                chk.Invalidate();
                onToggle(stripe);
            };

            var tip = new ToolTip
            {
                InitialDelay = 300,
                ReshowDelay = 100,
                AutoPopDelay = 2000,
                ShowAlways = true
            };

            tip.SetToolTip(chk, "Mark Complete");

            stripe.Tag = chk;
            stripe.Controls.Add(chk);
            LayoutCheck();

            return stripe;
        }

        /// <summary>
        /// Gets the accent color associated with a task list name.
        /// </summary>
        /// <param name="listName">The list name.</param>
        /// <returns>The corresponding accent color.</returns>
        public static Color GetListAccentColor(string listName)
        {
            string v = (listName ?? string.Empty).Trim();

            return v switch
            {
                "Daily" => AccentDaily,
                "Weekly" => AccentWeekly,
                "All Tasks" => AccentAll,
                "All" => AccentAll,
                _ => AccentAll
            };
        }

        /// <summary>
        /// Draws the outline for a task card panel.
        /// </summary>
        private static void TaskCard_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Panel card) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int r = (card.Tag is int rr) ? rr : 14;
            var rect = new Rectangle(0, 0, card.ClientSize.Width - 1, card.ClientSize.Height - 1);

            using var path = RoundedPath(rect, r);
            using var pen = new Pen(Color.FromArgb(200, 210, 220));
            e.Graphics.DrawPath(pen, path);
        }

        /// <summary>
        /// Styles a task edit button.
        /// </summary>
        /// <param name="b">The button to style.</param>
        public static void StyleTaskEditButton(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.UseVisualStyleBackColor = false;
            b.BackColor = Color.FromArgb(245, 245, 245);
            b.ForeColor = Color.Black;
            b.Size = new Size(52, 28);
        }

        /// <summary>
        /// Creates a rounded region for a control.
        /// </summary>
        private static Region Rounded(Control c, int r)
        {
            if (c.Width <= 0 || c.Height <= 0) return new Region();

            var rect = new Rectangle(0, 0, c.Width, c.Height);
            using var path = RoundedPath(rect, r);
            return new Region(path);
        }

        /// <summary>
        /// Creates a rounded graphics path for a rectangle.
        /// </summary>
        private static GraphicsPath RoundedPath(Rectangle rect, int r)
        {
            int d = r * 2;
            var p = new GraphicsPath();

            if (rect.Width <= 0 || rect.Height <= 0) return p;

            p.StartFigure();
            p.AddArc(rect.X, rect.Y, d, d, 180, 90);
            p.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            p.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            p.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            p.CloseFigure();

            return p;
        }

        /// <summary>
        /// Applies shared styling behavior for action buttons.
        /// </summary>
        private static void StyleActionButtonBase(Button b, Color baseColor)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.UseVisualStyleBackColor = false;
            b.BackColor = baseColor;
            b.ForeColor = Color.Black;
            b.FlatAppearance.BorderSize = 0;
            b.Padding = new Padding(6, 0, 6, 0);

            b.MouseEnter -= ActionHoverEnter;
            b.MouseLeave -= ActionHoverLeave;
            b.MouseEnter += ActionHoverEnter;
            b.MouseLeave += ActionHoverLeave;

            b.Tag = baseColor;

            b.SizeChanged -= PrimaryButton_SizeChanged;
            b.SizeChanged += PrimaryButton_SizeChanged;

            b.Region = Rounded(b, 12);
        }

        /// <summary>
        /// Handles hover enter behavior for action buttons.
        /// </summary>
        private static void ActionHoverEnter(object? sender, EventArgs e)
        {
            if (sender is Button b && b.Tag is Color c)
                b.BackColor = Blend(c, Color.White, 0.15f);
        }

        /// <summary>
        /// Handles hover leave behavior for action buttons.
        /// </summary>
        private static void ActionHoverLeave(object? sender, EventArgs e)
        {
            if (sender is Button b && b.Tag is Color c)
                b.BackColor = c;
        }

        /// <summary>
        /// Blends two colors by a specified amount.
        /// </summary>
        private static Color Blend(Color from, Color to, float amount)
        {
            int r = (int)(from.R + (to.R - from.R) * amount);
            int g = (int)(from.G + (to.G - from.G) * amount);
            int b = (int)(from.B + (to.B - from.B) * amount);

            return Color.FromArgb(from.A, r, g, b);
        }

        /// <summary>
        /// Styles an Add action button.
        /// </summary>
        public static void StyleAddButton(Button b) => StyleActionButtonBase(b, AccentAdd);

        /// <summary>
        /// Styles a Delete action button.
        /// </summary>
        public static void StyleDeleteButton(Button b) => StyleActionButtonBase(b, AccentDelete);
    }
}
