using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class CafePageForm : Form
    {
        private readonly string pageTitle;
        private readonly string description;

        public CafePageForm(string title, string text)
        {
            pageTitle = title;
            description = text;
            Build();
        }

        private void Build()
        {
            Text = pageTitle + " — BrewFlow";
            Size = pageTitle == "Поддержка" ? new Size(620, 410) : new Size(850, 560);
            BackColor = CafeStyle.Background;
            StartPosition = FormStartPosition.CenterParent;
            Controls.Add(CafeStyle.Heading(pageTitle, 35, 25, 740));

            if (pageTitle == "Поддержка")
            {
                Label contacts = new Label {
                    Text = description,
                    Location = new Point(40, 85),
                    Size = new Size(520, 55),
                    Font = new Font("Microsoft Sans Serif", 10),
                    ForeColor = CafeStyle.Brown
                };
                Controls.Add(contacts);

                Label prompt = new Label {
                    Text = "Ваш вопрос:",
                    Location = new Point(40, 155),
                    AutoSize = true,
                    ForeColor = CafeStyle.Brown
                };
                Controls.Add(prompt);

                TextBox message = new TextBox {
                    Location = new Point(40, 180),
                    Size = new Size(520, 105),
                    Multiline = true,
                    Font = new Font("Microsoft Sans Serif", 10)
                };
                Controls.Add(message);

                Button send = new Button {
                    Text = "Отправить",
                    Location = new Point(40, 305),
                    Size = new Size(140, 35)
                };
                CafeStyle.StyleButton(send);
                send.Click += delegate {
                    if (string.IsNullOrWhiteSpace(message.Text))
                    {
                        MessageBox.Show("Напишите сообщение.", "BrewFlow");
                        return;
                    }
                    MessageBox.Show("Спасибо! Обращение сохранено в демо-версии.", "BrewFlow");
                    message.Clear();
                };
                Controls.Add(send);

                Button backSupport = new Button {
                    Text = "Назад",
                    Location = new Point(420, 305),
                    Size = new Size(140, 35)
                };
                CafeStyle.StyleButton(backSupport);
                backSupport.Click += delegate { Close(); };
                Controls.Add(backSupport);
                return;
            }

            if (pageTitle == "Мои заказы")
            {
                Label info = new Label {
                    Text = "Ваши последние заказы",
                    Location = new Point(40, 85),
                    AutoSize = true,
                    Font = new Font("Segoe Print", 12),
                    ForeColor = CafeStyle.Brown
                };
                Controls.Add(info);

                ListBox list = new ListBox {
                    Location = new Point(40, 125),
                    Size = new Size(740, 280),
                    Font = new Font("Microsoft Sans Serif", 11),
                    BackColor = Color.Bisque
                };
                if (CafeData.Orders.Count == 0)
                    list.Items.Add("Заказов пока нет. Самое время выбрать любимый кофе!");

                foreach (CoffeeOrder order in CafeData.Orders)
                    list.Items.Add(order.Number + "   |   " +
                        order.Date.ToString("dd.MM.yyyy HH:mm") + "   |   " +
                        order.Total.ToString("0.00") + " руб.   |   " +
                        order.PaymentMethod + "   |   " + order.DeliveryType +
                        (string.IsNullOrEmpty(order.Address) ? "" : "   |   " + order.Address) +
                        "   |   " + order.Status);

                Controls.Add(list);
            }
            else
            {
                Label body = new Label {
                    Text = description,
                    Location = new Point(40, 100),
                    Size = new Size(740, 220),
                    Font = new Font("Segoe Print", 12),
                    ForeColor = CafeStyle.Brown
                };
                Controls.Add(body);
            }

            Button back = new Button {
                Text = "Назад",
                Location = new Point(650, 445),
                Size = new Size(130, 35)
            };
            CafeStyle.StyleButton(back);
            back.Click += delegate { Close(); };
            Controls.Add(back);
        }
    }

    public class ExtraSectionsForm : Form
    {
        public ExtraSectionsForm()
        {
            Text = "Разделы BrewFlow";
            Size = new Size(850, 590);
            BackColor = CafeStyle.Background;
            StartPosition = FormStartPosition.CenterParent;
            Controls.Add(CafeStyle.Heading("BrewFlow — дополнительные разделы", 25, 20, 790));
            AddPage("История заказов", "Просмотр оформленных заказов", 45, 100,
                delegate { CafeData.ShowPage(this, new CafePageForm("Мои заказы", "")); });
            AddPage("Бонусная программа", "Ваши бонусы и скидки", 420, 100,
                delegate { CafeData.ShowPage(this, new CafePageForm("Бонусы",
                    "За каждый заказ начисляются бонусы. Используйте промокоды BREW10 и COFFEE15 для скидок.")); });
            AddPage("Доставка", "Информация о доставке", 45, 205,
                delegate { CafeData.ShowPage(this, new CafePageForm("Доставка",
                    "Демо-режим: самовывоз из кофейни. После оформления заказа статус отображается в истории заказов.")); });
            AddPage("Отзывы", "Оставить отзыв о кофейне", 420, 205,
                delegate { CafeData.ShowPage(this, new CafePageForm("Отзывы",
                    "Спасибо, что помогаете нам становиться лучше!")); });
            AddPage("Поддержка", "Связаться с нами", 45, 310,
                delegate { CafeData.ShowPage(this, new CafePageForm("Поддержка",
                    "Телефон: +7 (900) 000-00-00\r\nПочта: support@brewflow.ru")); });
            AddPage("Оплата", "Способы оплаты и безопасность", 420, 310,
                delegate { CafeData.ShowPage(this, new CafePageForm("Оплата",
                    "Доступны наличные, банковская карта и демонстрационная онлайн-оплата. Данные карт не сохраняются.")); });
            AddPage("Профиль", "Информация о клиенте", 45, 415,
                delegate { CafeData.ShowPage(this, new CafePageForm("Профиль",
                    "Личный кабинет BrewFlow. В этой учебной версии данные профиля демонстрационные.")); });
            Button close = new Button { Text = "Назад", Location = new Point(660, 500), Size = new Size(130, 35) };
            CafeStyle.StyleButton(close);
            close.Click += delegate { Close(); };
            Controls.Add(close);
        }

        private void AddPage(string title, string desc, int x, int y, Action action)
        {
            Button button = new Button {
                Text = title + "\r\n" + desc,
                Location = new Point(x, y),
                Size = new Size(330, 75),
                Font = new Font("Segoe Print", 10, FontStyle.Bold)
            };
            CafeStyle.StyleButton(button);
            button.Click += delegate { action(); };
            Controls.Add(button);
        }
    }

    public class PaymentForm : Form
    {
        private readonly ComboBox payment;
        private readonly ComboBox delivery;
        private readonly TextBox address;

        public string PaymentMethod { get; private set; }
        public string DeliveryType { get; private set; }
        public string Address { get; private set; }

        public PaymentForm(decimal amount)
        {
            Text = "Оформление заказа — BrewFlow";
            Size = new Size(560, 460);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = CafeStyle.Background;
            StartPosition = FormStartPosition.CenterParent;

            Controls.Add(CafeStyle.Heading("Оформление заказа", 25, 20, 500));

            Label total = new Label {
                Text = "К оплате: " + amount.ToString("0.00") + " руб.",
                Location = new Point(35, 75),
                Size = new Size(470, 32),
                Font = new Font("Segoe Print", 14, FontStyle.Bold),
                ForeColor = CafeStyle.Brown
            };
            Controls.Add(total);

            Controls.Add(MakeLabel("Оплата", 40, 125));
            payment = new ComboBox {
                Location = new Point(40, 153),
                Size = new Size(460, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            payment.Items.AddRange(new object[] {
                "Наличными при получении",
                "Банковской картой при получении",
                "Онлайн-оплата"
            });
            payment.SelectedIndex = 0;
            Controls.Add(payment);

            Controls.Add(MakeLabel("Получение заказа", 40, 200));
            delivery = new ComboBox {
                Location = new Point(40, 228),
                Size = new Size(460, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            delivery.Items.AddRange(new object[] { "Самовывоз из кофейни", "Доставка курьером" });
            delivery.SelectedIndex = 0;
            Controls.Add(delivery);

            address = new TextBox {
                Location = new Point(40, 290),
                Size = new Size(460, 27),
                Enabled = false
            };
            address.Text = "Адрес доставки";
            Controls.Add(address);

            delivery.SelectedIndexChanged += delegate {
                address.Enabled = delivery.SelectedIndex == 1;
                if (!address.Enabled) address.Clear();
                else address.Text = "";
            };

         

            Button confirm = new Button {
                Text = "Подтвердить",
                Location = new Point(40, 360),
                Size = new Size(180, 35)
            };
            CafeStyle.StyleButton(confirm);
            confirm.Click += Confirm;
            Controls.Add(confirm);

            Button cancel = new Button {
                Text = "Отмена",
                Location = new Point(360, 360),
                Size = new Size(140, 35)
            };
            CafeStyle.StyleButton(cancel);
            cancel.Click += delegate { DialogResult = DialogResult.Cancel; Close(); };
            Controls.Add(cancel);
        }

        private Label MakeLabel(string text, int x, int y)
        {
            return new Label {
                Text = text,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Segoe Print", 10),
                ForeColor = CafeStyle.Brown
            };
        }

        private void Confirm(object sender, EventArgs e)
        {
            if (delivery.SelectedIndex == 1 && string.IsNullOrWhiteSpace(address.Text))
            {
                MessageBox.Show("Укажите адрес доставки.", "BrewFlow");
                address.Focus();
                return;
            }

            PaymentMethod = payment.SelectedItem.ToString();
            DeliveryType = delivery.SelectedItem.ToString();
            Address = delivery.SelectedIndex == 1 ? address.Text.Trim() : "";

            if (payment.SelectedIndex == 2)
                MessageBox.Show("Демо-оплата успешно имитирована. Реальные деньги не списываются.", "BrewFlow");

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
